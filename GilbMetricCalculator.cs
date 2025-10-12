namespace Lab2;

using System;
using System.Text.RegularExpressions;

public class GilbMetricCalculator
{
    private int absoluteComplexity = 0;
    private int maxDepth = 0;
    private int totalLinesOfCode = 0;
    
    private static readonly string[] controlKeywords = {
        "if", "else", "else if", "switch", "case", "for", "while", "repeat"
    };

    public void Calculate(string swiftCode)
    {
        absoluteComplexity = 0;
        maxDepth = 0;
        totalLinesOfCode = swiftCode.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;

        string cleanedCode = CleanCode(swiftCode);
        string[] lines = cleanedCode.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            
            int startIndex = 0;
            while (startIndex < line.Length) 
            {
                bool found = false;
                
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
        
        CalculateMaxDepthSimplified(cleanedCode);
    }
    
    private string CleanCode(string code)
    {
        code = Regex.Replace(code, @"//.*", "");
        code = Regex.Replace(code, @"/\*[\s\S]*?\*/", "");
        code = code.Replace("{", " { ").Replace("}", " } ");
        return code;
    }
    
    private void CalculateMaxDepthSimplified(string code)
    {
        int currentDepth = 0;
        
        int index = 0;
        bool keepLooping;
        
        do 
        {
            keepLooping = false;
            if (index < code.Length) 
            {
                char c = code[index];
                
                if (c == '{')
                {
                    currentDepth++;
                    switch (currentDepth)
                    {
                        case int d when d > maxDepth:
                            maxDepth = d;
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
    
    public int GetAbsoluteComplexity() => absoluteComplexity;
    
    public double GetRelativeComplexity() 
    {
        return totalLinesOfCode > 0 ? (double)absoluteComplexity / totalLinesOfCode : 0.0;
    }
    
    public int GetMaxDepth() => maxDepth;
}