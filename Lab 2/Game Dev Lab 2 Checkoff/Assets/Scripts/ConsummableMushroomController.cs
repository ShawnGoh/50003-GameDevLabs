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
    }

    void  OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player")){
            Debug.Log("Mushroom Hit Player");
            // spawn the mushroom prefab slightly above the box
            mushroombody.velocity = Vector2.zero;
        }
        if (col.gameObject.CompareTag("Obstacles") && col.gameObject.name!="TopCollider"){
            Debug.Log("Mushroom Hit Obstacle");
            // spawn the mushroom prefab slightly above the box
            currentVelocity = mushroombody.velocity;
            direction *=-1;
            currentVelocity.x = direction*speed;
            mushroombody.velocity = currentVelocity;
            
            
        }
    }
    void  OnBecameInvisible(){
        Debug.Log("Mushroom No longer Visisble");
        Destroy(gameObject);	
    }
}
