using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeartPieceUI : MonoBehaviour
{
    [SerializeField] public string description;
    [SerializeField] Camera cam;
    private TextMeshProUGUI _text;
    private RectTransform _rectTransform;
    void Start()
    {
        GameObject canvasGO = new GameObject();
        canvasGO.AddComponent<Canvas>();
        canvasGO.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
        canvasGO.transform.SetParent(this.transform);
        GameObject text = new GameObject();
        _text = text.AddComponent<TextMeshProUGUI>();
        //_canvas.renderMode = RenderMode.WorldSpace;
        text.transform.localScale = canvasGO.transform.localScale;
        text.transform.SetParent(canvasGO.transform);        
        //_text.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        _text.SetText(description);
        _text.color = Color.black;
        //_text.fontSize = 12;
        _text.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 200);
        this._rectTransform = canvasGO.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        //canvasGO.transform.position = new Vector3(0f, 3f, 8f);
        //Quaternion q = transform.rotation;
        //canvasGO.transform.rotation = Quaternion.Inverse(q);
        
        //rectTransform.LookAt(cam.transform.position);
        Vector3 vec = _rectTransform.position - cam.transform.position;
        _rectTransform.rotation = Quaternion.LookRotation(vec, Vector3.up);
        Vector3 pos = this.gameObject.transform.position;
        pos = new Vector3(pos.x + 6, pos.y + 2, pos.z);
        _rectTransform.position = pos;
        //rectTransform = _text.GetComponent<RectTransform>();
        //rectTransform.LookAt(cam.transform.position);
    }
}
