using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleHandler : MonoBehaviour
{
    [SerializeField] public Material green;
    [SerializeField] public Material gray;
    [SerializeField] private HeartBehaviour _animatedHeart;
    public enum Parts { Aorta = 0, LeftChamber = 1, RightChamber = 2, LungArteria = 3 };
    private int _partsSolved = 0;
    private Material[][] _initialMaterials = new Material[4][];

    public void GrayComponents()
    {
        string[] names = System.Enum.GetNames(typeof(Parts));
        Transform[] transforms = new Transform[names.Length];
        for (int i = 0; i < names.Length; i++)
        {
            transforms[i] = transform.Find(names[i]);
        }

        //get initialMaterials
        for (int i = 0; i < transforms.Length; i++)
        {
            MeshRenderer meshRenderer = transforms[i].GetComponent<MeshRenderer>();
            _initialMaterials[i] = meshRenderer.materials;

            //set to gray
            Material[] newMaterials = new Material[meshRenderer.materials.Length];
            for (int j = 0; j < newMaterials.Length; j++)
            {
                newMaterials[j] = gray;
            }
            meshRenderer.materials = newMaterials;
        }
    }

    public void PartSolved(Collider part)
    {
        MeshRenderer meshRenderer = part.GetComponent<MeshRenderer>();
        switch(part.name)
        {
            case "Aorta":
                meshRenderer.materials = _initialMaterials[(int)Parts.Aorta];
                break;

            case "LeftChamber":
                meshRenderer.materials = _initialMaterials[(int)Parts.LeftChamber];
                break;

            case "RightChamber":
                meshRenderer.materials = _initialMaterials[(int)Parts.RightChamber];
                break;

            case "LungArteria":
                meshRenderer.materials = _initialMaterials[(int)Parts.LungArteria];
                break;
        }
        _partsSolved++;
        if(_partsSolved == 4)
        {
            this._animatedHeart.Show();
            Destroy(this.gameObject);
        }
        
    }
}
