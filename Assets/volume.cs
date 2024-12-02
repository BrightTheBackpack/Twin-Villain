using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class volume : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource m_audio;
    public Slider volumeSlider;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changeVolume(){
        m_audio.volume = volumeSlider.value;
    }
}
