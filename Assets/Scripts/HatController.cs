using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class HatController : MonoBehaviour
{
    public int healthModifier = 0;
    public int speedModifier = 0;
    private float speedRatio = 1f;

    public PlayerController player;
    public SpriteRenderer spriteRenderer;

    public void Awake()
    {
        if (!transform.TryGetComponent<SpriteRenderer>(out spriteRenderer))
            Debug.LogError($"Hat: {transform.name} has no sprite renderer");

    }

    public void Equip(PlayerController player)
    {
        if (player != null && spriteRenderer != null)
        {
            // assigning stats
            this.player = player;
            player.currHat = this;
            player.maxHealth += healthModifier;
            player.currentHealth = player.maxHealth;
            float orignalSpeed = player.topSpeed;
            player.topSpeed += speedModifier;
            speedRatio = player.topSpeed / orignalSpeed;
            player.acceleration =  speedRatio * 10;
            player.deccelerationScalar = speedRatio * 10;

            // assigning graphic
            player.hatRenderer.gameObject.SetActive(true);
            player.hatRenderer.sprite = spriteRenderer.sprite;
            player.hatRenderer.color = spriteRenderer.color;
        }
    }

    public void Unequip()
    {
        if (player != null)
        {
            // assigning players
            player.currHat = null;
            player.maxHealth -= healthModifier;
            player.topSpeed -= speedModifier;
            player.acceleration = 10;
            player.deccelerationScalar = 10;
        }
    }


}
