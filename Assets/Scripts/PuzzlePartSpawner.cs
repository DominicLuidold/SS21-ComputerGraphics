using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePartSpawner : MonoBehaviour
{
    [SerializeField] public PuzzleHandler puzzleHandler;
    [SerializeField] public GameObject spawnPoint;
    private Transform[] _childs;
    private int _counter = 0;

    void Start()
    {
        string[] names = System.Enum.GetNames(typeof(PuzzleHandler.Parts));
        _childs = new Transform[names.Length];
        for(int i = 0; i < names.Length; i++)
        {
            _childs[i] = transform.Find(names[i]);
        }
        _childs[0].gameObject.SetActive(true);
    }


    public void PartSolved(Collider other)
    {
        _counter++;
        puzzleHandler.PartSolved(other);
        if(_counter == _childs.Length)
        {
            Destroy(this.gameObject);
        } else
        {
            _childs[_counter].gameObject.SetActive(true);
        }
    }
}
