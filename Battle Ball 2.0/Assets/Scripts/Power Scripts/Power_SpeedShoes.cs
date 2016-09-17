using UnityEngine;
using System.Collections;

public class Power_SpeedShoes : Power_DefaultLogic
{
    //New forward speed that the player will move while boosted
    public float fastForwardSpeed = 20;
    //New strafe speed that the player will move while boosted
    public float fastStrafeSpeed = 15;
    //New backward speed that the player will move while boosted
    public float fastBackwardSpeed = 10;

    //Vector3 to store the player's default move speeds. (Forward, Strafe, Backward)
    private Vector3 defaultSpeeds;

    //The amount of time this power is active
    public float duration = 5;
    private float durationCounter = 0;



    //Function called on the first frame
    private void Start()
    {
        Player_2DMovement moveRef = this.transform.parent.GetComponent<Player_2DMovement>();

        //Saves the owner player's default speeds to use later
        this.defaultSpeeds = new Vector3();
        this.defaultSpeeds.x = moveRef.forwardSpeed;
        this.defaultSpeeds.y = moveRef.strafeSpeed;
        this.defaultSpeeds.z = moveRef.backwardSpeed;
    }


    //Overrides the base function from Power_DefaultLogic. FixedUpdate is called once per frame
    protected override void FixedUpdate ()
    {
        //Calls the base update to reduce the cooldown (if applicable)
        base.FixedUpdate();

        //Counts down the amount of time that this speed boost is active
        if(this.durationCounter > 0)
        {
            this.durationCounter -= Time.deltaTime;

            //If the speed boost is over, reduces the player's speed back to normal
            if(this.durationCounter <= 0)
            {
                this.SetDefaultSpeeds();
            }
        }
	}


    //Overrides the base function from Power_DefaultLogic. Called from the Event Manager through the ActivateEVT delegate event
    protected override void ActivatePower(EVTData data_)
    {
        //This power won't activate if the activating player wasn't this one, if it was the other slot, or if this is on cooldown
        if(data_.playerPowerActivate.playerID != this.ownerPlayer ||
            data_.playerPowerActivate.slot != this.slot ||
            this.rechargeCounter > 0)
        {
            return;
        }
        Debug.Log("Speed Shoes used, Slot: " + data_.playerPowerActivate.slot);
        //Sets the players new speeds and starts the cooldown timer and duration timer
        this.SetNewSpeeds();
        this.durationCounter = this.duration;
        this.rechargeCounter = this.rechargeTime;
    }


    //Function called from ActivatePower. Sets this owner player's movement speed to higher values
    private void SetNewSpeeds()
    {
        Player_2DMovement moveRef = this.transform.parent.GetComponent<Player_2DMovement>();
        moveRef.forwardSpeed = this.fastForwardSpeed;
        moveRef.strafeSpeed = this.fastStrafeSpeed;
        moveRef.backwardSpeed = this.fastBackwardSpeed;
    }


    //Function called from FixedUpdate. Sets this owner player's movement speed to the default values
    private void SetDefaultSpeeds()
    {
        Player_2DMovement moveRef = this.transform.parent.GetComponent<Player_2DMovement>();
        moveRef.forwardSpeed = this.defaultSpeeds.x;
        moveRef.strafeSpeed = this.defaultSpeeds.y;
        moveRef.backwardSpeed = this.defaultSpeeds.z;
    }
}
