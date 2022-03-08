using System;
using System.Collections.Generic;
using System.Linq;

namespace AcsTypes.Types
{
    public class Name
    {
        public readonly string SortNamePart;
        public readonly string OtherNamePart;
        public readonly string FullName;

        public Name(string name)
        {
            if (name.Contains("("))
            {
                name = name.SubstringBefore("(").Trim();
            }


            FullName = name;

            if (!FullName.Contains(" "))
            {
                SortNamePart = FullName;
                OtherNamePart = "";
            }
            else
            {
                var parts = FullName.Split(" ");

                if (parts[0].ToUpper() == parts[0])
                {
                    var others = parts.Skip(1);
                    OtherNamePart = parts[0].Trim();
                    SortNamePart = String.Join(" ", others);
                }
                else if (parts[0] == "Lord")
                {
                    var others = parts.Skip(1);
                    var sort = parts.Last();
                    OtherNamePart = String.Join(" ", others);
                    SortNamePart = sort;
                }
                else if (parts[0] == "Sir" || parts[0] == "Earl" || parts[0] == "Duke" || parts[0] == "Nawab")
                {
                    if (parts[1] == "of")
                    {
                        var others = parts.DropLast(1);
                        var sort = parts.Skip(2);
                        OtherNamePart = String.Join(" ", others);
                        SortNamePart = sort.First();
                    }
                    else
                    {
                        SortNamePart = FullName;
                        OtherNamePart = "";
                    }
                }
            }
        }
    }

    static class Helper
    {
        public static string SubstringBefore(this string text, string stopAt)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                int charLocation = text.IndexOf(stopAt, StringComparison.Ordinal);

                if (charLocation > 0)
                {
                    return text.Substring(0, charLocation);
                }
            }

            return String.Empty;
        }
        
        public static IReadOnlyList<T> DropLast<T>(this IReadOnlyList<T> list, int size)
        {
            return list.Take(list.Count - size).ToList();
        }
    }
}