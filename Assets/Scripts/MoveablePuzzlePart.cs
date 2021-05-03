using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveablePuzzlePart : MonoBehaviour
{
    [SerializeField] private PuzzleHandler puzzleHandler;
    private Collider _other;
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;

    private void Start()
    {
        _initialPosition = this.transform.position;
        _initialRotation = this.transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name.Equals(this.name))
        {
            this._other = other;
            MeshRenderer meshRenderer = other.GetComponent<MeshRenderer>();
            Material[] newMaterials = new Material[meshRenderer.materials.Length];
            for (int i = 0; i < meshRenderer.materials.Length; i++)
            {
                newMaterials[i] = puzzleHandler.green;
            }
            meshRenderer.materials = newMaterials;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.name.Equals(this.name))
        {
            this._other = null;
            MeshRenderer meshRenderer = other.GetComponent<MeshRenderer>();
            Material[] newMaterials = new Material[meshRenderer.materials.Length];
            for (int i = 0; i < meshRenderer.materials.Length; i++)
            {
                newMaterials[i] = puzzleHandler.gray;
            }
            meshRenderer.materials = newMaterials;
        }
    }

    public void OnReleased()
    {
        if(this._other != null)
        {
            puzzleHandler.PartSolved(_other);
            Destroy(this.gameObject);
        } else
        {
            this.transform.position = _initialPosition;
            this.transform.rotation = _initialRotation;
        }
    }
}
