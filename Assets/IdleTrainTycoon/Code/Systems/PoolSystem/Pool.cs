using System.Collections.Generic;
using UnityEngine;

namespace IdleTrainTycoon.Code.Systems.PoolSystem
{
    public class Pool<T> where T : Component
    {
        private readonly T _prefab;
        private readonly Transform _container;
        private readonly Queue<T> _queue = new();

        public Pool(T prefab, Transform container = null)
        {
            _prefab = prefab;
            _container = container;
        }

        public T Get()
        {
            if (_queue.Count > 0)
            {
                var obj = _queue.Dequeue();
                obj.gameObject.SetActive(true);
                return obj;
            }
            else
            {
                var obj = Object.Instantiate(_prefab, _container);
                obj.gameObject.SetActive(true);
                return obj;
            }
        }

        public void Return(T obj)
        {
            obj.gameObject.SetActive(false);
            _queue.Enqueue(obj);
        }

        public void Clear()
        {
            foreach (var obj in _queue)
            {
                Object.Destroy(obj.gameObject);
            }

            _queue.Clear();
        }
    }
}