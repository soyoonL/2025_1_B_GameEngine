using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCam;  
    public CinemachineFreeLook freeLookCam;       
    public bool usingFreeLook = false;

    public float jumpPower = 5f;
    public bool isGrounded;
    private Vector3 velocity;

    void Start()
    {
        virtualCam.Priority = 10;
        freeLookCam.Priority = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            usingFreeLook = !usingFreeLook;
            if (usingFreeLook)
            {
                freeLookCam.Priority = 20;   
                virtualCam.Priority = 0;
                GameObject player = GameObject.Find("Player");
                player.GetComponent<NewBehaviourScript>().enabled = false;

                if (isGrounded && Input.GetKey(KeyCode.Space))
                {
                    velocity.y = jumpPower;
                }
            }
            else
            {
                virtualCam.Priority = 20;   
                freeLookCam.Priority = 0;
                GameObject player = GameObject.Find("Player");
                player.GetComponent<NewBehaviourScript>().enabled = true;
            }
        }
    }
}