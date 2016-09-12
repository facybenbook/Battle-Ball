using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[RequireComponent(typeof(TrailRenderer))]
[RequireComponent(typeof(Collider2D))]
public class BattleBallLogic : MonoBehaviour
{
    //ID of the last player that hit this ball and their team
    private Players lastPlayerHit = Players.None;
    private Teams lastPlayerHitsTeam = Teams.None;

    //Quick reference to this object's trail renderer
    private TrailRenderer ballTrail;

    //The amount of time between when the hit sound effect plays (to avoid over-playing it)
    public float minTimeBetweenHitSound = 0.3f;
    private float hitSoundTimer = 0f;

    //Unity event invoked when this ball is hit to play a sound cue
    public UnityEvent onHitSoundEvent;



	// Use this for initialization
	private void Awake ()
    {
        this.ballTrail = this.GetComponent<TrailRenderer>();
	}
	

	// Update is called once per frame
	private void Update ()
    {
        //Reduces the timer for when the hit sound can play
        if (this.hitSoundTimer > 0)
            this.hitSoundTimer -= Time.deltaTime;
	}


    //Function called when this object starts colliding with another object
    private void OnCollision2DStart(Collider2D otherObj_)
    {
        //If the sound timer is finished, plays the sound and resets the timer
        if(this.hitSoundTimer <= 0)
        {
            this.onHitSoundEvent.Invoke();
            this.hitSoundTimer = this.minTimeBetweenHitSound;
        }

        //If the object hit is a player
        if(otherObj_.gameObject.GetComponent<Player_IDTag>() != null)
        {
            Player_IDTag quickRefID = otherObj_.gameObject.GetComponent<Player_IDTag>();

            //Stores the info of the player that hits this ball, and changes the trail color of this ball to match the player's color
            this.lastPlayerHit = quickRefID.playerID;
            this.lastPlayerHitsTeam = quickRefID.playerTeam;
            this.ballTrail.material.SetColor("_EmissionColor", quickRefID.playerColor);
        }
    }
    

    //Function called when this object collides with another object's trigger collider
    private void OnTrigger2DStart(Collider2D otherObj_)
    {
        //If the object hit is a goal
        if(otherObj_.gameObject.GetComponent<BallGoal>() != null)
        {
            //Dispatches a GoalScoredEVT to the event manager that sends data about the goal: the player who scored, the player's team, and the team that they scored against
            EVTData scoreEVT = new EVTData();
            scoreEVT.goalScored = new GoalScoredEVT(lastPlayerHit, lastPlayerHitsTeam,
                                    otherObj_.gameObject.GetComponent<BallGoal>().teamGoalID);
            Manager_EventManager.TriggerEvent(GoalScoredEVT.eventName, scoreEVT);
        }
    }
}
