using UnityEngine;
using System.Collections;

public class Power_FireBeam : Power_FireProjectile
{
    //The maximum distance that the fired beam can travel
    public float beamRange = 10;

    //The amount of damage dealt to a player who was hit by this beam
    public int damageDealt = 3;



    //Overrides the base function from Power_FireProjectile. Instead of launching a projectile, it casts a ray to deal damage
    protected override void FireProjectile()
    {
        //Finds the ray start point in front of the player
        Vector3 startLoc = this.transform.position;
        startLoc += new Vector3(Mathf.Cos(this.transform.eulerAngles.z * Mathf.Deg2Rad) * this.spawnOffset * this.transform.parent.localScale.x,
                                Mathf.Sin(this.transform.eulerAngles.z * Mathf.Deg2Rad) * this.spawnOffset * this.transform.parent.localScale.x,
                                0);

        //Adds angle variance for the ray
        float startAngle = Random.Range(-this.angleVariance, this.angleVariance) + this.transform.eulerAngles.z;

        //The XY direction to cast the ray
        Vector2 direction = new Vector2(Mathf.Cos(startAngle * Mathf.Deg2Rad),
                                        Mathf.Sin(startAngle * Mathf.Deg2Rad));

        //Creates a new 2D ray to be cast in space
        RaycastHit2D hitResults = Physics2D.Raycast(startLoc, direction, this.beamRange);

        //If a player is hit, they take damage
        if(hitResults.rigidbody != null && hitResults.rigidbody.gameObject.GetComponent<Player_Health>() != null)
        {
            Player_Health playerHit = hitResults.rigidbody.gameObject.GetComponent<Player_Health>();
            playerHit.DamagePlayer(this.damageDealt);
        }

        //Finds the end point of the beam
        Vector3 endLoc = new Vector3();
        if(hitResults.rigidbody == null)
        {
            endLoc.x = (direction.x * this.beamRange) + startLoc.x;
            endLoc.y = (direction.y * this.beamRange) + startLoc.y;
        }
        else
        {
            endLoc.x = hitResults.point.x;
            endLoc.y = hitResults.point.y;
        }

        //Finds the midpoint of the beam so we can spawn a beam prefab
        Vector3 midpoint = startLoc + endLoc;
        midpoint = midpoint / 2;

        //Creates the beam prefab at the midpoint and rotates to the start angle
        GameObject beamObject = GameObject.Instantiate(this.projectileToShoot,
                                                        midpoint,
                                                        Quaternion.Euler(0, 0, startAngle)) as GameObject;

        //Scales the beam to match the ray length
        if (hitResults.rigidbody != null)
        {
            beamObject.transform.localScale = new Vector3(beamObject.transform.localScale.x * hitResults.distance,
                                                          beamObject.transform.localScale.y,
                                                          beamObject.transform.localScale.z);
        }
        else
        {
            beamObject.transform.localScale = new Vector3(beamObject.transform.localScale.x * this.beamRange,
                                                          beamObject.transform.localScale.y,
                                                          beamObject.transform.localScale.z);
        }
    }
}
