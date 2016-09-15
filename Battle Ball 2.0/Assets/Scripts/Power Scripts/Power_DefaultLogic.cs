using UnityEngine;
using System.Collections;

public class Power_DefaultLogic : MonoBehaviour
{
    //The icon that is used to display this power on the UI
    public Sprite powerIcon;

    //The ID for the player that controlls when this power activates
    protected Players ownerPlayer = Players.None;
    //The slot that this power takes up (Main or Secondary)
    protected PowerSlot slot = PowerSlot.MainPower;

    //The amount of time it takes for this power to recharge after being used
    public float rechargeTime = 1;
    protected float rechargeCounter = 0;

    private DelegateEvent<EVTData> activateEVT;



	// Use this for initialization
	protected virtual void Awake ()
    {
        this.activateEVT = new DelegateEvent<EVTData>(this.ActivatePower);
	}
	

    //Function called when this component is enabled
    protected virtual void OnEnable()
    {
        Manager_EventManager.StartListening(PlayerPowerActivateEVT.eventName, this.activateEVT);
    }


    //Function called when this component is disabled
    protected virtual void OnDisable()
    {
        Manager_EventManager.StopListening(PlayerPowerActivateEVT.eventName, this.activateEVT);
    }


	// FixedUpdate is called once per frame
	protected virtual void FixedUpdate ()
    {
	    if(this.rechargeCounter > 0)
        {
            this.rechargeCounter -= Time.deltaTime;
        }
	}


    //Function called externally. Sets the ID for the player that can activate this power
    public void SetOwnerPlayer(Players ownerPlayer_ = Players.None, PowerSlot slot_ = PowerSlot.MainPower)
    {
        //If there is no player that owns this object, it can't be told when to be used
        if (ownerPlayer_ == Players.None)
            return;

        this.ownerPlayer = ownerPlayer_;
        this.slot = slot_;
    }


    //Function called from the Event Manager through the ActivateEVT delegate event. Used to activate this power
    protected virtual void ActivatePower(EVTData data_)
    {

    }
}
