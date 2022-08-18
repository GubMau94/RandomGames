using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DiskManager : MonoBehaviour
{
    [SerializeField, Range(10, 100)]
    private int speed = 20;

    private Rigidbody _rigidbody;
    private GameManager gameManager;
    private Vector3 _position;

    // Start is called before the first frame update
    void Start()
    {
        _position = transform.position;

        _rigidbody = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();

        RandomStartingDirection();
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody.velocity = _rigidbody.velocity.normalized * speed;
    }

    /// <summary>
    /// Gestisce la direzione di partenza del disco ogni volta che viene riposizionato al centro del campo
    /// </summary>
    private void RandomStartingDirection()
    {
        int i = Random.Range(0, 2);
        switch (i)
        {
            case 0:
                _rigidbody.velocity = Vector3.right;
                break;
            case 1:
                _rigidbody.velocity = Vector3.left;
                break;
        }
        transform.rotation = Quaternion.identity;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("AreaGoal1"))
        {
            gameManager.IncreaseScorePlayer2();
            transform.position = _position;
            RandomStartingDirection();
        }

        if (other.CompareTag("AreaGoal2"))
        {
            gameManager.IncreaseScorePlayer1();
            transform.position = _position;
            RandomStartingDirection();
        }

        if (other.CompareTag("OutsideField"))
        {
            transform.position = _position;
            RandomStartingDirection();
        }
    }
}
