using UnityEngine;
using System.Collections;

public class Camera_Weight : MonoBehaviour
{
    //Amount of priority for the camera to follow this object
    public int weight = 1;
    //Distance from the camera object that this object is added to the weight system
    public float addDistance = 10f;
    //Distance away from the camera that this object is dropped from the weight system.
    //This should ALWAYS be greater than the AddDistance
    public float dropDistance = 11f;
    //True if this object should always be on screen
    public bool alwaysOnScreen = false;
    //Reference to the camera object
    private GameObject cameraRef;
    //Bool that determines if this object is currently being tracked
    private bool isOnScreen = false;



    // Use this for initialization
    public void Awake ()
    {
        //If this should always remain on screen, adds it to the camera's list of objects to follow
        if(this.alwaysOnScreen)
        {
            CameraMount_FollowWeights.AddWeightedObject(this.gameObject);
            this.enabled = false;
        }

        //Makes sure the Drop Distance is greater than the Add Distance to prevent errors with the weight system
        if(this.dropDistance <= this.addDistance)
        {
            this.dropDistance = this.addDistance + 1;
        }

        //Gets the reference to the game object that the camera is on
        this.cameraRef = CameraMount_FollowWeights.thisCam.gameObject;
    }
	


	// Update is called once per frame
	private void FixedUpdate ()
    {
        //Determines if this object should be added to the camera. We don't care if the camera's null, because HandleCamera handles that for us
        this.isOnScreen = HandleCamera(this.cameraRef, this.isOnScreen);
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
            Vector2 thisObjPos = new Vector2(this.transform.position.x, this.transform.position.y);
            Vector2 camObjPos = new Vector2(cameraObj_.transform.position.x, cameraObj_.transform.position.y);
            float dist = Vector2.Distance(thisObjPos, camObjPos);

            //If the distance is greater than the drop distance, it won't be tracked anymore
            if(dist >= this.dropDistance)
            {
                CameraMount_FollowWeights.DropWeightedObject(this.gameObject);
                isOnScreen = false;
            }
        }
        //If this object isn't being tracked by the designated camera, finds out if it should be added
        else
        {
            Vector2 thisObjPos = new Vector2(this.transform.position.x, this.transform.position.y);
            Vector2 camObjPos = new Vector2(cameraObj_.transform.position.x, cameraObj_.transform.position.y);
            float dist = Vector2.Distance(thisObjPos, camObjPos);

            //If the distance is less than the add distance, it will be tracked
            if (dist <= this.addDistance)
            {
                CameraMount_FollowWeights.AddWeightedObject(this.gameObject);
                isOnScreen = true;
            }
        }

        return isOnScreen;
    }
}
