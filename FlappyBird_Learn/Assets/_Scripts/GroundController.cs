using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    private BoxCollider2D _myBoxCollider;
    [SerializeField] GameObject otherGround;

    // Start is called before the first frame update
    void Start()
    {
        _myBoxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x <= -7.60)
        {
            transform.position = new Vector2((otherGround.transform.position.x + (_myBoxCollider.bounds.extents.x)*2), transform.position.y);
        }
    }
}
