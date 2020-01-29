using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VHS
{
    public class PoolManager : Singleton<PoolManager>
    {
        private Dictionary<IPoolable,List<IPoolable>> m_pool = new Dictionary<IPoolable, List<IPoolable>>();

        public static IPoolable SpawnPoolable(IPoolable _poolable, Vector3 _position, Quaternion _rotation, Vector3 _scale, Transform _parent = null)
        {
            IPoolable _poolableInstance = Instance.GetPoolable(_poolable);

            if(_poolableInstance == null)
                return null;

            _poolableInstance.Transform.SetPositionAndRotation(_position,_rotation);
            _poolableInstance.Transform.SetParent(_parent);
            _poolableInstance.Transform.localScale = _scale;

            _poolableInstance.Transform.gameObject.SetActive(true);
            return _poolableInstance.OnReuse();
        }

        private IPoolable GetPoolable(IPoolable _poolable)
        {
            List<IPoolable> _poolableList = null;
            IPoolable _poolableInstance = null;

            if(!m_pool.ContainsKey(_poolable))
            {
                CreateNewPool();

                void CreateNewPool()
                {
                    _poolableList = new List<IPoolable>();
                    m_pool[_poolable] = _poolableList;

                    CreateNewPoolable();
                }
            }
            else
            {
                _poolableList = m_pool[_poolable];

                for (int i = _poolableList.Count - 1; i >= 0 ; i--)
                {
                    if(_poolableList[i].IsPoolable)
                        return _poolableList[i];
                }

                CreateNewPoolable();
            }

            void CreateNewPoolable()
            {
                _poolableInstance = GameObject.Instantiate(_poolable.Transform.gameObject).GetComponentInChildren<IPoolable>();
                _poolableList.Add(_poolableInstance);
            }

            return _poolableInstance;
        }
    }
}
