using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    protected SpriteRenderer theSR;
    public Sprite defaultImage;
    public Sprite pressedImage;
    public bool canBePressed;
    protected bool up = true;
    public int ghostPenalty = 0;

    public KeyCode keyToPress;

    protected void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
    }

    virtual protected void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if(canBePressed && up)
            {
                up = false;
            }
            else if(!canBePressed && up)
            {
                BattleSystem.points -= ghostPenalty;
                up = false;
            }
            theSR.sprite = pressedImage;
            up = false;
        }
        if(Input.GetKeyUp(keyToPress))
        {
            theSR.sprite = defaultImage;
            up = true;
        }
        
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Note")
        {
            canBePressed = true;
        }
    }
    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Note")
        {
            canBePressed = false;
        }
    }
}
