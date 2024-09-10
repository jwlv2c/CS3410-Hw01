using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text timerText;
    public Text victoryText;
    public Button restartButton;

    private Rigidbody2D playerBody;
    private float timer = 0.0f;
    private int gameDuration = 60; //60 seconds long
    private float captureTime;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        timer = 0;
        timerText.text = "Time to go: " + timer.ToString();
        victoryText.text = "";
        restartButton.gameObject.SetActive(false);
        
    }

    private void Update()
    {
        timer += Time.deltaTime;
        int seconds = (int)timer % 60;
        int timeToGo = gameDuration - seconds;
        timerText.text = "Time to go: " + timeToGo.ToString();
    }

    void FixedUpdate()
    {
        Vector2 moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        playerBody.velocity = moveDirection * speed;
        CheckTimer();
    }

    public void OnCollisionEnter2D(Collision2D otherObject)
    {
        if(otherObject.gameObject.CompareTag("Obstacle"))
        {
            captureTime = timer*100;
            captureTime = (int)captureTime;
            captureTime = captureTime / 100;
            timerText.gameObject.SetActive(false);
            victoryText.text = "You Survived for " + captureTime.ToString() + " seconds";
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
            timerText.gameObject.SetActive(false);
            victoryText.text = "You Win!\\ You Survived for " + captureTime.ToString() + " seconds";
            restartButton.gameObject.SetActive(true);
        }
    }

    public void OnRestartButtonPress()
    {
        SceneManager.LoadScene("MainScene");
    }
}
