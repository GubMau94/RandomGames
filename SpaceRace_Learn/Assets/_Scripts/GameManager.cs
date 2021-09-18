using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject asteroidLeft, asteroidRight, victoryPanel;
    private Vector2 spawnPosLeft, spawnPosRight;

    [SerializeField, Range(0, 20)] private float time, repeatTime;
    [SerializeField] private TMP_Text scorePlayer1, scorePlayer2, victoryPlayer;
    [SerializeField, Range(1, 99)] private int winPoints; 

    // Start is called before the first frame update
    void Start()
    {
        spawnPosLeft = new Vector2(-3.0f, 0.0f);
        spawnPosRight = new Vector2(3.0f, 0.0f);

        InvokeRepeating("SpawnAsteroids", time, repeatTime);

        scorePlayer1.text = "";
        scorePlayer2.text = "";

        victoryPanel.SetActive(false);

        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        ScoreManagement();
        WinCondition();
    }

    /// <summary>
    /// Gestisce l'istanziazione degli asteroidi
    /// </summary>
    private void SpawnAsteroids()
    {
        spawnPosLeft = new Vector2(spawnPosLeft.x, Random.Range(-1.6f, 4.8f));
        spawnPosRight = new Vector2(spawnPosRight.x, Random.Range(-1.6f, 4.8f));

        Instantiate(asteroidLeft, spawnPosLeft, asteroidLeft.transform.rotation);
        Instantiate(asteroidRight, spawnPosRight, asteroidRight.transform.rotation);
    }

    /// <summary>
    /// Stampa sullo score i punti totalizzati dai giocatori
    /// </summary>
    private void ScoreManagement()
    {
        scorePlayer1.text = "" + PlayerController.pointsPlayer1;
        scorePlayer2.text = "" + PlayerController.pointsPlayer2;
    }

    /// <summary>
    /// Reinizia la partuta una volta che uno dei due giocatori ha vinto
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Verifica che le condizioni di vittoria si verifichino e visualizza il vincitore
    /// </summary>
    private void WinCondition()
    {
        if(PlayerController.pointsPlayer1 >= winPoints)
        {
            victoryPanel.SetActive(true);
            victoryPlayer.text = "PLAYER 1 WON!!";
            Time.timeScale = 0;
        }

        if (PlayerController.pointsPlayer2 >= winPoints)
        {
            victoryPanel.SetActive(true);
            victoryPlayer.text = "PLAYER 2 WON!!";
            Time.timeScale = 0;
        }
    }
}
