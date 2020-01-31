using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VHS
{
    public static class ExtensionMethods
    {
        public static void _CallWithDelay(this MonoBehaviour _mono,Action _method,float _delay) => _mono.StartCoroutine(_CallWithDelayRoutine(_method,_delay));

        private static IEnumerator _CallWithDelayRoutine(Action _method,float _delay)   
        {
            yield return new WaitForSeconds(_delay);
            _method();
        }

    }
}
