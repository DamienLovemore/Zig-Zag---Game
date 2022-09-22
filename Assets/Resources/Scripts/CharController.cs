using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    public Transform rayStart;

    private Rigidbody rb;
    private bool walkingRight = true;
    private Animator anim;
    private GameManager gameManager;
    public GameObject crystalEffect;

    private float charMoveSpeed = 2;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        //Searches the game for the only one
        //GameManager
        gameManager = FindObjectOfType<GameManager>();
    }

    private void FixedUpdate()
    {
        //If the game has not started yet, them
        //it does not move.
        if(!gameManager.gameStarted)
        {
            return;
        }
        else
        {
            //Starts the animation of running
            //(In the beggining the player is idle)
            anim.SetTrigger("gameStarted");
        }

        //Makes the character move from its point to
        //forward, but not instantly with  a certain
        //delay specified by the Time.deltaTime
        rb.transform.position = transform.position + transform.forward * charMoveSpeed * Time.deltaTime;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Switch();
        }

        RaycastHit hit;

        //If nothing is down towards the bottom and it has fallen below
        //the road level then it should trigger falling.
        //(Some cases the player fall and get onto the next platform,
        //so it should only play if it really has fallen off)
        if((!Physics.Raycast(rayStart.position, -transform.up, out hit, Mathf.Infinity)) && (transform.position.y <=0.35))
        {
            //Activates the trigger so the animator switch between
            //running to falling
            anim.SetTrigger("isFalling");
        }

        //If the player has fallen below a determinated height
        //then it should trigger GameOver
        if(transform.position.y < -5)
        {
            gameManager.EndGame();
        }
    }

    private void Switch()
    {
        //If the game has not started, then the player should
        //not be able to move the character
        if(!gameManager.gameStarted)
        {
            return;
        }

        walkingRight = !walkingRight;

        if (walkingRight)
            transform.rotation = Quaternion.Euler(0, 45, 0);
        else
            transform.rotation = Quaternion.Euler(0, -45, 0);
    }

    public float GetCharSpeed()
    {
        return this.charMoveSpeed;
    }

    public IEnumerator IncreaseCharSpeed()
    {
        bool keepIncreasing = true;

        while (keepIncreasing)
        {
            if (this.charMoveSpeed < 6)
            {
                this.charMoveSpeed += 0.06f;
            }
            else
            {
                keepIncreasing = false;
            }

            yield return new WaitForSeconds(3);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //If the player has collided a crystal it
        //should destroy it, and increase the score
        if(other.tag == "Crystal")
        {
            //Increases the player score
            gameManager.IncreaseScore();

            //Plays the coin collect sound effect
            AudioSource collectEffect = GameObject.Find("CollectEffect").GetComponent<AudioSource>();
            collectEffect.Play();

            //Instiate a particle effect for collecting the coin,
            //that disappers affter two seconds.
            //Syntax:
            //The object to instantiate, the position where to place it, the rotation of the object
            GameObject g = Instantiate(crystalEffect, rayStart.transform.position, Quaternion.identity);
            Destroy(g, 2);

            //Destroy the collect coin immediately
            Destroy(other.gameObject);
        }
    }
}
