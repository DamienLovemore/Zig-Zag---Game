using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    public static BackgroundLoop instance;

    private void Awake()
    {
        //If it loaded the game for the first time,
        //them store its reference
        if (instance == null)
            instance = this;
        //If threre was a backgroundLoop for playing
        //music and now there is another, destroy the
        //other
        else if (instance != this)
            //When realoding the scene it creates the
            //things again
            Destroy(gameObject);

        //Makes the object that plays music to not
        //be destroyed when realoading the scene
        //(GameOver)
        DontDestroyOnLoad(gameObject);
    }
}
