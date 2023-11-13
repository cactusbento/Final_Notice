using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class StartMenuPlayer : MonoBehaviour
{
    [Header("UI")]
    public Image image;
    public TMP_Text playerName;

    [Header("Stats")]
    public int playerNumber;

    bool selectInput;
    bool backInput;
    bool startInput;
    Vector2 directionInput;
    StartMenuPlayerManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("StartMenuPlayerInputManager").GetComponent<StartMenuPlayerManager>();
        manager.PlayerJoined(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(startInput && playerNumber == 1)
        {
            manager.Play();
        }

        if(backInput && playerNumber == 1)
        {
            manager.Quit();
        }
    }

    public void OnSelect(InputAction.CallbackContext ctx) => selectInput = ctx.ReadValueAsButton();
    public void OnBack(InputAction.CallbackContext ctx) => backInput = ctx.ReadValueAsButton();
    public void OnStart(InputAction.CallbackContext ctx) => startInput = ctx.ReadValueAsButton();
    public void OnDirection(InputAction.CallbackContext ctx) => directionInput = ctx.ReadValue<Vector2>();
}
