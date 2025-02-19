using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emplacements : MonoBehaviour
{
    public static Transform[] emplacements;

    // Start is called before the first frame update
    void Awake()
    {
        emplacements = new Transform[transform.childCount];
        for (int i = 0; i < emplacements.Length; i++)
        {
            emplacements[i] = transform.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
