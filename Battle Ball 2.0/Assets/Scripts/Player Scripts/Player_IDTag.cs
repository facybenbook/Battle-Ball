using UnityEngine;
using System.Collections;

public class Player_IDTag : MonoBehaviour
{
    //Which player controller moves this object
    public Players playerID = Players.P1;

    //Which team this player is currently on
    public TeamColors playerTeam = TeamColors.None;

    //Reference to the controller that gives this player inputs
    public Manager_ControllerInput playerController;

    private DelegateEvent<EVTData> changeTeamEVT;



	// Use this for initialization
	private void Awake ()
    {
        //Gets the static reference from the Controller Input Manager for the correct controller
        switch (this.playerID)
        {
            case Players.P1:
                this.playerController = Manager_ControllerInputManager.P1Controller;
                break;
            case Players.P2:
                this.playerController = Manager_ControllerInputManager.P2Controller;
                break;
            case Players.P3:
                this.playerController = Manager_ControllerInputManager.P3Controller;
                break;
            case Players.P4:
                this.playerController = Manager_ControllerInputManager.P4Controller;
                break;
            case Players.P5:
                this.playerController = Manager_ControllerInputManager.P5Controller;
                break;
            case Players.P6:
                this.playerController = Manager_ControllerInputManager.P6Controller;
                break;
            case Players.P7:
                this.playerController = Manager_ControllerInputManager.P7Controller;
                break;
            case Players.P8:
                this.playerController = Manager_ControllerInputManager.P8Controller;
                break;
        }
    }


    //Function called when this component is enabled
    private void OnEnable()
    {

    }

    //Function called when this component is disabled
    private void OnDisable()
    {

    }
}


//Enums to reference different players
public enum Players
{
    P1,
    P2,
    P3,
    P4,
    P5,
    P6,
    P7,
    P8,
    None,
    All
}


//Enums to reference different teams of players identified by the team color
public enum TeamColors
{
    None,
    All,
    Red,
    Blue,
    Green,
    Orange,
    Purple,
    Yellow,
    Black,
    White
}