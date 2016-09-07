using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Misc_PauseGameLogic : MonoBehaviour
{
    public bool RestrictUnpause = false;

    //Boolean determining if the game is currently paused
    [HideInInspector]
    public static bool IsGamePaused = false;

    //The amount of time after pausing the game that a player can unpause (to prevent it from happening multiple times on accident)
    public float PauseDelay = 0.2f;
    private float DelayCount = 0;

    //Event invoked when the game is paused
    public UnityEvent OnPause;
    //Event invoked when the game is unpaused
    public UnityEvent OnUnpause;

    //The ID of the player who paused the game. Only They can unpause it.
    private Players PlayerWhoPaused = Players.None;



	// Use this for initialization
	void Start ()
    {
        transform.position = new Vector3(0,0,0);
	}
	


	// Update is called once per frame
	void Update ()
    {
        //Input will only be taken if the delay is up
        if (DelayCount <= 0)
        {
            //If a player pauses from the keyboard
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseButtonHit(Players.P1);
            }
            //If player 1 pauses
            else if (Manager_ControllerInputManager.P1Controller.Start_Button_Pressed)
            {
                PauseButtonHit(Players.P1);
            }
            //If player 2 pauses
            else if (Manager_ControllerInputManager.P2Controller.Start_Button_Pressed)
            {
                PauseButtonHit(Players.P2);
            }
            //If player 3 pauses
            else if (Manager_ControllerInputManager.P3Controller.Start_Button_Pressed)
            {
                PauseButtonHit(Players.P3);
            }
            //If player 4 pauses
            else if (Manager_ControllerInputManager.P4Controller.Start_Button_Pressed)
            {
                PauseButtonHit(Players.P4);
            }
            //If player 5 pauses
            else if (Manager_ControllerInputManager.P5Controller.Start_Button_Pressed)
            {
                PauseButtonHit(Players.P5);
            }
            //If player 6 pauses
            else if (Manager_ControllerInputManager.P6Controller.Start_Button_Pressed)
            {
                PauseButtonHit(Players.P6);
            }
            //If player 7 pauses
            else if (Manager_ControllerInputManager.P7Controller.Start_Button_Pressed)
            {
                PauseButtonHit(Players.P7);
            }
            //If player 8 pauses
            else if (Manager_ControllerInputManager.P8Controller.Start_Button_Pressed)
            {
                PauseButtonHit(Players.P8);
            }
        }
    }



    //Handles the logic of what happens when the pause button is hit based on who hit the pause button
    private void PauseButtonHit(Players playerWhoPushedButton_)
    {
        //If the game is currently paused and we don't care who unpauses it, we're unpausing it
        if(IsGamePaused && !RestrictUnpause)
        {
            IsGamePaused = false;
            OnUnpause.Invoke();
            Time.timeScale = 1;
        }
        //If the game is currently paused and this is the same player who paused it, we're unpausing it
        else if (IsGamePaused && RestrictUnpause && PlayerWhoPaused == playerWhoPushedButton_)
        {
            IsGamePaused = false;
            OnUnpause.Invoke();
            Time.timeScale = 1;
        }
        //If the game is currently not paused, we're pausing it and keeping track of who paused it
        else
        {
            IsGamePaused = true;
            OnPause.Invoke();
            PlayerWhoPaused = playerWhoPushedButton_;
            Time.timeScale = 0;
        }
    }



    //Function called from external sources to unpause the game regardless of who pauses it
    public void ExternalUnpause()
    {
        if(IsGamePaused)
        {
            IsGamePaused = false;
            OnUnpause.Invoke();
            Time.timeScale = 1;
        }
    }
}
