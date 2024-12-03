using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/SceneData")]
public class SceneData : ScriptableObject
{
     //public string[] ;
    [SerializeField] public List<string> sceneObjects = new List<string>();
    //public string[] SceneObjects => sceneObjects;
}