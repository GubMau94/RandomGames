using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private float speed, rotSpeed;
    [SerializeField] private GameObject projectilePrefab;

    private bool whiteRotPos, whiteRotNeg, blackRotPos, blackRotNeg;
    private Vector3 muzzleWhite, muzzleBlack;

    // Start is called before the first frame update
    void Start()
    {
        muzzleWhite = GameObject.FindGameObjectWithTag("MuzzleWhite").transform.position;
        muzzleBlack = GameObject.FindGameObjectWithTag("MuzzleBlack").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward(speed);
        JetsRotation(whiteRotPos, whiteRotNeg, blackRotPos, blackRotNeg);

        ProjectileSpawn();
    }
    
    /// <summary>
    /// Gestisce il movimento in avanti dei jet a velocità costante
    /// </summary>
    /// <param name="jetSpeed"> velocità alla quale si muovono i jet </param>
    private void MoveForward(float jetSpeed)
    {
        transform.Translate(Vector2.right * jetSpeed * Time.deltaTime, Space.Self);
    }

    /// <summary>
    /// Gestisce la rotazione a sinistra e destra dei jet
    /// </summary>
    /// <param name="whitePos">rotazione positiva del jet bianco</param>
    /// <param name="whiteNeg">rotazione negativa del jet bianco</param>
    /// <param name="blackPos">rotazione positiva del jet nero</param>
    /// <param name="blackNeg">rotazione negativa del jet nero</param>
    private void JetsRotation(bool whitePos, bool whiteNeg, bool blackPos, bool blackNeg)
    {
        whitePos = Input.GetKey(KeyCode.A);
        whiteNeg = Input.GetKey(KeyCode.D);
        blackPos = Input.GetKey(KeyCode.LeftArrow);
        blackNeg = Input.GetKey(KeyCode.RightArrow);

        if (gameObject.CompareTag("Player1"))
        {
            if (whitePos)
            {
                transform.Rotate(Vector3.forward * rotSpeed * Time.deltaTime, Space.Self);
            }
            if (whiteNeg)
            {
                transform.Rotate(Vector3.forward * -rotSpeed * Time.deltaTime, Space.Self);
            }
        }

        if (gameObject.CompareTag("Player2"))
        {
            if (blackPos)
            {
                transform.Rotate(Vector3.forward * rotSpeed * Time.deltaTime, Space.Self);
            }
            if (blackNeg)
            {
                transform.Rotate(Vector3.forward * -rotSpeed * Time.deltaTime, Space.Self);
            }
        }

    }

    /// <summary>
    /// Istanzia il proiettile dopo avere premuto il tasto assegnato:
    /// W = fuoco per il jet bianco / 
    /// UpArrow = fuoco per il jet nero
    /// </summary>
    private void ProjectileSpawn()
    {
        muzzleWhite = GameObject.FindGameObjectWithTag("MuzzleWhite").transform.position;
        muzzleBlack = GameObject.FindGameObjectWithTag("MuzzleBlack").transform.position;

        if (gameObject.CompareTag("Player1") && Input.GetKeyDown(KeyCode.W))
        {
            Instantiate(projectilePrefab, muzzleWhite, transform.rotation);
        }

        if (gameObject.CompareTag("Player2") && Input.GetKeyDown(KeyCode.UpArrow))
        {
            Instantiate(projectilePrefab, muzzleBlack, transform.rotation);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile") && gameObject.CompareTag("Player1"))
        {
            GameManager.whiteLife--;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Projectile") && gameObject.CompareTag("Player2"))
        {
            GameManager.blackLife--;
            Destroy(collision.gameObject);
        }
    }

}
