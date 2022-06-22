using TMPro;


public class GameManager :  Singleton<GameUI>
{

	override  public  void  Awake(){
		base.Awake();
		// other instructions...
	}

    public  delegate  void gameEvent();

    public  static  event  gameEvent OnPlayerDeath;

    public  static  event  gameEvent OnScore;

	public TMP_Text score;
	private  int playerScore =  0;
	
	public  void  increaseScore(){
		playerScore  +=  1;
		score.text  =  "SCORE: "  +  playerScore.ToString();
	}

    public  void  increaseScoreSpawnEnemy(){
		playerScore  +=  1;
		score.text  =  "SCORE: "  +  playerScore.ToString();
        OnScore();
	}

    public  void  damagePlayer(){
        OnPlayerDeath();
    }


}
