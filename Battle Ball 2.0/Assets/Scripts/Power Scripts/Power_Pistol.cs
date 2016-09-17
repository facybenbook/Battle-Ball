using UnityEngine;
using System.Collections;

public class Power_Pistol : Power_DefaultLogic
{
    //A reference to the bullet prefab that this script shoots
    public GameObject bullet;

    //The number of bullets in a clip for this gun
    public uint clipSize = 6;
    private uint bulletsInClip = 0;

    //The minimum amount of time that has to pass between bullets firing
    public float fireSpeed = 0.2f;
    private float currentFireTime = 0;

    //The amount of force exerted on the player when fired
    public float recoilForce = 10;
    //A private reference to this owner's rigidbody so we can have recoil
    private Rigidbody2D playerRigidBody;



	// Use this for initialization
	private void Start ()
    {
        //Loads all bullets in the clip to max
        this.bulletsInClip = this.clipSize;

        //Gets the reference to the player's rigidbody
        this.playerRigidBody = this.transform.parent.GetComponent<Rigidbody2D>();
	}


    //Overrides the base function from Power_DefaultLogic. FixedUpdate is called once per frame
    protected override void FixedUpdate()
    {
        //Counts down the power cooldown (reload time)
        if(this.rechargeCounter > 0)
        {
            this.rechargeCounter -= Time.deltaTime;

            //If we're done reloading, the clip is filled up
            if(this.rechargeCounter <= 0)
            {
                this.bulletsInClip = this.clipSize;
            }
        }
    }


    //Overrides the base function from Power_DefaultLogic. Called from the Event Manager through the ActivateEVT delegate event
    protected override void ActivatePower(EVTData data_)
    {
        //This power won't activate if the activating player wasn't this one, if it was the other slot, or if this is on cooldown
        if (data_.playerPowerActivate.playerID != this.ownerPlayer ||
            data_.playerPowerActivate.slot != this.slot ||
            this.rechargeCounter > 0 ||
            this.currentFireTime > 0)
        {
            return;
        }

        //Spawns in a new bullet in front of the player
        Debug.Log("Offset the bullet in front of the player");
        Vector3 spawnLoc = this.transform.position;
        GameObject newBullet = GameObject.Instantiate(this.bullet, spawnLoc, this.transform.rotation) as GameObject;

        //Forces the player backward to give this gun recoil
        Debug.Log("Add backwards recoil");

        //Subtracts a bullet from the current clip
        this.bulletsInClip -= 1;

        //If we still have bullets in the clip, the fire speed cooldown is started
        if(this.bulletsInClip > 0)
        {
            this.currentFireTime = this.fireSpeed;
        }
        //If no bullets are left, then the recharge counter is started (basically reload time)
        else
        {
            this.rechargeCounter = this.rechargeTime;
        }
    }
}
