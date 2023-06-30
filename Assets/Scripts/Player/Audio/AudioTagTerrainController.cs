using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class AudioTagTerrainController : MonoBehaviour {
    public GameObject theTerrain;
    public string thisTag;
    public string defaultTag;


    void Start()
    {
        defaultTag = theTerrain.tag;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            theTerrain.tag = thisTag;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            theTerrain.tag = defaultTag;
        }
    }
}
