using UnityEngine;

namespace Core
{
    public class PooledObject : MonoBehaviour
    {
        private ObjectPool _pool;
        public ObjectPool GetPool()
        {
            return _pool;
        }

        public void SetPool(ObjectPool pool)
        {
            _pool = pool;
        }
    }
}