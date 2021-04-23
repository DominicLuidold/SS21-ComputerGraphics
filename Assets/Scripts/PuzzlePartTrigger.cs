using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePartTrigger : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [SerializeField] private Collider objective;
    [SerializeField] private PuzzleHandler puzzleHandler;
    private bool lookAt = false;
    private Rigidbody _rigidBody;
    private bool _solved = false;
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

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
            if (_rigidBody != null)
            {
                _rigidBody.useGravity = false;
            }
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
            if (_rigidBody != null)
            {
                //_rigidBody.useGravity = true; //for dropping the rigidBody when needed
            }
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

    private void OnTriggerEnter(Collider other)
    {
        /*if(other.Equals(objective))
        {
            Material[] newMaterials = new Material[_meshRenderer.materials.Length];
            for(int i = 0; i < _meshRenderer.materials.Length; i++)
            {
                newMaterials[i] = _green;
            }
            _meshRenderer.materials = newMaterials;
        }*/
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.Equals(objective))
        {
            MeshRenderer meshRenderer = other.GetComponent<MeshRenderer>();
            Material[] newMaterials = new Material[meshRenderer.materials.Length];
            for (int i = 0; i < meshRenderer.materials.Length; i++)
            {
                newMaterials[i] = puzzleHandler.green;
            }
            meshRenderer.materials = newMaterials;

            if(!lookAt && !_solved)
            {
                puzzleHandler.PartSolved(other);
                _solved = true;
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.Equals(objective))
        {
            MeshRenderer meshRenderer = other.GetComponent<MeshRenderer>();
            Material[] newMaterials = new Material[meshRenderer.materials.Length];
            for (int i = 0; i < meshRenderer.materials.Length; i++)
            {
                newMaterials[i] = puzzleHandler.gray;
            }
            meshRenderer.materials = newMaterials;
        }
    }
}
