using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

// Структура грузов, которые в единичном экземпляре не влезут в самолет по габаритам
public struct LargeCargo
{
    public int nAirType;        // Тип самолета
    public int nCargoType;      // Тип груза
    public string sAirName;     // Название самолета
    public string sCargoName;   // Название груза
    public int nAmount;         // Количество груза данного вида
}

// Структура выходных данных для формы результата
public struct OutputData
{
    //public double dTSerMax;   // Максимальное время серии для одного типа самолета
    public string sAirName;   // Название самолета
    public double dTpdb;      // Общее время серии
    public double dL;         // Длина площадки приземления
    public double dB;         // Ширина площадки приземления
}

// Структура входных данных для расчета на форме результата
public struct HeiData
{
    public double dHist;    // Истинная высота над наивысшим препятсятвием
    public double dHprep;   // Высота наивысшей точки препятствий
    public double dDHprep;  // Превышение препятствий над наивысшей точкой рельефа
    public double dHrel;    // Высота наивысшей точки рельефа
    public double dDHrel;   // Превышение рельефа над ТПП
    public double dHdes;    // Безопасная высота полета
}

public partial class Form_Main : Form
{
    bool m_bBase;                    // Флаг, используется ли БД
    List<string[]> m_AirData;        // Данные о самолетах из БД
    List<string[]> m_CargoData;      // Данные о грузе из БД
    List<Aircraft> m_Aircrafts;      // Список самолетов для высадки всех типов
    List<OutputData> m_nOutputData;  // Выходные данные для каждого типа самолета

    List<decimal> m_nCargos;          // Список единиц каждого вида груза 
    List<LargeCargo> m_LargeCargos;   // Список грузов, которые не вмещаются в кабину     
    decimal       m_nCargoSum;        // Общее число грузов

    HeiData m_HeighData;              // Сведения о высотах
    const int m_nPpl = 350;           // Количество парашютистов для высадки
    const int c_nW   = 100;           // Путевая скорость самолета, м/с, используется в формеле (8)

    public Form_Main()
    {
        // Инициализация полей класса
        m_bBase = false;
        m_Aircrafts = new List<Aircraft>();
        m_nOutputData = new List<OutputData>();
        m_nCargos   = new List<decimal>();
        m_LargeCargos = new List<LargeCargo>();

        InitializeComponent();
        m_bBase = Query.TryConnect();     // Проверка подключения к БД
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        RichTextFormat();
        groupBox3.Enabled = false;

        // Если есть подключение к базе
        if (m_bBase)
        {
            m_AirData = Query.Select("SELECT * FROM Aircraft");    // Считать данные о самолетах
            PrintCargoNames();
        }
    }

    // Форматирование подписей для отображения подстрочного текста
    void RichTextFormat()
    {
        richTextBox1.SelectionStart = 1;
        richTextBox1.SelectionLength = 4;
        richTextBox1.SelectionCharOffset = -5;

        richTextBox2.SelectionStart = 1;
        richTextBox2.SelectionLength = 3;
        richTextBox2.SelectionCharOffset = -5;

        richTextBox3.SelectionStart = 2;
        richTextBox3.SelectionLength = 3;
        richTextBox3.SelectionCharOffset = -5;
    }

    // Проверить количество введенных грузов
    private int CheckCagroCount()
    {
        // Очистить данные прошлых измерений
        m_nCargoSum = 0;
        m_nCargos.Clear();

        // Подсчет по всем числовым полям
        foreach (NumericUpDown Num in groupBox2.Controls.OfType<NumericUpDown>())
        {
            m_nCargos.Insert(0, Num.Value);
            //m_nCargos.Add(Num.Value);         // Т.к. считает снизу, то Add не подходит
            m_nCargoSum += Num.Value;
        }

        if (m_nCargoSum > 39)
            return 0;

        return 1;
    }

    // Проверка, выбрана ли местность
    private int CheckRadio()
    {
        int nRet = 1;

        // Отмечены ли RadioButton
        if ((!radio_mount.Checked && !radio_flat.Checked) ||
             (radio_mount.Checked && !radio_less.Checked && !radio_more.Checked))
            nRet = 0;

        return nRet;
    }

