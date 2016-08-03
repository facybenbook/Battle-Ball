using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manager_SoundManager : MonoBehaviour
{
    //A reference to this manager that can be accessed anywhere
    public static Manager_SoundManager SoundManager;

    //The current method that the user is hearing the sound effects
    public SoundProfile CurrentSoundProfile = SoundProfile.ComputerSpeakers;

    public bool MuteMusic = false;
    [Range(0, 1.0f)]
    public float MusicVolume = 1.0f;

    public bool MuteSoundEffects = false;
    [Range(0, 1.0f)]
    public float SoundEffectVolume = 1.0f;

    public bool MuteDialogue = false;
    [Range(0, 1.0f)]
    public float DialogueVolume = 1.0f;

    public bool MuteAll = false;
    [Range(0, 1.0f)]
    public float GlobalVolume = 1.0f;

    //Slider for the headphone volume, going from mute to max
    [Range(0, 1.0f)]
    public float HeadphoneVol = 1.0f;
    //Slider for the computer speaker volume, going from mute to max
    [Range(0, 1.0f)]
    public float CompSpeakerVol = 1.0f;
    //Slider for the Room speaker volume, going from mute to max
    [Range(0, 1.0f)]
    public float RoomSpeakerVol = 1.0f;



    void Awake()
    {
        //If there isn't already a static reference to this manager, this instance becomes the static reference
        if (SoundManager == null)
        {
            SoundManager = GetComponent<Manager_SoundManager>();
        }
    }


    //Public function that sends out an EVENT for all SoundEmitterExtraSettings components to receive
    public void ChangeSoundProfile(SoundProfile newProfile_)
    {
        SoundManager.CurrentSoundProfile = newProfile_;

        DispatchSoundChangeEvt();
    }


    //Public function that changes the volume of a 
    public void ChangeProfileVolume(SoundProfile changedProfile_, float newVolume_)
    {
        //Makes sure that the volume given is within 0 and 1
        float goodVol = newVolume_;
        if (goodVol > 1.0f)
            goodVol = 1.0f;
        else if (goodVol < 0)
            goodVol = 0;


        switch(changedProfile_)
        {
            case SoundProfile.Headphones:
                SoundManager.HeadphoneVol = goodVol;
                break;
            case SoundProfile.ComputerSpeakers:
                SoundManager.CompSpeakerVol = goodVol;
                break;
            case SoundProfile.RoomSpeakers:
                SoundManager.RoomSpeakerVol = goodVol;
                break;
            default:
                SoundManager.CompSpeakerVol = goodVol;
                break;
        }

        DispatchSoundChangeEvt();
    }


    //Changes the global volume
    public void ChangeGlobalVolume(float newVolume_)
    {
        SoundManager.GlobalVolume = newVolume_;

        if (SoundManager.GlobalVolume > 1)
            SoundManager.GlobalVolume = 1.0f;
        else if (SoundManager.GlobalVolume < 0)
            SoundManager.GlobalVolume = 0;

        DispatchSoundChangeEvt();
    }


    //Changes the global volume
    public void ChangeMusicVolume(float newVolume_)
    {
        SoundManager.MusicVolume = newVolume_;

        if (SoundManager.MusicVolume > 1)
            SoundManager.MusicVolume = 1.0f;
        else if (SoundManager.MusicVolume < 0)
            SoundManager.MusicVolume = 0;
        
        DispatchSoundChangeEvt();
    }


    //Changes the global volume
    public void ChangeSoundEffectVolume(float newVolume_)
    {
        SoundManager.SoundEffectVolume = newVolume_;

        if (SoundManager.SoundEffectVolume > 1)
            SoundManager.SoundEffectVolume = 1.0f;
        else if (SoundManager.SoundEffectVolume < 0)
            SoundManager.SoundEffectVolume = 0;

        DispatchSoundChangeEvt();
    }


    //Changes the global volume
    public void ChangeDialogueVolume(float newVolume_)
    {
        SoundManager.DialogueVolume = newVolume_;

        if (SoundManager.DialogueVolume > 1)
            SoundManager.DialogueVolume = 1.0f;
        else if (SoundManager.DialogueVolume < 0)
            SoundManager.DialogueVolume = 0;

        DispatchSoundChangeEvt();
    }


    public void ToggleMuteAll(bool isMuted_)
    {
        SoundManager.MuteAll = isMuted_;
        DispatchSoundChangeEvt();
    }


    public void ToggleMuteMusic(bool isMuted_)
    {
        SoundManager.MuteMusic = isMuted_;
        DispatchSoundChangeEvt();
    }


    public void ToggleMuteDialogue(bool isMuted_)
    {
        SoundManager.MuteDialogue = isMuted_;
        DispatchSoundChangeEvt();
    }


    public void ToggleMuteSFX(bool isMuted_)
    {
        SoundManager.MuteSoundEffects = isMuted_;
        DispatchSoundChangeEvt();
    }


    private void DispatchSoundChangeEvt()
    {
        Manager_EventManager.TriggerEvent("SoundSettingsChanged");
    }


    public void Cutout(EVTData data_)
    {

    }
}
