using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutBounds : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > 4.0f || transform.position.x < -4.0f)
        {
            Destroy(gameObject);
        }
    }
}
