using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartMenu_Instructions : MonoBehaviour
{
    public int playersReady = 0;

    /// <summary>
    /// Should be controlled by multiplayer component to determine
    /// how many players are in the game.
    /// </summary>
    public int maxPlayers = 4;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        TextMeshProUGUI tmp = this.GetComponent<TextMeshProUGUI>();
        string instructionString = "Press Start to Ready";
        if (playersReady >= maxPlayers) {
            instructionString = "Press Start to Begin";
        }
        tmp.text = string.Format("{0}\n{1:0}/{2:0} Ready", instructionString, playersReady, maxPlayers);
    }
}
