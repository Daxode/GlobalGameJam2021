using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class CamSwap : MonoBehaviour {
    public CinemachineVirtualCamera Sparrow;
    public CinemachineVirtualCamera Winton;
    public CinemachineVirtualCamera Draw;

    void Start() {
        Sparrow.Priority = 1;
        Winton.Priority = 0;
        Draw.Priority = 0;
    }

    void Update() {
        if (Input.GetKeyUp(KeyCode.E) && Draw.Priority != 2) {
            if (Sparrow.Priority == 1) {
                Sparrow.Priority = 0;
                Winton.Priority = 1;
            }
            else {
                Sparrow.Priority = 1;
                Winton.Priority = 0;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Q)) {
            if (Draw.Priority == 2) {
                Draw.Priority = 0;
            }
            else {
                Draw.Priority = 2;
            }
        }
    }
}