using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    private float timeCounter = 0;

    public float speed, width, height, offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime * speed;

        float x = Mathf.Cos(timeCounter) * width;
        float y = Mathf.Sin(timeCounter) * height;
        float z = 0;

        transform.position = new Vector2(x, y + offset);
    }
}
