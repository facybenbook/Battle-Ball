using UnityEngine;
using System.Collections;

public class Camera_Weight : MonoBehaviour
{
    //Determines if the distance between objects tracks the Z plane
    public bool Track2D = true;

    //If this is on, player1's camera will always follow it regardless of the Add or Drop distance
    public bool AlwaysRemainOnCam1 = false;
    //If this is on, player2's camera will always follow it retardless of the Add or Drop distance
    public bool AlwaysRemainOnCam2 = false;
    //If this is on, player3's camera will always follow it regardless of the Add or Drop distance
    public bool AlwaysRemainOnCam3 = false;
    //If this is on, player4's camera will always follow it retardless of the Add or Drop distance
    public bool AlwaysRemainOnCam4 = false;

    //Amount of priority for the camera to follow this object
    public int Weight = 1;
    //Distance from the camera object that this object is added to the weight system
    public float AddDistance = 10f;
    //Distance away from the camera that this object is dropped from the weight system.
    //This should ALWAYS be greater than the AddDistance
    public float DropDistance = 11f;

    //References to the Camera_FollowWeightLogic cameras and if this object is currently on the camera's screen
    private GameObject Camera1Ref;
    private bool OnScreen1 = false;
    private GameObject Camera2Ref;
    private bool OnScreen2 = false;
    private GameObject Camera3Ref;
    private bool OnScreen3 = false;
    private GameObject Camera4Ref;
    private bool OnScreen4 = false;



    // Use this for initialization
    void Start ()
    {
        //Finds the static references of all cameras
        Camera1Ref = CameraMount_FollowWeights.Player1Cam;
        Camera2Ref = CameraMount_FollowWeights.Player2Cam;
        Camera3Ref = CameraMount_FollowWeights.Player3Cam;
        Camera4Ref = CameraMount_FollowWeights.Player4Cam;

        //If this object always needs to be tracked by a player, adds their weight to the designated camera
        if (AlwaysRemainOnCam1 && Camera1Ref != null)
            Camera1Ref.GetComponent<CameraMount_FollowWeights>().AddWeightedObject(gameObject);

        if (AlwaysRemainOnCam2 && Camera2Ref != null)
            Camera2Ref.GetComponent<CameraMount_FollowWeights>().AddWeightedObject(gameObject);

        if (AlwaysRemainOnCam3 && Camera3Ref != null)
            Camera3Ref.GetComponent<CameraMount_FollowWeights>().AddWeightedObject(gameObject);

        if (AlwaysRemainOnCam4 && Camera4Ref != null)
            Camera4Ref.GetComponent<CameraMount_FollowWeights>().AddWeightedObject(gameObject);

        //Makes sure the Drop Distance is greater than the Add Distance to prevent errors with the weight system
        if(DropDistance <= AddDistance)
        {
            DropDistance = AddDistance + 1;
        }
    }
	


	// Update is called once per frame
	void FixedUpdate ()
    {
        //Determines if this object should be added to each of the cameras. We don't care if they're null, because HandleCamera handles that for us
        OnScreen1 = HandleCamera(Camera1Ref, OnScreen1);
        OnScreen2 = HandleCamera(Camera1Ref, OnScreen2);
        OnScreen3 = HandleCamera(Camera1Ref, OnScreen3);
        OnScreen4 = HandleCamera(Camera1Ref, OnScreen4);
    }

    
    //Adds and drops this object from the designated camera when it gets in or out of range and returns a bool based on if this object is being tracked by the camera
    private bool HandleCamera(GameObject cameraObj_, bool onCamera_)
    {
        //If the designated camera doesn't exist, nothing happens
        if (cameraObj_ == null)
            return false;

        bool isOnScreen = onCamera_;

        //If this object is already being tracked by the designated camera, finds out if it should be dropped
        if(onCamera_)
        {
            //Tracks the distance between only X and Y coords
            if(Track2D)
            {
                Vector2 thisObj = new Vector2(transform.position.x, transform.position.y);
                Vector2 camObj = new Vector2(cameraObj_.transform.position.x, cameraObj_.transform.position.y);
                float dist = Vector2.Distance(thisObj, camObj);

                //If the distance is greater than the drop distance, it won't be tracked anymore
                if(dist >= DropDistance)
                {
                    cameraObj_.GetComponent<CameraMount_FollowWeights>().DropWeightedObject(gameObject);
                    isOnScreen = false;
                }
            }
            //Tracks the distance between X, Y, and Z coords
            else
            {
                float dist = Vector3.Distance(transform.position, cameraObj_.transform.position);

                //If the distance is greater than the drop distance it won't be tracked anymore
                if(dist >= DropDistance)
                {
                    cameraObj_.GetComponent<CameraMount_FollowWeights>().DropWeightedObject(gameObject);
                    isOnScreen = false;
                }
            }
        }
        //If this object isn't being tracked by the designated camera, finds out if it should be added
        else
        {
            //Tracks the distance between only X and Y coords
            if (Track2D)
            {
                Vector2 thisObj = new Vector2(transform.position.x, transform.position.y);
                Vector2 camObj = new Vector2(cameraObj_.transform.position.x, cameraObj_.transform.position.y);
                float dist = Vector2.Distance(thisObj, camObj);

                //If the distance is less than the add distance, it will be tracked
                if (dist <= AddDistance)
                {
                    cameraObj_.GetComponent<CameraMount_FollowWeights>().AddWeightedObject(gameObject);
                    isOnScreen = true;
                }
            }
            //Tracks the distance between X, Y, and Z coords
            else
            {
                float dist = Vector3.Distance(transform.position, cameraObj_.transform.position);

                //If the distance is less than the add distance it will be tracked 
                if (dist <= AddDistance)
                {
                    cameraObj_.GetComponent<CameraMount_FollowWeights>().AddWeightedObject(gameObject);
                    isOnScreen = true;
                }
            }
        }

        return isOnScreen;
    }
}
