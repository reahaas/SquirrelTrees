using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField]
    private Canvas mainMenuCanvas;

    [SerializeField]
    private GameObject player3;
    [SerializeField]
    private GameObject player3Panel;
    [SerializeField]
    private GameObject player4;
    [SerializeField]
    private GameObject player4Panel;

    private TextMeshProUGUI mainMenuText;
    private Button startGameButton;
    private Button SetTwoPlayersModeButton;
    private Button SetFourPlayersModeButton;
    private static bool _isGameRunning = false;
    private static string winnerName = "";

    private static int playerMode = 2;
    private List<GameObject> fourPlayersModeObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        this.mainMenuCanvas.enabled = false;
        this.mainMenuText = this.mainMenuCanvas.GetComponentInChildren<TextMeshProUGUI>();

        Button [] mainMenuButtons = this.mainMenuCanvas.GetComponentsInChildren<Button>();

        this.startGameButton = mainMenuButtons[0];
        startGameButton.onClick.AddListener(resetGame);

        this.SetTwoPlayersModeButton = mainMenuButtons[1];
        SetTwoPlayersModeButton.onClick.AddListener(setTwoPlayersMode);

        this.SetFourPlayersModeButton = mainMenuButtons[2];
        SetFourPlayersModeButton.onClick.AddListener(setFourPlayersMode);

        this.fourPlayersModeObjects.Add(player3);
        this.fourPlayersModeObjects.Add(player3Panel);
        this.fourPlayersModeObjects.Add(player4);
        this.fourPlayersModeObjects.Add(player4Panel);
        setPlayersMode();
    }

    // Update is called once per frame
    void Update()
    {
        if (! _isGameRunning){
            this.mainMenuCanvas.enabled = true;
            this.mainMenuText.text = "GameOver" + "\n" + "Winner Name: " + winnerName;
            Time.timeScale = 0;
        }
    }

    public static void gameOver(string winner){
        winnerName = winner;
        _isGameRunning = false;
    }

    public void resetGame()
    {
        _isGameRunning = true;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void setTwoPlayersMode(){
        playerMode = 2;
        setPlayersMode();
    }
    
    public void setFourPlayersMode(){
        playerMode = 4;
        setPlayersMode();
    }

    public void setPlayersMode(){  
        if (playerMode == 2){
            foreach(GameObject obj in this.fourPlayersModeObjects){
                obj.SetActive(false);
            }
        }
        else{
            foreach(GameObject obj in this.fourPlayersModeObjects){
                obj.SetActive(true);
            }
        }
    }

}
