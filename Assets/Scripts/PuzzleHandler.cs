using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleHandler : MonoBehaviour
{
    [SerializeField] public Material green;
    [SerializeField] public Material gray;
    [SerializeField] public Material orange;
    [SerializeField] private HeartBehaviour _animatedHeart;
    public enum Parts { Aorta = 0, LeftChamber = 1, RightChamber = 2, LungArteria = 3 };
    private int _partsSolved = 0;
    private Material[][] _initialMaterials = new Material[4][];
    private Parts _currentPart;
    private int _currentIndex;
    public void GrayComponents()
    {
        string[] names = System.Enum.GetNames(typeof(Parts));
        _currentIndex = 0;
        _currentPart = 0;
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
        try
        {
            Parts currentPart = (Parts)System.Enum.Parse(typeof(Parts), part.name);
            MeshRenderer meshRenderer = part.GetComponent<MeshRenderer>();
            meshRenderer.materials = _initialMaterials[(int)currentPart];
            
            _partsSolved++;
            if (_partsSolved == 4)
            {
                this._animatedHeart.Show();
                Destroy(this.gameObject);
            }
            _currentIndex++;
            _currentPart = (Parts)_currentIndex;
        } catch (System.Exception)
        {
            Debug.Log("exceptioneeee");
            //should never be called!
        }
    }

    public Parts getCurrentPart()
    {
        return _currentPart;
    }
}
