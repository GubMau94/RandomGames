using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Hole") || collision.CompareTag("Monster") || collision.CompareTag("Power"))
        {
            Destroy(collision.gameObject);
        }
    }
}
