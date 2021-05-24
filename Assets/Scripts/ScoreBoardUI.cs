using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoardUI : MonoBehaviour
{
    private RectTransform _rectTransform;

    [SerializeField] Camera cam;

    void Start()
    {
        _rectTransform = this.GetComponent<RectTransform>();
    }

    void Update()
    {
        // TODO - Doesn't work?
        //Vector3 vec = _rectTransform.position - cam.transform.position;
        //_rectTransform.rotation = Quaternion.LookRotation(vec, Vector3.up);
    }
}
