using UnityEngine;
using System.Collections;

public class Gameplay_SpawnObjects : MonoBehaviour
{
    //True if this spawns objects when activated
    public bool spawnOnStart = false;

    //The game object prefab that this script will spawn
    public GameObject objectToSpawn;

    //The number of objects to spawn
    public uint numberToSpawn = 1;

    //The angle variance that objects can be spawned
    public float angleVariance = 0;

    //The minimum and maximum offset that objects can spawn from this spawner
    public Vector2 offsetMinMax = new Vector2(0, 0);



	// Use this for initialization
	private void Start ()
    {
        //Starts spawning objects if told to do so on Start
        if (this.spawnOnStart)
            this.Spawn();
	}


    //Function can be called from Awake and externally. Spawns the designated prefab
    public void Spawn()
    {
        //Creating variables to hold the random angle and offset
        float spawnAngle;
        float offsetDist;
        //Creating a variable to hold the spawn location
        Vector3 spawnLoc;

        //Loops through to spawn as many objects as we should
        for(uint o = 0; o < this.numberToSpawn; ++o)
        {
            spawnAngle = Random.Range(-this.angleVariance, this.angleVariance);
            offsetDist = Random.Range(this.offsetMinMax.x, this.offsetMinMax.y);

            //Finds the position to spawn the new object at
            spawnLoc = this.transform.position;
            spawnLoc += new Vector3(Mathf.Cos( (this.transform.eulerAngles.z + spawnAngle) * Mathf.Deg2Rad) * offsetDist,
                                    Mathf.Sin( (this.transform.eulerAngles.z + spawnAngle) * Mathf.Deg2Rad) * offsetDist,
                                    0);
        }
    }
}
