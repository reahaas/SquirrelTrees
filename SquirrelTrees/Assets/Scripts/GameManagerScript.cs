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
    private Button SetThreePlayersModeButton;    
    private Button SetFourPlayersModeButton;
    private static bool _isGameRunning = false;
    private static string winnerName = "";

    // playerMode must be static to survive the scene reload on reset game!!
    private static int playerMode = 2;
    private List<GameObject> fourPlayersModeObjects = new List<GameObject>();
    private List<GameObject> threePlayersModeObjects = new List<GameObject>();
    private List<GameObject> threeRemovePlayersModeObjects = new List<GameObject>();
    private List<GameObject> emptyList = new List<GameObject>();
    private List<GameObject> ActivePlayersModeObjects;
    private List<GameObject> NonActivePlayersModeObjects;
    
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

        this.SetThreePlayersModeButton = mainMenuButtons[2];
        SetThreePlayersModeButton.onClick.AddListener(setThreePlayersMode);

        this.SetFourPlayersModeButton = mainMenuButtons[3];
        SetFourPlayersModeButton.onClick.AddListener(setFourPlayersMode);

        this.fourPlayersModeObjects.Add(player3);
        this.fourPlayersModeObjects.Add(player3Panel);
        this.fourPlayersModeObjects.Add(player4);
        this.fourPlayersModeObjects.Add(player4Panel);
        
        this.threePlayersModeObjects.Add(player3);
        this.threePlayersModeObjects.Add(player3Panel);

        
        this.threeRemovePlayersModeObjects.Add(player4);
        this.threeRemovePlayersModeObjects.Add(player4Panel);

        setPlayersMode();
    }

    // Update is called once per frame
    void Update()
    {
        if (! _isGameRunning && Time.timeScale != 0){
            this.mainMenuCanvas.enabled = true;
            if (winnerName != ""){
                this.mainMenuText.text = "GameOver" + "\n" + "Winner: " + winnerName;
            }
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
    
    public void setThreePlayersMode(){
        playerMode = 3;
        setPlayersMode();
    }

    public void setFourPlayersMode(){
        playerMode = 4;
        setPlayersMode();
    }

    public void setPlayersMode(){ 
        // Check game mode
        if (playerMode == 2){
            this.NonActivePlayersModeObjects = this.fourPlayersModeObjects;
            this.ActivePlayersModeObjects = this.emptyList;
        }
        else if (playerMode == 3){
            this.ActivePlayersModeObjects = this.threePlayersModeObjects;
            this.NonActivePlayersModeObjects = this.threeRemovePlayersModeObjects;
        }
        else if (playerMode == 4){
            this.ActivePlayersModeObjects = this.fourPlayersModeObjects;
            this.NonActivePlayersModeObjects = this.emptyList;
        }
        else{
            Debug.Log("playerMode is not valid: " + playerMode);
        }

        // Set active objects
        foreach(GameObject obj in this.ActivePlayersModeObjects){
            obj.SetActive(true);
        }

        // Set non active objects
        foreach(GameObject obj in this.NonActivePlayersModeObjects){
            obj.SetActive(false);
        }
    }

}
