using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    
    public static Transform[] points;
        
    // Start is called before the first frame update
    void Awake()
    {
        //create the number of spaces in the array equal to the number of children under this item (Track1)
        points = new Transform[transform.childCount];
        //assign the ith child to the ith number in the array e.g. the 1st child to the 1st number in the array, 2nd to the 2nd etc
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }
}
