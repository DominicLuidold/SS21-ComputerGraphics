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
    private readonly float _angle = 40; //maximum angle of rotation
    private Vector3 _initialPosition;

    void Start()
    {
        this.transform.rotation = Random.rotation;
        _initialPosition = this.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals(this.name)) //assign _other variable for Update() method
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
            if (Quaternion.Angle(_otherParentTransform.rotation, this.transform.rotation) < _angle) // mark with green material if angle is correct
            {
                _currentMaterial = puzzleHandler.green;
            }
            else // mark with orange material if angle is not correct but part is in the right spot
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
        if (other.name.Equals(this.name)) // mark with gray material
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
 
            if(puzzleHandler.GetCurrentPart().ToString().Equals(_other.name) && Quaternion.Angle(_otherParentTransform.rotation, this.transform.rotation) < _angle) //part is solved if it is on the right spot and has correct rotation
            {
                puzzleHandler.PartSolved(_other);
                Destroy(this.gameObject);
            } else if(_currentMaterial.name == puzzleHandler.orange.name) //show toast message if part is not rotated right or if it is added in wrong order
            {
                _toaster.ShowToast("Try to rotate the " + puzzleHandler.GetPartName(_other.name) + " into correct position.\nOtherwise it would ruin the blood flow...");
            }
            else
            {
                _toaster.ShowToast("The " + puzzleHandler.GetPartName(_other.name) + " should not be added to the heart now.\nOtherwise it would confuse the heart...");
            }
            
        }
    }

    public void OnResetPosition() // put back into initial position (is called by the Reset Puzzle Parts Button) 
    {
        this.transform.position = _initialPosition;
    }
}
