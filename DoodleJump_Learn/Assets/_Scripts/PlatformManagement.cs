using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformManagement : MonoBehaviour
{
    private GameObject player;
    private BoxCollider2D _boxCollider;

    [SerializeField] private platformBehaviour PlatformBehaviour;
    private enum platformBehaviour
    {
        standard,
        bluePlatform,
        redPlatform
    }

    private BoxCollider2D cameraBoxCollider;
    private float positionMovementX, leftDirection, rightDirection;
    private Vector3 moveDirection;
    [SerializeField, Range(0, 10)] private float speed;


    // Start is called before the first frame update
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();

        if(SceneManager.GetActiveScene().name == "Gameplay")
        {
            cameraBoxCollider = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BoxCollider2D>();
            positionMovementX = cameraBoxCollider.bounds.extents.x - _boxCollider.bounds.extents.x;
        }
        


        moveDirection = Vector3.left;

        leftDirection = Random.Range(-positionMovementX, -1.1f);
        rightDirection = Random.Range(1.1f, positionMovementX);
    }

    // Update is called once per frame
    void Update()
    {
        PlatformTrigger();
        PlatformSpecialBehaviour();
    }

    /// <summary>
    /// Fa in modo che le piattaforme sopra il player siano dei trigger
    /// </summary>
    private void PlatformTrigger()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player.transform.position.y < (transform.position.y + 0.6f))
        {
            _boxCollider.isTrigger = true;
        }
        else
        {
            _boxCollider.isTrigger = false;
        }
    }

    private void PlatformSpecialBehaviour()
    {
        switch (PlatformBehaviour)
        {
            case platformBehaviour.standard:
                //piattaforme base che non hanno un comportamento speciale
                break;
            case platformBehaviour.bluePlatform:
                
                    transform.Translate(moveDirection * speed * Time.deltaTime);

                    if (transform.position.x >= rightDirection)
                    {
                        moveDirection = Vector3.left;
                    }
                    else if (transform.position.x <= leftDirection)
                    {
                        moveDirection = Vector3.right;
                    }
               
                break;
            case platformBehaviour.redPlatform:
                //da implementare
                break;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("KillZone"))
        {
            Destroy(gameObject);
        }
    }


}
