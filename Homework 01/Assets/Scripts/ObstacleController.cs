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
    private Rigidbody2D obstacleBody;
    
    // Start is called before the first frame update
    void Start()
    {
        float randomX = Random.Range(-1.0f, 1.0f);
        float randomY = Random.Range(-1.0f, 1.0f);
        velocityDir = new Vector2(randomX*speed, randomY*speed);
        obstacleBody = GetComponent<Rigidbody2D>();
        obstacleBody.velocity = velocityDir;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, angleVel * Time.deltaTime);
    }

    /*
    public void OnCollisionEnter2D(Collision2D otherObject)
    {
        if(otherObject.gameObject.CompareTag("Background"))
        {
            bool movingRight = obstacleBody.velocity.x >= 0;
            bool movingUp = obstacleBody.velocity.y >= 0;
            bool reflected = false;

            if(movingRight)
            {
                if ((otherObject.transform.position.y >= transform.position.y) && movingUp)
                {
                    velocityDir.y *= -1;
                    obstacleBody.velocity = velocityDir;
                    reflected = true;
                }
                if ((otherObject.transform.position.x >= transform.position.x) && !reflected)
                {
                    velocityDir.x *= -1;
                    obstacleBody.velocity = velocityDir;
                }
            } else
            {

            }

            //is otherObject above us?
            

            //is otherObject below us?
            if ((otherObject.transform.position.y <= transform.position.y) && !reflected)
            {
                velocityDir.y *= -1;
                obstacleBody.velocity = velocityDir;
                reflected = true;
            }

            //is OtherObject left of us?
            if ((otherObject.transform.position.x >= transform.position.x) && !reflected)
            {
                velocityDir.x *= -1;
                obstacleBody.velocity = velocityDir;
                reflected = true;
            }

            //is OtherObject right of us?
            
        }
    }
    */

}
