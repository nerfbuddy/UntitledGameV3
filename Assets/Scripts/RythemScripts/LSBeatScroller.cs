using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSBeatScroller : BeatScroller
{
    void Start()
    {
        goScroller = true;
        beatTempo = beatTempo / 60f;
    }

    // Update is called once per frame
    void Update()
    {
        if (goScroller)
            Note.transform.position += new Vector3(beatTempo * Time.deltaTime, 0f, 0f);
    }
}
