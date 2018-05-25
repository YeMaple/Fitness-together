using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface GenericAccessInterface
    {
        T GetById<T>(int id) where T : class;
        T Add<T>(T obj) where T : class;
        bool Delete<T>(int id) where T : class;
        T Update<T>(T obj, int id) where T : class;
    }
}
