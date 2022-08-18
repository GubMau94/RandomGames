using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreTextP1, scoreTextP2, timer;

    [SerializeField]
    private int matchTime;

    [SerializeField]
    private GameObject player1_Win, player2_Win;

    private float timerCounter;
    private int scorePlayer1, scorePlayer2;
    private const string SCORE1 = "SCORE1", SCORE2 = "SCORE2";

    // Start is called before the first frame update
    void Start()
    {
        scorePlayer1 = PlayerPrefs.GetInt(SCORE1);
        scorePlayer2 = PlayerPrefs.GetInt(SCORE2);

        ShowScore();

        timerCounter = matchTime;

        player1_Win.SetActive(false);
        player2_Win.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ShowScore();
        TimerManager();
        
        Winner();
    }

    /// <summary>
    /// Incrementa il punteggio del giocatore 1
    /// </summary>
    public void IncreaseScorePlayer1()
    {
        scorePlayer1++;
        PlayerPrefs.SetInt(SCORE1, scorePlayer1);        
    }

    /// <summary>
    /// Incrementa il punteggio del giocatore 2
    /// </summary>
    public void IncreaseScorePlayer2()
    {
        scorePlayer2++;
        PlayerPrefs.SetInt(SCORE2, scorePlayer2);
    }

    /// <summary>
    /// Visualizza il punteggio attuale di entrambi i giocatori
    /// </summary>
    private void ShowScore()
    {
        scoreTextP1.text = "Player 1: " + scorePlayer1;
        scoreTextP2.text = "Player 2: " + scorePlayer2;
    }

    /// <summary>
    /// Gestisce il tempo della partita
    /// </summary>
    /// <returns></returns>
    private void TimerManager()
    {
        if(timerCounter > 0)
        {
            timerCounter -= Time.deltaTime;
            timer.text = "" + Mathf.Clamp(Mathf.CeilToInt(timerCounter), 0, matchTime);
        }        
    }

    /// <summary>
    /// Restituisce lo stato della partita allo scadere del tempo
    /// </summary>
    /// <param name="score1"> punteggio del giocatore 1 </param>
    /// <param name="score2"> punteggio del giocatore 2 </param>
    /// <returns> 0: partita in svolgimento, 1: giocatore 1 vince, 2: giocatore 2 vince, 3: pareggio ed in attesa di un vincitore </returns>
    private int MatchStatusManager(int score1, int score2)
    {
        if(timerCounter <= 0)
        {
            if (score1 > score2)
            {
                return 1;
            }
            else if(score1 < score2)
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }
        return 0;        
    }

    private void Winner()
    {
        if (timerCounter <= 0)
        {
            switch (MatchStatusManager(scorePlayer1, scorePlayer2))
            {
                case 1:
                    player1_Win.SetActive(true);
                    player2_Win.SetActive(false);
                    break;

                case 2:
                    player2_Win.SetActive(true);
                    player1_Win.SetActive(false);
                    break;

                case 3: //draw
                    break;

                default: //playing
                    break;
            }
        }
        
    }
}
