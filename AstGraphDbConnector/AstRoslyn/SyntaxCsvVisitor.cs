using AstDomain;
using AstShared;
using System;

namespace AstRoslyn
{
    public class SyntaxCsvVisitor : ISyntaxCsvVisitor
    {
        public readonly ICsvGenerator _csvGenerator;

        public SyntaxCsvVisitor(ICsvGenerator csvGenerator)
        {
            _csvGenerator = csvGenerator;
        }

        public void Visit(Config config, Type t)
        {
            _csvGenerator.CreateRecordForType(t, config.SyntaxCsvFile, config.SyntaxCsvHeader);
        }
    }
}
