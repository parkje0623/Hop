using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private float jumpForce;

    private Vector3 mousePos;
    private Vector3 targetPos;
    private Vector3 hitPos;
    private float zAxis;
    private bool touchPlatform;
    private float speedControl;
    private Text scoreboard;
    private GameObject restartGame;
    private GameObject quitGame;
    private Text trackHighScore;

    public static int score;
    public static bool isAlive;
    
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        scoreboard = GameObject.Find("Score").GetComponent<Text>();
        restartGame = GameObject.Find("Restart");
        restartGame.SetActive(false);
        quitGame = GameObject.Find("Quit");
        quitGame.SetActive(false);
        trackHighScore = GameObject.Find("HighScore").GetComponent<Text>();
        trackHighScore.text = "High Score: " + PlayerPrefs.GetInt("highScore").ToString();

        isAlive = true;
        zAxis = 9f;
        touchPlatform = false;
        score = 0;
        Physics.gravity = new Vector3(0, -16.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 1.8f)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
        else if (transform.position.y < 0)
        {
            Destroy(gameObject);
            if (score > PlayerPrefs.GetInt("highScore"))
            {
                PlayerPrefs.SetInt("highScore", score);
                PlayerPrefs.Save();
            }

            isAlive = false;
            restartGame.SetActive(true);
            quitGame.SetActive(true);
        }

        if (!touchPlatform)
        {
            mousePos = Input.mousePosition;
            mousePos.x = Mathf.Clamp(mousePos.x, 0, 1800);
            mousePos.y = Mathf.Clamp(mousePos.y, 0, 1000);
            targetPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, 300.0f, zAxis));
            transform.position = new Vector3(targetPos.x, transform.position.y, targetPos.z);
        }

        scoreboard.text = score.ToString();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Cube")
        {
            touchPlatform = true;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            rb.velocity = new Vector3(0, 0, 0);
            rb.AddForce(0, jumpForce, 0);
            collision.gameObject.GetComponent<Renderer>().material.color = new Color32(255, 192, 203, 1);
            PlatformGenerator.platformNum--;
            score++;
            checkScore();
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Cube")
        {
            touchPlatform = false;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Cube")
        {
            hitPos = collision.gameObject.transform.position;
            collision.gameObject.transform.position = Vector3.MoveTowards(hitPos, hitPos + new Vector3(0, -5, 0),
                10.0f * Time.deltaTime);
        }
    }

    private void checkScore()
    {
        if (score % 5 == 0 && score != 0 && score <= 80)
        {
            speedControl = Physics.gravity.y + Physics.gravity.y * 0.173f;
            Physics.gravity = new Vector3(0, speedControl, 0);
            PlatformGenerator.IncreaseSpeed();
        }
        else if (score >= 68)
        {
            Physics.gravity = new Vector3(0, speedControl + 2f, 0);
        }
    }
}
