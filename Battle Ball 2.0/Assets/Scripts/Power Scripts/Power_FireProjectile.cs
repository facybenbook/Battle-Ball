using UnityEngine;
using System.Collections;

public class Power_FireProjectile : Power_DefaultLogic
{
    //A reference to the projectile prefab that this script shoots
    public GameObject projectileToShoot;

    //The number of projectiles that can be fired before having to reload
    public uint clipSize = 6;
    private uint projectilesInClip = 0;

    //The minimum amount of time that has to pass between firing projectiles
    public float fireSpeed = 0.2f;
    private float currentFireTime = 0;

    //The amount of force exerted on the player when fired
    public float recoilForce = 10;

    //The distance projectiles are offset from the player when spawned
    public float spawnOffset = 0.8f;

    //The variance in angle of the projectile when fired
    public float angleVariance = 0;



    // Use this for initialization
    private void Start()
    {
        //Loads all projectiles in the clip to max
        this.projectilesInClip = this.clipSize;
    }


    //Overrides the base function from Power_DefaultLogic. FixedUpdate is called once per frame
    protected override void FixedUpdate()
    {
        //Counts down the power cooldown (reload time)
        if (this.rechargeCounter > 0)
        {
            this.rechargeCounter -= Time.deltaTime;

            //If we're done reloading, the clip is filled up
            if (this.rechargeCounter <= 0)
            {
                this.projectilesInClip = this.clipSize;
            }
        }

        //Counts down the fire time
        if (this.currentFireTime > 0)
        {
            this.currentFireTime -= Time.deltaTime;
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

        //Spawns the projectile to fire
        this.FireProjectile();

        //Forces the player backward to give this gun recoil
        this.Recoil();

        //Subtracts a projectile from the current clip
        this.projectilesInClip -= 1;

        //If we still have projectiles in the clip, the fire speed cooldown is started
        if (this.projectilesInClip > 0)
        {
            this.currentFireTime = this.fireSpeed;
        }
        //If no projectiles are left, then the recharge counter is started (basically reload time)
        else
        {
            this.rechargeCounter = this.rechargeTime;
        }
    }


    //Function called from ActivatePower. Spawns a projectile, aims it, and tells it which player fired it
    protected virtual void FireProjectile()
    {
        //Spawns in a new projectile in front of the player
        Vector3 spawnLoc = this.transform.position;
        spawnLoc += new Vector3(Mathf.Cos(this.transform.eulerAngles.z * Mathf.Deg2Rad) * this.spawnOffset * this.transform.parent.localScale.x,
                                Mathf.Sin(this.transform.eulerAngles.z * Mathf.Deg2Rad) * this.spawnOffset * this.transform.parent.localScale.x,
                                0);
        GameObject newProjectile = GameObject.Instantiate(this.projectileToShoot, spawnLoc, this.transform.rotation) as GameObject;
        newProjectile.GetComponent<BulletLogic>().owner = this.ownerPlayer;

        //Adds angle variance for the projectile
        if (this.angleVariance != 0)
        {
            float offset = Random.Range(-this.angleVariance, this.angleVariance);
            newProjectile.transform.eulerAngles += new Vector3(0, 0, offset);
        }
    }


    //Function called from ActivatePower. Pushes the player owner backward whenever a shot is fired
    protected virtual void Recoil()
    {
        //Gets the angle for the opposite direction the player's facing
        float backwardAngle = this.transform.eulerAngles.z + 180;

        //Finds the force of the recoil based on the backward angle
        Vector2 newForce = new Vector2();
        newForce.x = Mathf.Cos(backwardAngle * Mathf.Deg2Rad) * this.recoilForce;
        newForce.y = Mathf.Sin(backwardAngle * Mathf.Deg2Rad) * this.recoilForce;

        //Gets the reference to the player's rigidbody and moves it backward
        Rigidbody2D playerRigidBody = this.transform.parent.GetComponent<Rigidbody2D>();
        playerRigidBody.AddForce(newForce);
    }
}
