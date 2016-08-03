using UnityEngine;
using UnityEngine.Events; //For events
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic; //For dictionary

public class Manager_EventManager : MonoBehaviour
{
    //The dictionary where we hold all of our events and their name tags
    private Dictionary<string, List< DelegateEvent<EVTData>>> EventDictionary;
    //A reference to this event manager that can be accessed anywhere
    public static Manager_EventManager EVTManager;


    //Initializes this event manager and the event dictionary
    void Awake()
    {
        //If there isn't already a static reference to this event manager, this instance becomes the static reference
        if(EVTManager == null)
        {
            EVTManager = GetComponent<Manager_EventManager>();
        }
        else
        {
            //Debug.LogError("ERROR: Manager_EventManager.Awake, there is already a static instance of EVTManager");
        }

        //Initializes a new dictionary to hold all events
        if (EventDictionary == null)
        {
            EventDictionary = new Dictionary<string, List< DelegateEvent<EVTData>>>();
        }
    }



    //Adds the given UnityAction to the dictionary of events under the given event name
    public static void StartListening(string evtName_, DelegateEvent<EVTData> evtListener_)
    {
        List< DelegateEvent<EVTData>> startListeningDelegate = null;

        //Checks to see if our entry for the event dictionary is found. If so, adds the listener to the event
        if(EVTManager.EventDictionary.TryGetValue(evtName_, out startListeningDelegate))
        {
            startListeningDelegate.Add(evtListener_);
        }
        //If an existing entry isn't found, a new entry is created and added to the dictionary
        else
        {
            startListeningDelegate = new List<DelegateEvent<EVTData>>();
            startListeningDelegate.Add(evtListener_);
            EVTManager.EventDictionary.Add(evtName_, startListeningDelegate);
        }
    }



    //Removes the given UnityAction from the dictionary of events with the given event name
    public static void StopListening(string evtName_, DelegateEvent<EVTData> evtListener_)
    {
        if (EVTManager == null)
            return;

        List< DelegateEvent<EVTData>> stopListeningDelegate = null;

        //Checks to see if our entry for the event dictionary is found. If so, removes the listener from the event
        if(EVTManager.EventDictionary.TryGetValue(evtName_, out stopListeningDelegate))
        {
            stopListeningDelegate.Remove(evtListener_);
        }
        //If an existing entry isn't found, nothing happens
    }



    //Invokes the event with the given name, calling all functions attached to the event
    public static void TriggerEvent(string evtName_, EVTData dataPassed_ = null)
    {
        List< DelegateEvent<EVTData>> triggerDelegate = null;
        
        //Null event data can't be sent, so we send an empty data event instead
        if(dataPassed_ == null)
        {
            dataPassed_ = new EVTData();
        }

        //Checks to see if our entry for the event dictionary is found. If so, invokes the event to call all functions attached to it
        if(EVTManager.EventDictionary.TryGetValue(evtName_, out triggerDelegate))
        {
            foreach( DelegateEvent<EVTData> evt_ in triggerDelegate)
            {
                evt_(dataPassed_);
            }
        }
    }
}

//The delegate that we use to call all of our events
public delegate void DelegateEvent<T>(T data_) where T : EVTData;

/*
    BASE EVENT CLASS
abstract public class EventData
{ }


    EXAMPLE EVENT
public class PlayerdeathEvt : eventdata 
{ 
    public int PlayerNum = 1; 
}


    EXAMPLE EVENT
class TransitionEvent : DefaultEvent
{
    public SceneList NextScene;

    public TransitionEvent(SceneList nextScene)
    {
        NextScene = nextScene;
    }
}


Jetyl: scenelist is another custom thing I made
Jetyl: basically a string. so ignore that
Jetyl: to make a zero style event system, you'd need at least 2 (or 3) scripts
Jetyl: (josh had 2, I had 3)

Jetyl: none of these classes inheret from monobehavior
Jetyl: you'd need at least a static EventSystem class, and a public Event class (this is seperate from the eventdata class)
Jetyl: the event class contains a bunch of strings like this:
Jetyl: public static readonly String DefaultEvent = "DefaultEvent";
Jetyl: these are what's happening under the hood when you say events.defaultevent, when you are connecting and dispatching events
Jetyl: unlike zero, where you'd add the sends event line, which is doing this for you under the hood, for this sytem, every time you add an
event like playerdeathEvt, you'd need to add a static readonly string declaring it.
Jetyl: the only other thing really needed in the event class, is this stuff:

    //Nonstatic
public string EventName = DefaultEvent;
    
public Events() { }

public Events(string eventName)
{
    EventName = eventName;
}

public static implicit operator string(Events value)
{
    return value.EventName;
}
    
public static implicit operator Events (string value)
{
    return new Events(value);
}


Jetyl: which are operators that let you treat an event like a string, and vice versa
Jetyl: beyond that, the majority/intirety of your event system is going to be handled in the eventSystem Class
Jetyl: now this is where me and josh's event systems differ, but the starting point is the same.
Jetyl: do you know what dictionaries are code-wise?
dasuberfisch: yea, i use them for several of my scripts, including my current event system
Jetyl: cool
Jetyl: so that's going to be the backbone of an event system.
Jetyl: what josh had was a dictionary of a dictionary of an Action<eventdata>
Jetyl: the first dictionary taking an object as a key, and the second taking a string
Jetyl: this lets you say, basically, object.connect(Event.someeventname, functionCall)
Jetyl: similar to zero's Zero.connect( object, event.someeventname, functioncall)
Jetyl: now, the one hitch in this is, in using Action<eventdata> requires all your events to send the eventdata class, and what josh did, was just typecast it at its destination to get access to the data you sent.
Jetyl: now, I don't like that. to messy, and its an unnesseary step you "can" remove, but as I found out, its more complicated
Jetyl: he also had a memeory leak in it, which I also fixed
Jetyl: for me, what I had was a dictionary of Eventhandler's, another custom class I made
Jetyl: and inside the eventhandler was a dictionary that held the cuntion calls, and took the string as a key
Jetyl: shit, I forgot a thing
Jetyl: it wasn't a dictionary of a dictionary of an action<eventdata>
Jetyl: it was a dictionary of a dictionary of a list of action<eventdata>
Jetyl: so an event could activate multiple functions
Jetyl: minor missing part, but my bad
Jetyl: anyways
Jetyl: now, the part that got me stumped for the longest time was getting the eventhandlers to allow the type of eventdata being sent be generic so it could be defined at runtime,
instead of always eventdata, the empty event class
Jetyl: that lead me to the use of delagates, and some code that I frankly don't 100% know why they work, but do
Jetyl: they are as follows:

public delegate void EventDelegate<T>(T e) where T : EventData;
    private delegate void EventDelegate(EventData e);

Jetyl: these are generic delegates. a custom delagate that's looks like this:
Jetyl: public class EventDelegate<T>  where T: EventData
{

}
Jetyl: and in my function that's adding the listener, we have this magic line of code:

Jetyl: EventDelegate internalDel = (e) => del((T)e);
Jetyl: where del is the delagte of the action<T> we sent
Jetyl: not sure what this line is doing, or why the code needs it to work, but it does

*/
