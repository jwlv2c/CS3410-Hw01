using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;


public class PlayerController : MonoBehaviour
{
    /*
        Script Purpose:
            Controls the Player, as well as normal game functions

        Commentary:
            Most complicated game object since it has to control the player, and 
            UI texts. Really, the most complicated part of this script was how to 
            capture the end-game states as snapshots and not as continually updating
            values for the Timer and Score.
    */

    public float speed;
    public Text timerText;
    public Text victoryText;
    public Text ScoreText;
    public Button restartButton;
    public GameObject safeZone;

    private Rigidbody2D playerBody;
    private float timer = 0.0f;
    private int gameDuration = 60; //60 seconds long
    private float captureTime;
    private float captureScore;
    private float score;
    private float scoreDisplay;
    private readonly int scoreFactor = 1;
    private bool gameRunning = true;

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
        if(gameRunning ) CheckTimer();
    }

    void FixedUpdate()
    {
        Vector2 moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        playerBody.velocity = moveDirection * speed;

        //Prevents excess UFO rotations
        if (Math.Abs(playerBody.angularVelocity) > 5)
        {
            if (playerBody.angularVelocity < 0) playerBody.angularVelocity = -5;
            else playerBody.angularVelocity = 5;
        }
        if(gameRunning) UpdateScore();
    }
    
    private void CheckTimer()
    {
        if (timer > gameDuration) EndGame(false); //Game has been won!
    }   

    private void UpdateScore()
    {
        Vector3 distanceVector = playerBody.transform.position - safeZone.transform.position;
        float distanceToSafeZone = distanceVector.magnitude;
        float safeZoneRadius = safeZone.transform.localScale.x / 2 + 2 * playerBody.transform.localScale.x;

        //outside the safezone
        if (distanceToSafeZone > safeZoneRadius) score += distanceToSafeZone * scoreFactor * Time.deltaTime;

        if((distanceToSafeZone - safeZoneRadius) < 0) distanceToSafeZone = safeZoneRadius; //Forces multiplier to be 0

        scoreDisplay = (int)((Math.Floor((score * 100))) / 100);

        //Originally going to make the multiplier a float, but decided to cast it as an int to make it a little cleaner on the UI
        ScoreText.text = "Score: " + scoreDisplay.ToString() + "\n Multiplier: " + (int)(Math.Floor((distanceToSafeZone - safeZoneRadius)*100))/100;

    }

    public void OnCollisionEnter2D(Collision2D otherObject)
    {
        if (otherObject.gameObject.CompareTag("Obstacle") && gameRunning) EndGame(true); //Player collided with Obstacle object. End of game is called

        if (otherObject.gameObject.CompareTag("Background")) playerBody.velocity = Vector2.zero;
    }

    private void EndGame(bool hitObject)
    {
        gameRunning = false;
        timerText.gameObject.SetActive(false);
        ScoreText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(true);

        captureTime = timer * 100;
        captureTime = (int)captureTime;
        captureTime = captureTime / 100;
        captureScore = (int)scoreDisplay;
        
        if (hitObject) victoryText.text = "";
        else victoryText.text = "You Win!";

        victoryText.text += "You Survived for " + captureTime.ToString() + " seconds\n Your Score: " + captureScore.ToString();

    }

    public void OnRestartButtonPress()
    {
        SceneManager.LoadScene("MainScene");
    }
}
