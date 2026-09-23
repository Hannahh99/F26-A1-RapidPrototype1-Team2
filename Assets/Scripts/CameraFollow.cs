using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Target variable
    private Transform target;

    //For smooth movement & offset positioning
    public float smoothRate = 1.5f;
    public Vector3 cameraOffset = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        //Look for a target by its tag and get its position
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //LateUpdate runs after Update is finished
    void LateUpdate()
    {
        //Local Vector3 variable to get target position every time.
        //This also allows us to 'separate' the z-position of the camera
        //because we DO NOT want the z to be the same as the target!
        //Z = TRANSFORM.position.z, not TARGET.position.z!!!
        Vector3 newPos = new Vector3(target.position.x,
            target.position.y, transform.position.z);

        //Now we can move the camera!
        /*Lerp is "Linear Interpolation" and is a smooth transition between
         * two vectors. The parameters are (starting point, target point,
         * time to get there). In our case we are moving the camera
         * from its current position to the target position calculated above
         * plus the offset at the rate of time * smoothRate for some pizazz*/
        transform.position = Vector3.Lerp(transform.position,
                newPos + cameraOffset, Time.deltaTime * smoothRate);
    }
}
