using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsummableMushroomController : MonoBehaviour
{

    public Rigidbody2D mushroombody;
    private Vector2 currentPosition;
    public float speed;

    public float direction = -1;
    private Vector2 currentVelocity;

    private bool collected = false;
    private bool despawn = false;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Mushroom Spawned");
        mushroombody.GetComponent<Rigidbody2D>();
        mushroombody.AddForce(Vector2.up  *  20, ForceMode2D.Impulse);
        mushroombody.AddForce(Vector2.left  *  speed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (collected){
            mushroombody.transform.localScale += new Vector3(1f, 1f, 1f);
            if (mushroombody.transform.localScale.y > 2f)
            {
               gameObject.transform.GetComponent<SpriteRenderer>().enabled = false;
            }
            
        }
    }

    void  OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player")){
            // spawn the mushroom prefab slightly above the box
            mushroombody.velocity = Vector2.zero;
            collected = true;
            CentralManager.centralManagerInstance.increaseScoreSpawnEnemy();
            
        }
        if (col.gameObject.CompareTag("Obstacles") && col.gameObject.name!="TopCollider"){
            // spawn the mushroom prefab slightly above the box
            currentVelocity = mushroombody.velocity;
            direction *=-1;
            currentVelocity.x = direction*speed;
            mushroombody.velocity = currentVelocity;
            
            
        }
    }
    // void  OnBecameInvisible(){
    //     Debug.Log("Mushroom No longer Visisble");
    //     Destroy(gameObject);	
    // }
}
