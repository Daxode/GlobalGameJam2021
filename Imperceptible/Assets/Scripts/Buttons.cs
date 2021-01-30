using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Buttons : MonoBehaviour {
    private Canvas _canvas;
    public SpriteRenderer pencil;
    void Start() {
        _canvas = this.GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0)) {
            var mpos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            
        }
    }

    public void GreenButton() {
        print("test");
    }
}
