using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class BallGoal : MonoBehaviour
{
    //The ID for the team that owns this goal
    public Teams teamGoalID = Teams.None;
}
