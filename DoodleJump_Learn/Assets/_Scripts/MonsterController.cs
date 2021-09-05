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
    private bool moveLeft, moveRight;

    [SerializeField, Range(0, 10)] private int speed;

    [SerializeField] private monsterType MonsterType;
    private enum monsterType
    {
        monster1, //nessun comportamento speciale
        monster2,
        monster3,
        monster4,
        monster5, //tipologia di mostro che si gira a sinistra e destra durante il movimento orizzontale
        monster6, 
        monster7
    }


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
        SpecialMosnterBehaviour();
    }

    /// <summary>
    /// Fa in modo che il mostro si muovo da sinistra a destra. Inizia sempre verso sinistra
    /// </summary>
    private void MoveRightLeft()
    {        
        transform.Translate(moveDirection * speed * Time.deltaTime);

        if (transform.position.x >= rightDirection)
        {            
            moveLeft = true;
            moveRight = false;
            moveDirection = Vector3.left;
        }
        else if (transform.position.x <= leftDirection)
        {            
            moveRight = true;
            moveLeft = false;
            moveDirection = Vector3.right;
        }
    }

    /// <summary>
    /// Si occupa di gestire i movimenti speciali dei mostri in base alla tipologia scelta
    /// </summary>
    private void SpecialMosnterBehaviour()
    {
        switch (MonsterType)
        {
            case monsterType.monster1:
                //Da implementare se necessario
                break;
            case monsterType.monster2:
                //Da implementare se necessario
                break;
            case monsterType.monster3:
                //Da implementare se necessario
                break;
            case monsterType.monster4:
                //Da implementare se necessario
                break;
            case monsterType.monster5:
                
                if (moveLeft)
                {
                    _anim.SetBool("TurnLeft", true);
                } else if (moveRight)
                {
                    _anim.SetBool("TurnLeft", false);
                }

                break;
            case monsterType.monster6:
                //Da implementare se necessario
                break;
            case monsterType.monster7:
                //Da implementare se necessario
                break;
        }
    }

}
