using System;
using System.Collections;
using UnityEngine;

namespace Utils
{
    public class CoroutineHelper : MonoBehaviour
    {
        private static CoroutineHelper _singleton;

        private void Awake()
        {
            _singleton = this;
        }

        public static Coroutine Run(IEnumerator enumerator)
        {
            return _singleton.StartCoroutine(enumerator);
        }
    }
}