using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField] [TextArea] private string[] dialogue;
    [SerializeField] private Sprite[] icon;
    [SerializeField] private Response[] responses;

    public string[] Dialogue => dialogue;

    public Sprite[] Icon => icon;

    public bool HasResponses => Responses != null && Responses.Length > 0;
    
    public Response[] Responses => responses;
}
