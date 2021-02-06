using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;

// This script is attached to the player.
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    // All the variables that you can change that effect the feel of the gameplay are public and accessible in the inspector.
   


    Rigidbody2D rb;
    float maxvelocity ;
    private bool onground;

    public float minmass = 1;
    public float maxmass = 100000;
    public float velocitylimit = 3;
    public float gravity_multiplier;
    public float dissertation_multiplier = 0.5f;
    public float acceleration_multiplier = 80f;
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        maxvelocity = velocitylimit;
    }

  
    void FixedUpdate()
    {
        //Physics

        RotateWithSpeed();
        AddMovements();
        SimulateGravity();
    }

    private void Update()
    {
        //Input

        //Adds extra mass when player presses so that the player can accelerate and decelerate faster.
        if (Input.GetMouseButton(0))
        {
            rb.mass = maxmass;
            rb.gravityScale = 3.5f;
        }

        if (Input.GetMouseButtonUp(0))
        {    
            rb.mass = minmass;
            rb.gravityScale = 1f;
        }
    }

  
    void RotateWithSpeed ()
    {
        //Makes the player rotate with respect to it's velocity on the Y axis.
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        rb.SetRotation(Quaternion.Euler(new Vector3(0, 0, angle)));
        
    }
    void AddMovements()
    {
        //Pushes the player forward and allows for accelerations and decelerations.

        if (rb.velocity.x < maxvelocity && rb.velocity.x > velocitylimit)
        {

            rb.AddForce(new Vector2(acceleration_multiplier * rb.mass, 0));

        }

        if (rb.velocity.x > maxvelocity && rb.velocity.x > velocitylimit)
        {

            if (rb.velocity.y > 0)
            {

                if (maxvelocity > velocitylimit)
                {
                    maxvelocity -= 0.1f * dissertation_multiplier * rb.mass * Time.fixedDeltaTime;
                }

            }

        }

        if (rb.velocity.y < 0)
        {
            maxvelocity = rb.velocity.x;

        }


        if (rb.velocity.x < 3)
        {
            rb.velocity = new Vector2(3, rb.velocity.y);
        }

        if (maxvelocity < velocitylimit)
        {
            maxvelocity = velocitylimit;
        }
       
    }
    void SimulateGravity()
    {  //Adds an extra force downward that makes heavier objects fall faster.
        rb.AddForce(Vector2.down * rb.mass * rb.gravityScale * gravity_multiplier);
    }


    //Two functions allows to reset speed if the player collided with something or have extra speed if they smoothly slided.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        onground = true;
        if (onground)
        {
            maxvelocity = rb.velocity.x;
        }
        onground = false;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        onground = false;
    }
}

