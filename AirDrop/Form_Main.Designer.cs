partial class Form_Main
{
    /// <summary>
    /// Обязательная переменная конструктора.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Освободить все используемые ресурсы.
    /// </summary>
    /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Код, автоматически созданный конструктором форм Windows

    /// <summary>
    /// Требуемый метод для поддержки конструктора — не изменяйте 
    /// содержимое этого метода с помощью редактора кода.
    /// </summary>
    private void InitializeComponent()
    {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Main));
            this.BT_Change = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TB_DHrel = new System.Windows.Forms.TextBox();
            this.TB_Hrel = new System.Windows.Forms.TextBox();
            this.TB_Hprep = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radio_more = new System.Windows.Forms.RadioButton();
            this.radio_less = new System.Windows.Forms.RadioButton();
            this.radio_mount = new System.Windows.Forms.RadioButton();
            this.radio_flat = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Num8 = new System.Windows.Forms.NumericUpDown();
            this.Num7 = new System.Windows.Forms.NumericUpDown();
            this.Num6 = new System.Windows.Forms.NumericUpDown();
            this.Num5 = new System.Windows.Forms.NumericUpDown();
            this.Num4 = new System.Windows.Forms.NumericUpDown();
            this.Num3 = new System.Windows.Forms.NumericUpDown();
            this.Num2 = new System.Windows.Forms.NumericUpDown();
            this.Num1 = new System.Windows.Forms.NumericUpDown();
            this.Cargo8 = new System.Windows.Forms.Label();
            this.Cargo7 = new System.Windows.Forms.Label();
            this.Cargo6 = new System.Windows.Forms.Label();
            this.Cargo5 = new System.Windows.Forms.Label();
            this.Cargo4 = new System.Windows.Forms.Label();
            this.Cargo3 = new System.Windows.Forms.Label();
            this.Cargo2 = new System.Windows.Forms.Label();
            this.Cargo1 = new System.Windows.Forms.Label();
            this.BT_Calculate = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num1)).BeginInit();
            this.SuspendLayout();
            // 
            // BT_Change
            // 
            this.BT_Change.Location = new System.Drawing.Point(340, 450);
            this.BT_Change.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BT_Change.Name = "BT_Change";
            this.BT_Change.Size = new System.Drawing.Size(302, 53);
            this.BT_Change.TabIndex = 0;
            this.BT_Change.Text = "Просмотр БД";
            this.BT_Change.UseVisualStyleBackColor = true;
            this.BT_Change.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.richTextBox3);
            this.groupBox1.Controls.Add(this.richTextBox2);
            this.groupBox1.Controls.Add(this.richTextBox1);
            this.groupBox1.Controls.Add(this.TB_DHrel);
            this.groupBox1.Controls.Add(this.TB_Hrel);
            this.groupBox1.Controls.Add(this.TB_Hprep);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.radio_mount);
            this.groupBox1.Controls.Add(this.radio_flat);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(303, 430);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Исходные данные";
            // 
            // TB_DHrel
            // 
            this.TB_DHrel.Location = new System.Drawing.Point(143, 362);
            this.TB_DHrel.MaxLength = 10;
            this.TB_DHrel.Name = "TB_DHrel";
            this.TB_DHrel.Size = new System.Drawing.Size(134, 35);
            this.TB_DHrel.TabIndex = 8;
            this.TB_DHrel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_KeyPress);
            // 
            // TB_Hrel
            // 
            this.TB_Hrel.Location = new System.Drawing.Point(143, 304);
            this.TB_Hrel.MaxLength = 10;
            this.TB_Hrel.Name = "TB_Hrel";
            this.TB_Hrel.Size = new System.Drawing.Size(134, 35);
            this.TB_Hrel.TabIndex = 7;
            this.TB_Hrel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_KeyPress);
            // 
            // TB_Hprep
            // 
            this.TB_Hprep.Location = new System.Drawing.Point(143, 246);
            this.TB_Hprep.MaxLength = 10;
            this.TB_Hprep.Name = "TB_Hprep";
            this.TB_Hprep.Size = new System.Drawing.Size(134, 35);
            this.TB_Hprep.TabIndex = 6;
            this.TB_Hprep.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_KeyPress);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radio_more);
            this.groupBox3.Controls.Add(this.radio_less);
            this.groupBox3.Location = new System.Drawing.Point(19, 92);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(258, 125);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Горы";
            // 
            // radio_more
            // 
            this.radio_more.AutoSize = true;
            this.radio_more.Location = new System.Drawing.Point(26, 74);
            this.radio_more.Name = "radio_more";
            this.radio_more.Size = new System.Drawing.Size(211, 33);
            this.radio_more.TabIndex = 4;
            this.radio_more.TabStop = true;
            this.radio_more.Text = "Больше  2000м";
            this.radio_more.UseVisualStyleBackColor = true;
            // 
            // radio_less
            // 
            this.radio_less.AutoSize = true;
            this.radio_less.Location = new System.Drawing.Point(26, 34);
            this.radio_less.Name = "radio_less";
            this.radio_less.Size = new System.Drawing.Size(210, 33);
            this.radio_less.TabIndex = 3;
            this.radio_less.TabStop = true;
            this.radio_less.Text = "Меньше 2000м";
            this.radio_less.UseVisualStyleBackColor = true;
            // 
            // radio_mount
            // 
            this.radio_mount.AutoSize = true;
            this.radio_mount.Location = new System.Drawing.Point(180, 43);
            this.radio_mount.Name = "radio_mount";
            this.radio_mount.Size = new System.Drawing.Size(97, 33);
            this.radio_mount.TabIndex = 1;
            this.radio_mount.TabStop = true;
            this.radio_mount.Text = "Горы";
            this.radio_mount.UseVisualStyleBackColor = true;
            this.radio_mount.CheckedChanged += new System.EventHandler(this.radio_mount_CheckedChanged);
            // 
            // radio_flat
            // 
            this.radio_flat.AutoSize = true;
            this.radio_flat.Location = new System.Drawing.Point(19, 43);
            this.radio_flat.Name = "radio_flat";
            this.radio_flat.Size = new System.Drawing.Size(138, 33);
            this.radio_flat.TabIndex = 0;
            this.radio_flat.TabStop = true;
            this.radio_flat.Text = "Равнина";
            this.radio_flat.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Num8);
            this.groupBox2.Controls.Add(this.Num7);
            this.groupBox2.Controls.Add(this.Num6);
            this.groupBox2.Controls.Add(this.Num5);
            this.groupBox2.Controls.Add(this.Num4);
            this.groupBox2.Controls.Add(this.Num3);
            this.groupBox2.Controls.Add(this.Num2);
            this.groupBox2.Controls.Add(this.Num1);
            this.groupBox2.Controls.Add(this.Cargo8);
            this.groupBox2.Controls.Add(this.Cargo7);
            this.groupBox2.Controls.Add(this.Cargo6);
            this.groupBox2.Controls.Add(this.Cargo5);
            this.groupBox2.Controls.Add(this.Cargo4);
            this.groupBox2.Controls.Add(this.Cargo3);
            this.groupBox2.Controls.Add(this.Cargo2);
            this.groupBox2.Controls.Add(this.Cargo1);
            this.groupBox2.Location = new System.Drawing.Point(340, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(303, 430);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Загрузка";
            // 
            // Num8
            // 
            this.Num8.Location = new System.Drawing.Point(193, 374);
            this.Num8.Name = "Num8";
            this.Num8.Size = new System.Drawing.Size(97, 35);
            this.Num8.TabIndex = 15;
            // 
            // Num7
            // 
            this.Num7.Location = new System.Drawing.Point(193, 326);
            this.Num7.Name = "Num7";
            this.Num7.Size = new System.Drawing.Size(97, 35);
            this.Num7.TabIndex = 14;
            // 
            // Num6
            // 
            this.Num6.Location = new System.Drawing.Point(193, 278);
            this.Num6.Name = "Num6";
            this.Num6.Size = new System.Drawing.Size(97, 35);
            this.Num6.TabIndex = 13;
            // 
            // Num5
            // 
            this.Num5.Location = new System.Drawing.Point(193, 230);
            this.Num5.Name = "Num5";
            this.Num5.Size = new System.Drawing.Size(97, 35);
            this.Num5.TabIndex = 12;
            // 
            // Num4
            // 
            this.Num4.Location = new System.Drawing.Point(193, 182);
            this.Num4.Name = "Num4";
            this.Num4.Size = new System.Drawing.Size(97, 35);
            this.Num4.TabIndex = 11;
            // 
            // Num3
            // 
            this.Num3.Location = new System.Drawing.Point(193, 134);
            this.Num3.Name = "Num3";
            this.Num3.Size = new System.Drawing.Size(97, 35);
            this.Num3.TabIndex = 10;
            // 
            // Num2
            // 
            this.Num2.Location = new System.Drawing.Point(193, 87);
            this.Num2.Name = "Num2";
            this.Num2.Size = new System.Drawing.Size(97, 35);
            this.Num2.TabIndex = 9;
            // 
            // Num1
            // 
            this.Num1.Location = new System.Drawing.Point(193, 39);
            this.Num1.Name = "Num1";
            this.Num1.Size = new System.Drawing.Size(97, 35);
            this.Num1.TabIndex = 8;
            // 
            // Cargo8
            // 
            this.Cargo8.AutoSize = true;
            this.Cargo8.Location = new System.Drawing.Point(17, 377);
            this.Cargo8.Name = "Cargo8";
            this.Cargo8.Size = new System.Drawing.Size(78, 29);
            this.Cargo8.TabIndex = 7;
            this.Cargo8.Text = "Груз8";
            // 
            // Cargo7
            // 
            this.Cargo7.AutoSize = true;
            this.Cargo7.Location = new System.Drawing.Point(17, 329);
            this.Cargo7.Name = "Cargo7";
            this.Cargo7.Size = new System.Drawing.Size(78, 29);
            this.Cargo7.TabIndex = 6;
            this.Cargo7.Text = "Груз7";
            // 
            // Cargo6
            // 
            this.Cargo6.AutoSize = true;
            this.Cargo6.Location = new System.Drawing.Point(17, 281);
            this.Cargo6.Name = "Cargo6";
            this.Cargo6.Size = new System.Drawing.Size(78, 29);
            this.Cargo6.TabIndex = 5;
            this.Cargo6.Text = "Груз6";
            // 
            // Cargo5
            // 
            this.Cargo5.AutoSize = true;
            this.Cargo5.Location = new System.Drawing.Point(17, 233);
            this.Cargo5.Name = "Cargo5";
            this.Cargo5.Size = new System.Drawing.Size(78, 29);
            this.Cargo5.TabIndex = 4;
            this.Cargo5.Text = "Груз5";
            // 
            // Cargo4
            // 
            this.Cargo4.AutoSize = true;
            this.Cargo4.Location = new System.Drawing.Point(17, 185);
            this.Cargo4.Name = "Cargo4";
            this.Cargo4.Size = new System.Drawing.Size(78, 29);
            this.Cargo4.TabIndex = 3;
            this.Cargo4.Text = "Груз4";
            // 
            // Cargo3
            // 
            this.Cargo3.AutoSize = true;
            this.Cargo3.Location = new System.Drawing.Point(17, 137);
            this.Cargo3.Name = "Cargo3";
            this.Cargo3.Size = new System.Drawing.Size(78, 29);
            this.Cargo3.TabIndex = 2;
            this.Cargo3.Text = "Груз3";
            // 
            // Cargo2
            // 
            this.Cargo2.AutoSize = true;
            this.Cargo2.Location = new System.Drawing.Point(17, 90);
            this.Cargo2.Name = "Cargo2";
            this.Cargo2.Size = new System.Drawing.Size(78, 29);
            this.Cargo2.TabIndex = 1;
            this.Cargo2.Text = "Груз2";
            // 
            // Cargo1
            // 
            this.Cargo1.AutoSize = true;
            this.Cargo1.Location = new System.Drawing.Point(17, 42);
            this.Cargo1.Name = "Cargo1";
            this.Cargo1.Size = new System.Drawing.Size(78, 29);
            this.Cargo1.TabIndex = 0;
            this.Cargo1.Text = "Груз1";
            // 
            // BT_Calculate
            // 
            this.BT_Calculate.Location = new System.Drawing.Point(14, 450);
            this.BT_Calculate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BT_Calculate.Name = "BT_Calculate";
            this.BT_Calculate.Size = new System.Drawing.Size(302, 53);
            this.BT_Calculate.TabIndex = 3;
            this.BT_Calculate.Text = "Расчитать ПП";
            this.BT_Calculate.UseVisualStyleBackColor = true;
            this.BT_Calculate.Click += new System.EventHandler(this.BT_Calculate_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(43, 249);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox1.Size = new System.Drawing.Size(97, 38);
            this.richTextBox1.TabIndex = 9;
            this.richTextBox1.Text = "Нпреп";
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox2.Location = new System.Drawing.Point(43, 308);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox2.Size = new System.Drawing.Size(97, 38);
            this.richTextBox2.TabIndex = 10;
            this.richTextBox2.Text = "Нрел";
            // 
            // richTextBox3
            // 
            this.richTextBox3.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox3.Location = new System.Drawing.Point(30, 367);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.ReadOnly = true;
            this.richTextBox3.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox3.Size = new System.Drawing.Size(97, 38);
            this.richTextBox3.TabIndex = 11;
            this.richTextBox3.Text = "ΔНрел";
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 513);
            this.Controls.Add(this.BT_Calculate);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.BT_Change);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Программа расчета площадок приземления";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num1)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button BT_Change;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.RadioButton radio_flat;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Button BT_Calculate;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.RadioButton radio_more;
    private System.Windows.Forms.RadioButton radio_less;
    private System.Windows.Forms.RadioButton radio_mount;
    private System.Windows.Forms.TextBox TB_DHrel;
    private System.Windows.Forms.TextBox TB_Hrel;
    private System.Windows.Forms.TextBox TB_Hprep;
    private System.Windows.Forms.Label Cargo1;
    private System.Windows.Forms.NumericUpDown Num8;
    private System.Windows.Forms.NumericUpDown Num7;
    private System.Windows.Forms.NumericUpDown Num6;
    private System.Windows.Forms.NumericUpDown Num5;
    private System.Windows.Forms.NumericUpDown Num4;
    private System.Windows.Forms.NumericUpDown Num3;
    private System.Windows.Forms.NumericUpDown Num2;
    private System.Windows.Forms.NumericUpDown Num1;
    private System.Windows.Forms.Label Cargo8;
    private System.Windows.Forms.Label Cargo7;
    private System.Windows.Forms.Label Cargo6;
    private System.Windows.Forms.Label Cargo5;
    private System.Windows.Forms.Label Cargo4;
    private System.Windows.Forms.Label Cargo3;
    private System.Windows.Forms.Label Cargo2;
    private System.Windows.Forms.RichTextBox richTextBox1;
    private System.Windows.Forms.RichTextBox richTextBox3;
    private System.Windows.Forms.RichTextBox richTextBox2;
}

