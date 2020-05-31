using AstDomain;
using AstShared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AstRoslyn
{
    public class SyntaxCsvVisitor : ISyntaxCsvVisitor
    {
        public readonly ICsvGenerator _csvGenerator;
        public Dictionary<string, string> _lines;

        public SyntaxCsvVisitor(ICsvGenerator csvGenerator)
        {
            _csvGenerator = csvGenerator;
        }

        public void Visit(Config config, Type t)
        {
            if(_lines == null &&  File.Exists(config.SyntaxCsvFile))
            {
                _lines = File.ReadAllLines(config.SyntaxCsvFile).Skip(1).ToDictionary(l => l.Split('\t').First(), l => l);
            }
            else
            {
                _lines = new Dictionary<string, string>();
            }

            _csvGenerator.CreateRecordForType(t, config.SyntaxCsvFile, config.SyntaxCsvHeader, _lines);
        }
    }
}
