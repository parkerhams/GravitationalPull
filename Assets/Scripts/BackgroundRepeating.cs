using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRepeating : MonoBehaviour
{
    //Duplicated background - once the camera gets past a certain point on the background, it moves to make it look like it's continuing

    //Take the width of the background
    //And then take CameraPos
    [SerializeField]
    float BackgroundWidth;
    [SerializeField]
    Transform CameraPos;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Repeat();
    }

    //added 2x the width of it to the x position so that it would be perfectly set on the other side after you get to the certain point on the duplicated part
    void Repeat()
    {

    }
}
