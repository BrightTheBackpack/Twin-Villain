using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Add this to use the Button component
using UnityEngine.SceneManagement;
public class StartButton : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject tutorial;
    public GameObject canvas;
        public Button button; // Reference to the Button component
        public GameObject main;
        public GameObject levels;

     void Start()
    {
          if (button != null)
        {
            Debug.Log("Button found!");
            button.onClick.AddListener(OnButtonClick);
        }
        if(PlayerPrefs.GetInt("tutorial", 0) == 1){
            Debug.Log("tutorial");
            // tutorial.SetActive(true);?
            OnButtonClick();
        }
    }

    // Update is called once per frame

    void Update()
    {
        
    }
      void OnButtonClick()
    {   
        Debug.Log("Button clicked!");
        var levelss = levels.GetComponent<LevelManager>().Levels;

        levels.GetComponent<LevelManager>().currentLevel = PlayerPrefs.GetInt("level", 0);

        var level = levels.GetComponent<LevelManager>().currentLevel;
        Debug.Log("Button clicked!");
        
        canvas.SetActive(false);
        Debug.Log("level is " + level);
        if(level ==0 && PlayerPrefs.GetInt("tutorial", 0) == 0){
            Debug.Log("tutorial");
            // tutorial.SetActive(true);?
            SceneManager.UnloadSceneAsync("SampleScene");
            SceneManager.LoadScene("CutScene1");
            return;

        }
        if(level==0){
            tutorial.SetActive(true);
        }

        Debug.Log("generating level " + level);
        main.GetComponent<genBackground>().generateGrid(levelss[level]);
        // Add your code here to perform an action when the button is clicked
    }
}
