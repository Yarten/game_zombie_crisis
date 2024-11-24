using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    public static ObjectPool Instance { get; } = new ObjectPool();
    
    // 作为所有受管理 object 的根
    private GameObject _root = new GameObject("ObjectPool");

    struct ObjectQueue
    {
        public GameObject Root;
        public Queue<GameObject> Data;
    }
    
    // 分类存放 objects
    private Dictionary<string, ObjectQueue> _pools = new Dictionary<string, ObjectQueue>();

    public GameObject GetObject(GameObject prefab)
    {
        if (_pools.TryGetValue(prefab.name, out ObjectQueue queue))
        {
            if (queue.Data.Count != 0)
            {
                GameObject obj = queue.Data.Dequeue();
                obj.SetActive(true);
                return obj;
            }
        }
        else
        {
            queue = CreateObjectQueue(prefab.name);
        }
        
        GameObject newObj = Object.Instantiate(prefab, queue.Root.transform);
        newObj.SetActive(true);
        return newObj;
    }

    public void ReturnObject(GameObject obj)
    {
        string name = obj.name.Replace("(Clone)", "");
        obj.SetActive(false);
        
        if (!_pools.TryGetValue(name, out ObjectQueue queue))
            queue = CreateObjectQueue(name);
        
        queue.Data.Enqueue(obj);
    }

    private ObjectQueue CreateObjectQueue(string prefabName)
    {
        var queue = new ObjectQueue
        {
            Root = new GameObject(prefabName + "ObjectQueue"),
            Data = new Queue<GameObject>(),
        };
        
        queue.Root.transform.SetParent(_root.transform);
        _pools.Add(prefabName, queue);
        
        return queue;
    }
}
