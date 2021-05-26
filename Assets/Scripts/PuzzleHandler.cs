using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleHandler : MonoBehaviour
{
    [SerializeField] public Material green;
    [SerializeField] public Material gray;
    [SerializeField] public Material orange;
    [SerializeField] private HeartBehaviour _animatedHeart;
    [SerializeField] private ScoreBoard _scoreBoard;
    public enum Parts { RightChamber = 0, LungArteria = 1, LeftChamber = 2, Aorta = 3 };
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

        this._scoreBoard.StartTime(); //start score board time when components are grayed out
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
                this._scoreBoard.FinishLevel();
                Destroy(this.gameObject);
            } else
            {
                this._scoreBoard.FinishPuzzlePart(GetPartName(part.name));
            }
            _currentIndex++;
            _currentPart = (Parts)_currentIndex;

            
        } catch (System.Exception)
        {
            Debug.Log("exception");
            //should never be called since the parts are the same name as Parts enum!
        }
    }

    public Parts GetCurrentPart()
    {
        return _currentPart;
    }

    public string GetPartName(string part)
    {
        switch(part)
        {
            case nameof(Parts.RightChamber):
                {
                    return "Right Chamber";
                }

            case nameof(Parts.LungArteria):
                {
                    return "Lung Arteria";
                }

            case nameof(Parts.LeftChamber):
                {
                    return "Left Chamber";
                }

            case nameof(Parts.Aorta):
                {
                    return "Aorta";
                }


            default: return "";
        }
    }

    
}
