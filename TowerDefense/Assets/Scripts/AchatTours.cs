using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using TMPro;

public class AchatTours : MonoBehaviour
{
    public Material emplacement;
    public Material selection;

    public Transform selected;
    public Transform towerSelected;
    public Material towerMat;

    private int emplacementIndex = 0; 

    private bool choix = false;
    private bool amelioration = false;

    public TMP_Text solde;

    
    [Header("Addressable Tours Configurations")]
    [Tooltip("List of Addressable keys for tours prefabs.")]
    public List<string> toursKeys;

    private List<GameObject> loadedTours = new List<GameObject>();

    private GameObject tourPrefab;

    private int tourAchete;

    UnityEvent m_MyEvent;

    public GameObject mainGame;

    public AudioSource audioSource;

    public GameObject button_yes;
    public GameObject button_no;
    public GameObject button_north;
    public GameObject button_south;
    public GameObject button_east;
    public GameObject button_west;
    public GameObject pos_button;
    public GameObject button_ame_yes;

    public bool positioning = false;

    public GameObject tour_choisie;

    // Start is called before the first frame update
    void Start()
    {
        selected = Emplacements.emplacements[0];

        
        if (m_MyEvent == null)
            m_MyEvent = new UnityEvent();

        m_MyEvent.AddListener(PlayGoldSound);

        StartCoroutine(LoadTours());
    }


