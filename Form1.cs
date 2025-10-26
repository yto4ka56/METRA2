using System;
using System.IO;
using System.Windows.Forms;

namespace Lab2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string filePath = openFileDialog1.FileName;
                    string fileContent = File.ReadAllText(filePath);
                    
                    txtSwiftCode.Text = fileContent;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Ошибка при чтении файла: {ex.Message}", 
                        "Ошибка", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Error
                    );
                }
            }
        }
        
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            string swiftCode = txtSwiftCode.Text; 

            if (string.IsNullOrWhiteSpace(swiftCode))
            {
                MessageBox.Show(
                    "Пожалуйста, вставьте код Swift или откройте файл для анализа.", 
                    "Ошибка ввода", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning
                );
                return;
            }
            
            GilbMetricCalculator calculator = new GilbMetricCalculator();
            
            try
            {
                calculator.Calculate(swiftCode);
                
                lblAbsoluteComplexity.Text = 
                    "Абсолютная сложность: " + calculator.GetAbsoluteComplexity();
                
                lblRelativeComplexity.Text = 
                    $"Относительная сложность: {calculator.GetRelativeComplexity():F2}";
                
                lblMaxDepth.Text = 
                    "Максимальный уровень вложенности: " + calculator.GetMaxDepth();

                lblOperatorCount.Text =
                    "Количество операторов: " + calculator.GetOperatorCount();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Произошла ошибка при расчете метрики: {ex.Message}", 
                    "Ошибка расчета", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error
                );
            }
        }
    }
}