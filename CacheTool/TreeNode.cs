using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public interface TreeNode
{
    private static Regex INT_REGEX = new Regex(@"^\d+$");
    private static Regex FLOAT_REGEX = new Regex(@"^\d+(?:\.\d+)?f?$");

    public static Object Read(string content)
    {
        var lines = content.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        var stack = new Stack<Object>();
        string nextObjectName = null;
        bool inComment = false;
        Object lastStackItem = null;
        for (var i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            if (line.StartsWith("//"))
            {
                continue;
            }
            if (line.StartsWith("/*"))
            {
                inComment = true;
            }
            if (line.EndsWith("*/"))
            {
                inComment = false;
            }
            if (inComment)
            {
                continue;
            }

            if (line.Contains("//"))
            {
                line = line.Substring(0, line.IndexOf("//"));
            }

            var nextLine = i + 1 < lines.Length ? lines[i + 1] : null;

            if (line == "{")
            {
                // Recover from a corrupt file
                if (nextObjectName == null && lastStackItem != null)
                {
                    stack.Push(lastStackItem);
                }
                else
                {
                    stack.Push(new Object { Name = nextObjectName, Children = new List<TreeNode>() });
                    nextObjectName = null;
                }
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
                lastStackItem = item;
            }
            else
            {
                if (line == "")
                {
                    continue;
                }
                var parts = line.Split('=', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                if (parts.Length == 1)
                {
                    nextObjectName = parts[0];
                }
                else
                {
                    var hasParent = stack.TryPeek(out var parent);
                    var name = parts[0];
                    var value = parts[1];
                    var valueParts = value.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                    if (value == "true" || value == "false")
                    {
                        if (nextLine == "{")
                        {
                            stack.Push(new BoolKeyedObject { Name = name, Key = bool.Parse(value), Children = new List<TreeNode>() });
                            i++;
                        }
                        else
                        {
                            parent.Children.Add(new Bool { Name = name, Value = value == "true" });
                        }
                    }
                    else if (INT_REGEX.IsMatch(value))
                    {
                        if (nextLine == "{")
                        {
                            stack.Push(new IntKeyedObject { Name = name, Key = int.Parse(value), Children = new List<TreeNode>() });
                            i++;
                        }
                        else
                        {
                            parent.Children.Add(new Int { Name = name, Value = int.Parse(value) });
                        }
                    }
                    else if (FLOAT_REGEX.IsMatch(value))
                    {
                        parent.Children.Add(new Float { Name = name, Value = ParseFloat(value) });
                    }
                    else if (value == "null")
                    {
                        parent.Children.Add(new Null { Name = name, Value = null });
                    }
                    else if (valueParts.All(INT_REGEX.IsMatch))
                    {
                        parent.Children.Add(new IntArray { Name = name, Values = new List<int>(Array.ConvertAll(valueParts, int.Parse)) });
                    }
                    else if (valueParts.All(FLOAT_REGEX.IsMatch))
                    {
                        parent.Children.Add(new FloatArray { Name = name, Values = new List<float>(Array.ConvertAll(valueParts, ParseFloat)) });
                    }
                    else
                    {
                        if (value.StartsWith("\"") && value.EndsWith("\""))
                        {
                            value = value[1..^1];
                        }
                        if (nextLine == "{")
                        {
                            stack.Push(new StringKeyedObject { Name = name, Key = value, Children = new List<TreeNode>() });
                            i++;
                        }
                        else
                        {
                            parent.Children.Add(new String { Name = name, Value = value });
                        }
                    }
                }
            }
        }

        while (stack.Count > 1)
        {
            var item = stack.Pop();
            var parent = stack.Peek();
            parent.Children.Add(item);
        }

        return stack.Pop();
    }

    private static float ParseFloat(string value)
    {
        if (value.EndsWith("f"))
        {
            return float.Parse(value[..^1]);
        }
        return float.Parse(value);
    }

    public string Name { get; set; }

    class Object : TreeNode
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public List<TreeNode> Children { get; set; }
    }

    interface Key<T> : TreeNode
    {
        public T Key { get; set; }
    }

    class StringKeyedObject : Object, Key<string>
    {
        public string Key { get; set; }
    }

    class IntKeyedObject : Object, Key<int>
    {
        public int Key { get; set; }
    }

    class BoolKeyedObject : Object, Key<bool>
    {
        public bool Key { get; set; }
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
