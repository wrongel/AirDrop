using System;
using System.Collections.Generic;
using System.Windows.Forms;

public partial class Form_Data : Form
{
    List<string[]> Columns;         // Имена столбцов в текущей таблице
    List<string[]> Data;            // Данные в текущей таблице
    List<string[]> CargoData;       // Данные о грузах из главной формы
    string m_CurTableName;          // Текущее отображаемая таблица
    bool   m_bChange;               // Изменялась ли таблица

    public Form_Data(List<string[]> CData)
    {
        InitializeComponent();
        // Инициализация полей
        CargoData = CData;
        m_bChange = false;
    }

    private void Form_Data_Load(object sender, EventArgs e)
    {
        // Получить названия всех таблиц
        List<string[]> Tables = Query.Select("SELECT name FROM sqlite_master WHERE type = 'table'");

        dataGrid.AutoGenerateColumns = false;       // Необходимо для объединения строк
        // Не добавлять таблицу "sqlite_sequence" в ComboBox
        foreach (string[] str in Tables)
        {
            if (str[0] != "sqlite_sequence")
                comboBox_tables.Items.Add(str[0]);
        }
        // Начальная таблица с 1 номера (под 0 - sqlite_sequence)
        comboBox_tables.Text = Tables[1][0];
    }

    // Обработка события при выборе новой таблицы
    private void comboBox_tables_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Запись в БД обновленных данных
        if (m_bChange && MessageBox.Show("Сохранить изменения в таблице " + m_CurTableName + "?",
                                         "Внимание!", MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
            // Очистить изначально считанные данные
            Data.Clear();
            // Считать измененные данные из актуальной таблицы
            for (int i = 0; i < dataGrid.Rows.Count - 1; i++)       // Последний ряд пустой
            {
                string[] str = new string[Columns.Count];           // Массив строк одного ряда
                for (int j = 1; j < Columns.Count; j++)             // От единицы, потому что индексы не заносим
                {
                    // Проверка, все ли столбцы заполнены
                    if (dataGrid.Rows[i].Cells[j].Value == null)
                        str[j - 1] = "null";        // Если в ячейке пусто, то занести в базу "null"
                    else
                        str[j - 1] = dataGrid.Rows[i].Cells[j].Value.ToString();
                }
                // Добавления ряда в список
                Data.Add(str);
            }

            // Формирование последовательности названий столбцов для запроса
            string strCol = "(";                            // Запрос начинается с "("
            for (int i = 1; i < Columns.Count; i++)         // От 1, потому что индекс не заносим
            {
                strCol += Columns[i][1];
                if (i != Columns.Count - 1)
                    strCol += ",";                          // Разделитель между названиями столбцов
            }
            strCol += ")";      // Конец последовательности названий столбцов

            // Формирование последовательности значений таблицы для запроса
            string strValues = "('";
            for (int i = 0; i < Data.Count; i++)
            {
                for (int j = 0; j < Columns.Count - 1; j++)     // -1 потому что индекс не учитывается
                {
                    strValues += Data[i][j];
                    if (j != Columns.Count - 2)         // -2 потому что индекс не учитывается
                        strValues += "','";             // Разделитель для значений
                }
                if (i != Data.Count - 1)
                    strValues += "'),('";               // Разделитель для кортежей значений (заносим несколько строк в БД)
            }
            strValues += "')";  // Конец последовательности значений

            bool bFlag = true;  // Флаг, занесли ли в базу
            Query.InUpDel("DELETE FROM " + m_CurTableName, ref bFlag);      // Очищаем старую таблицу
            Query.InUpDel("INSERT INTO " + m_CurTableName + strCol + " VALUES " + strValues, ref bFlag);    // Вставка измененной таблицы
        }
        //--------------------------------
        // Отображение выбранной таблицы
        m_CurTableName = comboBox_tables.Text;      // Имя текущей таблицы

        Columns = Query.Select("PRAGMA table_info(" + m_CurTableName + ")");    // Получить названия столбцов
        Data    = Query.Select("SELECT * FROM " + m_CurTableName);              // Получить значение из текущей таблицы

        dataGrid.Rows.Clear();                  // Стираем старую таблицу
        dataGrid.ColumnCount = Columns.Count;   // Количество стобцов новой таблицы
        m_bChange = false;                      // Сброс флага об изменении

        // Заменить названия столбцов из БД на русские
        RussianColumns();
        // Задать новые названия столбцов
        for (int i = 0; i < Columns.Count; i++)
            dataGrid.Columns[i].Name = Columns[i][1];

        // Особое отображение для таблицы загрузок
        if (m_CurTableName == "Zagruzka")
            ReportSheet();
        else
        {
            // Отображение содержимого БД
            for (int i = 0; i < Data.Count; i++)
                dataGrid.Rows.Add(Data[i]);
        }

        dataGrid.Columns[0].Visible = false;          // Скрыть ID записи
    }

