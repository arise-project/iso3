using System;
using System.Collections.Generic;
using AstDomain;

namespace AstShared
{
    public interface ICsvGenerator
    {
        void CreateRecordForType(Type type, string fileName, string header, Dictionary<string, string> lines);
    }
}
