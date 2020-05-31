using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AstShared;

namespace AstRoslyn
{
    public class CsvGenerator : ICsvGenerator
    {
        public void CreateRecordForType(Type type, string fileName, string header)
        {
            int columnsCount = header.Split('\t').Length;

            StringBuilder b = new StringBuilder();

            b.Append(type.FullName);
            for(int i = 1; i<columnsCount; i++)
            {
                b.Append('\t');
            }

            File.AppendAllLines(fileName, new List<string> { b.ToString() });
        }
    }
}