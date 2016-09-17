using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Player_IDTag))]
public class Player_PowerSpawner : MonoBehaviour
{
    //List of power names that coorilate to the powerObjects list
    public List<Powers> powerNames;
    //List of power object prefabs to spawn on this player
    public List<GameObject> powerObjects;

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
        if(this.powerNames.Contains(power1_))
        {
            powerPrefab = this.powerObjects[this.powerNames.IndexOf(power1_)];
            GameObject power1Obj = GameObject.Instantiate(powerPrefab, this.transform.position, this.transform.rotation) as GameObject;
            power1Obj.transform.SetParent(this.transform);
            power1Obj.GetComponent<Power_DefaultLogic>().SetOwnerPlayer(this.playerID, PowerSlot.MainPower);
        }
    }


    //Function called from Awake. Spawns this player's secondary power object
    public void SetSecondaryPower(Powers power2_ = Powers.None)
    {
        if (power2_ == Powers.None)
            return;

        //Creates an instance of the power object's prefab, parents it to this player's location, and tells it to listen to this player
        GameObject powerPrefab;
        if (this.powerNames.Contains(power2_))
        {
            powerPrefab = this.powerObjects[this.powerNames.IndexOf(power2_)];
            GameObject power1Obj = GameObject.Instantiate(powerPrefab, this.transform.position, this.transform.rotation) as GameObject;
            power1Obj.transform.SetParent(this.transform);
            power1Obj.GetComponent<Power_DefaultLogic>().SetOwnerPlayer(this.playerID, PowerSlot.SecondaryPower);
        }
    }
}
