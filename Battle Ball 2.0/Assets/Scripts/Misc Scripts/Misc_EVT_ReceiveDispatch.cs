using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Misc_EVT_ReceiveDispatch : MonoBehaviour
{
    public string EventNameToListenFor;
    public string EventNameToDispatch;
    public UnityEvent EventOnReceive;
    private DelegateEvent<EVTData> CustomListener;


    // Use this for initialization
    void Awake()
    {
        CustomListener = new DelegateEvent<EVTData>(EventTriggered);
    }

    void OnEnable()
    {
        Manager_EventManager.StartListening(EventNameToListenFor, CustomListener);
    }

    void OnDisable()
    {
        Manager_EventManager.StopListening(EventNameToListenFor, CustomListener);
    }


    public void EventTriggered(EVTData data_)
    {
        EventOnReceive.Invoke();
    }


    //Calls an empty event using the event name to dispatch
    public void DispatchEvent()
    {
        Manager_EventManager.TriggerEvent(EventNameToDispatch);
    }
}
