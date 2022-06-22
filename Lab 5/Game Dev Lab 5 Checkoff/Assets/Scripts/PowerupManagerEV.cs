using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PowerupIndex
{
    GREENMUSHROOM = 0,
    REDMUSHROOM = 1
}
public class PowerupManagerEV : MonoBehaviour
{
  // reference of all player stats affected
  public IntVariable marioJumpSpeed;
  public IntVariable marioMaxSpeed;
  public PowerupInventory powerupInventory;
  public List<GameObject> powerupIcons;

  void Start()
  {
         Debug.Log(powerupInventory.gameStarted);
      if (!powerupInventory.gameStarted)
      {
          powerupInventory.gameStarted = true;
          powerupInventory.Setup(powerupIcons.Count);
          resetPowerup();
      }
      else
      {
          // re-render the contents of the powerup from the previous time
          for (int i = 0; i < powerupInventory.Items.Count; i++)
          {
              Powerup p = powerupInventory.Get(i);
              if (p != null)
              {
                  AddPowerupUI(i);
              }else{
                powerupIcons[i].SetActive(false);
              }
          }
      }
  }
    
  public void resetPowerup()
  {
        Debug.Log("reseting powerups");
      for (int i = 0; i < powerupIcons.Count; i++)
      {
          powerupIcons[i].SetActive(false);
      }
  }
    
  void AddPowerupUI(int index)
  {
      powerupIcons[index].SetActive(true);
  }

  public void AddPowerup(Powerup p)
  {
      powerupInventory.Add(p, (int)p.index);
      AddPowerupUI((int)p.index);
  }

  public void OnApplicationQuit()
  {
    Debug.Log("application Quit");
      resetPowerup();
      powerupInventory.Clear();
  }

public  void  consumePowerup(KeyCode k){
        switch(k){
            case  KeyCode.Z:
                if(powerupInventory.Get(0)!=null){
                    marioMaxSpeed.SetValue(marioMaxSpeed.Value + powerupInventory.Get(0).aboluteSpeedBooster);
                    marioJumpSpeed.SetValue(marioJumpSpeed.Value + powerupInventory.Get(0).absoluteJumpBooster);
                    StartCoroutine(removePowerup(0, powerupInventory.Get(0).duration));
                    powerupIcons[0].SetActive(false);
                    
                }
                break;
            case  KeyCode.X:
                if(powerupInventory.Get(1)!=null){
                    marioMaxSpeed.SetValue(marioMaxSpeed.Value + powerupInventory.Get(1).aboluteSpeedBooster);
                    marioJumpSpeed.SetValue(marioJumpSpeed.Value + powerupInventory.Get(1).absoluteJumpBooster);
                    StartCoroutine(removePowerup(1, powerupInventory.Get(1).duration));
                    powerupIcons[1].SetActive(false);
                }
                break;
            default:
                break;
        }
    }

  IEnumerator  removePowerup(int index, int duration){
		yield  return  new  WaitForSeconds(duration);
        Debug.Log("5 seconds ended, removing powerup");
        marioJumpSpeed.SetValue(marioJumpSpeed.Value-powerupInventory.Get(index).absoluteJumpBooster);
        marioMaxSpeed.SetValue(marioMaxSpeed.Value-powerupInventory.Get(index).aboluteSpeedBooster);
        powerupInventory.Remove(index);
        
	}
 }