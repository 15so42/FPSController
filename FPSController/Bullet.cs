using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public int recyleTime;
    public float speed=500;
    
	
    private void OnEnable()
    {
        StartCoroutine(RecyleC());
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    IEnumerator RecyleC()
    {
        yield return new WaitForSeconds(recyleTime);
        ObjectPool._instance.Recyle(gameObject);
    }
}
