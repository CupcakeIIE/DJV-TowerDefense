using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;

public class Game : MonoBehaviour
{
    public GameObject panelAchat;
    public GameObject affichage;

    private bool achat = false;

    public int gold = 50;

    public TMP_Text goldText;

    public int nbEnnemisVivants;

    public GameObject spawner;


    // Start is called before the first frame update
    void Start()
    {
        nbEnnemisVivants = spawner.GetComponent<Spawner>().nbMonstresToSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Escape) && achat)
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
        }*/

        goldText.text = "Monnaie : " + gold;

        if (nbEnnemisVivants <= 0)
        {
            Thread.Sleep(1000);
            panelAchat.SetActive(true);
            affichage.SetActive(false);
            achat = true;
        }
    }
}
