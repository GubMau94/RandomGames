using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool upPlayer1, downPlayer1, upPlayer2, downPlayer2;
    [SerializeField, Range(0, 20)] private float speed;

    // Update is called once per frame
    void Update()
    {
        Player1_Move();
        Player2_Move();

        CheckLimits();
    }

    /// <summary>
    /// Gestisce i movimenti del player 1
    /// </summary>
    private void Player1_Move()
    {
        if (gameObject.tag == "Player1")
        {
            upPlayer1 = Input.GetKey(KeyCode.W);
            downPlayer1 = Input.GetKey(KeyCode.S);

            if (upPlayer1 && !downPlayer1)
            {
                transform.Translate(Vector2.up * speed * Time.deltaTime);
            }
            else if (downPlayer1 && !upPlayer1)
            {
                transform.Translate(Vector2.down * speed * Time.deltaTime);
            }
        }        
    }

    /// <summary>
    /// Gestisce i movimenti del player 2
    /// </summary>
    private void Player2_Move()
    {
        if (gameObject.tag == "Player2")
        {
            upPlayer2 = Input.GetKey(KeyCode.UpArrow);
            downPlayer2 = Input.GetKey(KeyCode.DownArrow);

            if (upPlayer2 && !downPlayer2)
            {
                transform.Translate(Vector2.up * speed * Time.deltaTime);
            }
            else if (downPlayer2 && !upPlayer2)
            {
                transform.Translate(Vector2.down * speed * Time.deltaTime);
            }
        }        
    }

    /// <summary>
    /// Evita che i player escano dal campo di gioco
    /// </summary>
    private void CheckLimits()
    {
        if(transform.position.y >= 4.0f)
        {
            transform.position = new Vector2(transform.position.x, 4.0f);
        }

        if(transform.position.y <= -4.0f)
        {
            transform.position = new Vector2(transform.position.x, -4.0f);
        }
    }

}