    // Получить значения из текстовых полей
    private int GetInitialValues()
    {
        int nRet = 1;

        if (TB_Hprep.TextLength != 0)
        {
            TB_Hprep.BackColor = Color.White;
            m_HeighData.dHprep = Convert.ToDouble(TB_Hprep.Text);
        }
        else
        {
            TB_Hprep.BackColor = Color.Red;
            nRet = 0;
        }

        if (TB_Hrel.TextLength != 0)
        {
            TB_Hrel.BackColor = Color.White;
            m_HeighData.dHrel = Convert.ToDouble(TB_Hrel.Text);
        }
        else
        {
            TB_Hrel.BackColor = Color.Red;
            nRet = 0;
        }

        if (TB_DHrel.Text.Length != 0)
        {
            TB_DHrel.BackColor = Color.White;
            m_HeighData.dDHrel = Convert.ToDouble(TB_DHrel.Text);
        }
        else
        {
            TB_DHrel.BackColor = Color.Red;
            nRet = 0;
        }

        return nRet;
    }

    // Проверить вводимые значения из тестовых полей на выполнение условию Hпреп ≥ Hрел ≥ ΔHрел
    private string CheckInitialValues()
    {
        string sMes = "";

        if (m_HeighData.dHprep < m_HeighData.dHrel)
            sMes += "Должно выполняться неравенство: Hпреп ≥ Hрел\n";
        if (m_HeighData.dHprep < m_HeighData.dDHrel)
            sMes += "Должно выполняться неравенство: Hпреп ≥ ΔHрел\n";
        if (m_HeighData.dHrel < m_HeighData.dDHrel)
            sMes += "Должно выполняться неравенство: Hрел ≥ ΔHрел";

        return sMes;
    }

    // Обработка нажатия на "Изменить БД"
    private void button1_Click(object sender, EventArgs e)
    {
        // Создать модальный диалог
        Form_Data FData = new Form_Data(m_CargoData);
        this.Enabled = false;
        if (FData.ShowDialog() == DialogResult.Cancel)
            this.Enabled = true;
    }

    // Выбор местности "Горы"
    private void radio_mount_CheckedChanged(object sender, EventArgs e)
    {
        // Сделать активным выбор высоты гор
        if (radio_mount.Checked)
            groupBox3.Enabled = true;
        else
            groupBox3.Enabled = false;
    }

    // Отобразить названия грузов
    private void PrintCargoNames()
    {
        m_CargoData = Query.Select("SELECT * FROM Cargo");

        Cargo1.Text = m_CargoData[0][1];
        Cargo2.Text = m_CargoData[1][1];
        Cargo3.Text = m_CargoData[2][1];
        Cargo4.Text = m_CargoData[3][1];
        Cargo5.Text = m_CargoData[4][1];
        Cargo6.Text = m_CargoData[5][1];
        Cargo7.Text = m_CargoData[6][1];
        Cargo8.Text = m_CargoData[7][1];
    }

    // Форматирование полей ввода высот
    private void TB_KeyPress(object sender, KeyPressEventArgs e)
    {
        // Запрет всех символов, кроме управляющих, цифр и ','
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            e.Handled = true;

        // Запрет использования больше одного символа ','
        try
        {
            if ((e.KeyChar == ',') && ((sender as ComboBox).Text.IndexOf(',') > -1))
                e.Handled = true;
        }
        catch
        {
            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
                e.Handled = true;
        }
    }

    // Высчитать безопасную высоту полета
    void CalcSafeHeight()
    {
        // Превышение препятствий над наивысшей точкой рельефа
        m_HeighData.dDHprep = m_HeighData.dHprep - m_HeighData.dHrel;

        // Зависимость высоты над наивысшим препятсятвием от местности
        if (radio_flat.Checked)
            m_HeighData.dHist = 200;
        else if (radio_less.Checked)
            m_HeighData.dHist = 300;
        else if (radio_more.Checked)
            m_HeighData.dHist = 600;

        m_HeighData.dHdes = m_HeighData.dHist + m_HeighData.dHrel + m_HeighData.dDHprep;
    }

