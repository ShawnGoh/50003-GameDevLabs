using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerEV : MonoBehaviour
{

    private float force;
    public IntVariable marioUpSpeed;
    public IntVariable marioMaxSpeed;
    public GameConstants gameConstants;

    private Rigidbody2D marioBody;
    private Animator marioAnimator;
    private AudioSource marioJumpAudio;
    private SpriteRenderer marioSprite;
    public AudioClip dieSFX;

    private bool onGroundState = false;
    private bool faceRightState = true;
    private bool isDead = false;
    private bool isADKeyUp = true;
    private bool isSpacebarUp = true;
    private bool countScoreState = false;
    private float moveHorizontal;
    public float speed;
    public float upSpeed;
    
    public CustomCastEvent onConsume;
    

    // Start is called before the first frame update
    void Start()
    {
        marioUpSpeed.SetValue(gameConstants.playerMaxJumpSpeed);
        marioMaxSpeed.SetValue(gameConstants.playerStartingMaxSpeed);
        force = gameConstants.playerDefaultForce;

        marioAnimator = GetComponent<Animator>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioBody = GetComponent<Rigidbody2D>();
        marioJumpAudio = GetComponent<AudioSource>();
        
    }


    void Update()
    {
        if (Input.GetKeyDown("a")){
          faceRightState = false;
          isADKeyUp=false;
          if (Mathf.Abs(marioBody.velocity.x) >  1.0) 
	                marioAnimator.SetTrigger("onSkid");
      }

      if (Input.GetKeyDown("d")){
          faceRightState = true;
          isADKeyUp=false;
          if (Mathf.Abs(marioBody.velocity.x) >  1.0) 
	                marioAnimator.SetTrigger("onSkid");
      }

      
      if (Input.GetKeyDown("space")){
        isSpacebarUp = false;
      }

        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));



        if (Input.GetKeyUp("space")){
            isSpacebarUp = true;
        }

        if (Input.GetKeyUp("a") && !Input.GetKey(KeyCode.D)){
            isADKeyUp = true;
            marioBody.velocity = Vector2.zero;
        }

        if (Input.GetKeyUp("d") && !Input.GetKey(KeyCode.A)){
            isADKeyUp = true;
            marioBody.velocity = Vector2.zero;
        }

        if (Input.GetKeyDown("z")){
            UsePowerup(KeyCode.Z);
        }

        if (Input.GetKeyDown("x")){
            UsePowerup(KeyCode.X);
        }
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            //check if a or d is pressed currently
            if (!isADKeyUp)
            {
                float direction = faceRightState ? 1.0f : -1.0f;
                Vector2 movement = new Vector2(force * direction, 0);
                marioSprite.flipX = !faceRightState;
                if (marioBody.velocity.magnitude < marioMaxSpeed.Value)
                    marioBody.AddForce(movement);
                
                
            }

            if (!isSpacebarUp && onGroundState)
            {
                marioBody.AddForce(Vector2.up * marioUpSpeed.Value, ForceMode2D.Impulse);
                onGroundState = false;
                // part 2
                marioAnimator.SetBool("onGround", onGroundState);
                countScoreState = true; //check if goomba is underneath
            }
        }

    }

    // called when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground")) {
            onGroundState = true;
            countScoreState = false;
            marioAnimator.SetBool("onGround", onGroundState);
        }

        float yoffset = (this.transform.position.y  -  col.transform.position.y);
        if (col.gameObject.CompareTag("Obstacles") && yoffset > 0.75f) {
            onGroundState = true;
            marioAnimator.SetBool("onGround", onGroundState);
        }
        
    } 

    void  PlayJumpSound(){
        marioJumpAudio.PlayOneShot(marioJumpAudio.clip);
    }

    public void  PlayerDiesSequence(){
        // Mario dies
        Debug.Log("Mario dies");
        isDead = true;
        gameObject.transform.GetComponent<Rigidbody2D>().AddForce(Vector2.up  *  50, ForceMode2D.Impulse);
        gameObject.transform.GetComponent<Collider2D>().enabled = false;
        marioBody.gravityScale = 30;
        StartCoroutine(dead());
        PlayDieSound();


    }

    IEnumerator dead()
    {
        yield return new WaitForSeconds(1.0f);
        marioBody.bodyType = RigidbodyType2D.Static;
    }

    void  PlayDieSound(){
        marioJumpAudio.PlayOneShot(dieSFX);
    }

    public void UsePowerup(KeyCode k)
    {
        onConsume.Invoke(k);
    }



}