    private IEnumerator LoadTours()
    {
        foreach (var key in toursKeys)
        {
            AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(key);
            yield return handle;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                loadedTours.Add(handle.Result);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        solde.text = "" + mainGame.GetComponent<Game>().gold + "g";

        if (emplacementIndex == 0 && Emplacements.emplacements_occ[0] && Emplacements.nb_emplacements_occ != Emplacements.emplacements.Length)
        {            
            selected.GetComponent<MeshRenderer>().material = emplacement;

            int j = 0;
            while (Emplacements.emplacements_occ[j] && j < Emplacements.emplacements.Length)
            {
                j+=1;
            }
            if (j != Emplacements.emplacements.Length)
            {
                emplacementIndex = j;         
                selected = Emplacements.emplacements[j];
               // selected.GetComponent<MeshRenderer>().material = selection;
            }
        }
        else if (Emplacements.nb_emplacements_occ == Emplacements.emplacements.Length)
        {
            selected.GetComponent<MeshRenderer>().material = emplacement;
        }

        if (choix && Emplacements.nb_emplacements_occ != Emplacements.emplacements.Length && !positioning)
        {
                
            button_yes.SetActive(true);
            button_no.SetActive(true);


            selected.GetComponent<MeshRenderer>().material = selection;

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                selected.GetComponent<MeshRenderer>().material = emplacement;

                
                int f = emplacementIndex + 1;
                if (f >= Emplacements.emplacements.Length)
                {
                    f = 0;
                }

                int nb_iter = 0;
                while (Emplacements.emplacements_occ[f] && nb_iter < Emplacements.emplacements.Length)
                {
                    
                    if (f >= Emplacements.emplacements.Length)
                    {
                        f = 0;
                    }
                    f += 1;
                    nb_iter += 1;
                }
                
                if (f != Emplacements.emplacements.Length)
                {
                    emplacementIndex = f;
                    selected = Emplacements.emplacements[emplacementIndex];
                    selected.GetComponent<MeshRenderer>().material = selection;
                }
                
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                selected.GetComponent<MeshRenderer>().material = emplacement;

                
                
                int f = emplacementIndex - 1;
                if (f <= 0)
                {
                    f = Emplacements.emplacements.Length-1;
                }

                int nb_iter = 0;
                while (Emplacements.emplacements_occ[f] && nb_iter < Emplacements.emplacements.Length)
                {
                    
                    if (f <= 0)
                    {
                        f = Emplacements.emplacements.Length-1;
                    }
                    f -= 1;
                    nb_iter += 1;
                }
                
                if (f != Emplacements.emplacements.Length)
                {
                    emplacementIndex = f;
                    selected = Emplacements.emplacements[emplacementIndex];
                    selected.GetComponent<MeshRenderer>().material = selection;
                }
            }

           /* if (Input.GetKeyDown(KeyCode.Y))
            {
                choix = false;
                selected.GetComponent<MeshRenderer>().material = emplacement;
                Instantiate(tourPrefab, selected.position, selected.rotation);
                Emplacements.emplacements_occ[emplacementIndex] = true;
                Emplacements.nb_emplacements_occ += 1; 

                if (tourAchete == 3)
                {
                    m_MyEvent.Invoke();
                    mainGame.GetComponent<Game>().gold -= 100;
                }
                else if (tourAchete == 4)
                {
                    m_MyEvent.Invoke();
                    mainGame.GetComponent<Game>().gold -= 15;
                }
                else
                {
                    m_MyEvent.Invoke();
                    mainGame.GetComponent<Game>().gold -= 50;
                }
            } */
        }

        if (amelioration)
        {
            selected.GetComponent<MeshRenderer>().material = emplacement;

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    
                    GameObject clickedObject = hit.collider.gameObject;
                    Transform parentTransform = clickedObject.transform.parent;

                    if (parentTransform.gameObject.name == "Tour Variant(Clone)")
                    {
                        towerMat = hit.collider.GetComponent<MeshRenderer>().material;
                        hit.collider.GetComponent<MeshRenderer>().material = selection;
                        towerSelected = hit.collider.gameObject.transform;

                        button_ame_yes.SetActive(true);
                        button_no.SetActive(true);
                    }
                }
            }
        }

    }


    public void AchatTourEclair ()
    {
        if (mainGame.GetComponent<Game>().gold >= 50)
        {
            choix = true;
            selected.GetComponent<MeshRenderer>().material = emplacement;
            emplacementIndex = 0;
            selected = Emplacements.emplacements[0];
            selected.GetComponent<MeshRenderer>().material = selection;
            tourPrefab = loadedTours[0];
            tourAchete = 1;
        }
        else 
        {
            choix = false;
            selected.GetComponent<MeshRenderer>().material = emplacement;
        }
    }

    public void AchatArtillerieLourde ()
    {
        if (mainGame.GetComponent<Game>().gold >= 50)
        {
            choix = true;
            selected.GetComponent<MeshRenderer>().material = emplacement;
            emplacementIndex = 0;
            selected = Emplacements.emplacements[0];
            selected.GetComponent<MeshRenderer>().material = selection;
            tourPrefab = loadedTours[3];
            tourAchete = 2;
        }
        else 
        {
            choix = false;
            selected.GetComponent<MeshRenderer>().material = emplacement;
        }
    }

    public void AchatStGraal ()
    {
        if (mainGame.GetComponent<Game>().gold >= 100)
        {
            choix = true;
            selected.GetComponent<MeshRenderer>().material = emplacement;
            emplacementIndex = 0;
            selected = Emplacements.emplacements[0];
            selected.GetComponent<MeshRenderer>().material = selection;
            tourPrefab = loadedTours[2];
            tourAchete = 3;
        }
        else 
        {
            choix = false;
            selected.GetComponent<MeshRenderer>().material = emplacement;
        }
    }

    public void AchatLowCost ()
    {
        if (mainGame.GetComponent<Game>().gold >= 10)
        {
            choix = true;
            selected.GetComponent<MeshRenderer>().material = emplacement;
            emplacementIndex = 0;
            selected = Emplacements.emplacements[0];
            selected.GetComponent<MeshRenderer>().material = selection;
            tourPrefab = loadedTours[1];
            tourAchete = 4;
        }
        else 
        {
            choix = false;
            selected.GetComponent<MeshRenderer>().material = emplacement;
        }
    }


    public void Ameliorer ()
    {
        if (mainGame.GetComponent<Game>().gold >= 100)
        {
            amelioration = true;
        }
        else
        {
            amelioration = false;
        }
    }


    public void PlayGoldSound ()
    {
        audioSource.Play();
    }

    public void ButtonYes ()
    {
        choix = false;
        selected.GetComponent<MeshRenderer>().material = emplacement;
        tour_choisie = Instantiate(tourPrefab, selected.position, selected.rotation);
        Emplacements.emplacements_occ[emplacementIndex] = true;
        Emplacements.nb_emplacements_occ += 1; 

        if (tourAchete == 3)
        {
            m_MyEvent.Invoke();
            mainGame.GetComponent<Game>().gold -= 100;
        }
        else if (tourAchete == 4)
        {
            m_MyEvent.Invoke();
            mainGame.GetComponent<Game>().gold -= 15;
        }
        else
        {
            m_MyEvent.Invoke();
            mainGame.GetComponent<Game>().gold -= 50;
        }

        positioning = true;

        button_yes.SetActive(false);
        button_no.SetActive(false);
        button_north.SetActive(true);
        button_south.SetActive(true);
        button_east.SetActive(true);
        button_west.SetActive(true);
        pos_button.SetActive(true);
    }

    public void ButtonNo ()
    {
        choix = false;
        amelioration = false;
        selected.GetComponent<MeshRenderer>().material = emplacement;
        towerSelected.GetComponent<MeshRenderer>().material = towerMat;

        button_yes.SetActive(false);
        button_no.SetActive(false);
        button_ame_yes.SetActive(false);
    }

    public void Button_Ameliorer_Yes ()
    {
        amelioration = false;
        
        m_MyEvent.Invoke();
        mainGame.GetComponent<Game>().gold -= 100;

        towerSelected.GetComponent<MeshRenderer>().material = towerMat;
        GameObject tower = towerSelected.parent.gameObject;
        Transform weap = tower.transform.Find("Weapon");
        weap.GetComponent<ToursTirs>().degats_supp += 10;

        button_no.SetActive(false);
        button_ame_yes.SetActive(false);
    }

    public void RotateToNorth()
    {
        if (tour_choisie != null)
        {
            tour_choisie.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }

    public void RotateToEast()
    {
        if (tour_choisie != null)
        {
            tour_choisie.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public void RotateToSouth()
    {
        if (tour_choisie != null)
        {
            tour_choisie.transform.rotation = Quaternion.Euler(0, 270, 0);
        }
    }

    public void RotateToWest()
    {
        if (tour_choisie != null)
        {
            tour_choisie.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    
    public void Button_Pos_ok ()
    {
        if (positioning)
        {
            button_north.SetActive(false);
            button_south.SetActive(false);
            button_east.SetActive(false);
            button_west.SetActive(false);
            pos_button.SetActive(false);
            positioning = false;
        }
    }

}
