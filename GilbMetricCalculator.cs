namespace Lab2;

using System;
using System.Text.RegularExpressions;

public class GilbMetricCalculator
{
    private int absoluteComplexity = 0;
    private int maxDepth = 0;
    private double relativeComplexity = 0;
    public int operatorCount = 0;
    public Dictionary<string, int> OperatorCounts { get; private set; }
    
    private static readonly string[] controlKeywords = {
        "if", "else if", "case", "for", "while", 
    };
    
    private readonly HashSet<string> _operators = new HashSet<string>
    {
        "=", "+", "-", "*", "/", "%",
        
        "+=", "-=", "*=", "/=", "%=", "++", "--", 
        
        "==", "!=", ">", "<", ">=", "<=", "===", "!==", "&&", "||", "!",  
        "if", "else", "for", "while", "switch", "return", "continue", "break", "print"
    };
    
    private static readonly List<string> depthKeywords = new List<string>
    {
        "if",      
        "for", 
        "while",     
        "repeat",   
        "else"
        
    };
    
    
    public GilbMetricCalculator()
    {
        OperatorCounts = new Dictionary<string, int>();
    }
    
    public void ParseFile(string code)
    {
        OperatorCounts.Clear();
        
        var tokens = Tokenize(code);

        AnalyzeTokens(tokens);
    }
    
    
     private List<string> Tokenize(string code)
    {
        var tokens = new List<string>();
            var regex = new Regex(
                @"""[^""]*""|" + // 1. Строковые литералы
                @"(?<Op>\+=|-=|\*=|/=|%=|==|!=|<=|>=|===|!==|&&|\|\||\?\?|->|\\\(|\.\.\.|\.\.|\+\+|--)|" + // 2. Многосимвольные операторы
                @"(?<Sym>[+\-*/%=<>?!&|^~:;,(){}\[\]])|" + // 3. Односимвольные операторы
                @"(?<ID>[a-zA-Z_][a-zA-Z0-9_]*)|" + // 4. Идентификаторы/Ключевые слова
                @"(?<Num>\d+(\.\d+)?)", // 5. Числа
                RegexOptions.Compiled
            );
        foreach (Match match in regex.Matches(code))
        {
            if (!string.IsNullOrWhiteSpace(match.Value))
            {
                tokens.Add(match.Value);
            }
        }
        
        return tokens;
    }
    
    private void AnalyzeTokens(List<string> tokens)
    {
        for (int i = 0; i < tokens.Count; i++)
        {
            var currentToken = tokens[i];
            if (_operators.Contains(currentToken))
            {
                AddOperator(currentToken);
            }
        }
    }

    private void AddOperator(string op)
    {
        if (OperatorCounts.ContainsKey(op))
        {
            OperatorCounts[op]++;
        }
        else
        {
            OperatorCounts.Add(op, 1);
        }
    }
    
    private Dictionary<string, int> GetMergedOperators()
    {
        var mergedOperators = new Dictionary<string, int>();

        foreach (var op in OperatorCounts)
        {
            string key = op.Key;
            int count = op.Value;

            if (key == "else")
            {
                continue;
            }
            if (key == "if()")
            {
                key = "if...else";
            }
            else if (key == "switch")
            {
                key = "switch...case...default";
            }
            if (!mergedOperators.ContainsKey(key))
            {
                mergedOperators.Add(key, 0);
            }
            mergedOperators[key] += count;
        }
            
        return mergedOperators;
    }
    
    public void Calculate(string swiftCode)
    {
        absoluteComplexity = 0;
        maxDepth = 0;
        relativeComplexity = 0;
        string cleanedCode = CleanCode(swiftCode);
        ParseFile(cleanedCode);
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

        CalculateRelativeComplexity();
        CalculateMaxDepthSimplified(cleanedCode);
    }

    private void CalculateRelativeComplexity()
    {
        var mergedOperators = GetMergedOperators();
        operatorCount = mergedOperators.Values.Sum();
        relativeComplexity = absoluteComplexity / (double)operatorCount;
        
    }
    
    private string CleanCode(string code)
    {
        code = Regex.Replace(code, @"//.*", "");
        code = Regex.Replace(code, @"/\*[\s\S]*?\*/", "");
        code = code.Replace("{", " { ").Replace("}", " } ");
        return code;
    }
    
    private void CalculateMaxDepthSimplified(string cleanedCode)
    {
        int index = 0;
        int currentDepth = 0;
        maxDepth = 0;
        bool insideSwitch = false; 

        bool keepLooping;
        do
        {
            keepLooping = false;
            if (index < cleanedCode.Length)
            {
                char c = cleanedCode[index];
                
                if (char.IsLetter(c))
                {
                    bool keywordFound = false;
                    string currentSub = cleanedCode.Substring(index);
                    
                    foreach (string keyword in depthKeywords)
                    {
                        if (currentSub.StartsWith(keyword + " "))
                        {
                            currentDepth++;
                            index += keyword.Length;
                            keywordFound = true;
                            break;
                        }
                        if (currentSub.StartsWith("switch" + " "))
                        {
                            insideSwitch = true;
                            break;
                        }
                    }
                    
                    if ((currentSub.StartsWith("else ") || currentSub.StartsWith("else if ")))
                    {
                        index += currentSub.StartsWith("else ") ? 4 : 8; 
                        //keywordFound = true;
                    }

                    if (!keywordFound && insideSwitch && currentSub.StartsWith("case "))
                    {
                        currentDepth++;
                        index += 4; 
                        keywordFound = true;
                    }
                     if (!keywordFound && insideSwitch && currentSub.StartsWith("default:"))
                    {
                        index += 8; 
                        keywordFound = true;
                    }
                    
                    if (keywordFound)
                    {
                        if (currentDepth > maxDepth)
                        {
                            maxDepth = currentDepth - 1;
                        }
                        while (index < cleanedCode.Length && char.IsWhiteSpace(cleanedCode[index]))
                        {
                            index++;
                        }
                    }

                    if (!keywordFound)
                    {
                        index ++;
                    }
                }
                else if (c == '}')
                {
                    if (currentDepth > 0)
                    {
                        currentDepth--;
                    }
                    if (currentDepth == 0)
                    {
                        insideSwitch = false;
                    }
                    
                    index++;
                }
                else
                {
                    index++;
                }
                
                keepLooping = true;
            }
        } while (keepLooping);
    }
        
    
    public int GetAbsoluteComplexity() => absoluteComplexity;
    
    public double GetRelativeComplexity() => relativeComplexity;
    
    public int GetMaxDepth() => maxDepth;
    
    public int GetOperatorCount() => operatorCount;
}