using GenericToolKit.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericToolKit.Mvvm.Event
{
    public class GlobalEventCenter : SingletonStatic<GlobalEventCenter>
    {
        private ConcurrentDictionary<string, Delegate> _events
            = new ConcurrentDictionary<string, Delegate>();

        public bool TryAddEvent<TDelegate>(string name, TDelegate value ) where TDelegate : Delegate
            => _events.TryAdd(name, value);

        public bool TryRemoveEvent(string name)
            => _events.TryRemove(name, out Delegate _);

        public bool TryGetEvent<TDelegate>(string name, out TDelegate value) where TDelegate : Delegate
        {
            bool ret = _events.TryGetValue(name, out Delegate temp);
            value = (TDelegate)temp;
            return ret;
        }

        public TDelegate AddOrUpdate<TDelegate>(string name, TDelegate value, Func<string, Delegate, Delegate> updateFactory) where TDelegate : Delegate
            => _events.AddOrUpdate(name, value, updateFactory) as TDelegate;
    }
}
