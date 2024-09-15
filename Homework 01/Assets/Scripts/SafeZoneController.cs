using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZoneController : MonoBehaviour
{
    /*
        Script Purpose:
            This script controls the behavior of the Safe Zone, which the
            player can hide in to avoid objects on screen

        Commentary:
            This script shares a lot of behaviors with the ObstacleController CS
            script. Did think briefly of making a generic class, then doing
            some inheritence, but wasn't strictly necessary. It was a small
            enough project to not worry about it. 
    */ 

    public float speed = 5;
    private Vector2 velocityDir;
    private Rigidbody2D selfBody;
    private Vector3 lastVelocity;
    public GameObject playerObject;

    public float bounceFactor = 1; //0 - no bounce, 1 - reflect bounce, >1 - overbounce

    // Start is called before the first frame update
    void Start()
    {
        selfBody = GetComponent<Rigidbody2D>();

        float randomX = Random.Range(-1.0f, 1.0f);
        float randomY = Random.Range(-1.0f, 1.0f);
        velocityDir = new Vector2(randomX, randomY);
        selfBody.velocity = velocityDir * speed;
    }

    private void FixedUpdate()
    {
        if (selfBody.velocity.magnitude > speed) selfBody.velocity = velocityDir * speed;
        lastVelocity = selfBody.velocity;
    }

    private void OnCollisionEnter2D(Collision2D otherObject)
    {
        if (otherObject.gameObject.CompareTag("Background"))
        {
            speed = lastVelocity.magnitude;

            //Solution given on support forms since we can't use the material properties of the walls to bounce
            var direction = Vector3.Reflect(lastVelocity.normalized, otherObject.contacts[0].normal);
            velocityDir = direction;

            selfBody.velocity = bounceFactor * speed * velocityDir;
        }

    }
}