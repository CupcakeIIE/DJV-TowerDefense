using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public GameObject game;

    // Start is called before the first frame update
    void Start()
    {
        
        game = GameObject.Find ("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LooseHealth ()
    {
        this.GetComponent<Monstres>().life -= 10;

        if (this.GetComponent<Monstres>().life <= 0)
        {

            game.GetComponent<Game>().nbEnnemisVivants -= 1;
            game.GetComponent<Game>().score += Mathf.Abs(game.GetComponent<Game>().multi * 5);
            if (game.GetComponent<Game>().multi > 0)
            {
                game.GetComponent<Game>().multi += 1;
            }
            else 
            {
                game.GetComponent<Game>().multi = 1;
            }

            Destroy(this.gameObject);
        }
    }
}
