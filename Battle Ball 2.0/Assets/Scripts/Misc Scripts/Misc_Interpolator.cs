/*
Writer: Mitch Regan
Date: 4/18/2016

Description:
- Returns a value from 0 - 1.0 (sometimes -1.0 - 1.0) at a rate based on the
interpolation type and speed given by the user.
*/

using UnityEngine;
using System.Collections;


public class Misc_Interpolator// : MonoBehaviour
{
    //The way that this Interpolator reaches 1.0
    public EaseType Ease = EaseType.Linear;
    //The length of time it takes this Interpolator to reach 1.0
    private float Duration = 1.0f;
    //The current amount of time this interpolator has spent interpolating
    private float CurrentTime = 0;
    private int NumberOfJitters = 1;



    // Use this for initialization
    public Misc_Interpolator(EaseType ease_ = EaseType.Linear, float duration_ = 1.0f)
    {
        Ease = ease_;
        SetDuration(duration_);
    }


    //Sets the amount of time that the interpolation happens between
    public void SetDuration(float duration_)
    {
        Duration = duration_;

        //The time can't be negative
        if (Duration < 0)
        {
            Duration = 0;
        }
    }


    //Sets the number of times object 
    public void SetNumberOfJitters(int numJitters_)
    {
        //Sets the NumberOfJitters as long as it's not negative
        if (numJitters_ >= 0)
        {
            NumberOfJitters = numJitters_;
        }
    }


    //Adds (or reduces) time to the interpolator's current time
    public void AddTime(float timeDiff_)
    {
        CurrentTime = CurrentTime + timeDiff_;

        //Makes sure the time can't go above the current duration
        if (CurrentTime > Duration)
        {
            CurrentTime = Duration;
        }
        //Makes sure the time can't go below 0 (because time doesn't work that way)
        else if (CurrentTime < 0)
        {
            CurrentTime = 0;
        }
    }


    //Resets the current time to 0
    public void ResetTime()
    {
        CurrentTime = 0;
    }


    //Returns the progress in percent (0 - 1) without ease 
    public float GetPercent()
    {
        return (CurrentTime / Duration);
    }


    //Returns the progress of this interpolator
    public float GetProgress()
    {
        //If the current timer is at the duration, it returns the max value
        if (CurrentTime == Duration)
        {
            if (Ease != EaseType.JitterBetweenStay && Ease != EaseType.JitterOutsideStay)
            {
                return 1.0f;
            }
        }
        //If the current timer is at 0, it returns the lowest value
        else if (CurrentTime == 0)
        {
            return 0;
        }

        float progress = CurrentTime / Duration;
        float difference = 0;
        float offset = 0;

        //Otherwise, the progress needs to be calculated based on the ease type
        switch (Ease)
        {
            //Moves at a constant speed
            case EaseType.Linear:
                return progress;

            //Moves slow at first then speeds up
            case EaseType.SineIn:
                difference = Mathf.PI / 2;
                offset = Mathf.PI * 1.5f;
                return Mathf.Sin((difference * progress) + offset) + 1;

            //Moves fast at first then slows to a stop
            case EaseType.SineOut:
                difference = Mathf.PI / 2;
                return Mathf.Sin(difference * progress);

            //Moves slow at first, speeds up, then slows down to a stop
            case EaseType.SineInOut:
                difference = Mathf.PI;
                offset = Mathf.PI * 1.5f;
                return (Mathf.Sin((difference * progress) + offset) * 0.5f) + 0.5f;

            //Moves slow at first then speeds up. Faster than SineIn
            case EaseType.CubeIn:
                return Mathf.Pow(progress, 3);

            //Moves fast at first then slows to a stop. Faster than SineOut
            case EaseType.CubeOut:
                return 1 + Mathf.Pow((progress - 1), 3); //WRONG?

            //Moves slow at first, speeds up, then slows down to a stop. Faster than SineInOut
            case EaseType.CubeInOut:
                if (progress <= 0.5)
                {
                    return Mathf.Pow((2 * progress), 3) * 0.5f;
                }
                else
                {
                    return ((1 + Mathf.Pow(((progress - 1) * 2), 3)) * 0.5f) + 0.5f; //WRONG?
                }

            //Goes back and forth from 0 - 1 and ends at 1
            case EaseType.JitterBetweenIn:
                float jitterBI = (NumberOfJitters * (2 * Mathf.PI)) + (Mathf.PI);
                return (Mathf.Sin((1.5f * Mathf.PI) + jitterBI * progress) * 0.5f) + 0.5f;

            //Goes back and forth from 0 - 1 and ends at 0
            case EaseType.JitterBetweenStay:
                float jitterBS = (NumberOfJitters * (2 * Mathf.PI));
                return (Mathf.Sin((1.5f * Mathf.PI) + jitterBS * progress) * 0.5f) + 0.5f;

            //Goes back and forth from -1 - 1 and ends at 1
            case EaseType.JitterOutsideIn:
                float jitterOI = (NumberOfJitters * (2 * Mathf.PI)) + (0.5f * Mathf.PI);
                return Mathf.Sin(jitterOI * progress);

            //Goes back and forth from -1 - 1 and ends at 0
            case EaseType.JitterOutsideStay:
                float jitterOS = (NumberOfJitters * (2 * Mathf.PI));
                return Mathf.Sin(jitterOS * progress);

            //Returns linear by default
            default:
                return (CurrentTime / Duration);
        }
    }
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