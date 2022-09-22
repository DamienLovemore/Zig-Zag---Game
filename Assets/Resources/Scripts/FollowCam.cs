using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    //The target to follow
    public Transform target;
    //What is our position, in comparison
    // o the target
    private Vector3 offset;

    void Awake()
    {
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        //Do not forget about the offset, otherwise
        //the camera would be inside the player.
        transform.position = target.position + offset;
    }
}
