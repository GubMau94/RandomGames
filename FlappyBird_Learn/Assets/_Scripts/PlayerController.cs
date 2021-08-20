using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    [SerializeField, Range(1, 10)] private int speed;
    [SerializeField] private GameObject gameOver, controllerExplain, spawnManager, getReady;
    [SerializeField] private Button restartButton;
    [SerializeField] private TMP_Text pointsText;
    [SerializeField] private AudioClip flyClip, coinClip, dieClip;
    [SerializeField] private AudioSource audioSource;

    private int points, maxScore;
    private bool startEnable, paused = false;

    private Animator _anim;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();

        _rigidbody.bodyType = RigidbodyType2D.Kinematic;
        spawnManager.SetActive(false);

        points = 0;
        startEnable = false;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();

        pointsText.text = "" + points;

        if(Input.GetMouseButtonDown(0))
        {
            startEnable = true;
            controllerExplain.SetActive(false);
            getReady.SetActive(false);
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
            spawnManager.SetActive(true);
        }
    }

    private void MovePlayer()
    {
        if (Input.GetMouseButtonDown(0) && startEnable && !paused)
        {
            _rigidbody.velocity = new Vector2(0, speed);

            _anim.SetBool("Flying", true);
            audioSource.PlayOneShot(flyClip);
        }
        else
        {
            _anim.SetBool("Flying", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Time.timeScale = 0;
            gameOver.SetActive(true);
            restartButton.gameObject.SetActive(true);
            audioSource.PlayOneShot(dieClip);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coins"))
        {
            Destroy(collision.gameObject);
            points += 5;
            audioSource.PlayOneShot(coinClip);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Passed"))
        {
            points++;
        }
    }

    public void PauseGame()
    {
        if (!paused)
        {
            Time.timeScale = 0;
            paused = !paused;
        } else
        {
            Time.timeScale = 1;
            paused = !paused;
        }
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

}
