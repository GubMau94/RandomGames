using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool upPlayer1, upPlayer2;
    [SerializeField, Range(0, 20)] private float speed;

    private Vector3 player1Pos, player2Pos;
    public static int pointsPlayer1, pointsPlayer2;

    // Start is called before the first frame update
    void Start()
    {
        pointsPlayer1 = 0;
        pointsPlayer2 = 0;

        if (gameObject.CompareTag("Player1"))
        {
            player1Pos = transform.position;
        }

        if (gameObject.CompareTag("Player2"))
        {
            player2Pos = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    /// <summary>
    /// Gestisce il movimento verticale del player
    /// </summary>
    private void PlayerMovement()
    {
        upPlayer1 = Input.GetKey(KeyCode.W);
        upPlayer2 = Input.GetKey(KeyCode.UpArrow);

        if(gameObject.tag == "Player1" && upPlayer1)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }

        if (gameObject.tag == "Player2" && upPlayer2)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*************************************************************************
         ********** Gestisce la parte di collisione con gli asteroidi ************
         *************************************************************************/
        if (collision.CompareTag("Asteroid") && gameObject.CompareTag("Player1"))
        {
            transform.position = player1Pos;
        }

        if (collision.CompareTag("Asteroid") && gameObject.CompareTag("Player2"))
        {
            transform.position = player2Pos;
        }
        //*************************************************************************

        if (collision.CompareTag("EndLine"))
        {
            if (gameObject.CompareTag("Player1"))
            {
                pointsPlayer1++;
                transform.position = player1Pos;
            }

            if (gameObject.CompareTag("Player2"))
            {
                pointsPlayer2++;
                transform.position = player2Pos;
            }
        }
    }
}
