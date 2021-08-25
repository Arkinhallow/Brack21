using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovement : MonoBehaviour
{
    //arbitrary speed - will require testing
    public float speed = 10f;
    
    private Transform target;
    //index of the waypoints to be pursued by the drone
    private int waypointIndex = 0;

    void Start ()
    {
        //pursue the target that has the waypoint index of 0 i.e. the first waypoint in the array
        //points refers to the public static array from the waypoints script
        target = Waypoints.points[0];
    }
    
    void Update ()
    {
        //get direction from: target position minus current position
        Vector3 dir = target.position - transform.position;
        //move the drone in the Vector3 direction found in the above line
        //"normalized" results in nothing else influencing the speed except for the speed variable defined in this script
        //Time.deltaTime averages out the number across different systems so frame rate doesnt affect speed
        //the space we want to move relative to is world space (not local space)
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        //code used to judge how close to the waypoint the drone is
        //if the drone is within 0.3 units of the waypoint is is counted as having reached the waypoint and can move on
        if(Vector3.Distance(transform.position, target.position) <= 0.3f)
        {
            //drone finds the next waypoint to seek out
            GetNextWaypoint();
        }
        
    }

    void GetNextWaypoint ()
    {
        //if the index is larger than the total amount of waypoints (i.e. there are no more waypoints left to seek out - the drone is at the end of the path)
        if (waypointIndex >= Waypoints.points.Length - 1)
        {
            //end the game or adjust score here
            
            //destroy the drone object
            Destroy(gameObject);
            //prevent the rest of the code running before this object is destroyed
            return;
        }
        //add one to the index (to change which waypoint is being targeted)
        waypointIndex++;
        //get the waypoint at that index and set that as the target
        target = Waypoints.points[waypointIndex];
    }
}
