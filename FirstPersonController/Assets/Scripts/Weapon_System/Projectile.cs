using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace VHS
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] protected ProjectileData projectileData;

        protected virtual void Awake()
        {
            projectileData.InitProjectileSettings();
        }

        protected virtual void Update()
        {
            OnUpdate(Time.deltaTime);
        }

        public virtual void OnHit()
        {

        }

        public virtual void OnFire()
        {

        }

        public virtual void OnUpdate(float _deltaTime)
        {

        }
    }
}
