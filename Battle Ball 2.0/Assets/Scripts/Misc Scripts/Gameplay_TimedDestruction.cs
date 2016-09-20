using UnityEngine;
using System.Collections;

public class Gameplay_TimedDestruction : MonoBehaviour
{
    //The amount of time that this object is alive before it destroys itself
    public float lifetime = 10;


	// Update is called once per frame
	private void FixedUpdate ()
    {
        this.lifetime -= Time.deltaTime;

        //If this object's lifetime is below 0, it destroys itself
        if(this.lifetime <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }
	}
}
