using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPuzzleParts : MonoBehaviour
{
    [SerializeField] private List<MoveablePuzzlePartLevel2> _partsLevel2;
    
    public void OnResetPuzzleParts()
    {
        _partsLevel2.ForEach(part =>
        {
            if(part) //check if part was maybe removed already
            {
                part.OnResetPosition();
            }
        });
    }
}
