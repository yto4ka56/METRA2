namespace Lab2;

using System;
using System.Text.RegularExpressions;

public class GilbMetricCalculator
{
    private int absoluteComplexity = 0;
    private int maxDepth = 0;
    private int totalLinesOfCode = 0;
    
    // Swift ключевые слова для метрики Джилба
    private static readonly string[] controlKeywords = {
        "if", "else", "else if", "switch", "case", "for", "while", "repeat"
    };

    public void Calculate(string swiftCode)
    {
        // 1. Инициализация и подготовка данных
        absoluteComplexity = 0;
        maxDepth = 0;
        // Расчет LOC для S_rel
        totalLinesOfCode = swiftCode.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;

        string cleanedCode = CleanCode(swiftCode);
        string[] lines = cleanedCode.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        // 2. Расчет S_abs
        // Демонстрация использования оператора FOR
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            
            // Демонстрация использования оператора WHILE
            int startIndex = 0;
            while (startIndex < line.Length) 
            {
                bool found = false;
                
                // Итерация по ключевым словам
                foreach (string keyword in controlKeywords) 
                {
                    if (line.Substring(startIndex).StartsWith(keyword + " "))
                    {
                        absoluteComplexity++;
                        startIndex += keyword.Length;
                        found = true;
                        break;
                    }
                }
                
                if (!found) 
                {
                    startIndex++;
                }
            }
        }
        
        // 3. Расчет MaxDepth
        CalculateMaxDepthSimplified(cleanedCode);
    }
    
    private string CleanCode(string code)
    {
        code = Regex.Replace(code, @"//.*", "");
        code = Regex.Replace(code, @"/\*[\s\S]*?\*/", "");
        code = code.Replace("{", " { ").Replace("}", " } ");
        return code;
    }

    // Демонстрация IF-ELSE и SWITCH
    private void CalculateMaxDepthSimplified(string code)
    {
        int currentDepth = 0;
        
        // Демонстрация оператора DO-WHILE
        int index = 0;
        bool keepLooping;
        
        do 
        {
            keepLooping = false;
            if (index < code.Length) 
            {
                char c = code[index];

                // IF-ELSE для управления глубиной
                if (c == '{')
                {
                    currentDepth++;
                    // Оператор множественного выбора (SWITCH) для обновления MaxDepth
                    switch (currentDepth)
                    {
                        case int d when d > maxDepth:
                            maxDepth = d;
                            break;
                        default:
                            // Ничего не делаем
                            break;
                    }
                }
                else if (c == '}')
                {
                    currentDepth--;
                }
                
                index++;
                keepLooping = true;
            }
        } while (keepLooping);
    }

    // Методы доступа
    public int GetAbsoluteComplexity() => absoluteComplexity;
    
    public double GetRelativeComplexity() 
    {
        return totalLinesOfCode > 0 ? (double)absoluteComplexity / totalLinesOfCode : 0.0;
    }
    
    public int GetMaxDepth() => maxDepth;
}