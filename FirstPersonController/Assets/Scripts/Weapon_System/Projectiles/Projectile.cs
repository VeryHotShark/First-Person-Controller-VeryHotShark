using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace VHS
{
    public abstract class Projectile : MonoBehaviour, IPoolable
    {
        [SerializeField] protected ProjectileData projectileData;
        [SerializeField] protected LayerMask collisionLayers;

        protected float m_shotTimeStamp;
        protected Vector3 m_lastPosition;
        protected Vector3 m_currentDirection;
        protected Transform m_owner;

        public bool IsPoolable => !gameObject.activeSelf;
        public Transform Transform => transform;

        protected virtual void Awake() => CastData();
        protected abstract void CastData();

        protected virtual void OnEnable() => UpdateManager._OnUpdate += OnUpdate;
        protected virtual void OnDisable() => UpdateManager._OnUpdate -= OnUpdate;
        protected virtual void OnDestroy() => UpdateManager._OnUpdate -= OnUpdate;

        public virtual void OnUpdate(float _deltaTime)
        {
            UpdatePosition(_deltaTime);
            CheckForCollision();
            CheckForLifetime();
            UpdateLastPosition();
        }

        protected virtual void UpdatePosition(float _deltaTime) { }
        private void UpdateLastPosition() => m_lastPosition = transform.position;

        public virtual void CheckForCollision()
        {
            bool _hitSomething = Physics.Linecast(m_lastPosition, transform.position, out RaycastHit hitInfo, collisionLayers);

            if (_hitSomething)
            {
                if (hitInfo.collider.transform.IsChildOf(transform) || hitInfo.collider.transform.IsChildOf(m_owner))
                    return;

                OnHit();
            }
        }

        private void CheckForLifetime()
        {
            if (Time.time > m_shotTimeStamp + projectileData.GeneralSettings.MaxLiveDuration)
                gameObject.SetActive(false);
        }

        public virtual void OnFire(Transform _owner) => Init(_owner);

        private void Init(Transform _owner)
        {
            m_owner = _owner;
            m_shotTimeStamp = Time.time;
            m_lastPosition = transform.position;
            m_currentDirection = transform.forward;
        }

        public virtual void OnHit() => gameObject.SetActive(false);

        public IPoolable OnReuse() => this;
    }
}
