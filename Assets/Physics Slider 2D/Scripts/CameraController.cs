using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{


    // Test script just to follow the player and see whats going on around him


    public Transform target;
    
    void Start()
    {
        
    }

 
    void Update()
    {
        transform.position = new Vector3( target.transform.position.x , transform.position.y , -1);
    }
}
