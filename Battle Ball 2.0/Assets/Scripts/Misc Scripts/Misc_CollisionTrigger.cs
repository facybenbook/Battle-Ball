using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class Misc_CollisionTrigger : MonoBehaviour
{
    public bool Trigger2DStart = false;
    public bool Trigger2DStay = false;
    public bool Trigger2DExit = false;

    public bool Trigger3DStart = false;
    public bool Trigger3DStay = false;
    public bool Trigger3DExit = false;

    public bool Collision2DStart = false;
    public bool Collision2DStay = false;
    public bool Collision2DExit = false;

    public bool Collision3DStart = false;
    public bool Collision3DStay = false;
    public bool Collision3DExit = false;

    public string TagToLookFor = "";
    public string NameToLookFor = "";

    public List<CollisionTypes> TypesOfCollisions;
    public List<UnityEvent> EventList;
    Dictionary<CollisionTypes, UnityEvent> EventDictionary;


	// Use this for initialization
	void Start()
    {
        EventDictionary = new Dictionary<CollisionTypes, UnityEvent>();

        //Adds all of the events in the EventList to the EventDictionary
        int e = 0;
        foreach(CollisionTypes c in TypesOfCollisions)
        {
            EventDictionary.Add(c, EventList[e]);
            ++e;
        }

        
    }


    //Function called when this object is triggered
    public void TriggerThisObject(CollisionTypes evtCollisionType_)
    {
        //Checks to make sure that the type of collision has an associated event call
        if(EventDictionary.ContainsKey(evtCollisionType_))
        {
            EventDictionary[evtCollisionType_].Invoke();
        }
        //If there isn't an associated event call to the one given, logs a warning to the console
        else
        {
            Debug.LogWarning("WARNING: " + gameObject.name + " doesn't have an event call for " + evtCollisionType_);
        }
    }


    //Function called when this owner's 2D collider is triggered for the first frame
    void OnTriggerEnter2D(Collider2D otherObj_)
    {
        if (!Trigger2DStart)
            return;

        //If the name of the other object is the name we're looking for, triggers this object
        if(NameToLookFor != "")
        {
            if(otherObj_.name == NameToLookFor)
                TriggerThisObject(CollisionTypes.Trigger2DStart);
        }
        //Otherwise, if the name of the other object's tag is the tag we're looking for, triggers this object
        else if(TagToLookFor != "")
        {
            if (otherObj_.gameObject.tag == TagToLookFor)
                TriggerThisObject(CollisionTypes.Trigger2DStart);
        }
        //If we're not looking for any specific tag or name, this object is just triggered
        else
        {
            TriggerThisObject(CollisionTypes.Trigger2DStart);
        }
    }


    //Function called when this owner's 2D collider keeps being triggered
    void OnTriggerStay2D(Collider2D otherObj_)
    {
        if (!Trigger2DStay)
            return;

        //If the name of the other object is the name we're looking for, triggers this object
        if (NameToLookFor != "")
        {
            if (otherObj_.name == NameToLookFor)
                TriggerThisObject(CollisionTypes.Trigger2DStay);
        }
        //Otherwise, if the name of the other object's tag is the tag we're looking for, triggers this object
        else if (TagToLookFor != "")
        {
            if (otherObj_.gameObject.tag == TagToLookFor)
                TriggerThisObject(CollisionTypes.Trigger2DStay);
        }
        //If we're not looking for any specific tag or name, this object is just triggered
        else
        {
            TriggerThisObject(CollisionTypes.Trigger2DStay);
        }
    }


    //Function called when this owner's 2D collider stops being triggered
    void OnTriggerExit2D(Collider2D otherObj_)
    {
        if (!Trigger2DExit)
            return;

        //If the name of the other object is the name we're looking for, triggers this object
        if (NameToLookFor != "")
        {
            if (otherObj_.name == NameToLookFor)
                TriggerThisObject(CollisionTypes.Trigger2DExit);
        }
        //Otherwise, if the name of the other object's tag is the tag we're looking for, triggers this object
        else if (TagToLookFor != "")
        {
            if (otherObj_.gameObject.tag == TagToLookFor)
                TriggerThisObject(CollisionTypes.Trigger2DExit);
        }
        //If we're not looking for any specific tag or name, this object is just triggered
        else
        {
            TriggerThisObject(CollisionTypes.Trigger2DExit);
        }
    }


    //Function called when this owner's 3D collider is triggered for the first frame
    void OnTriggerEnter(Collider otherObj_)
    {
        if (!Trigger3DStart)
            return;

        //If the name of the other object is the name we're looking for, triggers this object
        if (NameToLookFor != "")
        {
            if (otherObj_.name == NameToLookFor)
                TriggerThisObject(CollisionTypes.Trigger3DStart);
        }
        //Otherwise, if the name of the other object's tag is the tag we're looking for, triggers this object
        else if (TagToLookFor != "")
        {
            if (otherObj_.gameObject.tag == TagToLookFor)
                TriggerThisObject(CollisionTypes.Trigger3DStart);
        }
        //If we're not looking for any specific tag or name, this object is just triggered
        else
        {
            TriggerThisObject(CollisionTypes.Trigger3DStart);
        }
    }


    //Function called when this owner's 3D collider keeps being triggered
    void OnTriggerStay(Collider otherObj_)
    {
        if (!Trigger3DStay)
            return;

        //If the name of the other object is the name we're looking for, triggers this object
        if (NameToLookFor != "")
        {
            if (otherObj_.name == NameToLookFor)
                TriggerThisObject(CollisionTypes.Trigger3DStay);
        }
        //Otherwise, if the name of the other object's tag is the tag we're looking for, triggers this object
        else if (TagToLookFor != "")
        {
            if (otherObj_.gameObject.tag == TagToLookFor)
                TriggerThisObject(CollisionTypes.Trigger3DStay);
        }
        //If we're not looking for any specific tag or name, this object is just triggered
        else
        {
            TriggerThisObject(CollisionTypes.Trigger3DStay);
        }
    }


    //Function called when this owner's 3D collider stops being triggered
    void OnTriggerExit(Collider otherObj_)
    {
        if (!Trigger3DExit)
            return;

        //If the name of the other object is the name we're looking for, triggers this object
        if (NameToLookFor != "")
        {
            if (otherObj_.name == NameToLookFor)
                TriggerThisObject(CollisionTypes.Trigger3DExit);
        }
        //Otherwise, if the name of the other object's tag is the tag we're looking for, triggers this object
        else if (TagToLookFor != "")
        {
            if (otherObj_.gameObject.tag == TagToLookFor)
                TriggerThisObject(CollisionTypes.Trigger3DExit);
        }
        //If we're not looking for any specific tag or name, this object is just triggered
        else
        {
            TriggerThisObject(CollisionTypes.Trigger3DExit);
        }
    }


    //Function called when this owner's 2D collider is hit for the first frame
    void OnCollisionEnter2D(Collision2D otherObj_)
    {
        if (!Collision2DStart)
            return;

        //If the name of the other object is the name we're looking for, triggers this object
        if (NameToLookFor != "")
        {
            if (otherObj_.gameObject.name == NameToLookFor)
                TriggerThisObject(CollisionTypes.Collision2DStart);
        }
        //Otherwise, if the name of the other object's tag is the tag we're looking for, triggers this object
        else if (TagToLookFor != "")
        {
            if (otherObj_.gameObject.tag == TagToLookFor)
                TriggerThisObject(CollisionTypes.Collision2DStart);
        }
        //If we're not looking for any specific tag or name, this object is just triggered
        else
        {
            TriggerThisObject(CollisionTypes.Collision2DStart);
        }
    }


    //Function called when this owner's 2D collider is hit for the first frame
    void OnCollisionStay2D(Collision2D otherObj_)
    {
        if (!Collision2DStay)
            return;

        //If the name of the other object is the name we're looking for, triggers this object
        if (NameToLookFor != "")
        {
            if (otherObj_.gameObject.name == NameToLookFor)
                TriggerThisObject(CollisionTypes.Collision2DStay);
        }
        //Otherwise, if the name of the other object's tag is the tag we're looking for, triggers this object
        else if (TagToLookFor != "")
        {
            if (otherObj_.gameObject.tag == TagToLookFor)
                TriggerThisObject(CollisionTypes.Collision2DStay);
        }
        //If we're not looking for any specific tag or name, this object is just triggered
        else
        {
            TriggerThisObject(CollisionTypes.Collision2DStay);
        }
    }


    //Function called when this owner's 2D collider is hit for the first frame
    void OnCollisionExit2D(Collision2D otherObj_)
    {
        if (!Collision2DExit)
            return;

        //If the name of the other object is the name we're looking for, triggers this object
        if (NameToLookFor != "")
        {
            if (otherObj_.gameObject.name == NameToLookFor)
                TriggerThisObject(CollisionTypes.Collision2DExit);
        }
        //Otherwise, if the name of the other object's tag is the tag we're looking for, triggers this object
        else if (TagToLookFor != "")
        {
            if (otherObj_.gameObject.tag == TagToLookFor)
                TriggerThisObject(CollisionTypes.Collision2DExit);
        }
        //If we're not looking for any specific tag or name, this object is just triggered
        else
        {
            TriggerThisObject(CollisionTypes.Collision2DExit);
        }
    }


    //Function called when this owner's 3D collider is hit for the first frame
    void OnCollisionEnter(Collision otherObj_)
    {
        if (!Collision3DStart)
            return;

        //If the name of the other object is the name we're looking for, triggers this object
        if (NameToLookFor != "")
        {
            if (otherObj_.gameObject.name == NameToLookFor)
                TriggerThisObject(CollisionTypes.Collision3DStart);
        }
        //Otherwise, if the name of the other object's tag is the tag we're looking for, triggers this object
        else if (TagToLookFor != "")
        {
            if (otherObj_.gameObject.tag == TagToLookFor)
                TriggerThisObject(CollisionTypes.Collision3DStart);
        }
        //If we're not looking for any specific tag or name, this object is just triggered
        else
        {
            TriggerThisObject(CollisionTypes.Collision3DStart);
        }
    }


    //Function called when this owner's 3D collider is hit for the first frame
    void OnCollisionStay(Collision otherObj_)
    {
        if (!Collision3DStay)
            return;

        //If the name of the other object is the name we're looking for, triggers this object
        if (NameToLookFor != "")
        {
            if (otherObj_.gameObject.name == NameToLookFor)
                TriggerThisObject(CollisionTypes.Collision3DStay);
        }
        //Otherwise, if the name of the other object's tag is the tag we're looking for, triggers this object
        else if (TagToLookFor != "")
        {
            if (otherObj_.gameObject.tag == TagToLookFor)
                TriggerThisObject(CollisionTypes.Collision3DStay);
        }
        //If we're not looking for any specific tag or name, this object is just triggered
        else
        {
            TriggerThisObject(CollisionTypes.Collision3DStay);
        }
    }


    //Function called when this owner's 3D collider is hit for the first frame
    void OnCollisionExit(Collision otherObj_)
    {
        if (!Collision3DExit)
            return;

        //If the name of the other object is the name we're looking for, triggers this object
        if (NameToLookFor != "")
        {
            if (otherObj_.gameObject.name == NameToLookFor)
                TriggerThisObject(CollisionTypes.Collision3DExit);
        }
        //Otherwise, if the name of the other object's tag is the tag we're looking for, triggers this object
        else if (TagToLookFor != "")
        {
            if (otherObj_.gameObject.tag == TagToLookFor)
                TriggerThisObject(CollisionTypes.Collision3DExit);
        }
        //If we're not looking for any specific tag or name, this object is just triggered
        else
        {
            TriggerThisObject(CollisionTypes.Collision3DExit);
        }
    }
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