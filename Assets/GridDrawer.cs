using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 
public class GridDrawer : MonoBehaviour
{   
   public int row = 10; // Number of rows
public int col = 10; // Number of columns
public float cellSize = 0.5f; // Size of each cell
public GameObject empty;
public GameObject wall;
public int startZ =0;
public GameObject grass;
private Vector3 lastGridPos;
public GameObject good;
public GameObject bad;
public GameObject portal;
public int sprite = 0;
public int test = 1;
public bool draw = false;
public List<List<int>> grid = new List<List<int>>();
public Color gridColor = Color.gray; // Grid color
public void ActivateDraw(bool activate){
    draw = activate;
}

public void ChangeSprite(int key){
    sprite = key;
}
public void ChangeDim(int key){
    if(key == 1){
      
        col++;
    }
    if(key == 2){
        if(col == 1)
            return;
    
        col--;
    }
    if(key == 3){
        row++;
    }
    if(key == 4){
        if(row == 1)
            return;
        row--;
    }
     var allthestuff = FindObjectsOfType<GameObject>();
    var filtered = allthestuff.Where(obj => obj.name.Contains("GridLine") || obj.name.Contains("Grass") || obj.name.Contains("Wall")).ToArray();
    foreach (var obj in filtered)
    {
        Destroy(obj);
    }

    DrawGrid();

}

public void DrawGrid()
{
    cellSize = Mathf.Min(6.0f/(float)col, 6.0f/(float)row);
    // Calculate the offsets to center the grid
    float rowOffset = row * cellSize * 0.5f;
    float colOffset = col * cellSize * 0.5f;
    float gridOffset = cellSize * 0.5f;
    // Draw horizontal lines
    for (int i = 0; i <= row; i++)
    {
        float y = i * cellSize - rowOffset; // Center y-coordinate
        CreateLine(
            new Vector3(-colOffset, y, -5), // Start
            new Vector3(colOffset, y, -5) // End
        );
        if(i == 0)
            continue;
    //    var emptytile =  Instantiate(empty, new Vector3((-colOffset)+gridOffset, y-gridOffset, 0), Quaternion.identity);
    //    emptytile.transform.localScale = new Vector3((float)cellSize, cellSize, 0.5f);
    }
    
    // Draw vertical lines
    for (int i = 0; i <= col; i++)
    {
        float x = i * cellSize - colOffset; // Center x-coordinate
        CreateLine(
            new Vector3(x, -rowOffset, -5), // Start
            new Vector3(x, rowOffset,-5) // End
        );
    }
    for(int i = 0; i <= col; i++){
        for(int j = 0; j <= row; j++){
            if(i == col || j == 0)
                continue;
            var x = (i * cellSize - colOffset) + gridOffset;
            var y = (j * cellSize - rowOffset) - gridOffset;
            var emptytile =  Instantiate(wall, new Vector3(x, y,0), Quaternion.identity);
            emptytile.transform.localScale = new Vector3((float)cellSize, cellSize, 0.5f);
            emptytile.name = $"Grid_{x}_{y}";
        }
    }
}

