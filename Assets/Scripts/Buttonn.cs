using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttonn : MonoBehaviour
{
    public void LoadScene(int sceneid)
    {
        SceneManager.LoadScene(sceneid);
    }

    public void ResetHealth(){
        Heartsystem.health = 3;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
