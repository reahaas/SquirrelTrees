using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("musicVolume")){
            PlayerPrefs.SetFloat("musicVolume", 1);
            this.loadVolume();
        }
        else{
            this.loadVolume();
        }
    }

    public void changeVolume(){
        AudioListener.volume = volumeSlider.value;
        saveVolume();
    }

    public void loadVolume(){
        this.volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    public void saveVolume(){
        PlayerPrefs.SetFloat("musicVolume", this.volumeSlider.value);
    }
}
