using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraMount_FollowWeights : MonoBehaviour
{
    //If this camera tracks Z distance
    public bool TrackZDist = false;
    //If this camera follows player 1
    public Players PlayerID = Players.P1;
    //A static reference to player cameras that objects with Camera_Weight can reference
    public static GameObject Player1Cam;
    public static GameObject Player2Cam;
    public static GameObject Player3Cam;
    public static GameObject Player4Cam;

    //An array to hold all of the objects that have the Camera_Weight component on them
    private List<GameObject> WeightObjects;
    //The sum of all weights of objects in the WeightObjects array
    private int WeightSum = 0;
    //The rate that this camera interps to the weight position
    [Range(0, 1.0f)]
    public float InterpSpeed = 0.95f;

    //Reference to this object's camera component
    private Camera WeightCamera;
    //The furthest out the camera can zoom based on object distance
    public float MaxZoom = 60;
    //The furthest in the camera can zoom based on object distance
    public float MinZoom = 30;
    //The furthest distance that affects zoom
    public float MaxZoomDist = 6;
    //The closest distance that affects zoom
    public float MinZoomDist = 0;



    // Use this for initialization
    void Start ()
    {
        //sets the static reference based on what camera this is
	    switch(PlayerID)
        {
            case Players.P1:
                Player1Cam = gameObject;
                break;
            case Players.P2:
                Player2Cam = gameObject;
                break;
            case Players.P3:
                Player3Cam = gameObject;
                break;
            case Players.P4:
                Player4Cam = gameObject;
                break;
            default:
                Player1Cam = gameObject;
                break;
        }

        //Initializes the list to hold weighted objects
        WeightObjects = new List<GameObject>();
        //Makes sure the sum of weights starts at 0
        WeightSum = 0;

        //Stores the reference to this object's camera component
        WeightCamera = GetComponent<Camera>();
	}
	


    //Function called by objects with the Camera_Weight component when they come into range
    public void AddWeightedObject(GameObject objToAdd_)
    {
        //makes sure the object being added has a weight
        if (objToAdd_.GetComponent<Camera_Weight>() == null)
        {
            Debug.LogWarning("WARNING: CameraMount_FollowWeights.AddWeightedObject, object doesn't have a weight");
            return;
        }

        //Adds the weighted object to the list of game objects
        WeightObjects.Add(objToAdd_);
        //Adds the weighted object's weight to the sum of all of them
        WeightSum += objToAdd_.GetComponent<Camera_Weight>().Weight;
    }



    //Function called by objects with Camera_Weight component when they drop out of range
    public void DropWeightedObject(GameObject objToDrop_)
    {
        //makes sure the object being dropped is in the list of game objects in the first place
        if (WeightObjects.Contains(objToDrop_))
        {
            //Removes the weighted object from the list of game objects
            WeightObjects.Remove(objToDrop_);
            //Subtracts the weighted object's weight from the sum of all of them
            WeightSum -= objToDrop_.GetComponent<Camera_Weight>().Weight;
        }
        else
        {
            Debug.LogWarning("WARNING: CameraMount_FollowWeights.DropWeightedObject, object to drop not found");
        }
    }



	// Update is called once per frame
	void FixedUpdate ()
    {
        //Gets the position that this camera needs to go to
        Vector3 interpPos = FindWeightPosition();

        //Finds the difference position
        Vector3 diff;

        if(TrackZDist)
        {
            diff = new Vector3(interpPos.x - transform.position.x,
                               interpPos.y - transform.position.y,
                               interpPos.z - transform.position.z);
        }
        else
        {
            diff = new Vector3(interpPos.x - transform.position.x,
                               interpPos.y - transform.position.y,
                               transform.position.z);
        }

        //Sets this camera's position so that it moves in the direction of the interpPos
        transform.position = transform.position + (diff * InterpSpeed);

        //Sets the camera's field of view based on the furthest tracked object's distance
        WeightCamera.fieldOfView += (FindZoom() - WeightCamera.fieldOfView) * InterpSpeed;
	}



    //Determines the location that this camera goes to each frame
    private Vector3 FindWeightPosition()
    {
        //The position that this function returns
        Vector3 returnPos = new Vector3();

        //Loops through each object in the list and adds its position to the returnPos a number of times equal to its weight
        for(int o = 0; o < WeightObjects.Count; ++o)
        {
            returnPos += (WeightObjects[o].transform.position * WeightObjects[o].GetComponent<Camera_Weight>().Weight);
        }

        //Divides the position by the sum of weights
        returnPos = returnPos / WeightSum;

        return returnPos;
    }


    //Sets the Field of View based on the furthest object's distance each frame
    private float FindZoom()
    {
        float zoom = 0;
        float furthestDist = 0;
        float currentDist = 0;

        //Finds object distances based on their XYZ distance from the camera
        if(TrackZDist)
        {
            //Loops through each tracked object to find the one furthest from this camera
            for(int z = 0; z < WeightObjects.Count; ++z)
            {
                //Stores the distance between the camera and the current object in the list
                currentDist = Vector3.Distance(transform.position, WeightObjects[z].transform.position);

                //If this current distance is the furthest away so far, we store it
                if(currentDist > furthestDist)
                {
                    furthestDist = currentDist;
                }
            }
        }
        //Otherwise ignores distance based on z pos
        else
        {
            //Loops through each tracked object to find the one furthest from this camera
            for(int z = 0; z < WeightObjects.Count; ++z)
            {
                //Stores the distance between the camera and the current object in the list
                currentDist = Vector2.Distance(new Vector2(transform.position.x, transform.position.y),
                                               new Vector2(WeightObjects[z].transform.position.x, WeightObjects[z].transform.position.y));

                //If this current distance is the furthest away so far, we store it
                if(currentDist > furthestDist)
                {
                    furthestDist = currentDist;
                }
            }
        }

        //If the furthest object is at or beyond the max zoom distance, the zoom is set to the highest allowed
        if(furthestDist >= MaxZoomDist)
        {
            zoom = MaxZoom;
        }
        //If the furthest object is at or closer than the min zoom distance, the zoom is set to the lowest allowed
        else if(furthestDist <= MinZoomDist)
        {
            zoom = MinZoom;
        }
        //If the furthest object is between the min and max zoom distance, we find the middleground based on the difference
        else
        {
            float zoomDiff = MaxZoom - MinZoom;
            float distDiff = MaxZoomDist - MinZoomDist;
            float distPercent = (furthestDist / MinZoomDist) / distDiff;

            zoom = (distPercent * zoomDiff) + MinZoom;
        }


        return zoom;
    }
}
