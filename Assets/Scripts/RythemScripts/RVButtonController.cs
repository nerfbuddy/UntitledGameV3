using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RVButtonController : ButtonController
{
    bool wDown = false;
    bool sDown = false;


    // Update is called once per frame
    protected override void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed && up)
            {
                up = false;
            }
            if (!canBePressed && up)
            {
                BattleSystem.points -= 1;
                up = false;
            }
            theSR.sprite = pressedImage;
            up = false;
        }
        if (Input.GetKeyUp(keyToPress))
        {
            theSR.sprite = defaultImage;
            up = true;
        }
        if(Input.GetKeyDown(KeyCode.W) && !wDown && transform.position.y < -1f)
        {
            transform.position = transform.position + new Vector3(0f, 1f, 0f);
        }
        if (Input.GetKeyDown(KeyCode.S) && !sDown && transform.position.y > -4f)
        {
            transform.position = transform.position + new Vector3(0f, -1f, 0f);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            wDown = false;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            sDown = false;
        }
    }
}
