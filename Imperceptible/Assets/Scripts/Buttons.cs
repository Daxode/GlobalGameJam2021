using System;
using Cinemachine;
using Image = UnityEngine.UI.Image;
using UnityEngine;
using Cursor = UnityEngine.Cursor;


public class Buttons : MonoBehaviour {
    private Color color = Color.green;
    private Canvas _canvas;
    public CinemachineVirtualCamera cam;
    public Action clearEvent;
    public RectTransform brush;
    public float scalespeed;
    private float scalevalue = 0.1f;
    private Image img;

    void Start() {
        _canvas = GetComponent<Canvas>();
        _canvas.enabled = false;
        img = brush.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update() {
        if (cam.Priority == 2) {
            _canvas.enabled = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
            var screenPoint = Input.mousePosition;
            screenPoint.z = 1.0f; //distance of the plane from the camera
            brush.position = Camera.main.ScreenToWorldPoint(screenPoint);
            img.color = getColor();
            img.color = new Color(img.color.r, img.color.g, img.color.b);

            scalevalue += scalespeed * Time.deltaTime * Input.mouseScrollDelta.y;
            brush.localScale = new Vector3(scalevalue, scalevalue, 1);
        }
        else {
            _canvas.enabled = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void GiveColorButton(Color color) {
        this.color = color;
    }

    public void GreenButton() {
        color = Color.green;
    }

    public void PurpleButton() {
        color = Color.magenta;
    }

    public void BlueButton() {
        color = Color.blue;
    }

    public void EraserButton() {
        color = Color.clear;
    }

    public Color getColor() {
        return color;
    }

    public void Clear() {
        clearEvent();
    }
}