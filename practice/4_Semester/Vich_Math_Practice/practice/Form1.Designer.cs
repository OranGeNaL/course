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
            this.divisionValueValueLable = new System.Windows.Forms.Label();
            this.divisionValueLabel = new System.Windows.Forms.Label();
            this.scaleScrollValueLable = new System.Windows.Forms.Label();
            this.scaleScrollLabel = new System.Windows.Forms.Label();
            this.divisionValueScroll = new System.Windows.Forms.TrackBar();
            this.drawFunc = new System.Windows.Forms.Button();
            this.scaleScroll = new System.Windows.Forms.TrackBar();
            this.firstPagePaint = new System.Windows.Forms.Panel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.divisionValueScroll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scaleScroll)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(3, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(920, 648);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.divisionValueValueLable);
            this.tabPage1.Controls.Add(this.divisionValueLabel);
            this.tabPage1.Controls.Add(this.scaleScrollValueLable);
            this.tabPage1.Controls.Add(this.scaleScrollLabel);
            this.tabPage1.Controls.Add(this.divisionValueScroll);
            this.tabPage1.Controls.Add(this.drawFunc);
            this.tabPage1.Controls.Add(this.scaleScroll);
            this.tabPage1.Controls.Add(this.firstPagePaint);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(912, 622);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // divisionValueValueLable
            // 
            this.divisionValueValueLable.AutoSize = true;
            this.divisionValueValueLable.Font = new System.Drawing.Font("Rubik", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.divisionValueValueLable.Location = new System.Drawing.Point(762, 135);
            this.divisionValueValueLable.Name = "divisionValueValueLable";
            this.divisionValueValueLable.Size = new System.Drawing.Size(29, 20);
            this.divisionValueValueLable.TabIndex = 7;
            this.divisionValueValueLable.Text = "0.1";
            // 
            // divisionValueLabel
            // 
            this.divisionValueLabel.AutoSize = true;
            this.divisionValueLabel.Font = new System.Drawing.Font("Rubik", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.divisionValueLabel.Location = new System.Drawing.Point(644, 135);
            this.divisionValueLabel.Name = "divisionValueLabel";
            this.divisionValueLabel.Size = new System.Drawing.Size(123, 20);
            this.divisionValueLabel.TabIndex = 6;
            this.divisionValueLabel.Text = "Цена деления:";
            // 
            // scaleScrollValueLable
            // 
            this.scaleScrollValueLable.AutoSize = true;
            this.scaleScrollValueLable.Font = new System.Drawing.Font("Rubik", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.scaleScrollValueLable.Location = new System.Drawing.Point(722, 54);
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
            this.scaleScrollLabel.Location = new System.Drawing.Point(644, 54);
            this.scaleScrollLabel.Name = "scaleScrollLabel";
            this.scaleScrollLabel.Size = new System.Drawing.Size(83, 20);
            this.scaleScrollLabel.TabIndex = 4;
            this.scaleScrollLabel.Text = "Масштаб:";
            // 
            // divisionValueScroll
            // 
            this.divisionValueScroll.Location = new System.Drawing.Point(648, 158);
            this.divisionValueScroll.Maximum = 1000;
            this.divisionValueScroll.Minimum = 100;
            this.divisionValueScroll.Name = "divisionValueScroll";
            this.divisionValueScroll.Size = new System.Drawing.Size(216, 45);
            this.divisionValueScroll.TabIndex = 3;
            this.divisionValueScroll.Value = 100;
            this.divisionValueScroll.Scroll += new System.EventHandler(this.divisionValueScroll_Scroll);
            // 
            // drawFunc
            // 
            this.drawFunc.Location = new System.Drawing.Point(789, 335);
            this.drawFunc.Name = "drawFunc";
            this.drawFunc.Size = new System.Drawing.Size(75, 23);
            this.drawFunc.TabIndex = 2;
            this.drawFunc.Text = "button1";
            this.drawFunc.UseVisualStyleBackColor = true;
            this.drawFunc.Click += new System.EventHandler(this.drawFunc_Click);
            // 
            // scaleScroll
            // 
            this.scaleScroll.Location = new System.Drawing.Point(648, 77);
            this.scaleScroll.Maximum = 100;
            this.scaleScroll.Minimum = 10;
            this.scaleScroll.Name = "scaleScroll";
            this.scaleScroll.Size = new System.Drawing.Size(216, 45);
            this.scaleScroll.TabIndex = 1;
            this.scaleScroll.Value = 10;
            this.scaleScroll.Scroll += new System.EventHandler(this.scaleScroll_Scroll);
            // 
            // firstPagePaint
            // 
            this.firstPagePaint.Location = new System.Drawing.Point(12, 10);
            this.firstPagePaint.Name = "firstPagePaint";
            this.firstPagePaint.Size = new System.Drawing.Size(601, 601);
            this.firstPagePaint.TabIndex = 0;
            this.firstPagePaint.Paint += new System.Windows.Forms.PaintEventHandler(this.firstPagePaint_Paint);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(912, 622);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 661);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.divisionValueScroll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scaleScroll)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel firstPagePaint;
        private System.Windows.Forms.TrackBar scaleScroll;
        private System.Windows.Forms.Button drawFunc;
        private System.Windows.Forms.Label scaleScrollValueLable;
        private System.Windows.Forms.Label scaleScrollLabel;
        private System.Windows.Forms.TrackBar divisionValueScroll;
        private System.Windows.Forms.Label divisionValueValueLable;
        private System.Windows.Forms.Label divisionValueLabel;
    }
}

