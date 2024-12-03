using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private DialogueUI dialogueUI;
    protected Player playerUnit;
    public DialogueResponseEvents DRE;
    void Start()
    {
        //DRE = GetComponents<DialogueResponseEvents>();
    }

    public void UpdateDialogueObject(DialogueObject dialogueObject)
    {
        this.dialogueObject = dialogueObject;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out OverworldPlayer player))
        {
            player.Interactable = this;
        }
        if (other.CompareTag("Player") && other.TryGetComponent(out Player player1))
        {
            playerUnit = player1;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out OverworldPlayer player))
        {
            if (player.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                player.Interactable = null;
            }
        }
    }
    void Update()
    {
        foreach (DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())
        {
            if (responseEvents.DialogueObject == DialogueUI.currentObject)
            {
                dialogueUI.AddResponseEvents(responseEvents.Events);
                break;
            }
        }
    }
    public void Interact(OverworldPlayer player)
    {
        foreach (DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())
        {
            if (responseEvents.DialogueObject == dialogueObject)
            {
                player.DialogueUI.AddResponseEvents(responseEvents.Events);
                break;
            }
        }

        player.DialogueUI.ShowDialogue(dialogueObject);
    }


    
}
