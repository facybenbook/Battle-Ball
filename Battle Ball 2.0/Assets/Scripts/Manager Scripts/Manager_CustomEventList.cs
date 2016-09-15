using UnityEngine;
using UnityEngine.Events;
using System.Collections;


//Class that's sent through all of our custom events as an argument. Contains an instance of all custom classes
public class EVTData
{
    public ButtonPressEVT buttonPress;
    public UISoundCueEVT soundCue;
    public UIMusicCueEVT musicCue;
    public SoundCutoutEVT soundCutout;

    public PlayerDeathEVT playerDeath;
    public PlayerPowerActivateEVT playerPowerActivate;
    public PlayerChangeTeamEVT playerChangeTeam;
    public GoalScoredEVT goalScored;
}


/****************************************************************** UI EVENTS ******************************************************************/

//Event data used whenever a button is pressed and we need to know who pressed what button
public class ButtonPressEVT
{
    //Static event name to use when triggering this event
    public static string eventName = "ButtonPress";

    public RectTransform buttonPressed = null;
    public Players playerWhoPressed = Players.None;
}


//Event data used when a UI element needs to play a sound
public class UISoundCueEVT
{
    //Static event name to use when triggering this event
    public static string eventName = "UISoundCue";

    public AudioClip soundToPlay = null;
    public float soundVolume = 1;
    public Players playerWhoInvoked = Players.None;
}


//Event data used when a UI element needs to play music
public class UIMusicCueEVT 
{
    //Static event name to use when triggering this event
    public static string eventName = "UIMusicCue";

    public AudioClip musicToPlay = null;
    public float musicVolume = 1;
    public Players playerWhoInvoked = Players.None;
}


//used when the sound cuts out after an impactful sound cue
public class SoundCutoutEVT
{
    //Static event name to use when triggering this event
    public static string eventName = "SoundCutout";

    //how long the cutout lasts
    public float stopDuration = 0;
    //How long it takes for sounds to return to normal levels again
    public float fadeInDuration = 0;

    //How low the music volume is set when the cutout initially happens
    [Range(0, 1.0f)]
    public float musicLowPoint = 0;
    //How low the dialogue volume is set when the cutout initially happens
    [Range(0, 1.0f)]
    public float dialogueLowPoint = 0;
    //How low the SFX volume is set when the cutout initially happens
    [Range(0, 1.0f)]
    public float sfxLowPoint = 0;
}


/****************************************************************** GAMEPLAY EVENTS ******************************************************************/

//Event data used when a player dies and we need to know who it was
public class PlayerDeathEVT 
{
    //Static event name to use when triggering this event
    public static string eventName = "PlayerDeath";

    //ID of the player that died and their team
    public Players playerID = Players.None;
    public Teams playerTeam = Teams.None;

    //Constructor function to set the player ID and their team
    public PlayerDeathEVT(Players playerID_ = Players.P1, Teams playerTeam_ = Teams.None)
    {
        this.playerID = playerID_;
        this.playerTeam = playerTeam_;
    }
}


//Event data used when a player uses a power
public class PlayerPowerActivateEVT
{
    //Static event name to use when triggering this event
    public static string eventName = "PlayerPowerActivate";

    //The player that used their power
    public Players playerID = Players.None;
    //The power slot of the used power (Main or Secondary)
    public PowerSlot slot = PowerSlot.MainPower;


    //Constructor for this event
    public PlayerPowerActivateEVT(Players playerID_ = Players.None, PowerSlot slot_ = PowerSlot.MainPower, bool power2Used_ = false)
    {
        this.playerID = playerID_;
        this.slot = slot_;
    }
}


//Event data used when a player changes their team
public class PlayerChangeTeamEVT
{
    //Static event name to use when triggering this event
    public static string eventName = "PlayerChangeTeam";

    //The player that's changing teams
    public Players playerID = Players.None;
    //The team that the player is switching to
    public Teams teamID = Teams.None;
    //The color of this player's team
    public Color teamColor = Color.white;

    //Constructor for this event
    public PlayerChangeTeamEVT(Players playerID_ = Players.None, Teams teamID_ = Teams.None, Color teamColor_ = new Color())
    {
        this.playerID = playerID_;
        this.teamID = teamID_;
        this.teamColor = teamColor_;
    }
}


//Event data used when a player scores a goal
public class GoalScoredEVT
{
    //Static event name to use when triggering this event
    public static string eventName = "GoalScored";

    //The player that scored the goal
    public Players playerID = Players.None;
    //The team that the scoring player is on
    public Teams playerTeamID = Teams.None;
    //The team that a goal was scored on
    public Teams scoredAgainstTeam = Teams.None;

    //Constructor for this event
    public GoalScoredEVT(Players playerID_ = Players.None, Teams playerTeamID_ = Teams.None, Teams scoredAgainstTeam_ = Teams.None)
    {
        this.playerID = playerID_;
        this.playerTeamID = playerTeamID_;
        this.scoredAgainstTeam = scoredAgainstTeam_;
    }
}