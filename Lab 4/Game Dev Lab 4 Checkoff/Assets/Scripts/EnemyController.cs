using System.Collections;
using UnityEngine;

public  class EnemyController : MonoBehaviour
{
	public  GameConstants gameConstants;
	private  int moveRight;
	private  float originalX;
	private  Vector2 velocity;
	private  Rigidbody2D enemyBody;

  private bool dead = false;

  private bool mariodead = false;
	
	void  Start()
	{
    // subscribe to player event
    GameManager.OnPlayerDeath  +=  EnemyRejoice;

		enemyBody  =  GetComponent<Rigidbody2D>();
		
		// get the starting position
		originalX  =  transform.position.x;
	
		// randomise initial direction
		moveRight  =  Random.Range(0, 2) ==  0  ?  -1  :  1;
		
		// compute initial velocity
		ComputeVelocity();
	}
	
	void  ComputeVelocity()
	{
			velocity  =  new  Vector2((moveRight) *  gameConstants.maxOffset  /  gameConstants.enemyPatroltime, 0);
	}
  
	void  MoveEnemy()
	{
		enemyBody.MovePosition(enemyBody.position  +  velocity  *  Time.fixedDeltaTime);
	}

	void  Update()
	{
		if(mariodead){
			enemyBody.transform.Rotate(0, 20, 0);
		}else{
			if (Mathf.Abs(enemyBody.position.x  -  originalX) <  gameConstants.maxOffset)
				{// move goomba
					MoveEnemy();
				}
				else
				{
					// change direction
					moveRight  *=  -1;
					ComputeVelocity();
					MoveEnemy();
				}
		}

	}

  	void  OnTriggerEnter2D(Collider2D other)
    {
      // check if it collides with Mario
      if (other.gameObject.tag  ==  "Player"){
        // check if collides on top
        float yoffset = (other.transform.position.y  -  this.transform.position.y);
        if (yoffset  >  0.75f){
          KillSelf();
        }
        else{
          // hurt player
          CentralManager.centralManagerInstance.damagePlayer();
        }
      }
	}

	void  KillSelf(){
		// enemy dies
    if(!dead)
    {
      CentralManager.centralManagerInstance.increaseScore();
      dead=true;
    }
		
		StartCoroutine(flatten());

	}

	IEnumerator  flatten(){

		int steps =  5;
		float stepper =  1.0f/(float) steps;

		for (int i =  0; i  <  steps; i  ++){
			this.transform.localScale  =  new  Vector3(this.transform.localScale.x, this.transform.localScale.y  -  stepper, this.transform.localScale.z);

			// make sure enemy is still above ground
			this.transform.position  =  new  Vector3(this.transform.position.x, gameConstants.groundSurface  +  GetComponent<SpriteRenderer>().bounds.extents.y, this.transform.position.z);
			yield  return  null;
		}

		this.gameObject.SetActive(false);

		yield  break;
	}

      // animation when player is dead
    void  EnemyRejoice(){
        Debug.Log("Enemy killed Mario");
        // do whatever you want here, animate etc
        // ...
		mariodead = true;
    }

}