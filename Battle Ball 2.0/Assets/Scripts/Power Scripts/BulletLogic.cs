using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class BulletLogic : MonoBehaviour
{
    //The amount of damage that this bullet deals when it hits an enemy
    public int damage = 2;
    //The velocity given to this bullet's rigid body when initialized
    public float moveSpeed = 5;
    //The amount of time before this bullet automatically destroys itself if nothing's hit
    public float lifetime = 5;

    //The unity event invoked when this bullet's lifetime is up
    public UnityEvent lifetimeDoneEvent;
    //The unity event invoked when this bullet collides with another object
    public UnityEvent collisionEvent;

    //A quick reference to this owner's rigid body component
    protected Rigidbody2D ownerRigidBody;
    //The player ID that this bullet will ignore
    [HideInInspector]
    public Players owner = Players.None;

    

	// Use this for initialization
	protected virtual void Start ()
    {
        this.ownerRigidBody = this.GetComponent<Rigidbody2D>();

        //Finds the amount of velocity to move this object
        float xVelocity = Mathf.Cos(this.transform.eulerAngles.z * Mathf.Deg2Rad) * this.moveSpeed;
        float yVelocity = Mathf.Sin(this.transform.eulerAngles.z * Mathf.Deg2Rad) * this.moveSpeed;

        this.ownerRigidBody.AddForce(new Vector2(xVelocity, yVelocity));
    }


    //FixedUpdate is called every frame
    protected virtual void FixedUpdate()
    {
        this.lifetime -= Time.deltaTime;

        //If this bullet's lifetime is up, it triggers the event for when this happens
        if(this.lifetime < 0)
        {
            this.lifetimeDoneEvent.Invoke();
        }
    }
	

	//Function called when this object collides with another object
	protected virtual void OnTriggerEnter2D (Collider2D otherObj_)
    {
        //If the other object hit was a player
	    if(otherObj_.GetComponent<Player_IDTag>() != null)
        {
            //If the player hit wasn't this bullet's owner (the one who fired it), they take damage
            if(otherObj_.GetComponent<Player_IDTag>().playerID != this.owner)
            {
                otherObj_.GetComponent<Player_Health>().DamagePlayer(this.damage);
            }
        }

        //Triggers the event for when this bullet collides with something
        this.collisionEvent.Invoke();
    }
}
