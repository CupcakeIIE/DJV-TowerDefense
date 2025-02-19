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

    private Transform selected;
    private int emplacementIndex = 0; 

    private bool choix = false;

    
    [Header("Addressable Tours Configurations")]
    [Tooltip("List of Addressable keys for tours prefabs.")]
    public List<string> toursKeys;

    private List<GameObject> loadedTours = new List<GameObject>();

    private GameObject tourPrefab;

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
                Instantiate(tourPrefab, selected.position, selected.rotation);
                Debug.Log(tourPrefab.transform.position);
                Debug.Log(selected.position);
            } 
        }
    }


    public void AchatTourEclair ()
    {
        choix = true;
        selected.GetComponent<MeshRenderer>().material = emplacement;
        emplacementIndex = 0;
        selected = Emplacements.emplacements[0];
        selected.GetComponent<MeshRenderer>().material = selection;
        tourPrefab = loadedTours[0];
    }

    public void AchatArtillerieLourde ()
    {
        Debug.Log("Lourde");
    }

    public void AchatStGraal ()
    {
        Debug.Log("Graal");
    }

    public void AchatLowCost ()
    {
        Debug.Log("LowCost");
    }

}
