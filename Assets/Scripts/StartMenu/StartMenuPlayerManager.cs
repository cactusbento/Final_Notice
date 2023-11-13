using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class StartMenuPlayerManager : MonoBehaviour
{
    [Header("Players")]
    [SerializeField] List<Sprite> playerSprites;
    [SerializeField] List<StartMenuPlayer> players;

    [Header("UI")]
    [SerializeField] TMP_Text startMessage;

    void Start()
    {
        startMessage.gameObject.SetActive(false);
    }
    
    void Update()
    {
        if (players.Count > 0)
        {
            startMessage.gameObject.SetActive(true);
        }
    }

    public void PlayerJoined(StartMenuPlayer newPlayer)
    {
        players.Add(newPlayer);
        newPlayer.transform.SetParent(transform);
        newPlayer.playerNumber = players.IndexOf(newPlayer) + 1;
        newPlayer.image.sprite = playerSprites[newPlayer.playerNumber - 1];
        newPlayer.playerName.text = $"Player {newPlayer.playerNumber}";
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Debug.Log("QUIT");
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
