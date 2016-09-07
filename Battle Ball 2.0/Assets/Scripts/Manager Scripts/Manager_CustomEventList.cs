using UnityEngine;
using UnityEngine.Events;
using System.Collections;


//Class that's sent through all of our custom events as an argument. Contains an instance of all custom classes
public class EVTData
{
    public ButtonPressEVT ButtonPress = null;
    public UISoundCueEVT SoundCue = null;
    public UIMusicCueEVT MusicCue = null;
    public SoundCutoutEVT SoundCutout = null;

    public PlayerDeathEVT PlayerDeath = null;
}


/****************************************************************** UI EVENTS ******************************************************************/

//Event data used whenever a button is pressed and we need to know who pressed what button
public class ButtonPressEVT
{
    public RectTransform ButtonPressed = null;
    public Players PlayerWhoPressed = Players.None;
}


//Event data used when a UI element needs to play a sound
public class UISoundCueEVT
{
    public AudioClip SoundToPlay = null;
    public float SoundVolume = 1;
    public Players PlayerWhoInvoked = Players.None;
}


//Event data used when a UI element needs to play music
public class UIMusicCueEVT 
{
    public AudioClip MusicToPlay = null;
    public float MusicVolume = 1;
    public Players PlayerWhoInvoked = Players.None;
}


//used when the sound cuts out after an impactful sound cue
public class SoundCutoutEVT
{
    //how long the cutout lasts
    public float StopDuration = 0;
    //How long it takes for sounds to return to normal levels again
    public float FadeInDuration = 0;

    //How low the music volume is set when the cutout initially happens
    [Range(0, 1.0f)]
    public float MusicLowPoint = 0;
    //How low the dialogue volume is set when the cutout initially happens
    [Range(0, 1.0f)]
    public float DialogueLowPoint = 0;
    //How low the SFX volume is set when the cutout initially happens
    [Range(0, 1.0f)]
    public float SFXLowPoint = 0;
}


/****************************************************************** GAMEPLAY EVENTS ******************************************************************/

//Event data used when a player dies and we need to know who it was
public class PlayerDeathEVT 
{
    public Players playerID = Players.None;
    public TeamColors playerTeam = TeamColors.None;

    //Constructor function to set the player ID and their team
    public PlayerDeathEVT(Players playerID_ = Players.P1, TeamColors playerTeam_ = TeamColors.None)
    {
        this.playerID = playerID_;
        this.playerTeam = playerTeam_;
    }
}