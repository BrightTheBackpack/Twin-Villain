using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTexture : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] frames;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Ensure the frames array is not null and has sprites
        if (frames == null || frames.Length == 0)
        {
            Debug.LogError("Frames array is not initialized or empty.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        var framesPerSecond = 2;

        var index = (int)(Time.time * framesPerSecond) % frames.Length;
        spriteRenderer.sprite = frames[index];
    }


}
