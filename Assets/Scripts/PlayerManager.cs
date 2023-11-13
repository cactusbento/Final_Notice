using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class PlayerManager : MonoBehaviour
{
    //[SerializeField] List<Sprite> playerSprites;
    [SerializeField] List<AnimatorController> playerAnimatorControllers;
    [SerializeField] List<PlayerController> players;
 
    public void PlayerJoined(PlayerController newPlayer)
    {
        players.Add(newPlayer);
        newPlayer.transform.parent = transform;
        newPlayer.animator.runtimeAnimatorController = playerAnimatorControllers[players.IndexOf(newPlayer)];
    }
}
