using UnityEngine;

namespace VHS
{
    public interface IPoolable
    {
        bool IsPoolable { get; }
        Transform Transform { get; }
        IPoolable OnReuse();
    }
}
