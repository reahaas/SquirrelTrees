using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
using System;
using UnityEngine.UI;
using TMPro;


public class PlayerMovement : MonoBehaviour{
    public float speed;
    public GameObject playerPanel;
    public TextMeshProUGUI textScore;
    public FixedJoystick fixedJoystick;
    public Canvas restBarCanvas;
    public GameObject treePrefab;
    public float spawnSize = 100;
    [SerializeField]
    private string playerName = "";
    [SerializeField]
    private AudioSource musicPlayerCollectCoin;
    [SerializeField]
    private AudioSource musicPlayerPlant;

    private Image restBarSlider;
    private float newRestBarScale;
    public Rigidbody rb;

    private string acornSymbol = "<sprite name=\"Acorn_emoji_score\">";
    private string treeSymbol = "<sprite name=\"Tree_emoji_score\">";
    private int coinsCount = 0;
    private int treesCount = 0;
    private int treeCost = 5;
    private int treeScoreValue = 10;
    private int winningScore = 50;

    public Vector3 previousePosition;

    public float fullRestTime = 1.4f;
    private float restingDurationRemains;
    private Boolean isResting;
    private Boolean isPlanted;

    void Start(){
        this.textScore = playerPanel.GetComponentInChildren<TextMeshProUGUI>();
        this.fixedJoystick = playerPanel.GetComponentInChildren<FixedJoystick>();
        this.restBarSlider = restBarCanvas.GetComponentInChildren<Image>();
        
        this.setMotionParameters();
        this.SetScoresText();
    }

    private bool fallFromBoard(){
        return this.transform.position.y <= -5;
    }

    private void sendToStartPosition(){
        this.transform.position = new Vector3(0,3,0);
    }

    private bool isInMotion(){
        return Vector3.Distance(this.previousePosition, this.transform.position) > 0.01f;
    }

    private void setMotionParameters(){
        this.restingDurationRemains = this.fullRestTime;
        this.isResting = false;
        this.restBarCanvas.enabled = false;
        this.isPlanted = false;
        
        this.previousePosition = this.transform.position;
    }

    private void setRestingParameters(){
        this.restingDurationRemains -= Time.deltaTime;
        this.isResting = true;
        this.restBarCanvas.enabled = true;
        
        Vector2 currentSize = this.restBarSlider.rectTransform.sizeDelta;
        this.newRestBarScale = (Math.Max(restingDurationRemains, 0) / fullRestTime) * 100;
        this.restBarSlider.rectTransform.sizeDelta = new Vector2(this.newRestBarScale, currentSize.y);
    }

    private bool isPlantingFinised(){
        return this.isResting && 
               this.newRestBarScale == 0 && 
               this.coinsCount >= this.treeCost && 
               ! this.isPlanted;
    }

    private void plantTree(){
        this.musicPlayerPlant.Play();
        Vector3 spawnPosition = this.transform.position;
        GameObject spawnedTree = Instantiate(this.treePrefab, spawnPosition, Quaternion.identity);
        spawnedTree.transform.localScale *= this.spawnSize;
        this.treesCount += 1;
        
        this.coinsCount -= this.treeCost;
        this.isPlanted = true;
    }

    private void judgeTheGame(){
        if (this.isWinning()){
            GameManagerScript.gameOver(this.playerName);
        } 
    }

    void Update(){        
        Debug.Log("Player distance" + Vector3.Distance(previousePosition, this.transform.position).ToString());

        if (this.fallFromBoard()){
            this.sendToStartPosition();
        }
        
        if (this.isInMotion()){
            this.setMotionParameters();
        }
        else
        {
            this.setRestingParameters();

            if (this.isPlantingFinised()){
                this.plantTree();
                this.judgeTheGame();
            }
        }
        
        if (Math.Abs(fixedJoystick.Vertical) >= 0.2f || Math.Abs(fixedJoystick.Horizontal) >= 0.2f){
            Vector3 direction = Vector3.forward * fixedJoystick.Vertical + 
                                Vector3.right * fixedJoystick.Horizontal;
            this.transform.position = this.transform.position + direction * speed * Time.deltaTime;
            this.transform.forward = direction;
        }
        this.SetScoresText();
    }

    private void SetScoresText(){
        textScore.text = acornSymbol + this.coinsCount.ToString() + "\n" +
                         treeSymbol + this.treesCount.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            musicPlayerCollectCoin.Play();
            this.coinsCount++;
            Destroy(other.gameObject);
            
            this.SetScoresText();
            this.judgeTheGame();
        }
    }

    private bool isWinning(){
        int treeScore = this.treesCount * this.treeScoreValue;
        int totalScore = treeScore + this.coinsCount;
        return (totalScore >= this.winningScore);
    }
}
