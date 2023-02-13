using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField]
    private Canvas gameOverCanvas;
    private TextMeshProUGUI textGameOver;
    private Button resetGameButton;
    private static bool _isGameOver = false;
    private static string winnerName = "";

    // Start is called before the first frame update
    void Start()
    {
        gameOverCanvas.enabled = false;
        this.textGameOver = gameOverCanvas.GetComponentInChildren<TextMeshProUGUI>();

        this.resetGameButton = gameOverCanvas.GetComponentInChildren<Button>();
        resetGameButton.onClick.AddListener(resetGame);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isGameOver){
            gameOverCanvas.enabled = true;
            this.textGameOver.text = "GameOver" + "\n" + "Winner Name: " + winnerName;
            Time.timeScale = 0;
        }
    }

    public static void gameOver(string winner){
        winnerName = winner;
        _isGameOver = true;
    }

    public void resetGame()
    {
        _isGameOver = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

}
