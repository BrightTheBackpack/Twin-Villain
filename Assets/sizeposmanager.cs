using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sizeposmanager : MonoBehaviour
{
    // Start is called before the first frame update
    private int lastScreenWidth;
    private int lastScreenHeight;

    void Start()
    {
          lastScreenWidth = Screen.width;
        lastScreenHeight = Screen.height;
        UpdateUI();
   
    }
      void UpdateUI()
    {
        int width = Screen.width;
        float height = Screen.height;
        float scale = 1024 / height;
        float posY = -height / 2f;

        RectTransform rectTransform = GetComponent<RectTransform>();

        // Output the resolution to the console
        Debug.Log("Screen Resolution: " + width + "x" + height);
        Debug.Log("Calculated Scale: (" + scale + ", " + scale + ", 0)");

        rectTransform.localScale = new Vector3(scale, scale, 1);
        Debug.Log("RectTransform Scale set to: " + rectTransform.localScale);
    }

    // Update is called once per frame
    void Update()
    {
        if (Screen.width != lastScreenWidth || Screen.height != lastScreenHeight)
        {
            lastScreenWidth = Screen.width;
            lastScreenHeight = Screen.height;
            UpdateUI();
        }
    }
}
