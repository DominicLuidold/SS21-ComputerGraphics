using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjects : MonoBehaviour
{
    private Collider _other;
    private Collider _heart;
    private LayerMask _layer;

    private void Start()
    {
        _layer = LayerMask.NameToLayer("Rotatable");
    }

    private void Update()
    {
        Collider currentObjectToRotate = null; ;
        if(_heart != null)
        {
            currentObjectToRotate = _heart;
        }
        else if(_other != null)
        {
            currentObjectToRotate = _other;
        }
        if (currentObjectToRotate != null)
        {
            Vector3 rotationVector = Vector3.zero;

#if UNITY_EDITOR
            if(!Input.GetKey(KeyCode.Space)) //only rotate when not holding space -> not dragging any object
            {
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
                currentObjectToRotate.transform.RotateAround(currentObjectToRotate.transform.position, rotationVector, 90 * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("human heart_anim") || other.name.Equals("human_heart_pieces"))
        {
            _heart = other;
        }
        else if(other.gameObject.layer == _layer)
        {
            _other = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.Equals("human heart_anim") || other.name.Equals("human_heart_pieces"))
        {
            _heart = null;
        }
        else if (other.gameObject.layer == _layer)
        {
            _other = null;
        }
    }
}
