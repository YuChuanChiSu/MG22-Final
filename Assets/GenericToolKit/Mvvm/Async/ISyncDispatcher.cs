using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericToolKit.Mvvm.Async
{
    #nullable enable
    public interface ISyncDispatcher
    {
        event Action? OnUpdate;
        Coroutine StartCoroutine(IEnumerator coroutine);
        void EnqueueOnMainThread(Action action, float time = 0f);
        void Dispose();
    }
}