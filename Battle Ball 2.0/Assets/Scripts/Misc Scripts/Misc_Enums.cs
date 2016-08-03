using UnityEngine;
using System.Collections;


//The sound system that the player is currently using
public enum SoundProfile
{
    ComputerSpeakers,
    RoomSpeakers,
    Headphones
}


//Type of sound that a sound emitter plays. Manager_SoundManager affects the volume differently based on this enum.
public enum SoundType
{
    Music,
    SFX,
    Dialogue,
    Cutout //Sound effects with this tag are exempt from being muted when the sound cuts out from things like explosions
}


//Enums to reference different players
public enum Players
{
    None,
    All,
    P1,
    P2,
    P3,
    P4,
    Keyboard
}


//Enums to reference different teams of players identified by the team number
public enum TeamNumbers
{
    None,
    All,
    Team1,
    Team2,
    Team3,
    Team4
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


public enum TeamPositions
{
    Weapons,
    Movement
}


//Enums used in Misc_CollisionTrigger script to determine what triggers it
public enum CollisionTypes
{
    Trigger2DStart,
    Trigger2DStay,
    Trigger2DExit,

    Trigger3DStart,
    Trigger3DStay,
    Trigger3DExit,

    Collision2DStart,
    Collision2DStay,
    Collision2DExit,

    Collision3DStart,
    Collision3DStay,
    Collision3DExit
}


//Enums used Manager_P#Controller scripts for button input
public enum ControllerButtons
{
    A_Button,
    B_Button,
    X_Button,
    Y_Button,

    Left_Bumper,
    Right_Bumper,

    Back_Button,
    Start_Button,

    Left_Stick_Click,
    Right_Stick_Click
}


//Enums used in Misc_Interpolator to determine the type of ease 
public enum EaseType
{
    Linear,
    SineIn,
    SineOut,
    SineInOut,
    CubeIn,
    CubeOut,
    CubeInOut,
    JitterBetweenStay,
    JitterOutsideStay,
    JitterBetweenIn,
    JitterOutsideIn
}



//Enums used in Manager_ScreenSettings to set the screen resolution size
public enum ScreenResolution
{
    r1920x1200,
    r1920x1080,
    r1680x1050,
    r1600x900,
    r1440x900,
    r1366x768,
    r1280x1024,
    r1280x800,
    r1024x768
}