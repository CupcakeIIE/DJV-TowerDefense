using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game : MonoBehaviour
{
    public GameObject panelAchat;
    public GameObject affichage;

    private bool achat = false;

    public int gold = 50;

    public TMP_Text goldText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && achat)
        {
            panelAchat.SetActive(false);
            affichage.SetActive(true);
            achat = false;
            panelAchat.GetComponent<AchatTours>().selected.GetComponent<MeshRenderer>().material = panelAchat.GetComponent<AchatTours>().emplacement; 
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !achat)
        {
            panelAchat.SetActive(true);
            affichage.SetActive(false);
            achat = true;
        }

        goldText.text = "Monnaie : " + gold;
    }
}
