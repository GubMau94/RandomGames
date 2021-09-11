using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject backgroundPrefab, backgroundBounds, player, highestPlatformAtStart, highestMonsterAtStart;
    [SerializeField] private GameObject[] platformPrefab, monstersPrefab, powerPrefabs;
    [SerializeField] private float maxSpawnPlatformPosX, minSpawnPlatformPosX;
    [SerializeField, Range(0.1f, 3.5f)] private float minSpaceBetweenPlatformsY, maxSpaceBetweenPlatformsY;
    [SerializeField] private TMP_Text score; 

    private float backgroundDimensionX, backgroundDimensionY, highestPlatformY, highestMonsterY, platformDimensionY, powerDimensionY;
    private int counterPlayerPos = 1;
    private int counterBackgroundPos = 4;
    public static int points = 0;
    private int counter = 0;

    private Vector2 backgroundPos;
    private Vector2 spawnPos;


    // Start is called before the first frame update
    void Start()
    {
        backgroundDimensionX = backgroundBounds.GetComponent<BoxCollider2D>().bounds.extents.x;
        backgroundDimensionY = backgroundBounds.GetComponent<BoxCollider2D>().bounds.extents.y;

        platformDimensionY = highestPlatformAtStart.GetComponent<BoxCollider2D>().bounds.extents.y;

        highestPlatformY = highestPlatformAtStart.transform.position.y;
        highestMonsterY = highestMonsterAtStart.transform.position.y;

        score.text = "" + 0;
    }

    // Update is called once per frame
    void Update()
    {
        BackgroundSpawner(backgroundDimensionY);
        BackgroundLimitsX(backgroundDimensionX);
        PlatformSpawner(highestPlatformY, minSpaceBetweenPlatformsY, maxSpaceBetweenPlatformsY);
        MonsterSpawner(monstersPrefab);

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

    /// <summary>
    /// Istanzia una piattaforma quando il player ha una distanza inferiore a 9[UM] da quella più alta
    /// </summary>
    /// <param name="platHighestY"></param>
    /// <param name="minSpaceY"></param>
    /// <param name="maxSpaceY"></param>
    private void PlatformSpawner(float platHighestY, float minSpaceY, float maxSpaceY)
    {
        if(platHighestY - player.transform.position.y <= 9)
        {
            int index = Random.Range(0, platformPrefab.Length);
            spawnPos = new Vector2(Random.Range(minSpawnPlatformPosX, maxSpawnPlatformPosX), Random.Range((platHighestY + minSpaceY), platHighestY + maxSpaceY));

            Instantiate(platformPrefab[index], spawnPos, platformPrefab[index].transform.rotation);

            counter++;

            if (index != 0 && counter > 10)
            {
                PowerSpawner(platformDimensionY);
                counter = 0;
            }

            highestPlatformY = spawnPos.y;
        }
    }

    /// <summary>
    /// Instanzia un mostro ogni volta che il player si avvicina a quello attuale a meno di 4[UM]
    /// </summary>
    /// <param name="monsterPrefab"></param>
    private void MonsterSpawner(GameObject[] monsterPrefab)
    {        
        if((highestMonsterY - player.transform.position.y) <= 4.0f)
        {
            int index = Random.Range(0, monsterPrefab.Length);
            Vector2 spawnPos = new Vector2(Random.Range(minSpawnPlatformPosX, Mathf.Abs(minSpawnPlatformPosX)), (highestMonsterY + Random.Range(12.5f, 25.5f)));
            Instantiate(monsterPrefab[index], spawnPos, monsterPrefab[index].transform.rotation);
               
            highestMonsterY = spawnPos.y;
        }
        
    }

    /// <summary>
    /// Instanzia i poteri solo sulle piattaforme verdi
    /// </summary>
    /// <param name="platDimY"></param>
    private void PowerSpawner(float platDimY)
    {
        int index = Random.Range(0, powerPrefabs.Length);
        powerDimensionY = powerPrefabs[index].GetComponent<CircleCollider2D>().radius;
        Vector2 spawnPosPower = new Vector2(spawnPos.x, (spawnPos.y + platformDimensionY + powerDimensionY));

        Instantiate(powerPrefabs[index], spawnPosPower, powerPrefabs[index].transform.rotation);
    }

} //class
