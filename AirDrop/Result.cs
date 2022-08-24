using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


public partial class Result : Form
{
    HeiData m_HeighData;              // Сведения о высотах, полученные из основной формы
    List<OutputData> m_Info;          // Выходные данные для каждого типа самолета, полученные из основной формы
    List<LargeCargo> m_LargeCargos;   // Список грузов, которые не вмещаются в кабину, полученные из основной формы 

    // Конструктор
    public Result(List<OutputData> list, List<LargeCargo> Larges, HeiData HData)
    {
        InitializeComponent();
        // Инициализация полей
        m_HeighData = HData;
        m_LargeCargos = Larges;
        m_Info = new List<OutputData>();
        m_Info.AddRange(list);
    }

    private void Result_Load(object sender, EventArgs e)
    {
        // Занесение данных в таблицу не загруженных грузов
        string[] sDataRow = new string[3];
        foreach (LargeCargo Large in m_LargeCargos)
        {
            sDataRow[0] = Large.sAirName;
            sDataRow[1] = Large.sCargoName;
            sDataRow[2] = Large.nAmount.ToString();
            dataGridView1.Rows.Add(sDataRow);
        }

        // Посчитать безопасный эшелон и время снижения
        Calculate();
        // Форматирование подписей к данным для корректного отображения индексов
        RichTextFormat();
        // Выровнять выходные данные по центру
        SetAligment();
        // Перевести фокус на таблицу грузов
        ActiveControl = dataGridView1;
    }

    // Посчитать безопасный эшелон и время снижения
    void Calculate()
    {
        int nPmin = 716;    // Минимальное атмосферное давление на участке
        double nT0 = 18;    // Теспература воздуха в наивысшей точке рельефа

        double dHispr = m_HeighData.dHist + m_HeighData.dHrel + m_HeighData.dHprep + (760 - nPmin) * 11;        // Формула (4)
        // Поправка высотомера
        double dDHt = (nT0 - 15) / 300 * dHispr;    // Формула (3)
        // Безопасный эшелон
        double dHesh = m_HeighData.dHist + m_HeighData.dHrel + m_HeighData.dDHrel + (760 - nPmin) * 11 - dDHt;

        // Расчет времени снижения
        int nhstab = 150;
        int ntstab = 5;
        int ntnap = 3;
        int nvsn_teh = 7;
        int nvsn_ppl = 5;

        // Время снижения для техники
        double dTsn_teh = (m_HeighData.dHdes - nhstab) / nvsn_teh + ntstab + ntnap;
        // Время снижения для парашютистов
        double dTsn_ppl = (m_HeighData.dHdes - nhstab) / nvsn_ppl + ntstab + ntnap;

        // Занесение данных на форму
        richTextBox4.Text = string.Format("Hб.дес. = {0:0.00} м", m_HeighData.dHdes);
        richTextBox5.Text = string.Format("Hб.эш. ≥ {0:0.00} м", dHesh);
        richTextBox6.Text = string.Format("Tсн = {0:0.00} c", dTsn_teh);
        richTextBox7.Text = string.Format("Tсн = {0:0.00} c", dTsn_ppl);
    }

