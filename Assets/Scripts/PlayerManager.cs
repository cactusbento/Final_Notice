using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    [Header("Players")]
    [SerializeField] List<RuntimeAnimatorController> playerAnimatorControllers;
    [SerializeField] List<PlayerController> players;

    [Header("UI")]
    [SerializeField] List<HealthBar> playerHealthBars;
    [SerializeField] Image winScreen;
    [SerializeField] Image loseScreen;

    int deadPlayerCount;
    int deadBossCount;

    void Start()
    {
        winScreen.gameObject.SetActive(false); 
        loseScreen.gameObject.SetActive(false);
    }
 
    public void PlayerJoined(PlayerController newPlayer)
    {
        players.Add(newPlayer);
        newPlayer.playerNumber = players.IndexOf(newPlayer) + 1;
        newPlayer.transform.SetParent(transform, false);
        newPlayer.animator.runtimeAnimatorController = playerAnimatorControllers[newPlayer.playerNumber - 1];
        
        HealthBar healthBar = playerHealthBars[newPlayer.playerNumber - 1];
        healthBar.gameObject.SetActive(true);
        healthBar.GetComponent<UITrackPlayer>().target = newPlayer.transform;
        newPlayer.healthBar = healthBar;
    }

    public void PlayerDied()
    {
        deadPlayerCount++;

        if(deadPlayerCount == players.Count && deadBossCount == 0)
        {
            //lose screen
            loseScreen.gameObject.SetActive(true);
        }
    }

    public void BossDied()
    {
        deadBossCount++;

        if(deadBossCount == 1 && deadPlayerCount < players.Count)
        {
            //win screen
            winScreen.gameObject.SetActive(true);
        }
    }
}