    // Рассчитать загрузку самолетов
    private void CalcCargoLoad()
    {
        // Очистка буферов
        m_Aircrafts.Clear();
        m_LargeCargos.Clear();
        m_nOutputData.Clear();

        // Цикл по всем видам самолетов
        for (int i = 0; i < m_AirData.Count; i++)
        {
            // Инициализация переменных данными из БД
            int nType      = Convert.ToInt32 (m_AirData[i][0]);
            double dMass   = Convert.ToDouble(m_AirData[i][3]);
            double dLength = Convert.ToDouble(m_AirData[i][6]);
            double dWidth  = Convert.ToDouble(m_AirData[i][7]);
            double dHeight = Convert.ToDouble(m_AirData[i][8]);

            int nTdist = 0;            // Время дистанции, используется в формуле (7)
            // Для разных типов самолетов разное время дистанции
            switch (nType)
            {
                case 1:
                    nTdist = 30;
                    break;
                case 2:
                    nTdist = 50;
                    break;
                case 3:
                    nTdist = 40;
                    break;
            }

            CheckCagroCount();          // Для инициализации m_nCargoSum и m_nCargos при новых типах
            int nPpl = m_nPpl;          // Количество парашютистов
            double[] dTseries;          // Массив времен серий для каждого самолета
            // Список самолетов одного вида
            List<Aircraft> OneTypeAirCrafts = new List<Aircraft>();
            // Пока не загружены все грузы и парашютисты
            while (m_nCargoSum != 0 || nPpl > 0)
            {
                // Ицициализировать новый самолет
                Aircraft ACraft = new Aircraft(nType, dMass, dLength, dWidth, dHeight);

                // Цикл по всем грузам
                for (int j = 0; j < m_nCargos.Count; j++)
                {
                    // Цикл по всем грузам одного типа
                    while (m_nCargos[j] != 0)
                    {
                        // Инициализация переменных данными из БД
                        int nCType = Convert.ToInt32(m_CargoData[j][0]);
                        double dCMass = Convert.ToDouble(m_CargoData[j][2]);
                        double dCLength = Convert.ToDouble(m_CargoData[j][3]);
                        double dCWidth = Convert.ToDouble(m_CargoData[j][4]);
                        double dCHeight = Convert.ToDouble(m_CargoData[j][5]);

                        // Если загрузили груз в самолет
                        if (ACraft.AddCargo(nCType, dCMass, dCLength, dCWidth, dCHeight) == 1)
                        {
                            m_nCargoSum--;  // Уменьшить общее количество
                            m_nCargos[j]--; // Уменьшить количество грузов одного вида
                        }
                        // Если не загрузили груз, а других грузов на борту нет
                        else if (ACraft.GetNumOfCargos() == 0)
                        {
                            // Инициализация груза, который не влезает в кабину
                            LargeCargo zLCarg = new LargeCargo();
                            zLCarg.nAirType = nType;
                            zLCarg.nCargoType = nCType;
                            zLCarg.sAirName = m_AirData[i][1];
                            zLCarg.sCargoName = m_CargoData[j][1];
                            zLCarg.nAmount = (int)m_nCargos[j];          // Если не влез один одного типа, значит все не влезут
                            // Добавление груза в список больших грузов
                            m_LargeCargos.Add(zLCarg);

                            m_nCargoSum -= m_nCargos[j];    // Уменьшить общее количество на кол-во грузов этого вида
                            m_nCargos[j] = 0;               // Все грузы этого вида слишком большие, нет смысла анализировать остальные
                        }
                        // Иначе переходим к следующему грузу
                        else
                            break;
                    }
                }
                // Парашютисты
                if (nPpl > 0)
                    nPpl = ACraft.AddPpl(nPpl);     // Добавить парашютистов

                // Добавляем в список, если что-то загрузили на борт
                if ((ACraft.GetNumOfCargos() > 0) || (ACraft.GetNumOfPpl() > 0))
                    OneTypeAirCrafts.Add(ACraft);
            }

            // Инициализация массива времен серий по каждому самолету
            dTseries = new double[OneTypeAirCrafts.Count];
            for (int j = 0; j < OneTypeAirCrafts.Count; j++)
                dTseries[j] = OneTypeAirCrafts[j].CalcSerTime();

            // Занесение выходных данных
            OutputData outData = new OutputData();
            outData.sAirName = m_AirData[i][1];     // Название самолета
            // Общее время серии
            outData.dTpdb = dTseries.Sum() + (OneTypeAirCrafts.Count - 1) * nTdist;      // Формула (7)
            // Длина площадки приземления
            outData.dL = dTseries.Max() * c_nW + 1000;        // Формула (8)
            // Ширина площадки приземления
            outData.dB = outData.dL * 0.3;
            m_nOutputData.Add(outData);

            // Добавить самолеты одного типа в общий список
            m_Aircrafts.AddRange(OneTypeAirCrafts);
        }
    }

