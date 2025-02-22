using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToursTirs : MonoBehaviour
{
    public GameObject munitionPrefab;
    public GameObject posDepart;
    public int frequenceTir = 10;
    public int speedMunition = 8;

    private int increment = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (increment == 500)
        {
            GameObject.Instantiate(munitionPrefab, posDepart.transform.position, posDepart.transform.rotation);
            increment = 0;
        }
        else
        {
            increment += 1;
        }
        
    }
}
