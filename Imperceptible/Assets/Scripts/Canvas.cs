using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class createTexture : MonoBehaviour {

    public Canvas canvas;
    // Start is called before the first frame update
    void Start() {
        var transform1 = canvas.transform;
        var texture = new Texture2D((int) transform1.position.x, (int) transform1.position.y, TextureFormat.ARGB32,
            false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(MouseButton.LeftMouse)) {
            
        }
            
        
    }
}
