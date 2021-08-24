using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D _rigidbody;
    private Animator _anim;

    private float horizontalMovement, highestPosY;
    private bool turnLeft = false;

    [SerializeField, Range(0.1f, 500f)] private float jumpForce;
    [SerializeField, Range(0.1f, 500f)] private float movementSpeed;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();

        highestPosY = 0;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        ScoreManager();
    }

    /// <summary>
    /// Gestisce sia il movimento del player sull'asse X, sia le relative animazioni 
    /// </summary>
    private void PlayerMovement()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector2.right * horizontalMovement * movementSpeed * Time.deltaTime);

        if(horizontalMovement == 1)
        {
            turnLeft = false;
            _anim.SetBool("TurnLeft", turnLeft);
        } else if (horizontalMovement == -1)
        {
            turnLeft = true;
            _anim.SetBool("TurnLeft", turnLeft);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            if (!turnLeft)
            {
                _anim.Play("JumpRight");
            } else if (turnLeft)
            {
                _anim.Play("JumpLeft");
            }
            
            _rigidbody.velocity = new Vector2(0, jumpForce);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("KillZone"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void ScoreManager()
    {
        if(transform.position.y > highestPosY)
        {
            highestPosY = transform.position.y;
        }

        GameManager.points = Mathf.RoundToInt(highestPosY * 10f);
    }
    

} //class