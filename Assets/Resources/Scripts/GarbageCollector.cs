using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollector : MonoBehaviour
{
    //When the object is not visible by the camera,
    //then it should be destroyed because it will
    //not be needed.
    //(The player does not walk backward)
    private void OnBecameInvisible()
    {
        //Checkts if the object is behind the player,
        //a road that was already crossed. And not a
        //new one that was generated and not yet
        //crossed.
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        if (gameObject.transform.position.z < playerPos.z)
            Destroy(gameObject);
    }
}
