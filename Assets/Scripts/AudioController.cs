using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
        public Sprite audioOn;
        public Sprite audioOff;
        public GameObject buttonAudio;

        public AudioClip clip;
        public AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        string spriteName = PlayerPrefs.GetString("ButtonSprite", "audioOn");
        if (spriteName == "audioOn")
        buttonAudio.GetComponent<Image>().sprite = audioOn;
        else if (spriteName == "audioOff")
        buttonAudio.GetComponent<Image>().sprite = audioOff;
    }

    public void OnOffAudio(){ 
        if(AudioListener.volume == 1){
        AudioListener.volume = 0;
        buttonAudio.GetComponent<Image>().sprite = audioOff;
        PlayerPrefs.SetString("ButtonSprite", "audioOff");
        }

        else{
        AudioListener.volume = 1;
        buttonAudio.GetComponent<Image>().sprite = audioOn;
        PlayerPrefs.SetString("ButtonSprite", "audioOn");
        }
    }

}
