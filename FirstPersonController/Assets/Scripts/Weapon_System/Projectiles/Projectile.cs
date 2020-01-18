using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace VHS
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] protected ProjectileData projectileData;

        protected float m_shotTimeStamp;


        protected virtual void Awake() => CastData();
        protected abstract void CastData();

        protected virtual void Update() => OnUpdate(Time.deltaTime);
        public virtual void OnUpdate(float _deltaTime)
        {
            if(Time.time > m_shotTimeStamp + projectileData.GeneralSettings.MaxLiveDuration)
                Destroy(gameObject);
        }

        public virtual void OnFire() => m_shotTimeStamp = Time.time;
        public virtual void OnHit() { }

    }
}
