using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Mathematics;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text timerText;
    public Text victoryText;
    public Text ScoreText;
    public Button restartButton;
    public GameObject safeZone;

    private Rigidbody2D playerBody;
    private float timer = 0.0f;
    private int gameDuration = 10;//60; //60 seconds long
    private float captureTime;
    private float captureScore;
    private float score;
    private float scoreDisplay;
    private readonly int scoreFactor = 1;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        timer = 0;
        timerText.text = "Time to go: " + timer.ToString();
        victoryText.text = "";
        restartButton.gameObject.SetActive(false);
        score = 0;
        ScoreText.text = "Score: 0.00";
    }

    private void Update()
    {
        timer += Time.deltaTime;
        int seconds = (int)timer % 60;
        int timeToGo = gameDuration - seconds;
        timerText.text = "Time to go: " + timeToGo.ToString();
        CheckTimer();
        UpdateScore();
    }

    void FixedUpdate()
    {
        Vector2 moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        playerBody.velocity = moveDirection * speed;

        //Prevents excess UFO rotations
        if (math.abs(playerBody.angularVelocity) > 5)
        {
            if (playerBody.angularVelocity < 0) playerBody.angularVelocity = -5; 
            else playerBody.angularVelocity = 5;
        }
    }

    public void OnCollisionEnter2D(Collision2D otherObject)
    {
        if(otherObject.gameObject.CompareTag("Obstacle"))
        {
            captureTime = timer*100;
            captureTime = (int)captureTime;
            captureTime = captureTime / 100;
            timerText.gameObject.SetActive(false);
            victoryText.text = "You Survived for " + captureTime.ToString() + " seconds\n Your Score:" + captureScore.ToString();
            restartButton.gameObject.SetActive(true);
        }

        if (otherObject.gameObject.CompareTag("Background"))
        {
            playerBody.velocity = Vector2.zero;
        }
    }

    private void CheckTimer()
    {
        if (timer > gameDuration)
        {
            captureTime = gameDuration;
            captureScore = scoreDisplay;
            timerText.gameObject.SetActive(false);
            ScoreText.gameObject.SetActive(false);
            restartButton.gameObject.SetActive(true);
            
            victoryText.text = "You Win! You Survived for " + captureTime.ToString() + " seconds\n Your Score: " + captureScore.ToString();
        }
    }

    public void OnRestartButtonPress()
    {
        SceneManager.LoadScene("MainScene");
    }

    private void UpdateScore()
    {
        Vector3 distanceVector = playerBody.transform.position - safeZone.transform.position;
        float distanceToSafeZone = distanceVector.magnitude;
        float safeZoneRadius = safeZone.transform.localScale.x / 2 + 2 * playerBody.transform.localScale.x;

        if (distanceToSafeZone > safeZoneRadius) //outside the safezone
        {
            score += distanceToSafeZone * scoreFactor * Time.deltaTime;
        }

        if((distanceToSafeZone - safeZoneRadius) < 0) distanceToSafeZone = safeZoneRadius; //Forces multiplier to be 0

        scoreDisplay = (math.floor(score * 100)) / 100;
        ScoreText.text = "Score: " + scoreDisplay.ToString() + "\n Multiplier: " + (math.floor((distanceToSafeZone - safeZoneRadius)*100))/100;

    }
}
