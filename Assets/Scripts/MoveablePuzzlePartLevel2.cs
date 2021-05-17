using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MoveablePuzzlePartLevel2 : MonoBehaviour
{
    private Collider _other;
    [SerializeField] public PuzzleHandler puzzleHandler;
    [SerializeField] private Toaster _toaster;
    private Material _currentMaterial;
    private Transform _otherParentTransform;
    private MeshRenderer _otherMeshRenderer;

    void Start()
    {
        this.transform.rotation = Random.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals(this.name))
        {
            _otherParentTransform = other.GetComponentInParent<Transform>(); //since rotation on other is based on the total rotation of the surrounding heart component
            _otherMeshRenderer = other.GetComponent<MeshRenderer>();
            this._other = other;
        }
    }

    void Update()
    {
        if(_other)
        {
            if (Quaternion.Angle(_otherParentTransform.rotation, this.transform.rotation) < 40)
            {
                _currentMaterial = puzzleHandler.green;
            }
            else
            {
                _currentMaterial = puzzleHandler.orange;
            }

            Material[] newMaterials = new Material[_otherMeshRenderer.materials.Length];
            for (int i = 0; i < _otherMeshRenderer.materials.Length; i++)
            {
                newMaterials[i] = _currentMaterial;
            }
            _otherMeshRenderer.materials = newMaterials;
        } 
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.Equals(this.name))
        {
            this._other = null;
            Material[] newMaterials = new Material[_otherMeshRenderer.materials.Length];
            for (int i = 0; i < _otherMeshRenderer.materials.Length; i++)
            {
                newMaterials[i] = puzzleHandler.gray;
            }
            _otherMeshRenderer.materials = newMaterials;
        }
    }

    public void OnReleased()
    {
        if (this._other != null)
        {
            if(puzzleHandler.getCurrentPart().ToString().Equals(_other.name))
            {
                puzzleHandler.PartSolved(_other);
                Destroy(this.gameObject);
            } else if(_currentMaterial.name == puzzleHandler.orange.name)
            {
                _toaster.ShowToast("Try to rotate the" + _other.name + " into correct position.\nIt would ruin the blood flow...");
            }
            else
            {
                _toaster.ShowToast("The " + _other.name + " should not be added to the heart now.\nIt would confuse the heart...");
            }
            
        }
    }
}
