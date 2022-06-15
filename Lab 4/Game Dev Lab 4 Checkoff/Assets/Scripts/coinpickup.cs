using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinpickup : MonoBehaviour
{
    private bool collected = false;
    private AudioSource coinCollectAudio;

    // Start is called before the first frame update
    void Start()
    {
        coinCollectAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void  OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.CompareTag("Player") &&  !collected){
            collected  =  true;
            PlayCoinCollected();
            Debug.Log("Player touched coin");
            gameObject.transform.parent.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
            CentralManager.centralManagerInstance.increaseScoreSpawnEnemy();
            GetComponent<BoxCollider2D>().enabled  =  false;
            Destroy(gameObject);
        }
    }  

    void  PlayCoinCollected(){
        coinCollectAudio.PlayOneShot(coinCollectAudio.clip);
        
    }     
}
