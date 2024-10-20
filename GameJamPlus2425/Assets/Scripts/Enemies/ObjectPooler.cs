using System;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public class ObjectPooler : MonoBehaviour
    {
        private void Awake()
        {
            SharedInstance = this;

            int itemCount = itemsToPool.Count;
            itemDictionary = new Dictionary<int, ObjectPoolItem>(itemCount);
            pooledObjectsDictionary = new Dictionary<int, HashSet<GameObject>>(itemCount); // Usando HashSet para pooling
            positions = new Dictionary<int, int>(itemCount);

            for (int i = 0; i < itemCount; i++)
            {
                int hash = itemsToPool[i].tag.GetHashCode();
                ObjectPoolItemToPooledObject(i, hash);
                itemDictionary.Add(hash, itemsToPool[i]);
            }
        }

        public void SetParentGroup(string tag, GameObject obj)
        {
            int hash = tag.GetHashCode();
            Transform parentGroup = GetOrCreateParentGroup(hash, itemDictionary[hash].objectToPool.name);
            obj.transform.SetParent(parentGroup);
        }

        private Transform GetOrCreateParentGroup(int hash, string objectName)
        {
            string groupName = objectName + "Group";
            Transform parentGroup = transform.Find(groupName);

            if (parentGroup == null)
            {
                GameObject groupObj = new GameObject(groupName);
                parentGroup = groupObj.transform;
                parentGroup.SetParent(transform);
            }

            return parentGroup;
        }

        public GameObject GetPooledObject(string tag)
        {
            int hash = tag.GetHashCode();
            
            if (!pooledObjectsDictionary.TryGetValue(hash, out HashSet<GameObject> objectSet))
            {
                Debug.LogError($"Tag not found in ObjectPooler: {tag}");
                return null;
            }

            foreach (var pooledObject in objectSet)
            {
                if (!pooledObject.activeInHierarchy)
                {
                    return pooledObject;
                }
            }

            if (itemDictionary[hash].shouldExpand)
            {
                GameObject newObject = Instantiate(itemDictionary[hash].objectToPool);
                newObject.SetActive(false);
                SetParentGroup(tag, newObject);
                objectSet.Add(newObject);
                return newObject;
            }

            return null;
        }

        public List<GameObject> GetAllPooledObjects(string tag)
        {
            int hash = tag.GetHashCode();
            if (pooledObjectsDictionary.TryGetValue(hash, out HashSet<GameObject> objectSet))
            {
                return new List<GameObject>(objectSet);
            }

            return null;
        }

        public void AddObject(string tag, GameObject GO, int amt = 3, bool exp = true)
        {
            int hash = tag.GetHashCode();
            if (pooledObjectsDictionary.ContainsKey(hash)) return;

            ObjectPoolItem item = new ObjectPoolItem(tag, GO, amt, exp);
            itemsToPool.Add(item);
            ObjectPoolItemToPooledObject(itemsToPool.Count - 1, hash);
            itemDictionary.Add(hash, item);
        }

        private void ObjectPoolItemToPooledObject(int index, int hash)
        {
            ObjectPoolItem objectPoolItem = itemsToPool[index];
            HashSet<GameObject> objectSet = new HashSet<GameObject>();

            Transform parentGroup = GetOrCreateParentGroup(hash, objectPoolItem.objectToPool.name);

            for (int i = 0; i < objectPoolItem.amountToPool; i++)
            {
                GameObject newObject = Instantiate(objectPoolItem.objectToPool);
                newObject.SetActive(false);
                newObject.transform.SetParent(parentGroup);
                objectSet.Add(newObject);
            }

            pooledObjectsDictionary[hash] = objectSet;
        }

        public static ObjectPooler SharedInstance;

        public List<ObjectPoolItem> itemsToPool;

        private Dictionary<int, ObjectPoolItem> itemDictionary;

        private Dictionary<int, HashSet<GameObject>> pooledObjectsDictionary;

        private Dictionary<int, int> positions;
    }
}
