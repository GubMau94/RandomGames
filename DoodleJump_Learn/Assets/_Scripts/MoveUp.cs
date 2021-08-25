using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUp : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    [SerializeField, Range(0.1f, 20f)] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody.velocity = Vector2.up * speed;
    }
}
