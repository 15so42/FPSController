using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool  {
    public Transform poolRoot;
    public static ObjectPool singleton;
    public static ObjectPool _instance
    {
        get
        {
            if (singleton == null)
                singleton = new ObjectPool();
            return singleton;
        }
        set { }
        
    }

    public Queue<GameObject> objects=new Queue<GameObject>();
	
	public void Recyle(GameObject go)
    {
        go.SetActive(poolRoot);
        go.SetActive(false);
        objects.Enqueue(go);
    }
    
    public GameObject GetObject()
    {
        if (objects.Count > 0)
        {
            GameObject go = objects.Dequeue();
            go.SetActive(true);
            return go;
        }
        else
            return null;
        
       
    }

}
