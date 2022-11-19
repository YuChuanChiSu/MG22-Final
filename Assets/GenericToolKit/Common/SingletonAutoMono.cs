using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GenericToolKit.Common
{
    #nullable enable
    public class SingletonAutoMono<TMonoBehavior> : MonoBehaviour where TMonoBehavior : MonoBehaviour
    {
        private static TMonoBehavior? _instance = null;
        private static bool _isDispose = false;

        public static TMonoBehavior? Instance
        {
            get
            {
                if (_isDispose)
                    return null;

                if (_instance == null)
                {
                    GameObject token = new GameObject(typeof(TMonoBehavior).Name);
                    DontDestroyOnLoad(token);
                    _instance = token.AddComponent<TMonoBehavior>();
                }

                return _instance;
            }
        }

        protected virtual void OnDestroy()
        {
            _isDispose = true;
        }
    }
}
