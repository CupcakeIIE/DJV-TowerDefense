using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class AchatTours : MonoBehaviour
{
    public Material emplacement;
    public Material selection;

    public Transform selected;
    private int emplacementIndex = 0; 

    private bool choix = false;

    
    [Header("Addressable Tours Configurations")]
    [Tooltip("List of Addressable keys for tours prefabs.")]
    public List<string> toursKeys;

    private List<GameObject> loadedTours = new List<GameObject>();

    private GameObject tourPrefab;


    public GameObject mainGame;

    // Start is called before the first frame update
    void Start()
    {
        selected = Emplacements.emplacements[0];

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
        if (choix)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                selected.GetComponent<MeshRenderer>().material = emplacement;
                emplacementIndex += 1;
                if (emplacementIndex >= Emplacements.emplacements.Length)
                {
                    emplacementIndex = 0;
                }
                selected = Emplacements.emplacements[emplacementIndex];
                selected.GetComponent<MeshRenderer>().material = selection;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                selected.GetComponent<MeshRenderer>().material = emplacement;
                emplacementIndex -= 1;
                if (emplacementIndex < 0)
                {
                    emplacementIndex = Emplacements.emplacements.Length - 1;
                }
                selected = Emplacements.emplacements[emplacementIndex];
                selected.GetComponent<MeshRenderer>().material = selection;
            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                choix = false;
                selected.GetComponent<MeshRenderer>().material = emplacement;
                Instantiate(tourPrefab, selected.position, selected.rotation);
            } 

            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                choix = false;
                selected.GetComponent<MeshRenderer>().material = emplacement;
                Debug.Log("stop");
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
            mainGame.GetComponent<Game>().gold -= 50;
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
            mainGame.GetComponent<Game>().gold -= 50;
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
            mainGame.GetComponent<Game>().gold -= 100;
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
            mainGame.GetComponent<Game>().gold -= 10;
        }
        else 
        {
            choix = false;
            selected.GetComponent<MeshRenderer>().material = emplacement;
        }
    }

}
