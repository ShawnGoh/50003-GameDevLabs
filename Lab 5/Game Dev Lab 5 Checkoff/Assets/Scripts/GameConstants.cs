using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName =  "GameConstants", menuName =  "ScriptableObjects/GameConstants", order =  1)]
public class GameConstants : ScriptableObject
{

    // for Scoring system
    int currentScore;
    int currentPlayerHealth;
    
    // Mario basic starting values
    public int playerStartingMaxSpeed = 5;
    public int playerMaxJumpSpeed = 30;
    public int playerDefaultForce = 150;
    public  float groundDistance =  -4.0f;

    // for Reset values
    Vector3 goombaSpawnPointStart = new Vector3(2.5f, -0.45f, 0); // hardcoded location
    // .. other reset values 

    
    // for BreakBrick.cs
    public  int breakTimeStep =  30;
    public  int breakDebrisTorque =  10;
    public  int breakDebrisForce =  10;
    
    public int maxOffset = 5;

    public int enemyPatroltime = 5;

    public int groundSurface = -1;

    public Vector3 enemyRotation = new Vector3(0, 20, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
