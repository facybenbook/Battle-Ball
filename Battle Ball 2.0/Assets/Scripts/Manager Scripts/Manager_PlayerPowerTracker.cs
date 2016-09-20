using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manager_PlayerPowerTracker : MonoBehaviour
{
    //Dictionary of lists for each player that tracks which powers they have equipped
    [HideInInspector]
    public Dictionary<Players, List<Powers>> playerList = new Dictionary<Players, List<Powers>>(8)
    {
        {Players.P1, new List<Powers>(2) { Powers.Rifle, Powers.Pistol } },
        {Players.P2, new List<Powers>(2) { Powers.None, Powers.None } },
        {Players.P3, new List<Powers>(2) { Powers.None, Powers.None } },
        {Players.P4, new List<Powers>(2) { Powers.None, Powers.None } },
        {Players.P5, new List<Powers>(2) { Powers.None, Powers.None } },
        {Players.P6, new List<Powers>(2) { Powers.None, Powers.None } },
        {Players.P7, new List<Powers>(2) { Powers.None, Powers.None } },
        {Players.P8, new List<Powers>(2) { Powers.None, Powers.None } }
    };



    //Function called externally. Changes which power a player has equipped
    public void SetPlayerPower(Players playerID_ = Players.None, PowerSlot slot_ = PowerSlot.MainPower, Powers newPower_ = Powers.None)
    {
        //Does nothing if no player is selected
        if (playerID_ == Players.None)
            return;

        //Gets a reference to the player powers we're changing
        List<Powers> selectedPower = new List<Powers>(2);

        if(this.playerList.TryGetValue(playerID_, out selectedPower) )
        {
            //Sets the slot for the new power
            if (slot_ == PowerSlot.MainPower)
            {
                selectedPower[0] = newPower_;
            }
            else
            {
                selectedPower[1] = newPower_;
            }
        }
    }
}



//Enum for which slot a power is in
public enum PowerSlot
{
    MainPower,
    SecondaryPower
}


//Enum for all of the different powers that players can use
public enum Powers
{
    None,
    Pistol,
    Rifle,
    Shotgun,
    Bomb,
    SpeedShoes,
    RicochetBlade,
    Sword,
    Shield,
    Hammer,
    Boomerang,
    Goo
}
