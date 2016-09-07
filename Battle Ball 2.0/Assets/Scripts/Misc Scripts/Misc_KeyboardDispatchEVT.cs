using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class Misc_KeyboardDispatchEVT : MonoBehaviour
{
    //The keyboard buttons that trigger events with the corresponding list index
    public List<KeyCode> KeysThatTriggerEvents;
    //The names of the events that the Event Manager triggers when the button with the same index is pressed
    public List<string> EventNamesToDispatch;
    //The unity events that are triggered when the button with the same index is pressed
    public List<UnityEvent> UnityEventsToTrigger;
        
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //Loops through and finds if any of the keys we're looking for are pressed
	    for(int i = 0; i < KeysThatTriggerEvents.Count; ++i)
        {
            if(Input.GetKey( KeysThatTriggerEvents[i]))
            {
                DispatchEvent(i);
            }
        }
	}


    public void DispatchEvent(int index_)
    {
        //Only dispatches the EVT if there's a cooresponding string
        if (index_ < EventNamesToDispatch.Count)
        {
            Manager_EventManager.TriggerEvent(EventNamesToDispatch[index_]);
        }

        //Only dispatches the Unity Event if there's a cooresponding event
        if (index_ < UnityEventsToTrigger.Count)
        {
            UnityEventsToTrigger[index_].Invoke();
        }
    }
}
