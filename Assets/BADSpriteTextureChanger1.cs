using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BADSpriteTextureChanger1 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject portal;
    public GameObject player;
    public GameObject hole;
    public GameObject ant;
    private LevelManager script;
    public Sprite[] framess;
    
    public int framesPerSecond;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        //  GetComponent<Renderer>().material.mainTexture = frames[0];

    }

    // Update is called once per frame
    void Update()
    {

        // int index = (int)(Time.time * framesPerSecond) % frames.Length;
        // Texture2D currentFrame = frames[index];
        // Rect rect = new Rect(0, 0, currentFrame.width, currentFrame.height);
        // Vector2 pivot = new Vector2(0.5f, 0.5f);

        // Update the sprite
         var index = (int)(Time.time * framesPerSecond) % framess.Length;
        spriteRenderer.sprite = framess[index];
        // spriteRenderer.sprite = Sprite.Create(currentFrame, rect, pivot);
        if(Input.GetKeyDown(KeyCode.W)){
            Collision((int)transform.position.x, (int)transform.position.y + 1);
        }
        if(Input.GetKeyDown(KeyCode.A)){
            Collision((int)transform.position.x + 1, (int)transform.position.y);
        }

        if(Input.GetKeyDown(KeyCode.S)){
            Collision((int)transform.position.x, (int)transform.position.y - 1);
        }

        if(Input.GetKeyDown(KeyCode.D)){
            Collision((int)transform.position.x - 1, (int)transform.position.y);
        }

        // spriteRenderer.sprite.texture = frames[index] ;



        
    }
     void Collision(int x, int y){
        if(player.GetComponent<SpriteTextureChanger>().dead){
            return;
        }
        script = ant.GetComponent<LevelManager>();
        var level = script.currentLevel;
        var holePos = hole.transform.position;
        if(portal.transform.position.x == x && portal.transform.position.y == y){
            player.GetComponent<SpriteTextureChanger>().Death();

        }
        if(script.Levels[level][x][y] != 1){
            transform.position = new Vector3(x, y, transform.position.z);
        }
        if(holePos.x == x && holePos.y == y){
            hole.transform.position = new Vector3(1000000, 1000000, transform.position.z);
            transform.position = new Vector3(1000000, 1000000, transform.position.z);
        }
    }
}
