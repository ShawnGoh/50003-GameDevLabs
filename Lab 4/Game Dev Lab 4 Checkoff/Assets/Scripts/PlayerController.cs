using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
 using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    public float speed;
    public float upSpeed;
    private Rigidbody2D marioBody;
    public float maxSpeed = 10;
    private bool onGroundState = false;
    private float moveHorizontal;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;



    public GameOverScreen GameOverScreen;

    private Animator marioAnimator;

    private AudioSource marioJumpAudio;


    // Start is called before the first frame update
    void Start()
    {
          // subscribe to player event
        GameManager.OnPlayerDeath  +=  PlayerDiesSequence;
        // Set to be 30 FPS
	    Application.targetFrameRate =  30;
	    marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioAnimator = GetComponent<Animator>();
        marioJumpAudio = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (Input.GetKeyDown("a") && faceRightState){
          faceRightState = false;
          marioSprite.flipX = true;
          if (Mathf.Abs(marioBody.velocity.x) >  1.0) 
	        marioAnimator.SetTrigger("onSkid");
      }

      if (Input.GetKeyDown("d") && !faceRightState){
          faceRightState = true;
          marioSprite.flipX = false;
          if (Mathf.Abs(marioBody.velocity.x) >  1.0) 
	        marioAnimator.SetTrigger("onSkid");
      }
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));

    if (Input.GetKeyDown("z")){
        CentralManager.centralManagerInstance.consumePowerup(KeyCode.Z,this.gameObject);
    }

    if (Input.GetKeyDown("x")){
        CentralManager.centralManagerInstance.consumePowerup(KeyCode.X,this.gameObject);
    }


    }

    // called when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground")) {
            onGroundState = true;
            marioAnimator.SetBool("onGround", onGroundState);
        }

        if (col.gameObject.CompareTag("Obstacles") && col.gameObject.transform.position.y<marioBody.position.y) {
            onGroundState = true;
            marioAnimator.SetBool("onGround", onGroundState);
        }
        
    } 



    void  FixedUpdate()
    {
        if (Input.GetKeyUp("a") || Input.GetKeyUp("d")){
            // stop
            marioBody.velocity = Vector2.zero;
        }

    
        moveHorizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(moveHorizontal) > 0){
            Vector2 movement = new Vector2(moveHorizontal, 0);
            if (marioBody.velocity.magnitude < maxSpeed)
                    marioBody.AddForce(movement * speed);
        }

        if (Input.GetKey("space") && onGroundState){
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            marioAnimator.SetBool("onGround", onGroundState);
        }

    }


    void OnTriggerEnter2D(Collider2D other)
    {
    }

    void  PlayerDiesSequence(){
        // Mario dies
        Debug.Log("Mario dies");

        gameObject.transform.GetComponent<Rigidbody2D>().AddForce(Vector2.up  *  50, ForceMode2D.Impulse);
        gameObject.transform.GetComponent<Collider2D>().enabled = false;
        
    }

    void  PlayJumpSound(){
        marioJumpAudio.PlayOneShot(marioJumpAudio.clip);
    }

    
}

