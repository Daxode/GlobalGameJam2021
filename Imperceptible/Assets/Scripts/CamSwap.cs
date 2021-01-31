using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public enum ViewActive {
    WinstonView,
    SparrowView,
    DrawView
}

public class CamSwap : MonoBehaviour {
    public CinemachineVirtualCamera Sparrow;
    public CinemachineVirtualCamera Winton;
    public CinemachineVirtualCamera Draw;
    public Action<ViewActive>       onViewChange;
    
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
                onViewChange(ViewActive.WinstonView);
            }
            else {
                Sparrow.Priority = 1;
                Winton.Priority = 0;
                onViewChange(ViewActive.SparrowView);
            }
        }
        else if (Input.GetKeyUp(KeyCode.Q)) {
            if (Draw.Priority == 2) {
                if (Sparrow.Priority == 1)
                    onViewChange(ViewActive.SparrowView);
                else
                    onViewChange(ViewActive.WinstonView);
                
                Draw.Priority = 0;
            }
            else {
                Draw.Priority = 2;
                onViewChange(ViewActive.DrawView);
            }
        }
    }
}