    // Отображение таблицы загрузок
    private void ReportSheet()
    {
        string[] sTypeCargos;           // Массив строк с типами грузов
        string sBuf = null;             // Буфер с одним типом груза
        int nCount = 0;                 // Количество одинаковых грузов в самолете
        string[] sRow;                  // Массив строк из ряда таблицы
        int nCargoIndex = 0;            // Тип груза в числовом формате

        // Цикл по всем рядам
        for (int i = 0; i < Data.Count; i++)
        {
            // Занесение самолетов только с парашютистами
            if (Data[i][5] == "0")          // Если грузов нет
            {
                Data[i][4] = "---";         // Тип груза
                dataGrid.Rows.Add(Data[i]); // Добавить ряд
                continue;                   // Перейти к следующему ряду
            }

            // Занесение смешанных и грузовых самолетов     
            sTypeCargos = Data[i][4].Split(' ');           // Разделить строку списка грузов на строки с одним типом груза
            nCount = 0;                                    // Сброс счетчика
            sBuf = sTypeCargos[0];                         // Занесение в буфер первого груза для сравнения с остальными
            // Цикл по всем грузам в самолете
            for (int j = 0; j < sTypeCargos.Length; j++)
            {
                // Если встретился новый тип груза
                if (sTypeCargos[j] != sBuf)
                {
                    // Изменить соответствующую строку в таблице
                    sRow = Data[i];
                    nCargoIndex = Convert.ToInt32(sBuf) - 1;    // Номер типа груза
                    sRow[4] = CargoData[nCargoIndex][1];        // Название груза
                    sRow[5] = nCount.ToString();                // Количество груза
                    dataGrid.Rows.Add(sRow);                    // Добавить измененную строку

                    nCount = 0;     // Сброс счетчика одинаковых грузов
                }
                // Если тип груза такой же, как и предыдущий
                sBuf = sTypeCargos[j];      // Занести в буфер тип груза
                nCount++;                   // Увеличить количество груза
            }
        }
    }

