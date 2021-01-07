using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public static class ObjectPool
{
    static Dictionary<GameObject, Pool> pools;
    static void init(GameObject initObj)
    {
        if(pools == null)
        {
            pools = new Dictionary<GameObject, Pool>();
        }
        if(initObj != null && pools.ContainsKey(initObj) == false)
        {
            pools[initObj] = new Pool(initObj);
        }
    }
    public static GameObject Spawn(GameObject prefab, Vector3 pos, Quaternion rotation)
    {
        init(prefab);
        return pools[prefab].Spawn(pos, rotation);
    }
    public static void Despawn(GameObject prefab)
    {
        PoolMember pmTemp = prefab.GetComponent<PoolMember>();
        if(pmTemp == null)
        {
            GameObject.Destroy(prefab);
        }
        else
        {
            pmTemp.myPool.Despawn(prefab);
        }
    }
    class Pool
    {
        int currentId = 1;
        Stack<GameObject> inactiveObj;
        GameObject objectPrefab;
        public Pool(GameObject prefab)
        {
            objectPrefab = prefab;
            inactiveObj = new Stack<GameObject>();
        }
        public GameObject Spawn(Vector3 pos, Quaternion rotation)
        {
            GameObject spawnObject;
            if(inactiveObj.Count == 0)
            {
                spawnObject = (GameObject)GameObject.Instantiate(objectPrefab, pos, rotation);
                spawnObject.name = objectPrefab.name + "_" + currentId;
                spawnObject.AddComponent<PoolMember>().myPool = this;
            }
            else
            {
                spawnObject = inactiveObj.Pop();
                if(spawnObject == null)
                {
                    return Spawn(pos, rotation);
                }
            }
            spawnObject.transform.position = pos;
            spawnObject.transform.rotation = rotation;
            spawnObject.SetActive(true);
            return spawnObject;
        }
        public void Despawn(GameObject despawnObj)
        {
            despawnObj.SetActive(false);
            inactiveObj.Push(despawnObj);
        }
    }
    class PoolMember: MonoBehaviour
    {
        public Pool myPool;
    }
 
}
