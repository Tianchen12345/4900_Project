using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlSimple : MonoBehaviour
{
    public bool fallThrough = false;



    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            fallThrough = true;
        }
        else
        {
            fallThrough = false;
        }
    }
}
