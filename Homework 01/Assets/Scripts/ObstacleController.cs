using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObstacleController : MonoBehaviour
{
    public float speed = 5;
    private Angle sinFunction;
    private float angleVel = 45f;
    private Vector2 velocityDir;
    private Rigidbody2D selfBody;
    private Vector3 lastVelocity;

    public float bounceFactor = 1; //0 - no bounce, 1 - reflect bounce, >1 - overbounce

    // Start is called before the first frame update
    void Start()
    {
        selfBody = GetComponent<Rigidbody2D>();

        float randomX = UnityEngine.Random.Range(-1.0f, 1.0f);
        float randomY = UnityEngine.Random.Range(-1.0f, 1.0f);
        velocityDir = new Vector2(randomX, randomY);
        selfBody.velocity = velocityDir * speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(0, 0, angleVel * Time.deltaTime);
        
        if (Math.Abs(selfBody.angularVelocity) > angleVel)
        {
            if (selfBody.angularVelocity < 0) selfBody.angularVelocity = -5;
            else selfBody.angularVelocity = 5;
        }

        if (selfBody.velocity.magnitude > speed ) selfBody.velocity = velocityDir * speed;
        lastVelocity = selfBody.velocity;
    }

    private void OnCollisionEnter2D(Collision2D otherObject) 
    {
        speed = lastVelocity.magnitude;
        var direction = Vector3.Reflect(lastVelocity.normalized, otherObject.contacts[0].normal);
        velocityDir = direction;

        selfBody.velocity = bounceFactor * speed * velocityDir;
    }

}
