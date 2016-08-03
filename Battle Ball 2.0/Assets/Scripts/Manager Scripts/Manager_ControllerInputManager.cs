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
    static public Manager_ControllerInputManager InputManagerInstance;

    //Static Controller Input classes for each player's controller so that we can find their input at any time
    static public Manager_ControllerInput P1Controller;
    static public Manager_ControllerInput P2Controller;
    static public Manager_ControllerInput P3Controller;
    static public Manager_ControllerInput P4Controller;


	// Use this for initialization
	void Start ()
    {
        //Does nothing if there's already a static instance of a Controller Input Manager
        if (InputManagerInstance != null)
            return;

        //If there isn't already a static instance of a Controller Input Manager, this becomes the static instance
        InputManagerInstance = GetComponent<Manager_ControllerInputManager>();

        //Creates new Controller Inputs for each player controller
        P1Controller = new Manager_ControllerInput();
        P1Controller.SetPlayerID(Players.P1);
        
        P2Controller = new Manager_ControllerInput();
        P2Controller.SetPlayerID(Players.P2);
        
        P3Controller = new Manager_ControllerInput();
        P3Controller.SetPlayerID(Players.P3);
        
        P4Controller = new Manager_ControllerInput();
        P4Controller.SetPlayerID(Players.P4);

    }
	

    //Update is called every frame and updates the Controller Input classes, since they don't inherit from Monobehavior
    void Update()
    {
        P1Controller.LogicUpdate();
        P2Controller.LogicUpdate();
        P3Controller.LogicUpdate();
        P4Controller.LogicUpdate();
    }

	
    //Used to disable all player input (NOTE: Individual controllers can be disabled through their static reference)
    public void DisableAllPlayerInput()
    {
        P1Controller.DisableInput();
        P2Controller.DisableInput();
        P3Controller.DisableInput();
        P4Controller.DisableInput();
    }


    //Used to re-enable all player input (NOTE: Individual controllers can be enabled through their static reference)
    public void EnableAllPlayerInput()
    {
        P1Controller.EnableInput();
        P2Controller.EnableInput();
        P3Controller.EnableInput();
        P4Controller.EnableInput();
    }


    //Toggles P1 Left Stick's Y to inverted and not inverted. (NOTE: This function is available here because UI elements can't access individual controllers)
    public void P1LeftInvertY(bool inverted_)
    {
        P1Controller.InvertLeftY(inverted_);
    }


    //Toggles P1 Right Stick's Y to inverted and not inverted. (NOTE: This function is available here because UI elements can't access individual controllers)
    public void P1RightInvertY(bool inverted_)
    {
        P1Controller.InvertRightY(inverted_);
    }


    //Toggles P1 Left Stick's Y to inverted and not inverted. (NOTE: This function is available here because UI elements can't access individual controllers)
    public void P2LeftInvertY(bool inverted_)
    {
        P2Controller.InvertLeftY(inverted_);
    }


    //Toggles P2 Right Stick's Y to inverted and not inverted. (NOTE: This function is available here because UI elements can't access individual controllers)
    public void P2RightInvertY(bool inverted_)
    {
        P2Controller.InvertRightY(inverted_);
    }


    //Toggles P3 Left Stick's Y to inverted and not inverted. (NOTE: This function is available here because UI elements can't access individual controllers)
    public void P3LeftInvertY(bool inverted_)
    {
        P3Controller.InvertLeftY(inverted_);
    }


    //Toggles P3 Right Stick's Y to inverted and not inverted. (NOTE: This function is available here because UI elements can't access individual controllers)
    public void P3RightInvertY(bool inverted_)
    {
        P3Controller.InvertRightY(inverted_);
    }


    //Toggles P4 Left Stick's Y to inverted and not inverted. (NOTE: This function is available here because UI elements can't access individual controllers)
    public void P4LeftInvertY(bool inverted_)
    {
        P4Controller.InvertLeftY(inverted_);
    }


    //Toggles P4 Right Stick's Y to inverted and not inverted. (NOTE: This function is available here because UI elements can't access individual controllers)
    public void P4RightInvertY(bool inverted_)
    {
        P4Controller.InvertRightY(inverted_);
    }


    //Sets the sensitivity for P1's camera (NOTE: This function is available here because UI elements can't access individual controllers)
    public void P1Sensitivity(float newSensitivity_)
    {
        P1Controller.SetLookSensitivity(newSensitivity_);
    }


    //Sets the sensitivity for P2's camera (NOTE: This function is available here because UI elements can't access individual controllers)
    public void P2Sensitivity(float newSensitivity_)
    {
        P2Controller.SetLookSensitivity(newSensitivity_);
    }


    //Sets the sensitivity for P3's camera (NOTE: This function is available here because UI elements can't access individual controllers)
    public void P3Sensitivity(float newSensitivity_)
    {
        P3Controller.SetLookSensitivity(newSensitivity_);
    }


    //Sets the sensitivity for P4's camera (NOTE: This function is available here because UI elements can't access individual controllers)
    public void P4Sensitivity(float newSensitivity_)
    {
        P4Controller.SetLookSensitivity(newSensitivity_);
    }
}
