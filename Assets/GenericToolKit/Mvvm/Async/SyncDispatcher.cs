using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GenericToolKit.Mvvm.Async
{
    #nullable enable
    public class SyncDispatcher : ISyncDispatcher, IDisposable
    {
        private GameObject _token;
        private SyncCollection _behavior;

        public event Action? OnUpdate
        {
            add => _behavior.OnUpdate += value;
            remove => _behavior.OnUpdate -= value;
        }

        public SyncDispatcher()
        {
            _token = new GameObject(nameof(SyncDispatcher));
            UnityEngine.Object.DontDestroyOnLoad(_token);
            _behavior = _token.AddComponent<SyncCollection>();            
        }

        public Coroutine StartCoroutine(IEnumerator coroutine)
            => _behavior.StartCoroutine(coroutine);

        public void EnqueueOnMainThread(Action action, float time = 0f)
            => _behavior.Enqueue(new SyncCollection.DelayedQueueItem { Action = action, Time = time });

        public void Dispose()
            => _behavior.Dispose();
    }
}
