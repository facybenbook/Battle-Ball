using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player_IDTag))]
public class Player_Health : MonoBehaviour
{
    //The max amount of health the player can have
    public int maxHealth = 8;
    [HideInInspector]
    public int currentHealth = 0;

    //Quick reference to this player's ID Tag component
    private Player_IDTag playerIDTag;

    //Amount of time it takes for this player to come back to life
    public float respawnTime = 4;
    private float respawnCounter = 0;
    //Amount of time that this player is invulnerable after death
    public float invulnerableTime = 1.5f;
    private float invulnerableCounter = 0;



	// Use this for initialization
	private void Awake ()
    {
        this.playerIDTag = this.GetComponent<Player_IDTag>();
        this.currentHealth = this.maxHealth;
	}
	

	// Update is called once per frame
	private void FixedUpdate ()
    {
        //Decreases the time that this player is invulnerable
        if (this.invulnerableCounter > 0)
            this.invulnerableCounter -= Time.fixedDeltaTime;

        //Decreases the time until this player respawns
        if (this.respawnCounter > 0)
            this.respawnCounter -= Time.fixedDeltaTime;
	}


    //Function to be called externally by UnityEvents. 
    public void ResetPosition()
    {

    }


    //Function called externally when this player takes damage
    public void DamagePlayer(int damageDealt_ = 0)
    {
        //Nothing happens if the damage dealt is below 0.
        if (damageDealt_ < 0)
            return;

        //Nothing happens if this player is currently invulnerable
        if (this.invulnerableCounter > 0)
            return;

        this.currentHealth -= damageDealt_;

        //If the player's health hits 0, they die
        if(this.currentHealth <= 0)
        {
            //Starts the death sequence for this player
            this.currentHealth = 0;
            this.invulnerableCounter = this.invulnerableTime;
            this.respawnCounter = this.respawnTime;

            //Dispatches a player death event saying that this player died
            EVTData deathEVT = new EVTData();
            deathEVT.playerDeath = new PlayerDeathEVT(this.playerIDTag.playerID, this.playerIDTag.playerTeam);
            Manager_EventManager.TriggerEvent(PlayerDeathEVT.eventName, deathEVT);
        }
    }
}
