using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu_Instructions : MonoBehaviour
{
    /// <summary>
    /// Should be controlled by multiplayer component to determin
    /// How many players are ready.
    /// </summary>
    public int playersReady = 0;

    /// <summary>
    /// Determined by the number of controllers
    /// that are connected to the game.
    /// 
    /// `Input.GetJoystickNames().Length`
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

        if (Input.GetKeyDown(KeyCode.JoystickButton7) ||
            Input.GetKeyDown(KeyCode.Alpha0)){
            playersReady += 1;
        }
        if (isReady) {
            if (Input.GetKeyDown(KeyCode.JoystickButton7) || 
                Input.GetKeyDown(KeyCode.Space)) {
                    SceneManager.LoadScene("SampleScene");
                }
        }

        int numControllers = Input.GetJoystickNames().Length;
        maxPlayers = (numControllers < 1) ? 1 : numControllers;
        string instructionString = "Press Start to Ready Up";
        isReady = false;
        if (playersReady >= maxPlayers) {
            instructionString = "Press Start to Begin";
            isReady = true;
        }
        tmp.text = string.Format("{0}\n{1:0}/{2:0} Ready", instructionString, playersReady, maxPlayers);
    }
}
