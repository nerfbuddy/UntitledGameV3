using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    public KeyCode keyToPress;
    float randf = 0;
    [SerializeField] int pointsAdded = 1;

    // Start is called before the first frame update
    void Start()
    {
        randf = Random.Range(-2.5f, 2.5f);
        transform.position = transform.position + new Vector3(randf, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(keyToPress))
        {
            if(canBePressed)
            {
                BattleSystem.points += pointsAdded;
                gameObject.SetActive(false);

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Activator")
        {
            canBePressed = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = false;
        }
    }
}
