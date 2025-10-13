namespace Lab2;

using System;
using System.Text.RegularExpressions;

public class GilbMetricCalculator
{
    private int absoluteComplexity = 0;
    private int maxDepth = 0;
    private double relativeComplexity = 0;
    public Dictionary<string, int> OperatorCounts { get; private set; }
    
    private static readonly string[] controlKeywords = {
        "if", "else if", "case", "for", "while", 
    };
    
    private readonly HashSet<string> _operators = new HashSet<string>
    {
        "=", "+", "-", "*", "/", "%", "??", ":", "?", ".", ",", ";", "{", "}", "[", "]", "(", ")",
        "\\()",
        
        "+=", "-=", "*=", "/=", "%=", "++", "--", "_",
        
        "==", "!=", ">", "<", ">=", "<=", "===", "!==", "&&", "||", "!",
        "func", "where", "class", "struct", "enum", "protocol", "if", "else", "for", "while", "repeat", "in", "do", "switch", "return", "var", "let", "import", "->", "continue", "break"
    };
    
    private readonly HashSet<string> _compoundOperators = new HashSet<string>
    {
        "if", "else", "func", "while", "for"
    };
    
    private readonly HashSet<string> _excludedOperands = new HashSet<string>
    {
        "Int", "String", "Double", "Float", "Bool", "Character", "Array", "Dictionary", "Set",
        "func", "class", "struct", "enum", "protocol", "import", "Foundation", "let", "var","default", "case",
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
        
            //var regex = new Regex(@"("".*?"")|(\+=|(?<!=)-=|(?<!=)\*=|(?<!=)/=|(?<!=)%=|(?<!=)==|(?<!=)!=|(?<!=)<=|(?<!=)>=|(?<!=)===|(?<!=)!==|&&|\|\||\?\?|->)|([+\-*/%=<>?!&|^~.:,;(){}\[\]])|([a-zA-Z_][a-zA-Z0-9_]*)|(\d+(\.\d+)?)", RegexOptions.Compiled);
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
            if (Regex.IsMatch(currentToken, @"^\d+(\.\d+)?$")) continue;
            // b) Строки (являются операндами)
            if (currentToken.StartsWith("\"") && currentToken.EndsWith("\"")) continue;
            // c) Исключенные ключевые слова/типы
            if (_excludedOperands.Contains(currentToken)) continue;
            /*if (currentToken.StartsWith("\"") && currentToken.EndsWith("\""))
            {

                var interpolationRegex = new Regex(@"\\\(.*?\)", RegexOptions.Compiled);
                foreach (Match match in interpolationRegex.Matches(currentToken))
                {
                    AddOperator("\\()");
                    var interpolatedContent = match.Value.Substring(2, match.Value.Length - 3).Trim(); 
                    if (!string.IsNullOrEmpty(interpolatedContent))
                    {
                        AnalyzeTokens(Tokenize(interpolatedContent));
                    }
                }
                
                continue;
            }

            if (i + 1 < tokens.Count && tokens[i + 1] == "(")
            {
                if (!_operators.Contains(currentToken) || _compoundOperators.Contains(currentToken))
                {
                    int closingParenIndex = FindClosingBracket(tokens, i + 1, "(", ")");
                    if (closingParenIndex != -1)
                    {
                        AddOperator(currentToken + "()");

                        AnalyzeTokens(tokens.GetRange(i + 2, closingParenIndex - (i + 2)));

                        i = closingParenIndex;
                        continue;
                    }
                }
            }
            
            if (currentToken == "if" || currentToken == "else")
            {
                AddOperator("if...else");
                continue;
            }*/

            if (_operators.Contains(currentToken) && !_excludedOperands.Contains(currentToken))
            {
                AddOperator(currentToken);
            }
        }
    }
    
    private int FindClosingBracket(List<string> tokens, int startIndex, string opening, string closing)
    {
        int balance = 1;
        for (int i = startIndex + 1; i < tokens.Count; i++)
        {
            if (tokens[i] == opening)
            {
                balance++;
            }
            else if (tokens[i] == closing)
            {
                balance--;
                if (balance == 0)
                {
                    return i;
                }
            }
        }
        return -1;
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

            if (key == "(" || key == ")")
            {
                key = "()";
            }
            else if (key == "{" || key == "}")
            {
                key = "{}";
            }
            else if (key == "[" || key == "]")
            {
                key = "[]";
            }
            else if (key == "else")
            {
                continue;
            }
            else if (key == "if()")
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

        if (mergedOperators.ContainsKey("()"))
        {
            mergedOperators["()"] /= 2;
        }
        if (mergedOperators.ContainsKey("{}"))
        {
            mergedOperators["{}"] /= 2;
        }
        if (mergedOperators.ContainsKey("[]"))
        {
            mergedOperators["[]"] /= 2;
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
        var operatorCount = mergedOperators.Values.Sum();
        relativeComplexity = absoluteComplexity / (double)operatorCount;
        /*int totalOperators = 0;
        foreach (var op in OperatorCounts)
        {
            totalOperators += op.Value;
        }

        // 3. Расчет Относительной сложности (S_rel)
        if (totalOperators > 0)
        {
            relativeComplexity = (double)absoluteComplexity / totalOperators;
        }
        else
        {
            relativeComplexity = 0.0;
        }*/
        
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
                
                foreach (string keyword in controlKeywords)
                {
                    if (index + keyword.Length < cleanedCode.Length && 
                        cleanedCode.Substring(index).StartsWith(keyword + " ")) // || cleanedCode.Substring(index).StartsWith("else" + " ")))
                    {
                        currentDepth++;
                        index += keyword.Length;
                        keywordFound = true;
                        break;
                    }
                }
                
                if (!keywordFound && cleanedCode.Substring(index).StartsWith("case "))
                {
                    currentDepth++;
                    index += 4; 
                    keywordFound = true;
                }
                
                if (keywordFound)
                {
                    switch (currentDepth)
                    {
                        case int d when d > maxDepth:
                            maxDepth = d;
                            break;
                        default:
                            break;
                    }
                    while (index < cleanedCode.Length && char.IsWhiteSpace(cleanedCode[index]))
                    {
                        index++;
                    }
                }

                if (!keywordFound)
                {
                    index++; 
                }
            }
            
            else if (c == '}')
            {
                if (currentDepth > 0)
                {
                    currentDepth--;
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
}