using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    //The prefab of one road
    public GameObject roadPrefab;
    //The width of the road
    public float offset = 0.707f;
    //The position of the last road part
    public Vector3 lastPos;
    //Counts how much new roads it has created
    public int roadCount = 0;
    
    public void StartBuilding()
    {
        InvokeRepeating("CreateNewRoadPart", 1f, 0.01f);
    }

    public void CreateNewRoadPart()
    {
        //Where the road should spawn
        Vector3 spawnPos = Vector3.zero;

        //Generates a random number
        float chance = Random.Range(0, 100);
        //Create it to the left
        if(chance < 50)
        {
            spawnPos = new Vector3(lastPos.x + offset, lastPos.y, lastPos.z + offset);
        }
        //Create it to the right
        else
        {
            spawnPos = new Vector3(lastPos.x - offset, lastPos.y, lastPos.z + offset);
        }

        //Create a new object in the specified position, with that rotation
        //Game Object, position (Vector3), rotation (Quaternion).
        GameObject g = Instantiate(roadPrefab, spawnPos, Quaternion.Euler(0, 45, 0));

        //Updates the last position to be the new generated object
        lastPos = g.transform.position;

        //Increases the number of roads created
        roadCount++;
        //Every 5 roads it "creates" a new crystal
        if(roadCount % 5 == 0)
        {
            //Gets the crystal attached to the road,
            //and activate (show it, and make it work)
            //(Stats deactivated)
            g.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
