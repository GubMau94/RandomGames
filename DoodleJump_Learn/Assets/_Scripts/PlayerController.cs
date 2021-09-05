using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D _rigidbody;
    private Animator _anim;
    private BoxCollider2D _boxCollider;
    private SpriteRenderer _spriteRenderer;
    private AudioSource _audioSource;


    private float horizontalMovement, highestPosY;
    private bool turnLeft, enableTakePowerActive, powerTrigger, springsActive, jetpackActive, rocketActive, propellerActive;
    public static bool powerActive;
    public static string powerName;

    private int jumpCounter;
    private float startingJumpForce;

    public static int maxScore;

    [SerializeField, Range(0.1f, 500f)] private float jumpForce;
    [SerializeField, Range(0.1f, 500f)] private float movementSpeed;
    [SerializeField] private GameObject projectilePrefab, mouth, projectileSpawnPos, propeller, jetpackLeft, jetpackRight, springShoes, rocketRight, rocketLeft;
    [SerializeField] private AudioClip fireClip, jumpClip, jetpackClip, rocketClip, propellerClip;
    [SerializeField] private SpriteRenderer jetpackLeftRenderer, jetpackRightRenderer, rocketLeftRenderer, rocketRightRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();

        turnLeft = false;
        powerActive = false;
        highestPosY = 0;
        jumpCounter = 0;
        startingJumpForce = jumpForce;

        //Reset data
        jetpackLeft.SetActive(false);
        jetpackRight.SetActive(false);
        rocketRight.SetActive(false);
        rocketLeft.SetActive(false);
        propeller.SetActive(false);
        springShoes.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        ScoreManager();
        PlayerShot();
        HighScore();
        PowerActivationManager();
        PowerManager();
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

            _audioSource.PlayOneShot(jumpClip);
            _rigidbody.velocity = new Vector2(0, jumpForce);

            if (springsActive)
            {
                PowerEvents.SpringShoesAnim();
                jumpCounter++;
            }
        }

        if(collision.gameObject.CompareTag("Platform") && powerTrigger)
        {
            enableTakePowerActive = true;
        }

        if (collision.gameObject.CompareTag("Monster"))
        {
            SceneManager.LoadScene(2);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("KillZone"))
        {
            SceneManager.LoadScene(2);            
        }

        if (collision.CompareTag("Power"))
        {
            powerTrigger = true;
        } 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Power"))
        {
            powerTrigger = false;
        }
    }

    /// <summary>
    /// Salva i punti attuale se sono superiori ai punit massimi raggiunti precedentemente
    /// </summary>
    private void HighScore()
    {
        if(GameManager.points > maxScore)
        {
            PlayerPrefs.SetInt("MAX_SCORE", GameManager.points);            
        }

        maxScore = PlayerPrefs.GetInt("MAX_SCORE");
    }

    /// <summary>
    /// Gestisce il sistema di puntuazione moltiplicando la posizione più alta raggiunta per un fattore pari a 10
    /// </summary>
    private void ScoreManager()
    {
        if(transform.position.y > highestPosY)
        {
            highestPosY = transform.position.y;
        }

        GameManager.points = Mathf.RoundToInt(highestPosY * 10f);
    }
    
    /// <summary>
    /// Spara un proiettile ogni volta che si preme space 
    /// </summary>
    private void PlayerShot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _anim.Play("IdleFire");
            _audioSource.PlayOneShot(fireClip);
            Instantiate(projectilePrefab, projectileSpawnPos.transform.position, projectilePrefab.transform.rotation);
        }
    }

    /// <summary>
    /// Gestisce l'attivazione dei poteri
    /// </summary>
    private void PowerActivationManager()
    {
        if (enableTakePowerActive && !powerActive)
        {
            switch (powerName)
            {
                case "jetpack":
                                        
                    jetpackLeft.SetActive(true);           
                    jetpackRight.SetActive(true);

                    if (turnLeft)
                    {
                        jetpackLeftRenderer.enabled = true;
                    }
                    else if (!turnLeft)
                    {
                        jetpackRightRenderer.enabled = true;
                    }

                    powerActive = true;                    

                    break;
                case "propeller":

                    
                    propeller.SetActive(true);
                    powerActive = true;

                    break;
                case "rocket":

                    rocketLeft.SetActive(true);
                    rocketRight.SetActive(true);

                    if (turnLeft)
                    {
                        rocketLeftRenderer.enabled = true;
                    }
                    else if (!turnLeft)
                    {
                        rocketRightRenderer.enabled = true;
                    }

                    powerActive = true;

                    break;
                case "springShoes":

                    
                    springShoes.SetActive(true);
                    powerActive = true;

                    break;
            }
        }
        
    }

    /// <summary>
    /// Gestisce il comportamento dei vari poteri
    /// </summary>
    private void PowerManager()
    {
        if (powerActive)
        {
            switch(powerName)
            {
                case "jetpack":

                    if (!jetpackActive)
                    {
                        _audioSource.PlayOneShot(jetpackClip);
                    }

                    jetpackActive = true;                    
                    _rigidbody.velocity = new Vector2(0, 15);
                    _boxCollider.enabled = false;

                    if (turnLeft)
                    {
                        jetpackLeftRenderer.enabled = true;
                        jetpackRightRenderer.enabled = false;
                    }
                    else if (!turnLeft)
                    {
                        jetpackRightRenderer.enabled = true;
                        jetpackLeftRenderer.enabled = false;
                    }

                    if (PowerEvents.animEnd)
                    {
                        powerName = "";
                        jetpackLeft.SetActive(false);
                        jetpackRight.SetActive(false);
                        _boxCollider.enabled = true;
                        jetpackActive = false;
                        powerActive = false;
                        enableTakePowerActive = false;
                        PowerEvents.animEnd = false;
                    }

                    break;
                case "propeller":

                    if (!propellerActive)
                    {
                        _audioSource.PlayOneShot(propellerClip);
                    }

                    propellerActive = true;                    
                    _rigidbody.velocity = new Vector2(0, 12);

                    if (PowerEvents.animEnd)
                    {
                        powerName = "";
                        propeller.SetActive(false);                        
                        powerActive = false;
                        enableTakePowerActive = false;
                        PowerEvents.animEnd = false;
                    }

                    break;
                case "rocket":

                    if (!rocketActive)
                    {
                        _audioSource.PlayOneShot(rocketClip);
                    }

                    rocketActive = true;                    
                    _rigidbody.velocity = new Vector2(0, 23);
                    _boxCollider.enabled = false;
                    _spriteRenderer.enabled = false;

                    if (rocketActive)
                    {
                        if (turnLeft)
                        {
                            rocketLeftRenderer.enabled = true;
                            rocketRightRenderer.enabled = false;
                        }
                        else if (!turnLeft)
                        {
                            rocketRightRenderer.enabled = true;
                            rocketLeftRenderer.enabled = false;
                        }
                    }

                    if (PowerEvents.animEnd)
                    {
                        powerName = "";
                        rocketRight.SetActive(false);
                        rocketLeft.SetActive(false);
                        _boxCollider.enabled = true;
                        _spriteRenderer.enabled = true;
                        rocketActive = false;
                        enableTakePowerActive = false;
                        powerActive = false;
                        PowerEvents.animEnd = false;
                    }

                    break;
                case "springShoes":

                    springsActive = true;
                    jumpForce = 16;
                    if(jumpCounter > 3)
                    {
                        powerName = "";
                        jumpForce = startingJumpForce;
                        jumpCounter = 0;
                        springsActive = false;
                        springShoes.SetActive(false);
                        enableTakePowerActive = false;
                        powerActive = false;

                    }

                    break;
            }
        }
    }

} //class
