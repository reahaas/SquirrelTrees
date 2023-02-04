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
    public Color color;
    public GameObject treePrefab;
    public float spawnSize = 100;
    [SerializeField]
    private AudioSource musicPlayerCollectCoin;
    [SerializeField]
    private AudioSource musicPlayerPlant;

    private Image restBarSlider;
    private float newRestBarScale;
    public Rigidbody rb;
    private int coinsCount = 0;
    private int treesCount = 0;
    private int treeCost = 5;

    public Vector3 previousePosition;

    public float fullRestTime = 1.4f;
    private float restingDurationRemains;
    private Boolean isResting;
    private Boolean isPlanted;

    void Start(){
        this.textScore = playerPanel.GetComponentInChildren<TextMeshProUGUI>();
        this.fixedJoystick = playerPanel.GetComponentInChildren<FixedJoystick>();
        this.restBarSlider = restBarCanvas.GetComponentInChildren<Image>();
        this.restBarCanvas.enabled = false;
        this.isPlanted = false;

        SetScoresText();
        this.previousePosition = this.transform.position;
        isResting = true;
        restingDurationRemains = fullRestTime;
    }

    void Update(){        
        Debug.Log("Player distance" + Vector3.Distance(previousePosition, this.transform.position).ToString());
        
        if (Vector3.Distance(this.previousePosition, this.transform.position) > 0.01f){
            restingDurationRemains = fullRestTime;
            isResting = false;
            this.restBarCanvas.enabled = false;
            this.isPlanted = false;
            
            this.previousePosition = this.transform.position;
        }
        else
        {
            restingDurationRemains -= Time.deltaTime;
            isResting = true;
            this.restBarCanvas.enabled = true;
            
            Vector2 currentSize = this.restBarSlider.rectTransform.sizeDelta;
            newRestBarScale = (Math.Max(restingDurationRemains, 0) / fullRestTime) * 100;
            this.restBarSlider.rectTransform.sizeDelta = new Vector2(newRestBarScale, currentSize.y);

            if (isResting && newRestBarScale == 0 && this.coinsCount >= treeCost && ! this.isPlanted){
                musicPlayerPlant.Play();
                Vector3 spawnPosition = this.transform.position;
                GameObject spawnedTree = Instantiate(treePrefab, spawnPosition, Quaternion.identity);
                spawnedTree.transform.localScale *= spawnSize;
                this.treesCount += 1;

                this.coinsCount -= treeCost;
                this.isPlanted = true;
            }
        }
        

        if (Math.Abs(fixedJoystick.Vertical) >= 0.2f || Math.Abs(fixedJoystick.Horizontal) >= 0.2f){
            Vector3 direction = Vector3.forward * fixedJoystick.Vertical + 
                                Vector3.right * fixedJoystick.Horizontal;
            transform.position = transform.position + direction * speed * Time.deltaTime;
            transform.forward = direction;
        }
        SetScoresText();

    }

    private void SetScoresText(){
        textScore.text = "Coins: " + this.coinsCount.ToString() + "\n" +
                         "Trees: " + this.treesCount.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))  // other.CompareTag("Collectable"))
        {
            musicPlayerCollectCoin.Play();
            this.coinsCount++;
            Destroy(other.gameObject);
        }
        
    }
}
