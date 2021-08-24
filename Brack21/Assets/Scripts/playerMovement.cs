using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame. Use FixedUpdate when using the physics system.
    void Update()
    {
        //if the d key is pressed and the position of the player is within the bounds of the game area (<4) then allow the player to move one step (2,0,0)
        //Useage of GetKeyDown as opposed to GetKey means this will only be implemented once per key press and will not repeat while the key is pressed.
        if(Input.GetKeyDown("d") && transform.position.x < 4)
        {
            //movement to the right
            transform.position += Vector3.right * 2;
        }

        //if the a key is pressed and the position of the player is within the bounds of the game area (>-4) then allow the player to move one step (-2,0,0)
        if(Input.GetKeyDown("a") && transform.position.x > -4)
        {
            //movement to the left
            transform.position += Vector3.left * 2;
        }
    }
}
