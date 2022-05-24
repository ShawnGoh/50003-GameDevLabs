using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public Transform enemyLocation;
    public TMP_Text scoreText;
    private int score = 0;
    private bool countScoreState = false;

    private bool increaseScore = false;

    public GameOverScreen GameOverScreen;


    // Start is called before the first frame update
    void Start()
    {
        // Set to be 30 FPS
	    Application.targetFrameRate =  30;
	    marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        if (Input.GetKeyDown("a") && faceRightState){
          Debug.Log("Left Key Released");
          faceRightState = false;
          marioSprite.flipX = true;
      }

      if (Input.GetKeyDown("d") && !faceRightState){
          Debug.Log("Right Key Released");
          faceRightState = true;
          marioSprite.flipX = false;
      }

    }

    // called when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground")) {
            Debug.Log("On the Ground");
            onGroundState = true;
            countScoreState = false; // reset score state
            if(increaseScore)
                score+=1;
            ScoreManager.instance.UpdatePoint(score);
            Debug.Log("SCORE:"+score.ToString());
        }
        
    } 



    void  FixedUpdate()
    {
        if (Input.GetKeyUp("a") || Input.GetKeyUp("d")){
            // stop
            Debug.Log("Directional Key Released");
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
            countScoreState = true; //check if Gomba is underneath
        }

                // when jumping, and Gomba is near Mario and we haven't registered our score
        if (!onGroundState && countScoreState)
        {
          if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
          {
              countScoreState = false;
              increaseScore = true;
              Debug.Log(score);
          }
        }
    }


  void OnTriggerEnter2D(Collider2D other)
  {
      if (other.gameObject.CompareTag("Enemy"))
      {
          Debug.Log("Collided with Gomba!");
          increaseScore = false;
          GameOverScreen.Setup(score);
      }
  }

    
}
