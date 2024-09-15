using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZoneController : MonoBehaviour
{
    public float speed = 5;
    private Vector2 velocityDir;
    private Rigidbody2D selfBody;
    private Vector3 lastVelocity;

    public float bounceFactor = 1; //0 - no bounce, 1 - reflect bounce, >1 - overbounce

    // Start is called before the first frame update
    void Start()
    {
        float randomX = Random.Range(-1.0f, 1.0f);
        float randomY = Random.Range(-1.0f, 1.0f);
        velocityDir = new Vector2(randomX * speed, randomY * speed);
        selfBody = GetComponent<Rigidbody2D>();
        selfBody.velocity = velocityDir;
    }

    private void OnCollisionEnter2D(Collision2D otherObject)
    {
        if (otherObject.gameObject.CompareTag("Background"))
        {
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, otherObject.contacts[0].normal);

            selfBody.velocity = direction * speed * bounceFactor;
        }

    }
}