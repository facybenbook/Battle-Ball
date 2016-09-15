using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Player_IDTag))]
public class Player_PowerSpawner : MonoBehaviour
{
    //Dictionary of power object prefabs to spawn on this player
    public Dictionary<Powers, GameObject> powerObjects;

    //Reference to this player's ID so that the spawned powers know which player controlls them
    private Players playerID;
    private List<Powers> playerPowers;



	// Use this for initialization
	private void Awake ()
    {
        this.playerID = this.GetComponent<Player_IDTag>().playerID;

        //As long as a reference to this player is found in the Player Power Tracker list, spawns 
        if(Manager_GlobalData.globalData.GetComponent<Manager_PlayerPowerTracker>().playerList.TryGetValue(this.playerID, out this.playerPowers))
        {
            this.SetMainPower(this.playerPowers[0]);
            this.SetSecondaryPower(this.playerPowers[1]);
        }
	}


    //Function called from Awake. Spawns this player's main power object
    public void SetMainPower(Powers power1_ = Powers.None)
    {
        if (power1_ == Powers.None)
            return;

        //Creates an instance of the power object's prefab, parents it to this player's location, and tells it to listen to this player
        GameObject powerPrefab;
        if(this.powerObjects.TryGetValue(power1_, out powerPrefab))
        {
            GameObject power1Obj = GameObject.Instantiate(powerPrefab, this.transform.position, this.transform.rotation) as GameObject;
            power1Obj.transform.SetParent(this.transform);
            power1Obj.GetComponent<Power_DefaultLogic>().SetOwnerPlayer(this.playerID);
        }
    }


    //Function called from Awake. Spawns this player's secondary power object
    public void SetSecondaryPower(Powers power2_ = Powers.None)
    {
        if (power2_ == Powers.None)
            return;

        //Creates an instance of the power object's prefab, parents it to this player's location, and tells it to listen to this player
        GameObject powerPrefab;
        if (this.powerObjects.TryGetValue(power2_, out powerPrefab))
        {
            GameObject power2Obj = GameObject.Instantiate(powerPrefab, this.transform.position, this.transform.rotation) as GameObject;
            power2Obj.transform.SetParent(this.transform);
            power2Obj.GetComponent<Power_DefaultLogic>().SetOwnerPlayer(this.playerID);
        }
    }
}
