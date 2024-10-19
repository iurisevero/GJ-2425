using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reference: https://theliquidfire.com/2015/07/06/object-pooling/ and https://theliquidfire.com/2016/02/24/poolers-part-1-of-4/
// Used in other projects as well

public class SetPooler : BasePooler
{
    public HashSet<Poolable> Collection = new HashSet<Poolable>();
    public override void Enqueue (Poolable item)
    {
        base.Enqueue(item);
        if (Collection.Contains(item))
            Collection.Remove(item);
    }

    public override Poolable Dequeue()
    {
        Poolable item = base.Dequeue();
        Collection.Add(item);
        return item;
    }

    public override void EnqueueAll()
    {
        foreach(Poolable item in Collection)
            base.Enqueue(item);
        Collection.Clear();
    }
}
