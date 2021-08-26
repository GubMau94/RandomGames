using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private CapsuleCollider2D _capsuleCollider;
    private BoxCollider2D cameraBoxCollider;
    private Animator _anim;

    private float positionMovementX, leftDirection, rightDirection;
    private Vector3 moveDirection;

    [SerializeField, Range(0, 10)] private int speed;



    // Start is called before the first frame update
    void Start()
    {
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        cameraBoxCollider = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BoxCollider2D>();
        _anim = GetComponent<Animator>();

        positionMovementX = cameraBoxCollider.bounds.extents.x - _capsuleCollider.bounds.extents.x;

        moveDirection = Vector3.left;

        leftDirection = Random.Range(-positionMovementX, -1.1f);
        rightDirection = Random.Range(1.1f, positionMovementX);

    }

    // Update is called once per frame
    void Update()
    {
        MoveRightLeft();
    }

    private void MoveRightLeft()
    {        
        transform.Translate(moveDirection * speed * Time.deltaTime);

        if (transform.position.x >= rightDirection)
        {
            _anim.SetBool("TurnLeft", true);
            moveDirection = Vector3.left;
        }
        else if (transform.position.x <= leftDirection)
        {
            _anim.SetBool("TurnLeft", false);
            moveDirection = Vector3.right;
        }
    }

}
