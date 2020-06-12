namespace practice
{
    partial class Form1
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.clearButton = new System.Windows.Forms.Button();
            this.drawFunc = new System.Windows.Forms.Button();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.togetherRadioButton = new System.Windows.Forms.RadioButton();
            this.rungeKuttaTest = new System.Windows.Forms.RadioButton();
            this.testTaskButton = new System.Windows.Forms.RadioButton();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.hLable = new System.Windows.Forms.Label();
            this.hTextBox = new System.Windows.Forms.TextBox();
            this.bLable = new System.Windows.Forms.Label();
            this.bTextBox = new System.Windows.Forms.TextBox();
            this.aLable = new System.Windows.Forms.Label();
            this.aTextBox = new System.Windows.Forms.TextBox();
            this.cursorYLable = new System.Windows.Forms.Label();
            this.cursorXLable = new System.Windows.Forms.Label();
            this.verticalScroll = new System.Windows.Forms.TrackBar();
            this.horizontalScroll = new System.Windows.Forms.TrackBar();
            this.divisionValueValueLable = new System.Windows.Forms.Label();
            this.divisionValueLabel = new System.Windows.Forms.Label();
            this.scaleScrollValueLable = new System.Windows.Forms.Label();
            this.scaleScrollLabel = new System.Windows.Forms.Label();
            this.divisionValueScroll = new System.Windows.Forms.TrackBar();
            this.scaleScroll = new System.Windows.Forms.TrackBar();
            this.firstPagePaint = new System.Windows.Forms.Panel();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.verticalScroll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.horizontalScroll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.divisionValueScroll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scaleScroll)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(3, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1075, 756);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.clearButton);
            this.tabPage1.Controls.Add(this.drawFunc);
            this.tabPage1.Controls.Add(this.tabControl2);
            this.tabPage1.Controls.Add(this.hLable);
            this.tabPage1.Controls.Add(this.hTextBox);
            this.tabPage1.Controls.Add(this.bLable);
            this.tabPage1.Controls.Add(this.bTextBox);
            this.tabPage1.Controls.Add(this.aLable);
            this.tabPage1.Controls.Add(this.aTextBox);
            this.tabPage1.Controls.Add(this.cursorYLable);
            this.tabPage1.Controls.Add(this.cursorXLable);
            this.tabPage1.Controls.Add(this.verticalScroll);
            this.tabPage1.Controls.Add(this.horizontalScroll);
            this.tabPage1.Controls.Add(this.divisionValueValueLable);
            this.tabPage1.Controls.Add(this.divisionValueLabel);
            this.tabPage1.Controls.Add(this.scaleScrollValueLable);
            this.tabPage1.Controls.Add(this.scaleScrollLabel);
            this.tabPage1.Controls.Add(this.divisionValueScroll);
            this.tabPage1.Controls.Add(this.scaleScroll);
            this.tabPage1.Controls.Add(this.firstPagePaint);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1067, 730);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Основная программа";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // clearButton
            // 
            this.clearButton.Font = new System.Drawing.Font("Rubik", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clearButton.Location = new System.Drawing.Point(919, 230);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(107, 40);
            this.clearButton.TabIndex = 20;
            this.clearButton.Text = "Очистить";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // drawFunc
            // 
            this.drawFunc.Font = new System.Drawing.Font("Rubik", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.drawFunc.Location = new System.Drawing.Point(966, 537);
            this.drawFunc.Name = "drawFunc";
            this.drawFunc.Size = new System.Drawing.Size(75, 23);
            this.drawFunc.TabIndex = 2;
            this.drawFunc.Text = "button1";
            this.drawFunc.UseVisualStyleBackColor = true;
            this.drawFunc.Click += new System.EventHandler(this.drawFunc_Click);
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Location = new System.Drawing.Point(703, 283);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(358, 248);
            this.tabControl2.TabIndex = 19;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.checkBox4);
            this.tabPage3.Controls.Add(this.checkBox3);
            this.tabPage3.Controls.Add(this.checkBox2);
            this.tabPage3.Controls.Add(this.checkBox1);
            this.tabPage3.Controls.Add(this.panel1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(350, 222);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Тестовый пример";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Font = new System.Drawing.Font("Rubik", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox1.Location = new System.Drawing.Point(10, 126);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(252, 20);
            this.checkBox1.TabIndex = 13;
            this.checkBox1.Text = "Отображать точки точного решения";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.togetherRadioButton);
            this.panel1.Controls.Add(this.rungeKuttaTest);
            this.panel1.Controls.Add(this.testTaskButton);
            this.panel1.Font = new System.Drawing.Font("Rubik", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.panel1.Location = new System.Drawing.Point(6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(327, 103);
            this.panel1.TabIndex = 12;
            // 
            // togetherRadioButton
            // 
            this.togetherRadioButton.AutoSize = true;
            this.togetherRadioButton.Location = new System.Drawing.Point(4, 64);
            this.togetherRadioButton.Name = "togetherRadioButton";
            this.togetherRadioButton.Size = new System.Drawing.Size(115, 24);
            this.togetherRadioButton.TabIndex = 2;
            this.togetherRadioButton.Text = "Всё вместе";
            this.togetherRadioButton.UseVisualStyleBackColor = true;
            this.togetherRadioButton.CheckedChanged += new System.EventHandler(this.togetherRadioButton_CheckedChanged);
            // 
            // rungeKuttaTest
            // 
            this.rungeKuttaTest.AutoSize = true;
            this.rungeKuttaTest.Location = new System.Drawing.Point(4, 41);
            this.rungeKuttaTest.Name = "rungeKuttaTest";
            this.rungeKuttaTest.Size = new System.Drawing.Size(271, 24);
            this.rungeKuttaTest.TabIndex = 1;
            this.rungeKuttaTest.Text = "Решение методом Рунге-Кутты";
            this.rungeKuttaTest.UseVisualStyleBackColor = true;
            this.rungeKuttaTest.CheckedChanged += new System.EventHandler(this.rungeKuttaTest_CheckedChanged);
            // 
            // testTaskButton
            // 
            this.testTaskButton.AutoSize = true;
            this.testTaskButton.Checked = true;
            this.testTaskButton.Location = new System.Drawing.Point(4, 17);
            this.testTaskButton.Name = "testTaskButton";
            this.testTaskButton.Size = new System.Drawing.Size(309, 24);
            this.testTaskButton.TabIndex = 0;
            this.testTaskButton.TabStop = true;
            this.testTaskButton.Text = "Точное решение тестового примера";
            this.testTaskButton.UseVisualStyleBackColor = true;
            this.testTaskButton.CheckedChanged += new System.EventHandler(this.testTaskButton_CheckedChanged);
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(319, 222);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Задача";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // hLable
            // 
            this.hLable.AutoSize = true;
            this.hLable.Font = new System.Drawing.Font("Rubik", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.hLable.Location = new System.Drawing.Point(796, 240);
            this.hLable.Name = "hLable";
            this.hLable.Size = new System.Drawing.Size(23, 20);
            this.hLable.TabIndex = 18;
            this.hLable.Text = "h:";
            // 
            // hTextBox
            // 
            this.hTextBox.Font = new System.Drawing.Font("Rubik", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.hTextBox.Location = new System.Drawing.Point(822, 237);
            this.hTextBox.Name = "hTextBox";
            this.hTextBox.Size = new System.Drawing.Size(51, 26);
            this.hTextBox.TabIndex = 17;
            // 
            // bLable
            // 
            this.bLable.AutoSize = true;
            this.bLable.Font = new System.Drawing.Font("Rubik", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bLable.Location = new System.Drawing.Point(714, 253);
            this.bLable.Name = "bLable";
            this.bLable.Size = new System.Drawing.Size(23, 20);
            this.bLable.TabIndex = 16;
            this.bLable.Text = "b:";
            // 
            // bTextBox
            // 
            this.bTextBox.Font = new System.Drawing.Font("Rubik", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bTextBox.Location = new System.Drawing.Point(739, 251);
            this.bTextBox.Name = "bTextBox";
            this.bTextBox.Size = new System.Drawing.Size(51, 26);
            this.bTextBox.TabIndex = 15;
            // 
            // aLable
            // 
            this.aLable.AutoSize = true;
            this.aLable.Font = new System.Drawing.Font("Rubik", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.aLable.Location = new System.Drawing.Point(714, 226);
            this.aLable.Name = "aLable";
            this.aLable.Size = new System.Drawing.Size(22, 20);
            this.aLable.TabIndex = 14;
            this.aLable.Text = "a:";
            // 
            // aTextBox
            // 
            this.aTextBox.Font = new System.Drawing.Font("Rubik", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.aTextBox.Location = new System.Drawing.Point(739, 223);
            this.aTextBox.Name = "aTextBox";
            this.aTextBox.Size = new System.Drawing.Size(51, 26);
            this.aTextBox.TabIndex = 13;
            // 
            // cursorYLable
            // 
            this.cursorYLable.AutoSize = true;
            this.cursorYLable.BackColor = System.Drawing.Color.Yellow;
            this.cursorYLable.Location = new System.Drawing.Point(668, 25);
            this.cursorYLable.Name = "cursorYLable";
            this.cursorYLable.Size = new System.Drawing.Size(35, 13);
            this.cursorYLable.TabIndex = 11;
            this.cursorYLable.Text = "label2";
            // 
            // cursorXLable
            // 
            this.cursorXLable.AutoSize = true;
            this.cursorXLable.BackColor = System.Drawing.Color.Yellow;
            this.cursorXLable.Location = new System.Drawing.Point(58, 9);
            this.cursorXLable.Name = "cursorXLable";
            this.cursorXLable.Size = new System.Drawing.Size(35, 13);
            this.cursorXLable.TabIndex = 10;
            this.cursorXLable.Text = "label1";
            // 
            // verticalScroll
            // 
            this.verticalScroll.Location = new System.Drawing.Point(10, 25);
            this.verticalScroll.Maximum = 0;
            this.verticalScroll.Minimum = -600;
            this.verticalScroll.Name = "verticalScroll";
            this.verticalScroll.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.verticalScroll.Size = new System.Drawing.Size(45, 601);
            this.verticalScroll.TabIndex = 9;
            this.verticalScroll.Scroll += new System.EventHandler(this.verticalScroll_Scroll);
            // 
            // horizontalScroll
            // 
            this.horizontalScroll.Location = new System.Drawing.Point(61, 632);
            this.horizontalScroll.Maximum = 0;
            this.horizontalScroll.Minimum = -600;
            this.horizontalScroll.Name = "horizontalScroll";
            this.horizontalScroll.Size = new System.Drawing.Size(601, 45);
            this.horizontalScroll.TabIndex = 8;
            this.horizontalScroll.Scroll += new System.EventHandler(this.horizontalScroll_Scroll);
            // 
            // divisionValueValueLable
            // 
            this.divisionValueValueLable.AutoSize = true;
            this.divisionValueValueLable.Font = new System.Drawing.Font("Rubik", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.divisionValueValueLable.Location = new System.Drawing.Point(884, 130);
            this.divisionValueValueLable.Name = "divisionValueValueLable";
            this.divisionValueValueLable.Size = new System.Drawing.Size(29, 20);
            this.divisionValueValueLable.TabIndex = 7;
            this.divisionValueValueLable.Text = "0.1";
            // 
            // divisionValueLabel
            // 
            this.divisionValueLabel.AutoSize = true;
            this.divisionValueLabel.Font = new System.Drawing.Font("Rubik", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.divisionValueLabel.Location = new System.Drawing.Point(766, 130);
            this.divisionValueLabel.Name = "divisionValueLabel";
            this.divisionValueLabel.Size = new System.Drawing.Size(123, 20);
            this.divisionValueLabel.TabIndex = 6;
            this.divisionValueLabel.Text = "Цена деления:";
            // 
            // scaleScrollValueLable
            // 
            this.scaleScrollValueLable.AutoSize = true;
            this.scaleScrollValueLable.Font = new System.Drawing.Font("Rubik", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.scaleScrollValueLable.Location = new System.Drawing.Point(844, 49);
            this.scaleScrollValueLable.Name = "scaleScrollValueLable";
            this.scaleScrollValueLable.Size = new System.Drawing.Size(29, 20);
            this.scaleScrollValueLable.TabIndex = 5;
            this.scaleScrollValueLable.Text = "0.1";
            this.scaleScrollValueLable.Click += new System.EventHandler(this.scaleScrollValueLable_Click);
            // 
            // scaleScrollLabel
            // 
            this.scaleScrollLabel.AutoSize = true;
            this.scaleScrollLabel.Font = new System.Drawing.Font("Rubik", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.scaleScrollLabel.Location = new System.Drawing.Point(766, 49);
            this.scaleScrollLabel.Name = "scaleScrollLabel";
            this.scaleScrollLabel.Size = new System.Drawing.Size(83, 20);
            this.scaleScrollLabel.TabIndex = 4;
            this.scaleScrollLabel.Text = "Масштаб:";
            // 
            // divisionValueScroll
            // 
            this.divisionValueScroll.Location = new System.Drawing.Point(770, 153);
            this.divisionValueScroll.Maximum = 1000;
            this.divisionValueScroll.Minimum = 100;
            this.divisionValueScroll.Name = "divisionValueScroll";
            this.divisionValueScroll.Size = new System.Drawing.Size(216, 45);
            this.divisionValueScroll.TabIndex = 3;
            this.divisionValueScroll.Value = 100;
            this.divisionValueScroll.Scroll += new System.EventHandler(this.divisionValueScroll_Scroll);
            // 
            // scaleScroll
            // 
            this.scaleScroll.Location = new System.Drawing.Point(770, 72);
            this.scaleScroll.Maximum = 200;
            this.scaleScroll.Minimum = 10;
            this.scaleScroll.Name = "scaleScroll";
            this.scaleScroll.Size = new System.Drawing.Size(216, 45);
            this.scaleScroll.TabIndex = 1;
            this.scaleScroll.TickFrequency = 10;
            this.scaleScroll.Value = 10;
            this.scaleScroll.Scroll += new System.EventHandler(this.scaleScroll_Scroll);
            // 
            // firstPagePaint
            // 
            this.firstPagePaint.BackColor = System.Drawing.Color.Gainsboro;
            this.firstPagePaint.Location = new System.Drawing.Point(61, 25);
            this.firstPagePaint.Name = "firstPagePaint";
            this.firstPagePaint.Size = new System.Drawing.Size(601, 601);
            this.firstPagePaint.TabIndex = 0;
            this.firstPagePaint.Paint += new System.Windows.Forms.PaintEventHandler(this.firstPagePaint_Paint);
            this.firstPagePaint.MouseHover += new System.EventHandler(this.firstPagePaint_MouseHover);
            this.firstPagePaint.MouseMove += new System.Windows.Forms.MouseEventHandler(this.firstPagePaint_MouseMove);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Font = new System.Drawing.Font("Rubik", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox2.Location = new System.Drawing.Point(10, 146);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(256, 20);
            this.checkBox2.TabIndex = 14;
            this.checkBox2.Text = "Отображать линии точного решения";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Font = new System.Drawing.Font("Rubik", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox3.Location = new System.Drawing.Point(10, 168);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(340, 20);
            this.checkBox3.TabIndex = 15;
            this.checkBox3.Text = "Отображать точки решения методом Рунге-Кутты";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Checked = true;
            this.checkBox4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox4.Font = new System.Drawing.Font("Rubik", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox4.Location = new System.Drawing.Point(10, 188);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(344, 20);
            this.checkBox4.TabIndex = 16;
            this.checkBox4.Text = "Отображать линии решения методом Рунге-Кутты";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 756);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Решение задачи Коши методом Рунге-Кутты";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.verticalScroll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.horizontalScroll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.divisionValueScroll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scaleScroll)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel firstPagePaint;
        private System.Windows.Forms.TrackBar scaleScroll;
        private System.Windows.Forms.Button drawFunc;
        private System.Windows.Forms.Label scaleScrollValueLable;
        private System.Windows.Forms.Label scaleScrollLabel;
        private System.Windows.Forms.TrackBar divisionValueScroll;
        private System.Windows.Forms.Label divisionValueValueLable;
        private System.Windows.Forms.Label divisionValueLabel;
        private System.Windows.Forms.TrackBar verticalScroll;
        private System.Windows.Forms.TrackBar horizontalScroll;
        private System.Windows.Forms.Label cursorYLable;
        private System.Windows.Forms.Label cursorXLable;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton togetherRadioButton;
        private System.Windows.Forms.RadioButton rungeKuttaTest;
        private System.Windows.Forms.RadioButton testTaskButton;
        private System.Windows.Forms.Label hLable;
        private System.Windows.Forms.TextBox hTextBox;
        private System.Windows.Forms.Label bLable;
        private System.Windows.Forms.TextBox bTextBox;
        private System.Windows.Forms.Label aLable;
        private System.Windows.Forms.TextBox aTextBox;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
    }
}

