using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Add this to use the Button component

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
            button.onClick.AddListener(OnButtonClick);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
      void OnButtonClick()
    {
        Debug.Log("Button clicked!");
        canvas.SetActive(false);
        tutorial.SetActive(true);
        main.GetComponent<genBackground>().generateGrid(levels.GetComponent<LevelManager>().Levels[0]);
        // Add your code here to perform an action when the button is clicked
    }
}
