using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToursTirs : MonoBehaviour
{
    public GameObject munitionPrefab;
    public GameObject posDepart;
    public int frequenceTir = 10;

    private int increment = 0;

    private float range = 25f;

    private Transform target;

    public int degats_supp = 0;
    
    [SerializeField] private ToursInfos tourData;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    private void UpdateTarget ()
    {
        GameObject[] monstres = GameObject.FindGameObjectsWithTag("Monstre");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestMonstre = null;

        foreach (GameObject monstre in monstres)
        {
            float distanceToMonstre = Vector3.Distance(transform.position, monstre.transform.position);
            if (distanceToMonstre < shortestDistance)
            {
                shortestDistance = distanceToMonstre;
                nearestMonstre = monstre;
            }
        }

        if (nearestMonstre != null && shortestDistance <= range)
        {
            target = nearestMonstre.transform;
        }
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        



        if (target != null)
        {
            transform.LookAt(target);
            
            if (increment == tourData.frequence)
            {
                int nb_proj = 0;
                while (nb_proj < tourData.projectiles)
                {
                    GameObject munition = GameObject.Instantiate(munitionPrefab, posDepart.transform.position, posDepart.transform.rotation);
                    munition.GetComponent<Munition>().degats = tourData.degats + degats_supp;
                    munition.GetComponent<Munition>().speedMunition = tourData.vitesse;
                    nb_proj += 1;
                }

                increment = 0;
            }
            else
            {
                increment += 1;
            }
        }
    }


    private void OnDrawGizmosSelected ()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
