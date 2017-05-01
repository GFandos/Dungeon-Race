using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerBallController : MonoBehaviour {

    public float speed;
    public float maxAngle = 95;
    public GameObject ball;
    public GameObject explosion;
    public Text points;
    public Text winText;
    public Button restart;

    private int puntuation;
    private bool onFloor = false;
    private Rigidbody body;
    private bool win = false;
    public GameObject[] coins = new GameObject[10];
    private Vector3 playerStartingPos;
    private Vector3 ballStartingPos;
    private AudioSource audio;

    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody>();
        puntuation = 0;
        restart.gameObject.SetActive(false);
        winText.gameObject.SetActive(false);
        playerStartingPos = transform.position;
        ballStartingPos = ball.gameObject.transform.position;
        audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if(!win)
        {

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(vertical, 0, -horizontal);

            body.AddForce(direction * speed);

            if (Input.GetKey(KeyCode.Space) && onFloor)
            {

                Vector3 verticalForce = new Vector3(0, 20, 0);
                body.AddForce(verticalForce * speed);
                onFloor = false;

            }
        }

        if(this.transform.position.y < -100)
        {
            this.gameObject.SetActive(false);
        }

    }

    void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.tag == "Terrain")
        {
            onFloor = true;
        }
        else if(collision.gameObject.tag == "Labyrinth")
        {

        }
        else if(collision.gameObject.tag == "BallTrigger")
        {

            collision.gameObject.SetActive(false);

            Vector3 force = new Vector3(-5000, 0, 0);

            ball.GetComponent<Rigidbody>().AddForce(force);

        }
        else if (collision.gameObject.tag == "Coin")
        {
            collision.gameObject.SetActive(false);
            puntuation++;
            points.text = "Coins: " + puntuation;
            audio.Play();

        }
        else if(collision.gameObject.tag == "Finish")
        {
            collision.gameObject.SetActive(false);
            win = true;
            winText.gameObject.SetActive(true);
            winText.text += puntuation + "/10";
            restart.gameObject.SetActive(true);
        }
        else
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            this.gameObject.SetActive(false);
            restart.gameObject.SetActive(true);
        }

        /*if(collision.gameObject.tag == "Spinner")
        {
            body.AddForce(collision.gameObject.GetComponent<Rigidbody>().velocity * 2000);
        }*/
  

    }

    public void restartGame()
    {
        win = false;
        restart.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void exitGame()
    {
        Application.Quit();
    }

}
