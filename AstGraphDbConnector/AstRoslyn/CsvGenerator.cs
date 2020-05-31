using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AstShared;

namespace AstRoslyn
{
    public class CsvGenerator : ICsvGenerator
    {
        public void CreateRecordForType(Type type, string fileName, string header, Dictionary<string, string> lines)
        {
            int columnsCount = header.Split('\t').Length;

            StringBuilder b = new StringBuilder();

            if(lines.ContainsKey(type.FullName))
            {
                int prevColumnsCount = lines[type.FullName].Split('\t').Length;
                b.Append(lines[type.FullName]);
                for(int i = prevColumnsCount; i<columnsCount; i++)
                {
                    b.Append('\t');
                }
            }
            else
            {
                b.Append(type.FullName);
                for(int i = 1; i<columnsCount; i++)
                {
                    b.Append('\t');
                }
            }

            File.AppendAllLines(fileName+".tmp", new List<string> { b.ToString() });
        }
    }
}