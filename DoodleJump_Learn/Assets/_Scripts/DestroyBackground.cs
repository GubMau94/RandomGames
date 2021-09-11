using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBackground : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if((Camera.main.transform.position.y - transform.position.y) > 200.0f)
        {
            Destroy(gameObject);
        }
    }
}
