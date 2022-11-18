using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericToolKit.Mvvm.Json
{
    public interface IJsonStorage
    {
        void Write<T>(string fileName, T obj);
        ValueTask WriteAsync<T>(string fileName, T obj);
        void Append<T>(string fileName, T obj);
        ValueTask AppendAsync<T>(string fileName, T obj);
        T Read<T>(string fileName);
        ValueTask<T> ReadAsync<T>(string fileName);
        IList<T> ReadList<T>(string fileName);
        ValueTask<IList<T>> ReadListAsync<T>(string fileName);

    }
}
