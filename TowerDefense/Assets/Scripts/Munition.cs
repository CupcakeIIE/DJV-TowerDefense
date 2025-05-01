using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Munition : MonoBehaviour
{
    
    private int speedMunition = 75;

    public int degats;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speedMunition);
    }
    
    void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.name != "Weapon")
        {
            if (collision.gameObject.tag == "Monstre")
            {
                GameObject monster = collision.gameObject.transform.parent.gameObject;
                monster.gameObject.GetComponent<Damage>().LooseHealth(degats);
                Destroy(this.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
