using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reference: https://theliquidfire.com/2015/07/06/object-pooling/ and https://theliquidfire.com/2016/02/24/poolers-part-1-of-4/
// Used in other projects as well

public class IndexedPooler : BasePooler
{
    public List<Poolable> Collection = new List<Poolable>();

    public Poolable GetItem(int index)
    {
        if(index < 0 || index >= Collection.Count)
            return null;
        return Collection[index];
    }

    public U GetScript<U> (int index) where U : MonoBehaviour
    {
        Poolable item = GetItem(index);
        if (item != null)
            return item.GetComponent<U>();
        return null;
    }

    public Action<Poolable, int> willEnqueueAtIndex;
    public Action<Poolable, int> didDequeueAtIndex;

    public void EnqueueByIndex (int index)
    {
        if (index < 0 || index >= Collection.Count)
            return;
        Enqueue(Collection[index]);
    }

    public override void Enqueue(Poolable item)
    {
        base.Enqueue(item);
        int index = Collection.IndexOf(item);
        if(index != -1)
        {
            if(willEnqueueAtIndex != null)
                willEnqueueAtIndex(item, index);
            Collection.RemoveAt(index);
        }
    }

    public override Poolable Dequeue ()
    {
        Poolable item = base.Dequeue ();
        Collection.Add(item);
        if (didDequeueAtIndex != null)
            didDequeueAtIndex(item, Collection.Count - 1);
        return item;
    }

    public override void EnqueueAll ()
    {
        for (int i = Collection.Count - 1; i >= 0; --i)
            Enqueue(Collection[i]);
    }
}
