using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UIElements;

public class createTexture : MonoBehaviour {

    public Canvas canvas;

    public Texture2D texture;

    public DecalProjector decal;

    public Material mat;
    // Start is called before the first frame update
    void Start() {
        var position = canvas.transform.position;
        texture = new Texture2D((int) position.x, (int) position.y, TextureFormat.ARGB32,
            false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) {
            print("Making Green Pixel");
            texture.SetPixel((int)Input.mousePosition.x, (int)Input.mousePosition.y, Color.green);
            texture.Apply();

            mat.mainTexture = texture;
          //  mat.shader = Shader.Find("HDRP/decal");
            // Material mat = decal.GetComponent<DecalProjector>().material;
            // mat.shader = Shader.Find ("HDRP/Decal");
            // mat.SetTexture("BaseColorMap", texture);




        }
            
        
    }
}
