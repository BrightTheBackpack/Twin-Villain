using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BADSpriteTextureChanger1 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject tutorial;
    public GameObject menu;
    public GameObject main;
    public GameObject Text1;
    public GameObject Text2;
    public GameObject Text3;
    public GameObject portal;
    public GameObject player;
    public GameObject hole;
    public bool still = false;
    public bool iswin = false;
    public GameObject ant;
    private LevelManager script;
    public Sprite[] framess;
    public GameObject thanks;
    
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

        Debug.Log($"{x}, {y}");
        if(tutorial.activeSelf){
            return;
        }
         if(menu.activeSelf){
            return;
        }
        if(player.GetComponent<SpriteTextureChanger>().dead){
            return;
        }
                
        script = ant.GetComponent<LevelManager>();
        var level = script.currentLevel;

        if(player.transform.position.x == x && player.transform.position.y == y){
            player.GetComponent<SpriteTextureChanger>().Death();
        }
        if(x == portal.transform.position.x && y == portal.transform.position.y){
                            player.GetComponent<SpriteTextureChanger>().Death();

        }
        script = ant.GetComponent<LevelManager>();
        // var level = script.currentLevel;
        var holePos = hole.transform.position;
        if(portal.transform.position.x == player.transform.position.x && portal.transform.position.y == player.transform.position.y){
            Debug.Log("Teleporting");
            Debug.Log($"{holePos.x}, {holePos.y}, {x}, {y}");
            if(holePos.x == transform.position.x && holePos.y == transform.position.y){
                Debug.Log(player.GetComponent<SpriteTextureChanger>().iswin);
                
                if(player.GetComponent<SpriteTextureChanger>().iswin){
                    still = false;
                                    if(script.currentLevel == 0){
                     
                    Text2.SetActive(true);
                     
                    Text1.SetActive(false);


                }
                if(script.currentLevel ==1){
                    Text3.SetActive(true);
                    Text2.SetActive(false);
                }
                Debug.Log("Win");
                PlayerPrefs.SetInt("level", script.currentLevel + 1);
                PlayerPrefs.Save(); 

                script.currentLevel = script.currentLevel + 1;
                Debug.Log(script.currentLevel);
                var allthestuff = FindObjectsOfType<GameObject>();
                Debug.Log(allthestuff.Length);
                var filtered = allthestuff.Where(obj => obj.name.Contains("Wall") || obj.name.Contains("Tile")).ToArray();
                Debug.Log(filtered.Length);
                foreach (var obj in filtered)
                {
                    Destroy(obj);
                }
                if(script.currentLevel == 9){
                    thanks.SetActive(true);
                    script.currentLevel = 0;
                }
                main.GetComponent<genBackground>().generateGrid(script.Levels[script.currentLevel]);

                }else{
                    still = false;
                    iswin = true;
                }
            }else{
                player.GetComponent<SpriteTextureChanger>().Death();
            }
        }

        if(script.Levels[level][x][y] != 1 && !still){
            Debug.Log("Moving");
            transform.position = new Vector3(x, y, transform.position.z);
        }
        if(holePos.x == x && holePos.y == y){
            still = true;
            hole.transform.position = new Vector3(1000, 1000, transform.position.z);
            transform.position = new Vector3(1000, 1000, transform.position.z);
        }
    }
}
