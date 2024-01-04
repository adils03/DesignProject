using Assets.Scripts.ibraynAgent;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType_
{
    BuyItem,
    SoldierMove,
}
public class Action
{
    public ActionType_ ActionType;
  
    public Action(ActionType_ actionType)
    {
        ActionType = actionType;
    }
   
}
public class BuyUnitAction : Action
{
    public ObjectType BuyUnitType { get; private set; }
    public Hex TargetHex { get; private set; }

    public BuyUnitAction(ObjectType buyUnitType, Hex targetHex)
        : base(ActionType_.BuyItem)
    {
        BuyUnitType = buyUnitType;
        TargetHex = targetHex;
    }
}

public class MoveUnitAction : Action
{
    public ObjectType UnitID { get; private set; }
    public Hex SourceHex { get; private set; }
    public Hex TargetHex { get; private set; }

    public MoveUnitAction(ObjectType unit, Hex sourceHex, Hex targetHex)
        : base(ActionType_.SoldierMove)
    {
        UnitID = unit;
        SourceHex = sourceHex;
        TargetHex = targetHex;
    }
}