    // Форматирование подписей к данным для корректного отображения индексов
    void RichTextFormat()
    {
        richTextBox1.Text = string.Format("Tпдб = {0} c", m_Info[0].dTpdb); // Текст подписи
        richTextBox1.SelectionStart = 1;        // Начало выделения
        richTextBox1.SelectionLength = 3;       // Конец выделения
        richTextBox1.SelectionCharOffset = -5;  // На сколько опустить индекс
        //richTextBox1.SelectionLength = 0;

        richTextBox2.Text = string.Format("Tпдб = {0} c", m_Info[1].dTpdb);
        richTextBox2.SelectionStart = 1;
        richTextBox2.SelectionLength = 3;
        richTextBox2.SelectionCharOffset = -5;

        richTextBox3.Text = string.Format("Tпдб = {0} c", m_Info[2].dTpdb);
        richTextBox3.SelectionStart = 1;
        richTextBox3.SelectionLength = 3;
        richTextBox3.SelectionCharOffset = -5;

        richTextBox4.SelectionStart = 1;
        richTextBox4.SelectionLength = 6;
        richTextBox4.SelectionCharOffset = -5;

        richTextBox5.SelectionStart = 1;
        richTextBox5.SelectionLength = 5;
        richTextBox5.SelectionCharOffset = -5;

        richTextBox6.SelectionStart = 1;
        richTextBox6.SelectionLength = 2;
        richTextBox6.SelectionCharOffset = -5;

        richTextBox7.SelectionStart = 1;
        richTextBox7.SelectionLength = 2;
        richTextBox7.SelectionCharOffset = -5;

    }

    // Выровнять выходные данные по центру
    void SetAligment()
    {
        foreach (GroupBox group in Controls.OfType<GroupBox>())
            foreach (RichTextBox rich in group.Controls.OfType<RichTextBox>())
            {
                rich.SelectAll();
                rich.SelectionAlignment = HorizontalAlignment.Center;
            }
    }

    // Рисование площадок приземления
    private void pictureBox1_Paint(object sender, PaintEventArgs e)
    {
        // Масштабный коэффициет, делим на максимальную длину
        double dScale = (pictureBox1.Width - 100) / m_Info.Max(x => x.dL);       // -100 для надписей
        // Треть высоты
        float dThird  = (float)pictureBox1.Height / 3;
        // Цвета для площадок приземления
        Brush[] MasBrush = { Brushes.ForestGreen, Brushes.Honeydew, Brushes.DarkCyan };

        // Цикл по трем площадкам
        for (int i = 0; i < 3; i++)
        {
            float fWidth  = (float)(m_Info[i].dL * dScale); // Длина прямоугольника
            float fHeight = (float)(m_Info[i].dB * dScale); // Ширина прямоугольника
         
            // Рисование прямоугольника
            e.Graphics.FillRectangle(MasBrush[i], (pictureBox1.Width / 2) - (fWidth / 2),
                (dThird / 2 - fHeight / 2) + i * dThird, fWidth, fHeight);
            // Обводка прямоугольника
            e.Graphics.DrawRectangle(Pens.Black, (pictureBox1.Width / 2) - (fWidth / 2),
                (dThird / 2 - fHeight / 2) + i * dThird, fWidth, fHeight);

            // Название самолета в центре прямоугольника
            SizeF textsize = e.Graphics.MeasureString(m_Info[i].sAirName, Font);
            e.Graphics.DrawString(m_Info[i].sAirName, Font, Brushes.Black, (pictureBox1.Width / 2) - (textsize.Width / 2),
                (dThird / 2 - textsize.Height / 2) + i * dThird);

            // Подпись длины площадки приземления
            string strL = string.Format("L = {0} м", m_Info[i].dL);
            textsize = e.Graphics.MeasureString(strL, Font);
            e.Graphics.DrawString(strL, Font, Brushes.Black, (pictureBox1.Width / 2) - (textsize.Width / 2),
               (dThird / 2 - fHeight / 2) + i * dThird - textsize.Height);
     
            // Повернутая на 90 градусов подпись ширины площадки приземления
            string strB = string.Format("B = {0} м", m_Info[i].dB);
            textsize = e.Graphics.MeasureString(strB, Font);
            e.Graphics.TranslateTransform(pictureBox1.Width, 0);
            e.Graphics.RotateTransform(270);    // Поворот на 270 градусов по часовой стрелке
            e.Graphics.DrawString(strB, Font, Brushes.Black, 
                - dThird / 2 - textsize.Width / 2 - i * dThird, - pictureBox1.Width / 2 - fWidth / 2 - textsize.Height);
            e.Graphics.ResetTransform();
        }
    }
}
