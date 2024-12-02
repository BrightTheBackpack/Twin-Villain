using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class SpriteTextureChanger : MonoBehaviour
{
    // Start is called before the first frame update
    public int moves = 0;
    public GameObject deathscreen;
    public TMP_Text maxMoves;
    public GameObject menu;
    public GameObject tutorial;
    public GameObject Text1;
    public GameObject Text2;
    public GameObject Text3;
    public GameObject teleporter;
    public GameObject main;
    public GameObject hole;
    public bool dead = false;
    public bool iswin = false;

    public GameObject enemy; 
    public GameObject ant;
    private LevelManager script;
    public Sprite[] framess;
    public SpriteRenderer spriteRenderer;
    public int framesPerSecond;

    void Start()
    {
        script.currentLevel = PlayerPrefs.GetInt("level", 0);
        Debug.Log(script.currentLevel);
        //  GetComponent<Renderer>().material.mainTexture = frames[0];

    }
    public void Death(){

        

        deathscreen.SetActive(true);
        dead = true;


    }
    public void Restart(){
        dead = false;
        moves = 0;
                        var allthestuff = FindObjectsOfType<GameObject>();
                Debug.Log(allthestuff.Length);
                var filtered = allthestuff.Where(obj => obj.name.Contains("Wall") || obj.name.Contains("Tile")).ToArray();
                Debug.Log(filtered.Length);
                foreach (var obj in filtered)
                {
                    Destroy(obj);
                }

                main.GetComponent<genBackground>().generateGrid(script.Levels[script.currentLevel]);

    }
    // Update is called once per frame
    void Update()
    {


        var index = (int)(Time.time * framesPerSecond) % framess.Length;
        spriteRenderer.sprite = framess[index];

        // Update the sprite
        // spriteRenderer.sprite = Sprite.Create(currentFrame, rect, pivot);
        if(Input.GetKeyDown(KeyCode.R)){
            moves = 0;
            
            Debug.Log("Restart");
            dead=false;
            script = ant.GetComponent<LevelManager>();
            var level = script.currentLevel;
            var allthestuff = FindObjectsOfType<GameObject>();
            Debug.Log(allthestuff.Length);
            var filtered = allthestuff.Where(obj => obj.name.Contains("Wall") || obj.name.Contains("Tile")).ToArray();
            Debug.Log(filtered.Length);
            foreach (var obj in filtered)
            {
                Destroy(obj);
            }

            main.GetComponent<genBackground>().generateGrid(script.Levels[script.currentLevel]);
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            tutorial.SetActive(true);
        }
        if(Input.GetKeyDown(KeyCode.W)){
            Collision((int)transform.position.x, (int)transform.position.y + 1);
        }
        if(Input.GetKeyDown(KeyCode.A)){
            Collision((int)transform.position.x - 1, (int)transform.position.y);
        }

        if(Input.GetKeyDown(KeyCode.S)){
            Collision((int)transform.position.x, (int)transform.position.y - 1);
        }

        if(Input.GetKeyDown(KeyCode.D)){
            Collision((int)transform.position.x + 1, (int)transform.position.y);
        }

        // spriteRenderer.sprite.texture = frames[index] ;



        
    }
    void Collision(int x, int y){
         script = ant.GetComponent<LevelManager>();
        var level = script.currentLevel;

        if(level == 8){
            moves += 1;
            if(moves > 44){
                Death();
            }
        }
        Debug.Log($"{x}, {y}");

        Text2.SetActive(false);
        Text3.SetActive(false);

        Text1.SetActive(false);
        if(tutorial.activeSelf){
            return;
        }
        if(dead){
            return;
        }
        if(menu.activeSelf){
            return;
        }

        var teleporterPos = teleporter.transform.position;
        var enemyPos = enemy.transform.position;
        var holePos = hole.transform.position;
        if(script.Levels[level][x][y] != 1){
            transform.position = new Vector3(x, y, transform.position.z);
        }
        Vector3 objectBPosition = enemy.transform.position;
        if(objectBPosition.x == x && objectBPosition.y == y){
            //death;
            Debug.Log("Death"); 
            Death();
        }
        if(holePos.x == x && holePos.y == y){
            Debug.Log("Death");
            Death();
        }   
        if(teleporterPos.x == x && teleporterPos.y == y){
            if(enemyPos.x == holePos.x && enemyPos.y == holePos.y){
                Debug.Log(enemy.GetComponent<BADSpriteTextureChanger1>().iswin);
                if(enemy.GetComponent<BADSpriteTextureChanger1>().iswin){
                    //win
                    if(script.currentLevel == 0){
                         
                        Text2.SetActive(true);
                         
                        Text1.SetActive(false);
                        }
                //win
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

                main.GetComponent<genBackground>().generateGrid(script.Levels[script.currentLevel]);
                    }else{
                        iswin = true;
                    }
            }else{
                Debug.Log("Death");
                Death();
            }
            
        }
        

    }
}
