using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Sound : MonoBehaviour
{

    
    UnityEvent m_MyEvent;

    public AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playVictory ()
    {
        audioSource.Play();
    }
}
