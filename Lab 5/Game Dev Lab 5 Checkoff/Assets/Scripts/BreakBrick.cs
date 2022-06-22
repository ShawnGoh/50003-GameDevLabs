using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBrick : MonoBehaviour
{
    private bool broken = false;
    public GameObject prefab;

    public GameObject coin;

    private AudioSource breakBlockAudio;


    // Start is called before the first frame update
    void Start()
    {
        breakBlockAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void  OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.CompareTag("Player") &&  !broken){
            broken  =  true;
            PlayBreakBlock();
            Debug.Log("Player touched breakable brick");
            // assume we have 5 debris per box
            for (int x =  0; x<5; x++){
                Instantiate(prefab, transform.position, Quaternion.identity);
            }
            gameObject.transform.parent.GetComponent<SpriteRenderer>().enabled  =  false;
            gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled  =  false;
            coin.GetComponent<SpriteRenderer>().enabled  =  true;
            coin.GetComponent<BoxCollider2D>().enabled  =  true;
            GetComponent<EdgeCollider2D>().enabled  =  false;
        }
    }        


    void  PlayBreakBlock(){
        breakBlockAudio.PlayOneShot(breakBlockAudio.clip);
    }                                                                   
}
