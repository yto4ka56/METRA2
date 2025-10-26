namespace Lab2
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtSwiftCode = new System.Windows.Forms.TextBox();
            btnCalculate = new System.Windows.Forms.Button();
            lblAbsoluteComplexity = new System.Windows.Forms.Label();
            lblRelativeComplexity = new System.Windows.Forms.Label();
            lblMaxDepth = new System.Windows.Forms.Label();
            btnOpenFile = new System.Windows.Forms.Button();
            openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            lblOperatorCount = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // txtSwiftCode
            // 
            txtSwiftCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
            txtSwiftCode.Font = new System.Drawing.Font("Segoe UI", 12F);
            txtSwiftCode.Location = new System.Drawing.Point(15, 75);
            txtSwiftCode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txtSwiftCode.Multiline = true;
            txtSwiftCode.Name = "txtSwiftCode";
            txtSwiftCode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            txtSwiftCode.Size = new System.Drawing.Size(989, 482);
            txtSwiftCode.TabIndex = 0;
            // 
            // btnCalculate
            // 
            btnCalculate.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            btnCalculate.Font = new System.Drawing.Font("Segoe UI", 14F);
            btnCalculate.Location = new System.Drawing.Point(739, 615);
            btnCalculate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btnCalculate.Name = "btnCalculate";
            btnCalculate.Size = new System.Drawing.Size(266, 97);
            btnCalculate.TabIndex = 1;
            btnCalculate.Text = "Рассчитать Метрику Джилба";
            btnCalculate.UseVisualStyleBackColor = true;
            btnCalculate.Click += btnCalculate_Click;
            // 
            // lblAbsoluteComplexity
            // 
            lblAbsoluteComplexity.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            lblAbsoluteComplexity.AutoSize = true;
            lblAbsoluteComplexity.Font = new System.Drawing.Font("Segoe UI", 14F);
            lblAbsoluteComplexity.Location = new System.Drawing.Point(15, 577);
            lblAbsoluteComplexity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblAbsoluteComplexity.Name = "lblAbsoluteComplexity";
            lblAbsoluteComplexity.Size = new System.Drawing.Size(330, 38);
            lblAbsoluteComplexity.TabIndex = 2;
            lblAbsoluteComplexity.Text = "Абсолютная сложность: ";
            // 
            // lblRelativeComplexity
            // 
            lblRelativeComplexity.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            lblRelativeComplexity.AutoSize = true;
            lblRelativeComplexity.Font = new System.Drawing.Font("Segoe UI", 14F);
            lblRelativeComplexity.Location = new System.Drawing.Point(15, 615);
            lblRelativeComplexity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblRelativeComplexity.Name = "lblRelativeComplexity";
            lblRelativeComplexity.Size = new System.Drawing.Size(358, 38);
            lblRelativeComplexity.TabIndex = 3;
            lblRelativeComplexity.Text = "Относительная сложность:";
            // 
            // lblMaxDepth
            // 
            lblMaxDepth.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            lblMaxDepth.AutoSize = true;
            lblMaxDepth.Font = new System.Drawing.Font("Segoe UI", 14F);
            lblMaxDepth.Location = new System.Drawing.Point(15, 652);
            lblMaxDepth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblMaxDepth.Name = "lblMaxDepth";
            lblMaxDepth.Size = new System.Drawing.Size(520, 38);
            lblMaxDepth.TabIndex = 4;
            lblMaxDepth.Text = "Максимальный уровень вложенности: ";
            // 
            // btnOpenFile
            // 
            btnOpenFile.Font = new System.Drawing.Font("Segoe UI", 14F);
            btnOpenFile.Location = new System.Drawing.Point(15, 18);
            btnOpenFile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btnOpenFile.Name = "btnOpenFile";
            btnOpenFile.Size = new System.Drawing.Size(244, 45);
            btnOpenFile.TabIndex = 5;
            btnOpenFile.Text = "Открыть файл";
            btnOpenFile.UseVisualStyleBackColor = true;
            btnOpenFile.Click += btnOpenFile_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.Filter = "Swift Code Files (*.swift)|*.swift|All files (*.*)|*.*";
            openFileDialog1.Title = "Выберите файл с кодом Swift для анализа";
            // 
            // lblOperatorCount
            // 
            lblOperatorCount.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            lblOperatorCount.AutoSize = true;
            lblOperatorCount.Font = new System.Drawing.Font("Segoe UI", 14F);
            lblOperatorCount.Location = new System.Drawing.Point(15, 694);
            lblOperatorCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblOperatorCount.Name = "lblOperatorCount";
            lblOperatorCount.Size = new System.Drawing.Size(331, 38);
            lblOperatorCount.TabIndex = 6;
            lblOperatorCount.Text = "Количество операторов:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1017, 768);
            Controls.Add(lblOperatorCount);
            Controls.Add(btnOpenFile);
            Controls.Add(lblMaxDepth);
            Controls.Add(lblRelativeComplexity);
            Controls.Add(lblAbsoluteComplexity);
            Controls.Add(btnCalculate);
            Controls.Add(txtSwiftCode);
            Font = new System.Drawing.Font("Segoe UI", 9F);
            Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Парсер Метрики Джилба (Swift/C# WinForms)";
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label lblOperatorCount;

        #endregion

        private System.Windows.Forms.TextBox txtSwiftCode;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Label lblAbsoluteComplexity;
        private System.Windows.Forms.Label lblRelativeComplexity;
        private System.Windows.Forms.Label lblMaxDepth;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1; // Новый компонент
    }
}