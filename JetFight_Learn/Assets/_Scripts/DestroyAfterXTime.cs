using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterXTime : MonoBehaviour
{
    [SerializeField, Range(1, 99)] private float destroyTime;
    private float counter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if(counter > destroyTime)
        {
            Destroy(gameObject);
        }
    }
}
