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
    public int score = 0;
    public int multi = 1;

    public TMP_Text goldText;
    public TMP_Text scoreText;
    public TMP_Text multiText;

    public int nbEnnemisVivants;

    public GameObject spawner;


    // Start is called before the first frame update
    void Start()
    {
        nbEnnemisVivants = 0;
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
        scoreText.text = "Score : " + score;
        multiText.text = "Multiplicateur : " + multi;

        if (nbEnnemisVivants <= 0 && !achat)
        {
            Thread.Sleep(1000);
            panelAchat.SetActive(true);
            affichage.SetActive(false);
            achat = true;
        }
    }


    public void ButtonNextVague()
    {
        panelAchat.SetActive(false);
        affichage.SetActive(true);
        panelAchat.GetComponent<AchatTours>().selected.GetComponent<MeshRenderer>().material = panelAchat.GetComponent<AchatTours>().emplacement; 
        nbEnnemisVivants = spawner.GetComponent<Spawner>().nbMonstresToSpawn;
        spawner.GetComponent<Spawner>().nbMonstresSpawn = spawner.GetComponent<Spawner>().nbMonstresToSpawn;
        achat = false;
        //StartCoroutine(spawner.GetComponent<Spawner>().SpawnMonstres());
    }
}
