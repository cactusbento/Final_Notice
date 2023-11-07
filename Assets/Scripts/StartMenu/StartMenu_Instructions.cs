using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartMenu_Instructions : MonoBehaviour
{
    /// <summary>
    /// Should be controlled by multiplayer component to determin
    /// How many players are ready.
    /// </summary>
    public int playersReady = 0;

    /// <summary>
    /// Should be controlled by multiplayer component to determine
    /// how many players are in the game.
    /// </summary>
    public int maxPlayers = 4;

    public bool isReady = false;

    TextMeshProUGUI tmp;
    // Start is called before the first frame update
    void Start()
    {
        tmp = this.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        string instructionString = "Press Start to Ready";
        isReady = false;
        if (playersReady >= maxPlayers) {
            instructionString = "Press Start to Begin";
            isReady = true;
        }
        tmp.text = string.Format("{0}\n{1:0}/{2:0} Ready", instructionString, playersReady, maxPlayers);
    }
}
