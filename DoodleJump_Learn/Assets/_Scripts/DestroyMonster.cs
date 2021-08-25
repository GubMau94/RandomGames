using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMonster : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile") || collision.CompareTag("KillZone"))
        {
            Destroy(gameObject);
        }
    }
}
