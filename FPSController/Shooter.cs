using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {
    public GameObject bullet;

    public void Shoot()//对象池
    {
        GameObject goFromPool = ObjectPool._instance.GetObject();
        if (goFromPool != null)
        {
           
            goFromPool.transform.position = transform.position;
            goFromPool.transform.forward = transform.forward;
            //Instantiate(goFromPool, transform.position, transform.rotation);
        }
        else
        {
            Instantiate(bullet, transform.position, transform.rotation);
        }
        
    }
}
