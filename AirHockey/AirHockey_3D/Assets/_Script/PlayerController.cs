using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField, Range(100, 5000)]
    private int speed;

    [SerializeField]
    private float fieldMaxLimitZ, fieldMinLimitZ, fieldMaxLimitX, fieldMinLimitX;

    [SerializeField]
    private bool invertCommandDirection = false;

    private Vector3 movement;
    private bool keyPressed;
    

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMovement();   
    }

    /// <summary>
    /// Gestisce il movimento del giocatore
    /// </summary>
    private void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.DownArrow) && !(transform.position.z >= fieldMaxLimitZ))
        {
            movement.z = 1; //0,0,1
        }
        else if (Input.GetKey(KeyCode.UpArrow) && !(transform.position.z <= fieldMinLimitZ))
        {
            movement.z = -1; //0,0,-1
        }
        else
        {
            movement.z = 0;
        }

        if (!invertCommandDirection)
        {
            if (Input.GetKey(KeyCode.LeftArrow) && !(transform.position.x >= fieldMaxLimitX))
            {
                movement.x = 1; //1,0,0
            }
            else if (Input.GetKey(KeyCode.RightArrow) && !(transform.position.x <= fieldMinLimitX))
            {
                movement.x = -1; //-1,0,0
            }
            else
            {
                movement.x = 0;
            }
        } 
        else
        {
            if (Input.GetKey(KeyCode.LeftArrow) && !(transform.position.x <= fieldMinLimitX))
            {
                movement.x = -1; //-1,0,0
            }
            else if (Input.GetKey(KeyCode.RightArrow) && !(transform.position.x >= fieldMaxLimitX))
            {
                movement.x = 1; //1,0,0
            }
            else
            {
                movement.x = 0;
            }
        }

        keyPressed = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow);
        if (!keyPressed)
        {
            _rigidbody.velocity = Vector3.zero;
            movement = Vector3.zero;
        }

        _rigidbody.velocity = movement.normalized * speed * Time.fixedDeltaTime;

        FieldLimits();

    }

    /// <summary>
    /// Definisce i limiti del campo da gioco
    /// </summary>
    private void FieldLimits()
    {
        if (transform.position.z > 13)
        {
            Vector3 maxPosZ = new Vector3(transform.position.x, transform.position.y, 13);
            transform.position = maxPosZ;
            _rigidbody.velocity = Vector3.zero;
        }

        if (transform.position.z < -13)
        {
            Vector3 maxPosZ = new Vector3(transform.position.x, transform.position.y, -13);
            transform.position = maxPosZ;
            _rigidbody.velocity = Vector3.zero;
        }

        if (transform.position.x < 10)
        {
            Vector3 maxPosX = new Vector3(10, transform.position.y, transform.position.z);
            transform.position = maxPosX;
            _rigidbody.velocity = Vector3.zero;
        }

        if (transform.position.x > 26)
        {
            Vector3 maxPosX = new Vector3(26, transform.position.y, transform.position.z);
            transform.position = maxPosX;
            _rigidbody.velocity = Vector3.zero;
        }
    }
}
