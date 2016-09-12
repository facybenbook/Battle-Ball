/*
file:   Manager_ControllerInputManager.cs
author: Mitch Regan
brief:
    Component that is always attached to the GlobalDataHolder prefab so that it remains unchanged
    when we transition to different scenes. This holds a Controller Input class for each player's
    controller so that we have a significantly more convenient place to find the input for controllers
    than Unity's default input manager.
*/
using UnityEngine;
using System.Collections;

public class Manager_ControllerInputManager : MonoBehaviour
{
    //A static reference to this instance of the Controller Input Manager so that we can reference it at any time
    static public Manager_ControllerInputManager inputManagerInstance;

    //Static Controller Input classes for each player's controller so that we can find their input at any time
    static public Manager_ControllerInput p1Controller;
    static public Manager_ControllerInput p2Controller;
    static public Manager_ControllerInput p3Controller;
    static public Manager_ControllerInput p4Controller;
    static public Manager_ControllerInput p5Controller;
    static public Manager_ControllerInput p6Controller;
    static public Manager_ControllerInput p7Controller;
    static public Manager_ControllerInput p8Controller;



    // Use this for initialization
    void Awake ()
    {
        //Does nothing if there's already a static instance of a Controller Input Manager
        if (inputManagerInstance != null)
            return;

        //If there isn't already a static instance of a Controller Input Manager, this becomes the static instance
        inputManagerInstance = GetComponent<Manager_ControllerInputManager>();

        //Creates new Controller Inputs for each player controller
        p1Controller = new Manager_ControllerInput();
        p1Controller.SetPlayerID(Players.P1);
        
        p2Controller = new Manager_ControllerInput();
        p2Controller.SetPlayerID(Players.P2);
        
        p3Controller = new Manager_ControllerInput();
        p3Controller.SetPlayerID(Players.P3);
        
        p4Controller = new Manager_ControllerInput();
        p4Controller.SetPlayerID(Players.P4);

        p5Controller = new Manager_ControllerInput();
        p5Controller.SetPlayerID(Players.P5);

        p6Controller = new Manager_ControllerInput();
        p6Controller.SetPlayerID(Players.P6);
        
        p7Controller = new Manager_ControllerInput();
        p7Controller.SetPlayerID(Players.P7);

        p8Controller = new Manager_ControllerInput();
        p8Controller.SetPlayerID(Players.P8);

    }
	

    //Update is called every frame and updates the Controller Input classes, since they don't inherit from Monobehavior
    void Update()
    {
        p1Controller.LogicUpdate();
        p2Controller.LogicUpdate();
        p3Controller.LogicUpdate();
        p4Controller.LogicUpdate();
        p5Controller.LogicUpdate();
        p6Controller.LogicUpdate();
        p7Controller.LogicUpdate();
        p8Controller.LogicUpdate();
    }

	
    //Used to disable all player input (NOTE: Individual controllers can be disabled through their static reference)
    public void DisableAllPlayerInput()
    {
        p1Controller.DisableInput();
        p2Controller.DisableInput();
        p3Controller.DisableInput();
        p4Controller.DisableInput();
        p5Controller.DisableInput();
        p6Controller.DisableInput();
        p7Controller.DisableInput();
        p8Controller.DisableInput();
    }


    //Used to re-enable all player input (NOTE: Individual controllers can be enabled through their static reference)
    public void EnableAllPlayerInput()
    {
        p1Controller.EnableInput();
        p2Controller.EnableInput();
        p3Controller.EnableInput();
        p4Controller.EnableInput();
        p5Controller.EnableInput();
        p6Controller.EnableInput();
        p7Controller.EnableInput();
        p8Controller.EnableInput();
    }
}