    private void CreateLine(Vector3 start, Vector3 end){
        GameObject line = new GameObject("GridLine");
        // line.transform.SetParent(transform);
        // line.transform.localPosition = Vector3.zero;
        // line.transform.localScale = Vector3.one;
        line.AddComponent<LineRenderer>();
        LineRenderer lr = line.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = gridColor;
        lr.endColor = gridColor;
        lr.startWidth = 0.05f;
        lr.endWidth = 0.05f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }
    // Start is called before the first frame update
    void Start()
    {
        // DrawGrid();
            lastGridPos = Vector3.zero; // Initialize lastGridPos
        InitializeGrid(); // Initialize the grid state


    }
      void InitializeGrid()
    {
        grid = new List<List<int>>();
        for (int i = 0; i < row; i++)
        {
            List<int> rowList = new List<int>();
            for (int j = 0; j < col; j++)
            {
                rowList.Add(0); // Initialize all cells to 0
            }
            grid.Add(rowList);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(draw && Input.GetMouseButton(0)){
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // Ensure z coordinate is zero
        // mousePos.x = mousePos.x  + (0.5f*cellSize);
        // mousePos.y = mousePos.y - (0.5f*cellSize);

        float gridX = Mathf.FloorToInt((mousePos.x + cellSize / 2) / cellSize) * cellSize  ;
        if(col % 2 == 0){
            gridX = Mathf.FloorToInt((mousePos.x + cellSize / 2) / cellSize) * cellSize + cellSize/2;
        }
        float gridY = Mathf.FloorToInt((mousePos.y + cellSize / 2) / cellSize) * cellSize ;
        if(row % 2 == 0){
            gridY = Mathf.FloorToInt((mousePos.y + cellSize / 2) / cellSize) * cellSize + cellSize/2;
        }
        Vector3 currentGridPos = new Vector3(gridX, gridY, 0);
        float halfGridWidth = (col % 2 == 0) ? cellSize * col / 2 : cellSize * (col - 1) / 2;
        float halfGridHeight = (row % 2 == 0) ? cellSize * row / 2 : cellSize * (row - 1) / 2;
        string gridPosName = $"Grid_{gridX}_{gridY}";
            int gridXIndex = Mathf.FloorToInt((gridX + halfGridWidth) / cellSize);
            int gridYIndex = Mathf.FloorToInt((gridY + halfGridHeight) / cellSize);
        Debug.Log(gridXIndex+" "+gridYIndex);
        if(currentGridPos != lastGridPos && (gridX >= -halfGridWidth && gridX <= halfGridWidth) && (gridY >= -halfGridHeight && gridY <= halfGridHeight)){
        GameObject existingObject = GameObject.Find(gridPosName);
        if(existingObject != null){
            if(sprite == 0 || sprite == 1)
                Destroy(existingObject);
            }
        if(sprite == 0){
            var emptytile = Instantiate(wall, new Vector3(gridX , gridY, 0), Quaternion.identity);
            emptytile.name = $"Grid_{gridX}_{gridY}";

            emptytile.transform.localScale = new Vector3((float)cellSize, cellSize, 0.5f);
        }
        if(sprite == 1){
            var emptytile = Instantiate(grass, new Vector3(gridX , gridY, 0), Quaternion.identity);
            emptytile.name = $"Grid_{gridX}_{gridY}";

            emptytile.transform.localScale = new Vector3((float)cellSize, cellSize, 0.5f);
        }
        if(sprite == 2){
            portal.transform.localScale = new Vector3((float)cellSize, cellSize, 0.5f);
            portal.transform.position = new Vector3(gridX , gridY, 0);
           
        }
        if(sprite == 3){
            empty.transform.localScale = new Vector3((float)cellSize, cellSize, 0.5f);
            empty.transform.position = new Vector3(gridX , gridY, -0.1f);
            empty.GetComponent<SpriteRenderer>().color  = Color.black;

        }
        if(sprite == 4){
            good.transform.localScale = new Vector3((float)cellSize, cellSize, 0.5f);
            good.transform.position = new Vector3(gridX , gridY, 0);
        }
        if(sprite == 5){
            bad.transform.localScale = new Vector3((float)cellSize, cellSize, -0.5f);
            bad.transform.position = new Vector3(gridX , gridY, 0);
        }
        grid[gridXIndex][gridYIndex] = sprite;
        startZ = startZ - 1;
        Debug.Log(startZ);
                Debug.Log(SaveGridState());
                // LoadGridState(SaveGridState());

        lastGridPos = currentGridPos;
        }
       
        }

        
    }
    public string SaveGridState()
    {
        string gridState = $"{row} {col} ";
        foreach (var rowList in grid)
        {
            foreach (var cell in rowList)
            {
                gridState += cell.ToString();
            }
        }
        return gridState;
    }
     public void LoadGridState(string gridState)
    {
        string[] parts = gridState.Split(' ');
        int rows = int.Parse(parts[0]);
        int cols = int.Parse(parts[1]);
        string gridData = parts[2];

        InitializeGrid(); // Reinitialize the grid

        int index = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                int spriteType = int.Parse(gridData[index].ToString());
                grid[i][j] = spriteType;
                index++;

                // Instantiate the corresponding object
                if (spriteType != 0)
                {
                    float gridX = (j - cols / 2) * cellSize;
                    float gridY = (i - rows / 2) * cellSize;
                    // float colOffset = cols.length * cellSize * 0.5f;
                    // float rowOffset = rows.length * cellSize * 0.5f;
                    // gridX = (i * cellSize - colOffset) + gridOffset;
                    // gridY = (j * cellSize - rowOffset) - gridOffset;
                    Vector3 position = new Vector3(gridX, gridY, 0);
                    GameObject newObject = null;

                    if (spriteType == 1)
                    {
                        newObject = Instantiate(wall, position, Quaternion.identity);
                    }
                    if (spriteType == 2)
                    {
                        newObject = Instantiate(grass, position, Quaternion.identity);
                    }
                    if (spriteType == 3)
                    {
                        newObject = Instantiate(portal, position, Quaternion.identity);
                    }
                    if (spriteType == 4)
                    {
                        newObject = Instantiate(empty, position, Quaternion.identity);
                        newObject.GetComponent<SpriteRenderer>().color = Color.black;
                    }
                    if (spriteType == 5)
                    {
                        newObject = Instantiate(good, position, Quaternion.identity);
                    }
                    if (spriteType == 6)
                    {
                        newObject = Instantiate(bad, position, Quaternion.identity);
                    }

                    if (newObject != null)
                    {
                        newObject.name = $"Grid_{gridX}_{gridY}";
                        newObject.transform.localScale = new Vector3(cellSize, cellSize, 0.5f);
                    }
                }
            }
        }
    }

}
