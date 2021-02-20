using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BunnyController : MonoBehaviour
{

    public float speed;
    private float boostTimer;

    private Rigidbody2D rb2d;

    private bool gameOver;
    private bool boosting;

    public Text scoreText;
    public Text healthText;

    private int score;
    private int health;

    public Camera MainCamera;
    public Camera LevelTwoCamera;

    public float timeInvincible = 3.0f;
    bool isInvincible;
    float invincibleTimer;
   
   
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D> ();
        gameOver = false;
        score = 0;
        SetScoreText ();
        health = 3;
        SetHealthText ();

        MainCamera.enabled = true;
        LevelTwoCamera.enabled = false;

        boostTimer = 0;
        boosting = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");

        float moveVertical = Input.GetAxis ("Vertical");

        Vector2 movement = new Vector2 (moveHorizontal, moveVertical);

        rb2d.AddForce (movement * speed);

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    void Update()
    {
          if (Input.GetKey(KeyCode.R))
        {
            if (gameOver == true)
            {
              SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if (boosting)
        {
            boostTimer += Time.deltaTime;
            if (boostTimer >= 3)
            {
                speed = 3;
                boostTimer = 0;
                boosting = false;
            }
        }

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("pickup"))
        {
            other.gameObject.SetActive (false);
            score = score + 1;
            SetScoreText ();
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive (false);
            health = health - 1;
            SetHealthText ();
        }

        if (other.gameObject.CompareTag("speed"))
        {
            other.gameObject.SetActive (false);
            boosting = true;
            speed = 10;
        }

        if (other.gameObject.CompareTag("ghost"))
        {
            other.gameObject.SetActive (false);
            if (health < 0)
            {
                if (isInvincible)
                {
                    return;
            
                    isInvincible = true;
                    invincibleTimer = timeInvincible;
                }
            }
        }
    }
    void SetScoreText ()
    {
        scoreText.text = "Score: " + score.ToString ();

        if (score == 10)
        {
            gameObject.transform.position = new Vector3(-42, 0.0f, -10);

            MainCamera.enabled = false;
            LevelTwoCamera.enabled = true;
        }

        if (score == 20)
        {
            //winText.text = "You Win!";
            Destroy(gameObject);
        }
        
    }

    void SetHealthText ()
    {
        healthText.text = "Lives: " + health.ToString ();

        if (health <= 0)
        {
            //loseText.text = "You Lose!";
            Destroy(gameObject);
        }

        if (health < 0)
        {
            if (isInvincible)
                return;
            
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
    }
}
