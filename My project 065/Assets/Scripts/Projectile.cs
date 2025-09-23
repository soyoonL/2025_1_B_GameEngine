using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed2 = 20f;
    public float lifeTime2 = 2f;
   
    void Start()
    {
        
        Destroy(gameObject, lifeTime2);
    }

    
    void Update()
    {
        transform.Translate(Vector3.forward*speed2*Time.deltaTime);

       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {

            Destroy(gameObject);

        }
        
    }
}
