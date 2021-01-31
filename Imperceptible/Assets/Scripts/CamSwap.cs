using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public enum ViewActive {
    WinstonView,
    SparrowView,
    DrawSparrowView,
    DrawWintonView
}

public class CamSwap : MonoBehaviour {
    [SerializeField] private CinemachineVirtualCamera Sparrow;
    [SerializeField] private CinemachineVirtualCamera Winton;
    [SerializeField] private CinemachineVirtualCamera Draw;
    public                   Action<ViewActive>       onViewChange;
    [SerializeField] private ViewActive               currentView;
    
    void Start() {
        SetWhoActive();
        onViewChange += active => currentView = active;
    }

    void SetWhoActive() {
        switch (currentView) {
            case ViewActive.DrawSparrowView:
                Sparrow.Priority = 1;
                Winton.Priority = 0;
                Draw.Priority = 2;
                break;
            case ViewActive.DrawWintonView:
                Sparrow.Priority = 0;
                Winton.Priority = 1;
                Draw.Priority = 2;
                break;
            case ViewActive.SparrowView:
                Sparrow.Priority = 1;
                Winton.Priority = 0;
                Draw.Priority = 0;
                break;
            case ViewActive.WinstonView:
                Sparrow.Priority = 0;
                Winton.Priority = 1;
                Draw.Priority = 0;
                break;
        }

        onViewChange(currentView);
    }

    public ViewActive GetCurrentView() {
        return currentView;
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
                onViewChange(ViewActive.DrawSparrowView);
            }
        }
    }
}