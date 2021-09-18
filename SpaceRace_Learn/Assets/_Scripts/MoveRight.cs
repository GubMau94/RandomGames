using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRight : MonoBehaviour
{
    [SerializeField, Range(0, 20)] private float speed;
    [SerializeField] private bool randomSpeed;

    private float internalSpeed;

    // Start is called before the first frame update
    void Start()
    {
        if (randomSpeed)
        {
            internalSpeed = Random.Range(0.3f, 1.5f);
        } else
        {
            internalSpeed = speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * internalSpeed * Time.deltaTime);
    }
}
