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
    public TeamColors playerTeam = TeamColors.None;

    //Constructor function to set the player ID and their team
    public PlayerDeathEVT(Players playerID_ = Players.P1, TeamColors playerTeam_ = TeamColors.None)
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
    //If the player used their power 1
    public bool power1Used = false;
    //If the player used their power 2
    public bool power2Used = false;

    //Constructor for this event
    public PlayerPowerActivateEVT(Players playerID_ = Players.None, bool power1Used_ = false, bool power2Used_ = false)
    {
        playerID = playerID_;
        power1Used = power1Used_;
        power2Used = power2Used_;
    }
}