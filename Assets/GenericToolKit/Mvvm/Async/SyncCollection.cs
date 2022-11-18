using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace GenericToolKit.Mvvm.Async
{
    #nullable enable
    internal class SyncCollection : MonoBehaviour, IDisposable
    {
        internal struct DelayedQueueItem
        {
            public Action Action;
            public float Time;
        }

        private IList<DelayedQueueItem> _currentItems
            = new List<DelayedQueueItem>();
    
        private IList<DelayedQueueItem> _waitItems
            = new List<DelayedQueueItem>();

        public int Count 
            => _waitItems.Count;

        public bool IsReadOnly
            => _waitItems.IsReadOnly;

        public event Action? OnUpdate;

        private void Update()
        {
            OnUpdate?.Invoke();

            lock (_waitItems)
            {
                _currentItems.Clear();

                float time = Time.time;
                _currentItems.AddRange(_waitItems.Where(item => item.Time <= time));

                for (int i = 0; i < _currentItems.Count; i++)
                    _waitItems.Remove(_currentItems[i]);
            }

            for (int i = 0; i < _currentItems.Count; i++)
                _currentItems[i].Action.Invoke();
        }

        private void OnDestroy()
            => Dispose();

        public void Enqueue(DelayedQueueItem item)
        {
            lock (_waitItems)
                _waitItems.Add(item);
        }

        public void Dispose()
        {
            OnUpdate = null;
        }
    }
}
