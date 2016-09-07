using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class Misc_ButtonMultiplayerLogic : MonoBehaviour
{
    //XYZ pos offset where P1's hilight is
    public Vector3 P1Offset = new Vector3(-75,15,0);
    //XYZ pos offset where P2's hilight is
    public Vector3 P2Offset = new Vector3(75, 15, 0);
    //XYZ pos offset where P3's hilight is
    public Vector3 P3Offset = new Vector3(-75, -15, 0);
    //XYZ pos offset where P4's hilight is
    public Vector3 P4Offset = new Vector3(75, -15, 0);

    //The XY width and height of highlights on this button
    public Vector2 HilightWidthHeight = new Vector2(30,30);

    //The players who are allowed to press this button
    public List<Players> AllowedPlayers = new List<Players>() {Players.All};

    //Bools tracking which players are hilighting this UI element
    private bool P1Hilighting = false;
    private bool P2Hilighting = false;
    private bool P3Hilighting = false;
    private bool P4Hilighting = false;

    private bool DropDownShowing = false;
    private Players DropDownPlayer = Players.None;



    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}


    //Activates this button as though it were clicked
    public void PressThisButton(Players playerWhoPressed_)
    {
        //If all players are allowed to press this button, OR if the person who is pressing the button is allowed to, this button is activated
        if(AllowedPlayers.Contains(Players.All) || AllowedPlayers.Contains(playerWhoPressed_))
        {
            GetComponent<Button>().onClick.Invoke();
        }
    }


    //Changes the value of this toggle
    public void ToggleThisToggle(Players playerWhoToggled_)
    {
        if(AllowedPlayers.Contains(Players.All) || AllowedPlayers.Contains(playerWhoToggled_))
        {
            GetComponent<Toggle>().isOn = !GetComponent<Toggle>().isOn;
            GetComponent<Toggle>().onValueChanged.Invoke(GetComponent<Toggle>().isOn);
        }
    }


    //Starts the dropdown of this dropdown menu
    public void DropMenuDown(Players playerWhoPressed_)
    {
        //If the drop down menu isn't showing
        if (!DropDownShowing)
        {
            if (AllowedPlayers.Contains(Players.All) || AllowedPlayers.Contains(playerWhoPressed_))
            {
                DropDownPlayer = playerWhoPressed_;
                GetComponent<Dropdown>().Show();
            }
        }
        //If the drop down menu is currently showing, only the player who showed it can hide it
        else if(DropDownPlayer == playerWhoPressed_)
        {
            DropDownPlayer = Players.None;
            GetComponent<Dropdown>().Hide();
        }
    }


    //Handles if this UI element is hilighted based on what player is hilighting it and if they're moving on or off this element
    public void HandleHilight(bool hilightOn_, Players playerID_)
    {
        switch(playerID_)
        {
            case Players.P1:
                P1Hilighting = hilightOn_;
                break;
            case Players.P2:
                P2Hilighting = hilightOn_;
                break;
            case Players.P3:
                P3Hilighting = hilightOn_;
                break;
            case Players.P4:
                P4Hilighting = hilightOn_;
                break;
        }

        //If any player is hilighting this ui element, it's hilighted
        if(P1Hilighting || P2Hilighting || P3Hilighting || P4Hilighting)
        {
            if(GetComponent<Button>() != null)
            {
                GetComponent<Button>().targetGraphic.color = GetComponent<Button>().colors.highlightedColor;
            }
            else if(GetComponent<Slider>() != null)
            {
                GetComponent<Slider>().targetGraphic.color = GetComponent<Slider>().colors.highlightedColor;
            }
            else if(GetComponent<Dropdown>() != null)
            {
                GetComponent<Dropdown>().targetGraphic.color = GetComponent<Dropdown>().colors.highlightedColor;
            }
            else if(GetComponent<Toggle>() != null)
            {
                GetComponent<Toggle>().targetGraphic.color = GetComponent<Toggle>().colors.highlightedColor;
            }
        }
        //Otherwise, this ui element isn't hilighted
        else
        {
            if (GetComponent<Button>() != null)
            {
                GetComponent<Button>().targetGraphic.color = GetComponent<Button>().colors.normalColor;
            }
            else if (GetComponent<Slider>() != null)
            {
                GetComponent<Slider>().targetGraphic.color = GetComponent<Slider>().colors.normalColor;
            }
            else if (GetComponent<Dropdown>() != null)
            {
                GetComponent<Dropdown>().targetGraphic.color = GetComponent<Dropdown>().colors.normalColor;
            }
            else if (GetComponent<Toggle>() != null)
            {
                GetComponent<Toggle>().targetGraphic.color = GetComponent<Toggle>().colors.normalColor;
            }
        }
    }
}
