using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public GameObject game;
    
    [SerializeField] private ToursInfos tourData;

    // Start is called before the first frame update
    void Start()
    {
        
        game = GameObject.Find ("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LooseHealth (int healthToLose)
    {
        this.GetComponent<Monstres>().life -= healthToLose;

        Debug.Log(this.GetComponent<Monstres>().life);
        if (this.GetComponent<Monstres>().life <= 0)
        {

            game.GetComponent<Game>().nbEnnemisVivants -= 1;
            game.GetComponent<Game>().score += Mathf.Abs(game.GetComponent<Game>().multi * this.GetComponent<Monstres>().score);
            if (game.GetComponent<Game>().multi > 0)
            {
                game.GetComponent<Game>().multi += 1;
            }
            else 
            {
                game.GetComponent<Game>().multi = 1;
            }

            game.GetComponent<Game>().gold += this.GetComponent<Monstres>().gold;

            Destroy(this.gameObject);
        }
    }
}
