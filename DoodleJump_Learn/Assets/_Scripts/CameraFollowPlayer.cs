using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] GameObject player, backgroundEnd, fallTrigger;
    private float backgroundPosY, fallTriggerPosY, timer;

    private void Start()
    {
        backgroundPosY = Mathf.Abs(backgroundEnd.transform.position.y);
        fallTriggerPosY = Mathf.Abs(fallTrigger.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerController.gameOver)
        {
            if (player.transform.position.y >= transform.position.y)
            {
                transform.position = new Vector3(transform.position.x, player.transform.position.y, -10);
            }

            backgroundEnd.transform.position = new Vector3(backgroundEnd.transform.position.x, (transform.position.y - backgroundPosY), backgroundEnd.transform.position.z);
            fallTrigger.transform.position = new Vector3(fallTrigger.transform.position.x, (transform.position.y - fallTriggerPosY), fallTrigger.transform.position.z);
        } else
        {
            timer += Time.deltaTime;

            if (timer < 1.3)
            {
                transform.position = new Vector3(transform.position.x, player.transform.position.y, -10);
                backgroundEnd.transform.position = new Vector3(backgroundEnd.transform.position.x, (transform.position.y - backgroundPosY), backgroundEnd.transform.position.z);
            }
            
        }

            
        
        
    }
}