    // Заменить названия столбцов из БД на русские
    private void RussianColumns()
    {
        for (int i = 0; i < Columns.Count; i++)
        {
            switch (Columns[i][1])
            {
                case "name_aircraft":
                    Columns[i][1] = "Самолет";
                    break;
                case "vzlet_massa":
                    Columns[i][1] = "Взлетная масса";
                    break;
                case "max_polez_nagruzka":
                    Columns[i][1] = "Макс. пол. нагрузка";
                    break;
                case "max_massa_topliva":
                    Columns[i][1] = "Макс. масса топлива";
                    break;
                case "v_kreysers":
                    Columns[i][1] = "Крейсер. скорость";
                    break;
                case "dlina_gruz_kabin":
                    Columns[i][1] = "Длина кабины";
                    break;
                case "shirina_gruz_kabin":
                    Columns[i][1] = "Ширина кабины";
                    break;
                case "visota_gruz_kabin":
                    Columns[i][1] = "Высота кабины";
                    break;
                case "max_dalnost_poleta":
                    Columns[i][1] = "Дальность полета";
                    break;
                case "potreb_dlina_vpp":
                    Columns[i][1] = "Требуемая ВПП";
                    break;
                case "tip_cargo":
                    Columns[i][1] = "Тип груза";
                    break;
                case "massa":
                    Columns[i][1] = "Масса";
                    break;
                case "dlina":
                    Columns[i][1] = "Длина";
                    break;
                case "shirina":
                    Columns[i][1] = "Ширина";
                    break;
                case "visota":
                    Columns[i][1] = "Высота";
                    break;
                case "id_drop":
                    Columns[i][1] = "Высадка";
                    break;
                case "amount_aircraft":
                    Columns[i][1] = "Кол-во самолетов";
                    break;
                case "list_cargo":
                    Columns[i][1] = "Груз";
                    break;
                case "amount_cargo":
                    Columns[i][1] = "Кол-во груза";
                    break;
                case "amount_ppl":
                    Columns[i][1] = "Парашютисты";
                    break;
            }
        }
    }

    // Событие при изменении содержимого ячейки
    private void dataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
        m_bChange = true;
    }

    // Событие при удалении строки
    private void dataGrid_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
    {
        m_bChange = true;
    }

    // Проверка совпадают ли Id записи у текущей строки и строки ниже
    bool SamePlane(int nRow)
    {
        DataGridViewCell cell1 = dataGrid[0, nRow];
        DataGridViewCell cell2 = dataGrid[0, nRow - 1];

        // Если ячейки пусты, то не анализировать
        if (cell1.Value == null || cell2.Value == null)
            return false;

        // Если значения одинаковые, то true, если нет, то false
        return cell1.Value.ToString() == cell2.Value.ToString();
    }

    // Проверка совпадают ли значения в текущей ячейке и ячейке ниже
    bool SameValue(int nColumn, int nRow)
    {
        DataGridViewCell cell1 = dataGrid[nColumn, nRow];
        DataGridViewCell cell2 = dataGrid[nColumn, nRow - 1];

        // Если ячейки пусты, то не анализировать
        if (cell1.Value == null || cell2.Value == null)
            return false;

        // Если значения одинаковые, то true, если нет, то false
        return cell1.Value.ToString() == cell2.Value.ToString();
    }

    // Событие при отрисовке ячейки
    private void dataGrid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
    {
        // Убрать у каждой ячейки низ, кроме заголовка
        if (e.RowIndex != -1)
            e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;

        // Не анализировать заголовки
        if (e.RowIndex < 1 || e.ColumnIndex < 0)
            return;

        // Объединение ячеек для высадки по 1 столбцу и одинаковому значению;
        // для самолета, количества самолетов и людей - по соответ. столбцам и одинаковому номеру записи в БД
        if ((SamePlane(e.RowIndex) && (e.ColumnIndex == 2 || e.ColumnIndex == 3 || e.ColumnIndex == 6)) ||
            (SameValue(e.ColumnIndex, e.RowIndex) && e.ColumnIndex == 1))
            e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
        else
            e.AdvancedBorderStyle.Top = dataGrid.AdvancedCellBorderStyle.Top;
    }

    // Событие при форматировании содержимого ячейки
    private void dataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        // Не анализировать заголовки
        if (e.RowIndex == 0)
            return;

        // Стирание одинаковых значений: для высадки по 1 столбцу и одинаковому значению;
        // для самолета, количества самолетов и людей - по соответ. столбцам и одинаковому номеру записи в БД
        if ((SamePlane(e.RowIndex) && (e.ColumnIndex == 2 || e.ColumnIndex == 3 || e.ColumnIndex == 6)) ||
            (SameValue(e.ColumnIndex, e.RowIndex) && e.ColumnIndex == 1))
        {
            e.Value = "";
            e.FormattingApplied = true;
        }
    }
}
