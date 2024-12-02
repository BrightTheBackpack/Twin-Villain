using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scenes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeScenes(){
        Debug.Log("Button clicked!");
        SceneManager.UnloadSceneAsync("CutScene1");
        SceneManager.LoadScene("SampleScene");
        PlayerPrefs.SetInt("tutorial", 1);
    }
}
