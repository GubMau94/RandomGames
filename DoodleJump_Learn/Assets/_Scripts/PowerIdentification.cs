using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerIdentification : MonoBehaviour
{
    [SerializeField] powerName PowerName;
    private enum powerName
    {
        springShoes,
        rocket,
        jetpack,
        propeller
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !PlayerController.powerActive)
        {
            PlayerController.powerName = PowerName.ToString();
        }
    }
}
