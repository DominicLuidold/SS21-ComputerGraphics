using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public HeartBehaviour heartBehaviour;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            heartBehaviour.RotateRight();
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            heartBehaviour.RotateLeft();
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            heartBehaviour.RotateUp();
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            heartBehaviour.RotateDown();
        }
    }
}
