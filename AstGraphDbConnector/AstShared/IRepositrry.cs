using System;
using System.Collections.Generic;
using AstDomain;

namespace AstShared
{
    public interface IRepository<T>
    {
        void Init(Config config);

        void Create(T element);

        void Update(string id, T element);

        void Delete (string id);

        List<T> Select();
    }
}