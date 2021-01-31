using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager;
using UnityEngine;

public class Drawing : MonoBehaviour {
    private Camera cam;
    public LayerMask mask;
    public Buttons _buttons;
    private Texture2D tex;
    private List<Renderer> renders = new List<Renderer>(); 

    void Start() {
        cam = GetComponent<Camera>();
        var drawArray = GameObject.FindGameObjectsWithTag("DrawTag");
        foreach (var drawtag in drawArray) {
            renders.Add(drawtag.GetComponent<Renderer>());
        }
        _buttons.clearEvent += () => Clear();
        Clear();
    }

    // Update is called once per frame
    void Update() {
        if (!Input.GetMouseButton(0)) {
            return;
        }

        RaycastHit hit;
        if (!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, float.MaxValue, mask))
            return;
        var rend = hit.transform.GetComponent<Renderer>();

        if (rend == null || rend.sharedMaterial == null) {
            return;
        }
        rend.material.mainTexture = tex;
        Vector2 pixelUV = hit.textureCoord;
        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;
        tex.SetPixel((int) pixelUV.x, (int) pixelUV.y, _buttons.getColor());
        tex.Apply();
    }

    void Clear() {
        tex = new Texture2D(100, 100);
        tex.filterMode = FilterMode.Point;
        
        var pixels = tex.GetPixels();
        for (int i = 0; i < pixels.Length; i++) {
            pixels[i] = Color.clear;
        }
        tex.SetPixels(pixels);
        tex.Apply();
        
        foreach (var rend in renders) {
            rend.material.mainTexture = tex;
        }
    }
}