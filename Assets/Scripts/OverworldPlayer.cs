using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class OverworldPlayer : MonoBehaviour
{

    [SerializeField] private DialogueObject dialogueObject;
    GameObject CVS;
    [SerializeField] private DialogueUI dialogueUI;
    public static bool playerControl = true;
    public float speed = 3.0f;
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    Animator animator;
    public static Vector2 lookDirection;
    public DialogueUI DialogueUI => dialogueUI;
    public IInteractable Interactable { get; set; }
    public PlayerData playerdata;
    GameObject batSys;
    BattleSystem BS;


    void Start()
    {
        CVS = GameObject.Find("Canvas");

        dialogueUI = CVS.GetComponent<DialogueUI>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (playerdata.OWPos != new Vector3(12345f, 0f, 0f))
           transform.position = playerdata.OWPos;
    }

    void Update()
    {
        
        if (DialogueUI.IsOpen)
        {
            playerControl = false;
            foreach (DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())
            {
                if (responseEvents.DialogueObject == DialogueUI.currentObject)
                {
                    dialogueUI.AddResponseEvents(responseEvents.Events);
                    break;
                }
            }
        }
        else
        {
            playerControl = true;
        }
        if (playerControl == true)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Interactable.Interact(this);
            }
        }
        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (playerControl == false)
        {
            vertical = 0f;
            horizontal = 0f;
        }
    }
    void FixedUpdate()
    {
        if (playerControl)
        {
            Vector2 position = rigidbody2d.position;
            position.x = position.x + speed * horizontal * Time.deltaTime;
            position.y = position.y + speed * vertical * Time.deltaTime;

            rigidbody2d.MovePosition(position);
        }
    }
}
