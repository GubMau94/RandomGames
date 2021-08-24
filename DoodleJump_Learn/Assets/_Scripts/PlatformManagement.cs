using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManagement : MonoBehaviour
{
    private GameObject player;
    private BoxCollider2D _boxCollider;


    // Start is called before the first frame update
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if(player.transform.position.y < transform.position.y + 0.5f)
        {
            _boxCollider.enabled = false;
        } else
        {
            _boxCollider.enabled = true;
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
