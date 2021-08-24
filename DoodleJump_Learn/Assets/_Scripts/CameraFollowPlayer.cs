using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] GameObject player, backgroundEnd;
    private float backgroundPosY;

    private void Start()
    {
        backgroundPosY = Mathf.Abs(backgroundEnd.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y >= transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, player.transform.position.y, -10);
        }

        backgroundEnd.transform.position = new Vector3(backgroundEnd.transform.position.x, (transform.position.y - backgroundPosY), backgroundEnd.transform.position.z);


    }
}
