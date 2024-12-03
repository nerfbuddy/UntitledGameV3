using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSButtonController : ButtonController

{
    protected override void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            BeatScroller.goScroller = false;
            theSR.sprite = pressedImage;
        }
        if (Input.GetKeyUp(keyToPress))
        {
            theSR.sprite = defaultImage;
        }
    }
}
