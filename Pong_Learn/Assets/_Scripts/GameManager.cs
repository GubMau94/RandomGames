using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scorePlayer1, scorePlayer2, winner;
    [SerializeField] private GameObject player1, player2, ballPrefab, winPanel;
    [SerializeField, Range(1, 20)] private int winScore;

    public static int pointsPlayer1, pointsPlayer2;
    public static bool scoreLeft, scoreRight, spawnBall;

    private Vector3 player1Pos, player2Pos;

    private gameStatus GameStatus;
    private enum gameStatus
    {
        loading,
        playing,
        gameOver
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreLeft = false;
        scoreRight = false;

        Instantiate(ballPrefab, Vector2.zero, ballPrefab.transform.rotation);
        winner.text = "";
        winPanel.SetActive(false);
        pointsPlayer1 = 0;
        pointsPlayer2 = 0;
        GameStatus = gameStatus.playing;
    }

    // Update is called once per frame
    void Update()
    {
        scorePlayer1.text = "" + pointsPlayer1;
        scorePlayer2.text = "" + pointsPlayer2;

        WinConditions(winScore);
        SpawnBall();        
    }

    /// <summary>
    /// Gestisce la condizione di vittoria
    /// </summary>
    /// <param name="winConditionScore">valore da raggiungere per vincere</param>
    private void WinConditions(int winConditionScore)
    {
        if (pointsPlayer1 >= winConditionScore)
        {
            winner.text = "PLAYER 1 WON!!";
            winPanel.SetActive(true);
            GameStatus = gameStatus.gameOver;
        }

        if (pointsPlayer2 >= winConditionScore)
        {
            winner.text = "PLAYER 2 WON!!";
            winPanel.SetActive(true);
            GameStatus = gameStatus.gameOver;
        }

    }

    /// <summary>
    /// Fa in modo che la pallina si istanzi nella posizione y del player che ha subito punto
    /// </summary>
    private void SpawnBall()
    {
        if(GameStatus == gameStatus.playing)
        {
            if (spawnBall)
            {
                spawnBall = false;
                player1Pos = player1.transform.position;
                player2Pos = player2.transform.position;

                if (scoreLeft)
                {
                    Vector2 pos1 = new Vector2(0, player1Pos.y);
                    Instantiate(ballPrefab, pos1, ballPrefab.transform.rotation);
                }
                else if (scoreRight)
                {
                    Vector2 pos2 = new Vector2(0, player2Pos.y);
                    Instantiate(ballPrefab, pos2, ballPrefab.transform.rotation);
                }
            }
        }        
    }

    /// <summary>
    /// Fa reinizializzare il gioco una volta che uno dei due player ha vinto (richiamata direttamente dal pulsante)
    /// </summary>
    public void RestartGame()
    {
        winPanel.SetActive(false);

        pointsPlayer1 = 0;
        pointsPlayer2 = 0;
        scoreLeft = false;
        scoreRight = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
       
}
