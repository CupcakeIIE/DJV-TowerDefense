using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Monstres : MonoBehaviour
{
    private float speed;

    private Transform target;
    private int waypointIndex = 0;

    public int score;
    public int gold;

    public int life;

    public GameObject game;

    [SerializeField] private MonstresInfos monstresData;

    public GameObject way;

    
    UnityEvent m_MyEvent;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        way = GameObject.Find ("Waypoint");
        audioSource = way.GetComponent<AudioSource>();
        
        if (m_MyEvent == null)
            m_MyEvent = new UnityEvent();

        m_MyEvent.AddListener(PlayFailSound);

        target = Waypoints.points[0];
        game = GameObject.Find ("GameManager");
        
        life = monstresData.health;
        speed = monstresData.speed;
        gold = monstresData.gold;
        score = monstresData.score;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) <= 0.2)
        {
            GetNextWaypoint();
        }
    }

    private void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.points.Length - 1)
        {
            
            m_MyEvent.Invoke();
            game.GetComponent<Game>().nbEnnemisVivants -= 1;
            game.GetComponent<Game>().score -= Mathf.Abs(game.GetComponent<Game>().multi * 5);
            if (game.GetComponent<Game>().multi > 0)
            {
                game.GetComponent<Game>().multi = -1;
            }
            else 
            {
                game.GetComponent<Game>().multi -= 1;
            }
            Destroy(gameObject);      
            return;  
        }

        waypointIndex++;
        target = Waypoints.points[waypointIndex];
    }



    
    void OnCollisionEnter (Collision collision)
    {
        Debug.Log("touche");
        if (collision.gameObject.tag == "bullet")
        {
            Destroy(this.transform);
            Destroy(collision.transform.gameObject);
        }
    }

    public void PlayFailSound ()
    {
        audioSource.Play();
    }
}
