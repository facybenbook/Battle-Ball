using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player_IDTag))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player_2DMovement : MonoBehaviour
{
    //Reference to the controller that gives this component inputs
    private Manager_ControllerInput playerController;

    //The speed when moving the same direction that we're looking
    public float forwardSpeed = 40;
    //The speed when moving backward from where we're looking
    public float backwardSpeed = 20;
    //The speed when moving sideways from where we're looking
    public float strafeSpeed = 30;

    //The current speed that this object is moving
    private Vector2 currentSpeed;

    //The max speed this player object moves
    public float maxSpeed = 60;

    //The buffer zone for angles that determine movement speed
    public float speedBufferAngle = 15;

    //The amount of drag applied to slow this object's movement
    [Range(0,1)]
    public float movementDrag = 0.95f;

    //The deadzone for the left stick where movement isn't registered
    [Range(0, 1)]
    public float inputDeadzone = 0.2f;

    //The direction that we're facing in degrees
    private float angleFacing = 0;
    //The direction that we're moving in degrees
    private float angleMoving = 0;
    //The difference between the movement and facing angles in degrees
    private float angleDiff = 0;

    //Quick reference to our required rigid body component
    private Rigidbody2D ourRigidBody;



	// Use this for initialization
	private void Awake ()
    {
        //Gets the reference to the correct controller for this player
        this.playerController = this.GetComponent<Player_IDTag>().playerController;

        //Gets the reference to our circle collider
        this.ourRigidBody = this.GetComponent<Rigidbody2D>();
	}
	

	// Update is called once per frame. Using FIEXED update because it handles changes in timescale better than regular update
	private void FixedUpdate ()
    {
        //Applies drag to the current movement
        this.ourRigidBody.velocity = this.ourRigidBody.velocity * this.movementDrag;

        //Rotates this object to face the direction that the right stick is facing
        if (Mathf.Abs(Mathf.Sqrt(this.playerController.RightStick.x * this.playerController.RightStick.x +
                                    this.playerController.RightStick.y * this.playerController.RightStick.y)) > this.inputDeadzone)
        {
            this.transform.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(-this.playerController.RightStick.y, this.playerController.RightStick.x) * Mathf.Rad2Deg);
        }

        //If the left stick on this player's controller is within the deadzone, no movement happens
        if (Mathf.Abs(Mathf.Sqrt(this.playerController.LeftStick.x * this.playerController.LeftStick.x +
                                    this.playerController.LeftStick.y * this.playerController.LeftStick.y)) < this.inputDeadzone)
        {
            return;
        }

        //Moves this object based on the left stick
        this.ApplyMovement();
	}


    //Function called from FixedUpdate. Determines movement based on the controller's left stick and the direction we're facing
    private void ApplyMovement()
    {
        //Finding the angle that we're currently facing, moving, and then gets the difference between them
        this.angleFacing = this.transform.localEulerAngles.z;
        this.angleMoving = Mathf.Atan2(this.playerController.LeftStick.y, this.playerController.LeftStick.x) * Mathf.Rad2Deg;
        this.angleDiff = Mathf.DeltaAngle(this.angleFacing, this.angleMoving);
        Debug.Log("Facing: " + this.angleFacing + ", Moving: :" + this.angleMoving + ", Diff: " + this.angleDiff);

        //Applying movement based on the direction of movement, that direction's given speed, and how far the left stick is tilted
        //Moving forward
        if (this.angleDiff < this.speedBufferAngle)
        {
            //Debug.Log("Forward");
            this.currentSpeed.x = Mathf.Cos(this.playerController.LeftStick.x) * this.forwardSpeed;
            this.currentSpeed.y = Mathf.Sin(this.playerController.LeftStick.y) * this.forwardSpeed;
        }
        //Between forward and strafing
        else if (this.angleDiff < (90 - this.speedBufferAngle))
        {
            //Debug.Log("Forward Strafe");
            float speedPercentX = Mathf.Cos(this.angleDiff) * (this.forwardSpeed - this.strafeSpeed) + this.strafeSpeed;
            float speedPercentY = Mathf.Sin(this.angleDiff) * (this.forwardSpeed - this.strafeSpeed) + this.strafeSpeed;

            this.currentSpeed.x = Mathf.Cos(this.playerController.LeftStick.x) * speedPercentX;
            this.currentSpeed.y = Mathf.Sin(this.playerController.LeftStick.y) * speedPercentY;
        }
        //Strafing
        else if (this.angleDiff < (90 + this.speedBufferAngle))
        {
            //Debug.Log("Strafe");
            this.currentSpeed.x = Mathf.Cos(this.playerController.LeftStick.x) * this.strafeSpeed;
            this.currentSpeed.y = Mathf.Sin(this.playerController.LeftStick.y) * this.strafeSpeed;
        }
        //Between backward and strafing
        else if (this.angleDiff < (180 - this.speedBufferAngle))
        {
            //Debug.Log("Backward Strafe");
            float speedPercentX = Mathf.Cos(this.angleDiff) * (this.strafeSpeed - this.backwardSpeed) + this.backwardSpeed;
            float speedPercentY = Mathf.Sin(this.angleDiff) * (this.strafeSpeed - this.backwardSpeed) + this.backwardSpeed;

            this.currentSpeed.x = Mathf.Cos(this.playerController.LeftStick.x) * speedPercentX;
            this.currentSpeed.y = Mathf.Sin(this.playerController.LeftStick.y) * speedPercentY;
        }
        //Moving backward
        else
        {
            //Debug.Log("Backward");
            this.currentSpeed.x = Mathf.Cos(this.playerController.LeftStick.x) * this.backwardSpeed;
            this.currentSpeed.y = Mathf.Sin(this.playerController.LeftStick.y) * this.backwardSpeed;
        }

        //Applies the new movement to this object's rigid body
        this.ourRigidBody.AddForce(this.currentSpeed);
        //Debug.Log("Velocity: " + this.ourRigidBody.velocity);
    }
}

