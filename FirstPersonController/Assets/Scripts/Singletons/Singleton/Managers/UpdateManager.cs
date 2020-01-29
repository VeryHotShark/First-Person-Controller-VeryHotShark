using System;
using UnityEngine;

namespace VHS
{
    public class UpdateManager : Singleton<UpdateManager>
    {
        public static Action<float> _OnUpdate = delegate { };
        public static Action<float> _OnFixedUpdate = delegate { };
        public static Action<float> _OnLateUpdate = delegate { };

        private void Update() => _OnUpdate(Time.deltaTime);
        private void FixedUpdate() => _OnFixedUpdate(Time.fixedDeltaTime);
        private void LateUpdate() => _OnLateUpdate(Time.smoothDeltaTime);
    }
}
