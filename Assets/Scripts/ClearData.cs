using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearData : MonoBehaviour
{
    [SerializeField] PlayerData PlayerData;
    [SerializeField] SceneData OverworldData;
    // Start is called before the first frame update
    void Start()
    {
        OverworldData.sceneObjects = null;
        PlayerData.OWPos = new Vector3(0f, 0f, -1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
