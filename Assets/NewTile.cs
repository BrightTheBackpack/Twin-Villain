using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTile : MonoBehaviour
{   
    public Color def;
    public bool offsettrue;
    public Color offset;
    public SpriteRenderer renderer;
    // public void Init(bool isOffset){
    //     renderer.color = isOffset ? def : offset;
    // }
    void Update(){
        var offsets = (transform.position.x % 2 == 0 && transform.position.y % 2 != 0) || (transform.position.x % 2 != 0 && transform.position.y % 2 ==0);
        if(offsets && offsettrue){
            renderer.color = offset;
        }
    }
    // Start is called before the first frame update
 
    // Update is called once per frame
   
}
