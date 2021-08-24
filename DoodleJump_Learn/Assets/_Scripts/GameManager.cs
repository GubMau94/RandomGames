using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject backgroundPrefab, backgroundBounds, player, highestPlatformAtStart;
    [SerializeField] private GameObject[] plaformPrefab;
    [SerializeField] private float maxSpawnPlatformPosX, minSpawnPlatformPosX;
    [SerializeField, Range(0.1f, 3.5f)] private float minSpaceBetweenPlatformsY, maxSpaceBetweenPlatformsY;
    [SerializeField] private TMP_Text score; 

    private float backgroundDimensionX, backgroundDimensionY, highestPlatformY;
    private int counterPlayerPos = 1;
    private int counterBackgroundPos = 4;
    public static int points = 0;

    private Vector2 backgroundPos;


    // Start is called before the first frame update
    void Start()
    {
        backgroundDimensionX = (backgroundBounds.GetComponent<BoxCollider2D>().bounds.extents.x);
        backgroundDimensionY = (backgroundBounds.GetComponent<BoxCollider2D>().bounds.extents.y);

        highestPlatformY = highestPlatformAtStart.transform.position.y;

        score.text = "" + 0;
    }

    // Update is called once per frame
    void Update()
    {
        BackgroundSpawner(backgroundDimensionY);
        BackgroundLimitsX(backgroundDimensionX);
        PlatformSpawner(highestPlatformY, minSpaceBetweenPlatformsY, maxSpaceBetweenPlatformsY);

        score.text = "" + points;
    }

    /// <summary>
    /// Instanzia uno sfondo successivo quando il player inizia lo sfondo precedente
    /// </summary>
    /// <param name="backgroundDimY">Parametro per identificare la dimensione massima dello sfondo in Y</param>
    private void BackgroundSpawner(float backgroundDimY)
    {
        if (player.transform.position.y > backgroundDimY * counterPlayerPos)
        {
            backgroundPos = new Vector2(0, backgroundDimY * counterBackgroundPos);
            counterPlayerPos += 2;
            Instantiate(backgroundPrefab, backgroundPos, Quaternion.identity);

            counterBackgroundPos += 2;
        }
    }

    /// <summary>
    /// Se il player esce dai margini del campo lo sposta verso il lato opposto
    /// </summary>
    /// <param name="backgroundDimX">Parametro per identificare la dimensione massima dello sfondo in X</param>
    private void BackgroundLimitsX(float backgroundDimX)
    {
        if(player.transform.position.x >= backgroundDimX)
        {
            player.transform.position = new Vector2(-backgroundDimX + 0.1f, player.transform.position.y);
        }

        if (player.transform.position.x <= -backgroundDimX)
        {
            player.transform.position = new Vector2(backgroundDimX - 0.1f, player.transform.position.y);
        }
    }

    private void PlatformSpawner(float platHighestY, float minSpaceY, float maxSpaceY)
    {
        if(platHighestY - player.transform.position.y <= 9)
        {
            int index = Random.Range(0, plaformPrefab.Length);
            Vector2 spawnPos = new Vector2(Random.Range(minSpawnPlatformPosX, maxSpawnPlatformPosX), Random.Range((platHighestY + minSpaceY), platHighestY + maxSpaceY));

            Instantiate(plaformPrefab[index], spawnPos, plaformPrefab[index].transform.rotation);

            highestPlatformY = spawnPos.y;
        }
    }

} //class
