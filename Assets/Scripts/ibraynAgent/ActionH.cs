using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionTypeH
{
    takeSoldierlevel1,
    takeSoldierlevel2,
    takeSoldierlevel3,
    takeSoldierlevel4,
    takeTowerLevel1,
    takeTowerLevel2,
    SoldierMove,
    takeFarm
}
public class ActionH
{
    ActionTypeH actionHType;
    bool actionCanDo;
  
    public ActionH(ActionTypeH actionHType,int totalGold)
    {
        
    }
    public void SetActionCanDo(int totalGold)
    {
       
    }


}