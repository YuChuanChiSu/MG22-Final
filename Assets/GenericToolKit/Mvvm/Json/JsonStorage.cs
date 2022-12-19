using GenericToolKit.Mvvm.Async;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GenericToolKit.Mvvm.Json
{
    public class JsonStorage : IJsonStorage
    {
        private ISyncDispatcher _dispatcher;

        public JsonStorage(ISyncDispatcher dispatcher)
            => _dispatcher = dispatcher;

        public void Write<T>(string fileName, T obj)
        { 
            using (FileStream fs = new FileStream(Path.Combine(GlobalConfiguration.JsonPath, $"{fileName}.txt"), FileMode.Create, FileAccess.Write))
               using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                sw.WriteLine(JsonUtility.ToJson(obj));
        }

        public async ValueTask WriteAsync<T>(string fileName, T obj)
        {
            using (FileStream fs = new FileStream(Path.Combine(GlobalConfiguration.JsonPath, $"{fileName}.txt"), FileMode.Create, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                    await sw.WriteLineAsync(JsonUtility.ToJson(obj));
        }

        public void Append<T>(string fileName, T obj)
        {
            using (FileStream fs = new FileStream(Path.Combine(GlobalConfiguration.JsonPath, $"{fileName}.txt"), FileMode.Append, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                    sw.WriteLine(JsonUtility.ToJson(obj));
        }

        public async ValueTask AppendAsync<T>(string fileName, T obj)
        {
            using (FileStream fs = new FileStream(Path.Combine(GlobalConfiguration.JsonPath, $"{fileName}.txt"), FileMode.Append, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                    await sw.WriteLineAsync(JsonUtility.ToJson(obj));
        }

        public T Read<T>(string fileName)
        {
            using (FileStream fs = new FileStream(Path.Combine(GlobalConfiguration.JsonPath, $"{fileName}.txt"), FileMode.Open, FileAccess.Read))
                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                    return JsonUtility.FromJson<T>(sr.ReadToEnd());
        }

        public async ValueTask<T> ReadAsync<T>(string fileName)
        {
            using (FileStream fs = new FileStream(Path.Combine(GlobalConfiguration.JsonPath, $"{fileName}.txt"), FileMode.Open, FileAccess.Read))
            using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                return JsonUtility.FromJson<T>(await sr.ReadToEndAsync());
        }

        public IList<T> ReadList<T>(string fileName)
        {
            IList<T> list = new List<T>();
            string[] jsons;

            using (FileStream fs = new FileStream(Path.Combine(GlobalConfiguration.JsonPath, $"{fileName}.txt"), FileMode.Open, FileAccess.Read))
                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                    jsons = sr.ReadToEnd().Split("\r\n");

            for (int i = 0; i < jsons.Length - 1; i++)
                list.Add(JsonUtility.FromJson<T>(jsons[i]));
            return list;
        }

        public async ValueTask<IList<T>> ReadListAsync<T>(string fileName)
        {
            IList<T> list = new List<T>();
            string[] jsons;

            using (FileStream fs = new FileStream(Path.Combine(GlobalConfiguration.JsonPath, $"{fileName}.txt"), FileMode.Open, FileAccess.Read))
            using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                jsons = (await sr.ReadToEndAsync()).Split('\r', '\n');

            for (int i = 0; i < jsons.Length; i++)
                list.Add(JsonUtility.FromJson<T>(jsons[i]));
            return list;
        }
    }
}
