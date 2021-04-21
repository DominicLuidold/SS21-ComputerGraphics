using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGrab : MonoBehaviour
{
    [SerializeField] private Transform destination;
    private bool lookAt = false;
    private Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update();
        Vector2 input = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);

#if UNITY_EDITOR
        if (lookAt && Input.GetMouseButton(0))
#else
        if (lookAt && Input.GetAxis("Oculus_CrossPlatform_SecondaryIndexTrigger") >= 0.8)
#endif
        {
            transform.parent = destination;
            rigidBody.useGravity = false;
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Mathf.Clamp(transform.localPosition.z + 0.005f * input.y, 0.3f, 10));

            if (Input.GetKey(KeyCode.C))
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Mathf.Clamp(transform.localPosition.z - 0.005f, 0.3f, 10));
            }
            if (Input.GetKey(KeyCode.X))
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Mathf.Clamp(transform.localPosition.z + 0.005f, 0.3f, 10));
            }
        }
        else
        {
            transform.parent = null;
            rigidBody.useGravity = true;
        }
    }

    public void Grab()
    {
        lookAt = true;
    }

    public void UnGrab()
    {
        lookAt = false;
    }
}
