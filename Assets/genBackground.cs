using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class genBackground : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Text1;
    public GameObject Text2;
    public GameObject Text3;
    public GameObject Hole;
    public GameObject Bad;
    public GameObject Main;
    public GameObject teleporter;
    public int level = 0;
    public GameObject ant;
    private LevelManager script;

    public Transform _cam;
    public int width;
    public int height;
    public GameObject BackgroundTile;
    public GameObject Wall;
    public void generateGrid(List<List<int>> grid){
        Debug.Log(grid[1][8]);

        for(int x =0; x<width; x++){
            for(int y = 0; y<height; y++){
                if(grid[x][y] == 5){
                    Bad.transform.position = new Vector3(x,y,0);
                }
                if(grid[x][y] == 4){
                    Main.transform.position = new Vector3(x,y,0);
                }
                if(grid[x][y] == 3){
                    Hole.transform.position = new Vector3(x,y,0);

                }
                if(grid[x][y] == 2){
                    teleporter.transform.position = new Vector3(x,y,0);
                }
                if(grid[x][y] == 1){
                    var spawned = Instantiate(Wall, new Vector3(x,y,0), Quaternion.identity);
                    spawned.name = $"Wall {x} {y}";
                }else{
                var spawned = Instantiate(BackgroundTile, new Vector3(x,y,0), Quaternion.identity);
                spawned.name = $"Tile {x} {y}";
                var isOffset = (x%2 == 0 && y%2 != 0) || (x%2 != 0 && y%2 ==0);}
                // spawned.Init(isOffset);
            }
        }
        _cam.transform.position = new Vector3((width/2) -0.5f, (height/2)-0.5f,-10);
    }
    void Start()
    {   
        Text1.transform.position = new Vector3(512,512,0);
 
                Text2.SetActive(false);
        Text3.SetActive(false);

        script = ant.GetComponent<LevelManager>();
        level = script.currentLevel;
        Debug.Log("test");
        Debug.Log(script.Levels[0][0] );

        generateGrid(script.Levels[level]);
    }

    // Update is called once per frame
    void Update()
    {
      
        // GameObject Tile = Instantiate(BackgroundTile, transform.position, transform.rotation); 
        // if(Timer < Index){
        //     Timer = Timer + Time.deltaTime;
        // }else{
        //     Timer = 0;
        //     Destroy(Tile);
        // }


    }
}
