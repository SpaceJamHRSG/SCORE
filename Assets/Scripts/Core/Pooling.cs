using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Core
{
    
    public class Pooling : MonoBehaviour
    {
        public static Pooling Instance { get; private set; }
        private Dictionary<PooledObject, ObjectPool> _prefab2Pool;
        private List<ObjectPool> _objectPools;
        private void Awake()
        {
            if (Instance != null) Destroy(this);
            else
            {
                Instance = this;
                DontDestroyOnLoad(this);
                _prefab2Pool = new Dictionary<PooledObject, ObjectPool>();
                _objectPools = new List<ObjectPool>();
            }
        }

        public void Spawn(PooledObject prefab)
        {
            if (!_prefab2Pool.ContainsKey(prefab))
            {
                GameObject poolObject = Instantiate(new GameObject(), transform);
                ObjectPool pool = poolObject.AddComponent<ObjectPool>();
                pool.Init(prefab, 50);
            }
        }

        public void Despawn(PooledObject obj)
        {
            obj.GetPool().Despawn(obj);
        }
    }
    
    public class ObjectPool : MonoBehaviour
    {
        private PooledObject _prefab;
        private Queue<PooledObject> _inactiveQueue;

        public void Init(PooledObject prefab, int initialSize)
        { ;
            _inactiveQueue = new Queue<PooledObject>();
            _prefab = prefab;
            AddToPool(initialSize);
        }

        private void AddToPool(int n)
        {
            for (int i = 0; i < n; i++)
            {
                PooledObject initObject = Instantiate(_prefab);
                initObject.gameObject.SetActive(false);
                initObject.transform.Translate(new Vector3(10000, 10000, 10000)); // potential collider issues
                _inactiveQueue.Enqueue(initObject);
            }
        }

        public void SpawnOne(Vector3 position, Quaternion rotation)
        {
            if (_inactiveQueue.TryDequeue(out PooledObject obj))
            {
                obj.gameObject.SetActive(true);
                Transform t = obj.transform;
                t.position = position;
                t.rotation = rotation;
            }
            else
            {
                AddToPool(20);
                PooledObject initObject = Instantiate(_prefab, position, rotation);
            }
        }

        public void Despawn(PooledObject obj)
        {
            obj.gameObject.SetActive(false);
            obj.transform.Translate(new Vector3(10000, 10000, 10000)); // potential collider issues
            _inactiveQueue.Enqueue(obj);
        }
    }
}