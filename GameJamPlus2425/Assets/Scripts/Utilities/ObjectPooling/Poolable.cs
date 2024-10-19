using UnityEngine;
using System.Collections;

// Reference: https://theliquidfire.com/2015/07/06/object-pooling/ and https://theliquidfire.com/2016/02/24/poolers-part-1-of-4/
// Used in other projects as well

public class Poolable : MonoBehaviour 
{
	public string key;
	public bool isPooled;
}