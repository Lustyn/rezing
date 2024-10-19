using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public interface TreeNode
{
    private static Regex INT_REGEX = new Regex(@"^\d+$");
    private static Regex FLOAT_REGEX = new Regex(@"^\d+\.\d+$");

    public static Object Read(string content)
    {
        var lines = content.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        var stack = new Stack<Object>();
        string nextObjectName = null;
        foreach (var untrimmedLine in lines)
        {
            var line = untrimmedLine.Trim();

            if (line == "{")
            {
                if (nextObjectName == null)
                {
                    throw new Exception("Expected object name before {");
                }

                stack.Push(new Object { Name = nextObjectName, Children = new List<TreeNode>() });
                nextObjectName = null;
            }
            else if (line == "}")
            {
                var item = stack.Pop();
                if (stack.Count == 0 && item is Object obj)
                {
                    return obj;
                }

                var parent = stack.Peek();

                parent.Children.Add(item);
            }
            else
            {
                if (line == "")
                {
                    continue;
                }
                var parts = line.Split('=', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 1)
                {
                    nextObjectName = parts[0].Trim();
                }
                else
                {
                    var parent = stack.Peek();
                    var name = parts[0].Trim();
                    var value = parts[1].Trim();
                    if (value == "true" || value == "false")
                    {
                        parent.Children.Add(new Bool { Name = name, Value = value == "true" });
                    }
                    else if (INT_REGEX.IsMatch(value))
                    {
                        parent.Children.Add(new Int { Name = name, Value = int.Parse(value) });
                    }
                    else if (FLOAT_REGEX.IsMatch(value))
                    {
                        parent.Children.Add(new Float { Name = name, Value = float.Parse(value) });
                    }
                    else if (value == "null")
                    {
                        parent.Children.Add(new Null { Name = name, Value = null });
                    }
                    else if (value.StartsWith("\"") && value.EndsWith("\""))
                    {
                        parent.Children.Add(new String { Name = name, Value = value[1..^1] });
                    }
                    else if (value.Contains(' '))
                    {
                        var valueParts = value.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        if (valueParts.Length == 0)
                        {
                            throw new Exception("Expected array, got nothing");
                        }

                        if (INT_REGEX.IsMatch(valueParts[0]))
                        {
                            parent.Children.Add(new IntArray { Name = name, Values = new List<int>(Array.ConvertAll(valueParts, int.Parse)) });
                        }
                        else if (FLOAT_REGEX.IsMatch(valueParts[0]))
                        {
                            parent.Children.Add(new FloatArray { Name = name, Values = new List<float>(Array.ConvertAll(valueParts, float.Parse)) });
                        }
                        else
                        {
                            throw new Exception("Unexpected value: " + value);
                        }
                    }
                    else
                    {
                        throw new Exception("Unexpected value: " + value);
                    }
                }
            }
        }

        throw new Exception("Expected object, got end of file");
    }

    public string Name { get; set; }

    class Object : TreeNode
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public List<TreeNode> Children { get; set; }
    }

    interface Value<T> : TreeNode
    {
        public T Value { get; set; }
    }

    class String : Value<string>
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    class Int : Value<int>
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }

    class Float : Value<float>
    {
        public string Name { get; set; }
        public float Value { get; set; }
    }

    class Bool : Value<bool>
    {
        public string Name { get; set; }
        public bool Value { get; set; }
    }

    class Null : Value<object>
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }

    interface Array<T> : TreeNode
    {
        public List<T> Values { get; set; }
    }

    class FloatArray : Array<float>
    {
        public string Name { get; set; }
        public List<float> Values { get; set; }
    }

    class IntArray : Array<int>
    {
        public string Name { get; set; }
        public List<int> Values { get; set; }
    }
}
