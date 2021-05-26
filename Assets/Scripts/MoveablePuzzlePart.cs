using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveablePuzzlePart : MonoBehaviour
{
    [SerializeField] private PuzzlePartSpawner _puzzlePartSpawner;
    private Collider _other;
    private Vector3 _initialPosition;

    private void Start()
    {
        _initialPosition = _puzzlePartSpawner.spawnPoint.transform.position;
        this.transform.position = _initialPosition;
        this.transform.rotation = Random.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name.Equals(this.name)) // mark with green material
        {
            this._other = other;
            MeshRenderer meshRenderer = other.GetComponent<MeshRenderer>();
            Material[] newMaterials = new Material[meshRenderer.materials.Length];
            for (int i = 0; i < meshRenderer.materials.Length; i++)
            {
                newMaterials[i] = _puzzlePartSpawner.puzzleHandler.green;
            }
            meshRenderer.materials = newMaterials;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.name.Equals(this.name)) // mark with gray material again
        {
            this._other = null;
            MeshRenderer meshRenderer = other.GetComponent<MeshRenderer>();
            Material[] newMaterials = new Material[meshRenderer.materials.Length];
            for (int i = 0; i < meshRenderer.materials.Length; i++)
            {
                newMaterials[i] = _puzzlePartSpawner.puzzleHandler.gray;
            }
            meshRenderer.materials = newMaterials;
        }
    }

    public void OnReleased()
    {
        if(this._other != null) // solve this part and destroy it, otherwise put it back to initial position
        {
            _puzzlePartSpawner.PartSolved(_other);
            Destroy(this.gameObject);
        } else
        {
            this.transform.position = _initialPosition;
        }
    }
}
