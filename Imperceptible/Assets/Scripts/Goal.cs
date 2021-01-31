using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public  bool        BothPlayers;
    private GameObject  player;
    public  string      scenePath;
    public  bool        winState = false;
    private Buttons     _buttons;
    private void Start() {
        _buttons = FindObjectOfType<Buttons>();
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
        if (!winState) SceneManager.LoadScene(scenePath);
        else {
            _buttons.OnWin();
        }
    }
}
