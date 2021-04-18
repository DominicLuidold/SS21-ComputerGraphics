using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBehaviour : MonoBehaviour
{
    public Animator animator;
    private bool heartBeating = true;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            heartBeating = !heartBeating;
            animator.SetBool("heartBeating", heartBeating);
        } 
        else if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            animator.ResetTrigger("playHeartBeat2");
            animator.SetTrigger("playHeartBeat1");
        } 
        else if(Input.GetKeyDown(KeyCode.Keypad2)) 
        {
            animator.ResetTrigger("playHeartBeat1");
            animator.SetTrigger("playHeartBeat2");
        }
    }

    public void SwitchHeartState()
    {
        heartBeating = !heartBeating;
        animator.SetBool("heartBeating", heartBeating);
    }

    public void SetHeartState(bool isBeating)
    {
        heartBeating = isBeating;
        animator.SetBool("heartBeating", heartBeating);
    }

    public void RotateLeft()
    {
        transform.Rotate(Vector3.up);
    }

    public void RotateRight()
    {
        transform.Rotate(Vector3.down);
    }

    public void RotateUp()
    {
        transform.Rotate(Vector3.right);
    }

    public void RotateDown()
    {
        transform.Rotate(Vector3.left);
    }
}
