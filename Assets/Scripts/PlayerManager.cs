using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class PlayerManager : MonoBehaviour
{
    //[SerializeField] List<Sprite> playerSprites;
    [SerializeField] List<AnimatorController> playerAnimatorControllers;
    [SerializeField] List<PlayerController> players;
 
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(transform.parent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerJoined(PlayerController newPlayer)
    {
        players.Add(newPlayer);
        newPlayer.transform.parent = transform;
        newPlayer.animator.runtimeAnimatorController = playerAnimatorControllers[players.IndexOf(newPlayer)];
    }
}
