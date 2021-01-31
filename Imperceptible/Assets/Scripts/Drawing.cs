using System.Collections.Generic;
using UnityEngine;

public class Drawing : MonoBehaviour {
    private Camera         cam;
    public  LayerMask      mask;
    public  Buttons        _buttons;
    private List<Renderer> renders = new List<Renderer>();
    private CamSwap        _camSwap;
    void Start() {
        _camSwap = FindObjectOfType<CamSwap>();
        cam = GetComponent<Camera>();
        var drawArray = GameObject.FindGameObjectsWithTag("DrawTag");
        foreach (var drawtag in drawArray) {
            var rend = drawtag.GetComponent<Renderer>();
            renders.Add(rend);
            rend.material.color = Color.white;
        }
        _buttons.clearEvent += () => Clear();
        Clear();
    }

    // Update is called once per frame
    void Update() {
        if (!Input.GetMouseButton(0) || !_camSwap.IsDrawViewActive()) {
            return;
        }

        RaycastHit hit;
        if (!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, float.MaxValue, mask))
            return;
        var rend = hit.transform.GetComponent<Renderer>();

        if (rend == null || rend.sharedMaterial == null) {
            return;
        }
        
        var     tex     = rend.material.mainTexture as Texture2D;
        Vector2 pixelUV = hit.textureCoord;
        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;
        tex.SetPixel((int) pixelUV.x, (int) pixelUV.y, _buttons.getColor());
        tex.Apply();
    }

    void Clear() {
        foreach (var rend in renders) {
            int size = (int)(rend.transform.lossyScale.magnitude * 25f);
            var tex = new Texture2D(size, size);
            tex.filterMode = FilterMode.Point;
        
            var pixels = tex.GetPixels();
            for (int i = 0; i < pixels.Length; i++) {
                pixels[i] = Color.clear;
            }
            tex.SetPixels(pixels);
            tex.Apply();
            
            rend.material.mainTexture = tex;
        }
    }
}