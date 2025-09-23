using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;   
    public Transform firePoint;
    public GameObject bulletPrefeb2;

    Camera cam;

    void Start()
    {
        cam = Camera.main;  
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Shoot2();
        }
    }

    void Shoot()
    {
       
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPoint;
        targetPoint = ray.GetPoint(50f);
        Vector3 direction = (targetPoint - firePoint.position).normalized;  

        
        GameObject proj = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(direction));
        
    }
    void Shoot2()
    {

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPoint;
        targetPoint = ray.GetPoint(50f);
        Vector3 direction = (targetPoint - firePoint.position).normalized;


        
        GameObject proj2 = Instantiate(bulletPrefeb2, firePoint.position, Quaternion.LookRotation(direction));
    }
}
