using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideBounds : MonoBehaviour
{
    private BoxCollider2D mapBounds;

    private float minBoundX, maxBoundX, minBoundY, maxBoundY;

    // Start is called before the first frame update
    void Start()
    {
        mapBounds = GameObject.Find("MapBounds").GetComponent<BoxCollider2D>();

        minBoundX = mapBounds.bounds.min.x;
        maxBoundX = mapBounds.bounds.max.x;
        minBoundY = mapBounds.bounds.min.y;
        maxBoundY = mapBounds.bounds.max.y;

        print("minX: " + minBoundX + "maxX: " + maxBoundX + "minY: " + minBoundY + "maxY: " + maxBoundY);
    }

    // Update is called once per frame
    void Update()
    {
        GroundBounds();
    }

    private void GroundBounds()
    {
        if (transform.position.x > maxBoundX)
        {
            transform.position = new Vector3(minBoundX, transform.position.y, transform.position.z);
        }

        if (transform.position.x < minBoundX)
        {
            transform.position = new Vector3(maxBoundX, transform.position.y, transform.position.z);
        }

        if (transform.position.y > maxBoundY)
        {
            transform.position = new Vector3(transform.position.x, minBoundY, transform.position.z);
        }

        if (transform.position.y < minBoundY)
        {
            transform.position = new Vector3(transform.position.x, maxBoundY, transform.position.z);
        }
    }
}
