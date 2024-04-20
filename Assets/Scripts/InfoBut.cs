using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfoBut : MonoBehaviour
{
    [SerializeField]
    GameObject infoButton;
    // Start is called before the first frame update
    void Start()
    {
        infoButton.SetActive(false);
    }

    // Update is called once per frame
    public void InfoStart()
    {
        infoButton.SetActive(true);
    }
    public void InfoOff()
    {
        infoButton.SetActive(false);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);   
        Time.timeScale = 1;
    }
    
    public void leaderBtn(){
        Social.ShowLeaderboardUI();
    }
}