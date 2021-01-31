using System;
using System.Collections;
using Image = UnityEngine.UI.Image;
using UnityEngine;
using UnityEngine.UI;
using Cursor = UnityEngine.Cursor;


public class Buttons : MonoBehaviour {
    [SerializeField] private RectTransform drawPanel;
    [SerializeField] private RectTransform playerPanel;
    [SerializeField] private RectTransform winPanel;

    [Header("Player Panel")]
    [SerializeField] private Color sparrowColor;
    [SerializeField] private Color wintonColor;
    [SerializeField] private Image playerRingImage;
    [SerializeField] private Text  playerNameText;
    
    public Action clearEvent;
    [Header("Brush Settings")]
    [SerializeField] private Color colorDrawColor = Color.green;
    [SerializeField] private RectTransform brush;
    [SerializeField] private float         scalespeed;
    private                  float         scalevalue = 0.1f;
    private                  Image         img;
    
    private CamSwap _camSwap;
    private Camera  _cam;
    private Color   colorGoToward;
    private string  goTowardPlayerName           = "";
    void Awake() {
        _cam = Camera.main;
        _camSwap = FindObjectOfType<CamSwap>();
        img = brush.GetComponent<Image>();
        
        _camSwap.onViewChange += ONViewChange;
    }

    private void ONViewChange(ViewActive viewActive) {
        switch (viewActive) {
            case ViewActive.DrawSparrowView:
            case ViewActive.DrawWintonView:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = false;
                drawPanel.gameObject.SetActive(true);
                playerPanel.gameObject.SetActive(false);
                break;
            case ViewActive.SparrowView:
                Cursor.lockState = CursorLockMode.Locked;
                drawPanel.gameObject.SetActive(false);
                playerPanel.gameObject.SetActive(true);
                colorGoToward = sparrowColor;
                goTowardPlayerName = "Sparrow";
                break;
            case ViewActive.WinstonView:
                Cursor.lockState = CursorLockMode.Locked;
                drawPanel.gameObject.SetActive(false);
                playerPanel.gameObject.SetActive(true);
                colorGoToward = wintonColor;
                goTowardPlayerName = "Winton";
                break;
        }
        
        StartCoroutine(GoToColor());
        StartCoroutine(GoToText());
    }

    
    // Update is called once per frame
    void Update() {
        if (_camSwap.GetCurrentView() == ViewActive.DrawSparrowView ||
            _camSwap.GetCurrentView() == ViewActive.DrawWintonView) {
            brush.position = Input.mousePosition;
            img.color = getColor();
            img.color = new Color(img.color.r, img.color.g, img.color.b);
            scalevalue += scalespeed * Time.deltaTime * Input.mouseScrollDelta.y;
            brush.localScale = new Vector3(scalevalue, scalevalue, 1);
        }
    }
    
    IEnumerator GoToColor() {
        Color oldColor = playerRingImage.color;
        for (float ft = 0f; ft < 1; ft += 0.01f) {
            playerRingImage.color = Color.Lerp(oldColor, colorGoToward, ft);
            yield return new WaitForSeconds(.01f);
        }
    }
    
    IEnumerator GoToText() {
        string oldText = playerNameText.text;
        
        for (int i = 0; i < oldText.Length; i++) {
            playerNameText.text = oldText.Substring(i, oldText.Length-i);
            yield return new WaitForSeconds(.1f);
        }
        
        for (int i = 0; i < goTowardPlayerName.Length; i++) {
            playerNameText.text = goTowardPlayerName.Substring(0, i+1);
            yield return new WaitForSeconds(.1f);
        }
    }

    public void OnWin() {
        drawPanel.gameObject.SetActive(false);
        playerPanel.gameObject.SetActive(false);
        winPanel.gameObject.SetActive(true);
    }

    public void GiveColorButton(Color color) {
        colorDrawColor = color;
    }

    public void GreenButton() {
        colorDrawColor = Color.green;
    }

    public void PurpleButton() {
        colorDrawColor = Color.magenta;
    }

    public void BlueButton() {
        colorDrawColor = Color.blue;
    }

    public void EraserButton() {
        colorDrawColor = Color.clear;
    }

    public Color getColor() {
        return colorDrawColor;
    }

    public void Clear() {
        clearEvent();
    }
}