using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    [SerializeField, Range(0,20)] private int force;

    private float timer;
    private bool leftArea, rightArea;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        if (GameManager.scoreLeft)
        {
            _rigidbody.AddForce(Vector2.left * force, ForceMode2D.Impulse);
        } else if (GameManager.scoreRight)
        {
            _rigidbody.AddForce(Vector2.right * force, ForceMode2D.Impulse);
        } else
        {
            _rigidbody.AddForce(Vector2.right * force, ForceMode2D.Impulse);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        AddForce();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TriggerRight"))
        {
            GameManager.scoreLeft = false;
            GameManager.scoreRight = true;
            GameManager.spawnBall = true;

            GameManager.pointsPlayer1++;
            Destroy(gameObject);
        }

        if (collision.CompareTag("TriggerLeft"))
        {
            GameManager.scoreLeft = true;
            GameManager.scoreRight = false;
            GameManager.spawnBall = true;

            GameManager.pointsPlayer2++;
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {        
        if (collision.CompareTag("AreaLeft"))
        {
            timer += Time.deltaTime;
            leftArea = true;
            rightArea = false;
        }
        
        if (collision.CompareTag("AreaRight"))
        {
            timer += Time.deltaTime;
            leftArea = false;
            rightArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("AreaLeft"))
        {
            timer = 0;
        }
        
        if (collision.CompareTag("AreaRight"))
        {
            timer = 0;
        }
    }

    /// <summary>
    /// Fa in modo in applicare una leggera forza alla pallina nel caso in cui rimanga incastrata in un lato del campo
    /// </summary>
    private void AddForce()
    {
        if(timer > 5 && leftArea)
        {
            _rigidbody.AddForce(Vector2.right * 5, ForceMode2D.Impulse);
            timer = 0;
        }
        
        if(timer > 5 && rightArea)
        {
            _rigidbody.AddForce(Vector2.left * 5, ForceMode2D.Impulse);
            timer = 0;
        }
    }

}
