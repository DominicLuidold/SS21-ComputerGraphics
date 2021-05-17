using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveablePuzzlePartLevel2 : MonoBehaviour
{
    private Collider _other;
    [SerializeField] public PuzzleHandler puzzleHandler;

    void Start()
    {
        this.transform.rotation = Random.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals(this.name))
        {
            Transform otherParentTransform = other.GetComponentInParent<Transform>(); //since rotation on other is based on the total rotation of the heart
            if(Quaternion.Angle(otherParentTransform.rotation, this.transform.rotation) < 40)
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
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.Equals(this.name))
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
        if (this._other != null)
        {
            puzzleHandler.PartSolved(_other);
            Destroy(this.gameObject);
        }
    }
}
