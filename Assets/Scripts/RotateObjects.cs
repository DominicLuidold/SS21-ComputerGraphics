using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjects : MonoBehaviour
{
    private Collider _other;
    private LayerMask _layer;

    private void Start()
    {
        _layer = LayerMask.NameToLayer("Rotatable");
    }

    private void Update()
    {
        if (_other != null)
        {
            Vector3 rotationVector = Vector3.zero;

#if UNITY_EDITOR
            if (Input.GetKey(KeyCode.W))
            {
                rotationVector = Vector3.right;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                rotationVector = Vector3.up;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                rotationVector = Vector3.left;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                rotationVector = Vector3.down;
            }

#else
            // only rotate when not holding PrimaryIndexTrigger since you use the touchpad for pulling and pulling an grabed object
            if(!OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
            {
                Vector2 input = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
                rotationVector = new Vector3(input.y, -input.x);
            }

#endif

            if (!rotationVector.Equals(Vector3.zero))
            {
                
                _other.transform.RotateAround(_other.transform.position, rotationVector, 90 * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == _layer)
        {
            Debug.Log("equals");
        } else
        {
            Debug.Log("not equal");
        }
        _other = other;
    }

    private void OnTriggerExit(Collider other)
    {
        _other = null;
    }
}
