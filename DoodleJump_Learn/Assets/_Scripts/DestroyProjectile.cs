using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectile : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if((transform.position.y - Camera.main.transform.position.y) > 20.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            Destroy(gameObject);
        }
    }
}