    // Добавить отчет о загрузке в БД
    private void AddReport()
    {
        bool bFlag = true;

        // Поиск самолетов с одинаковой загрузкой
        for (int i = 0; i < m_Aircrafts.Count - 1; i++)
        {
            for (int j = i + 1; j < m_Aircrafts.Count; j++)
                if (m_Aircrafts[i].m_sType == m_Aircrafts[j].m_sType &&
                    m_Aircrafts[i].GetListOfCargos() == m_Aircrafts[j].GetListOfCargos() &&
                    m_Aircrafts[i].GetNumOfPpl() == m_Aircrafts[j].GetNumOfPpl())
                {
                    m_Aircrafts[i].m_nAmount++;
                    m_Aircrafts.Remove(m_Aircrafts[j]);     // Удалить запись самолета с одинаковой загрузкой
                    j--;
                }
        }
        // Запись в базу
        Query.InUpDel("DELETE FROM zagruzka", ref bFlag);
        foreach (Aircraft Pln in m_Aircrafts)
        {
            Query.InUpDel("INSERT INTO Zagruzka (id_drop, name_aircraft, amount_aircraft, list_cargo, amount_cargo, amount_ppl) VALUES ('"
                + Pln.m_nType + "','" + Pln.m_sType + "','" + Pln.m_nAmount + "','" + Pln.GetListOfCargos() + "','"
                + Pln.GetNumOfCargos().ToString() + "','" + Pln.GetNumOfPpl().ToString() + "')", ref bFlag);
        }
    }

    // Обработка нажатия на "Расчитать ПП"
    private void BT_Calculate_Click(object sender, EventArgs e)
    {
        if (CheckRadio() == 0)   // Если не выбран рельеф
        {
            MessageBox.Show("Выберете местность", "Ошибка!");
            return;
        }

        if (GetInitialValues() == 0)   // Если не все поля заполнены
        {
            MessageBox.Show("Заполните все поля", "Ошибка!");
            return;
        }

        // Проверка вводимых высот
        string sMes = CheckInitialValues();
        if (sMes != "")         // Если строка не пустая
        {
            MessageBox.Show(sMes, "Ошибка ввода данных!");
            return;
        }

        if (CheckCagroCount() == 0)   // Если кол-во груза больше 39
        {
            MessageBox.Show("Количество груза равное " + m_nCargoSum + " не должно превышать 39 единиц", "Ошибка ввода данных!");
            return;
        }

        // Расчитать загрузку самолетов
        CalcCargoLoad();
        // Добавить отчет о загрузках в БД
        AddReport();
        // Рассчитать безопасную высоту полета
        CalcSafeHeight();

        // Отобразить окно с результатами
        Result Area = new Result(m_nOutputData, m_LargeCargos, m_HeighData);
        this.Enabled = false;
        if (Area.ShowDialog() == DialogResult.Cancel)
            this.Enabled = true;
    }
}
