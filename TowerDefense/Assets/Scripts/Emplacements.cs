using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emplacements : MonoBehaviour
{
    public static Transform[] emplacements;
    public static bool[] emplacements_occ;
    public static int nb_emplacements_occ = 0;

    // Start is called before the first frame update
    void Awake()
    {
        emplacements = new Transform[transform.childCount];
        emplacements_occ = new bool [emplacements.Length];

        for (int i = 0; i < emplacements.Length; i++)
        {
            emplacements[i] = transform.GetChild(i);
            emplacements_occ[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
