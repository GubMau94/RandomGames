using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int whiteLife, blackLife;

    [SerializeField, Range(1, 99)] private int jetLifes;
    [SerializeField] private TMP_Text lifeWhiteText, lifeBlackText, vicotryText;
    [SerializeField] private GameObject victoryPanel;
    

    // Start is called before the first frame update
    void Start()
    {
        whiteLife = blackLife = jetLifes;

        lifeWhiteText.text = "" + whiteLife;
        lifeBlackText.text = "" + blackLife;

        victoryPanel.SetActive(false);

        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        LifeUpdate(whiteLife, blackLife);

        blackLife = Mathf.Clamp(blackLife, 0, 99);
        whiteLife = Mathf.Clamp(whiteLife, 0, 99);

        WinCondition();
    }

    /// <summary>
    /// Tiene aggionato il testo visualizzato con le vite attuali
    /// </summary>
    /// <param name="whiteLifeFC">vite del jet bianco</param>
    /// <param name="blackLifeFC">vite del jet nero</param>
    private void LifeUpdate(int whiteLifeFC, int blackLifeFC)
    {
        lifeWhiteText.text = "" + whiteLifeFC;
        lifeBlackText.text = "" + blackLifeFC;
    }

    /// <summary>
    /// Verifica che le condizioni di vittoria si verifichino e mostra sullo schermo il vincitore
    /// </summary>
    private void WinCondition()
    {
        if(blackLife <= 0)
        {
            vicotryText.text = "WHITE PLAYER WON!!";
            victoryPanel.SetActive(true);
            Time.timeScale = 0;
            }

        if (whiteLife <= 0)
        {
            vicotryText.text = "BLACK PLAYER WON!!";
            victoryPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    /// <summary>
    /// Fa reiniziare la partita (richiamato dal pulsante di restart)
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
