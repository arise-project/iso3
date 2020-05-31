using System;
using AstDomain;

namespace AstShared
{
    public interface ICsvGenerator
    {
        void CreateRecordForType(Type type, string fileName, string header);
    }
}
