using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadURL : MonoBehaviour
{

    public void link(string url){
        Application.OpenURL(url);
    }
}
