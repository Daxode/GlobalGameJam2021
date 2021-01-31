using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class Buttons : MonoBehaviour {
    private Color color = Color.green;
    private Canvas _canvas;
    public CinemachineVirtualCamera cam;
    public Action clearEvent;
    void Start() {
        _canvas = GetComponent<Canvas>();
        _canvas.enabled = false;
    }

    // Update is called once per frame
    void Update() {
        if(cam.Priority == 2) {
            _canvas.enabled = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else {
            _canvas.enabled = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
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
