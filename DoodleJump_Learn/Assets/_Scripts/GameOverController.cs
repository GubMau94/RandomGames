using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private TMP_Text score, maxScore;

    // Start is called before the first frame update
    void Start()
    {
        score.text = "" + GameManager.points;
        maxScore.text = "" + PlayerController.maxScore;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
