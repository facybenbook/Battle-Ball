using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player_IDTag))]
public class Player_PowerActivator : MonoBehaviour
{
    //Controller button that activates power 1
    public ControllerButtons power1Button = ControllerButtons.X_Button;
    //Controller button that activates power 2
    public ControllerButtons power2Button = ControllerButtons.B_Button;

    //Deadzone for trigger inputs before they activate powers
    [Range(0, 0.95f)]
    public float triggerDeadzone = 0.2f;

    //Reference to this player's controller
    private Manager_ControllerInput playerController;



	// Use this for initialization
	private void Awake ()
    {
        this.playerController = this.GetComponent<Player_IDTag>().playerController;
	}
	

	// Update is called once per frame
	private void FixedUpdate ()
    {
        //Dispatches the event for activating this player's power 1
	    if(this.playerController.CheckButtonPressed(power1Button) || this.playerController.LeftTrigger > this.triggerDeadzone)
        {
            EVTData powerEVT = new EVTData();
            powerEVT.playerPowerActivate = new PlayerPowerActivateEVT(this.playerController.PlayerID, PowerSlot.MainPower, false);
            Manager_EventManager.TriggerEvent(PlayerPowerActivateEVT.eventName, powerEVT);
        }

        //Dispatches the event for activating this player's power 2
        if(this.playerController.CheckButtonPressed(power2Button) || this.playerController.RightTrigger > this.triggerDeadzone)
        {

            EVTData powerEVT = new EVTData();
            powerEVT.playerPowerActivate = new PlayerPowerActivateEVT(this.playerController.PlayerID, PowerSlot.SecondaryPower, true);
            Manager_EventManager.TriggerEvent(PlayerPowerActivateEVT.eventName, powerEVT);
        }
	}
}
