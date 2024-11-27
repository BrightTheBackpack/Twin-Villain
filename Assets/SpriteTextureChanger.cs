using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class SpriteTextureChanger : MonoBehaviour
{
    // Start is called before the first frame update
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
        //  GetComponent<Renderer>().material.mainTexture = frames[0];

    }
    public void Death(){
        List<List<int>> grid = new List<List<int>>
{
    new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
    new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
    new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
    new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
    new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
    new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
    new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
    new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
    new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
    new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
    new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
    new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
    new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
    new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
    new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
    new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
};
                dead=true;
                var allthestuff = FindObjectsOfType<GameObject>();
                script.currentLevel = 0;
                Debug.Log(allthestuff.Length);
                var filtered = allthestuff.Where(obj => obj.name.Contains("Wall") || obj.name.Contains("Tile")).ToArray();
                Debug.Log(filtered.Length);
                foreach (var obj in filtered)
                {
                    Destroy(obj);
                }

                main.GetComponent<genBackground>().generateGrid(grid);


    }
    // Update is called once per frame
    void Update()
    {


        var index = (int)(Time.time * framesPerSecond) % framess.Length;
        spriteRenderer.sprite = framess[index];

        // Update the sprite
        // spriteRenderer.sprite = Sprite.Create(currentFrame, rect, pivot);
        if(Input.GetKeyDown(KeyCode.R)){
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
        Debug.Log($"{x}, {y}");

        Text2.SetActive(false);
        Text3.SetActive(false);

        Text1.SetActive(false);
        if(dead){
            return;
        }
        script = ant.GetComponent<LevelManager>();
        var level = script.currentLevel;

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
