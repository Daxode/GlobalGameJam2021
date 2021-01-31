using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public bool BothPlayers;
    private BoxCollider boxCol;
    private GameObject player;
    public string scenePath;
    
    void Start() {
        var boxCol =gameObject.GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player") && player != null) {
            player = other.gameObject;
            SwapScene();

        }
        if (other.gameObject.CompareTag("Player")) {
            player = other.gameObject;
            if (BothPlayers == false) {
                SwapScene();
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            player = null;
        }
    }

    void SwapScene() {
        SceneManager.LoadScene(scenePath);
    }
}
