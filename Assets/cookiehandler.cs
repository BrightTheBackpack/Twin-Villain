using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class cookiehandler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject LevelManager;
    public GameObject cookie;
    public TMP_Text cookieText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(cookie.activeSelf == false){return;}
        cookieText.text  = LevelManager.GetComponent<LevelManager>().currentLevel + "/9 Recipe Fragments";

    }
}
