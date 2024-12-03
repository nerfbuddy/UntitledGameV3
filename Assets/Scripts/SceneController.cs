using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    //List<string> SceneList = new List<string>();
    [SerializeField] SceneData sceneData;
    
    void Start()
    {
        if (GameManager.KIB != null)
        {
            sceneData.sceneObjects.Add(GameManager.KIB);
        }
        /*
        if (sceneData.sceneObjects.Count > 0)
        {
            foreach (string objects in sceneData.sceneObjects)
            {
                if (objects != GameManager.KIB && GameManager.KIB != null)
                    sceneData.sceneObjects.Add(GameManager.KIB);
            }
        }
        else
        {
            if (GameManager.KIB != null)
            {
                sceneData.sceneObjects.Add(GameManager.KIB);
            }
               
        }
            
    */
        foreach (string objects in sceneData.sceneObjects)
        {
            GameObject.Find(objects).SetActive(false);
        }
    }

}
