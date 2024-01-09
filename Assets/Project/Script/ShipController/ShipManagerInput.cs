using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManagerInput : MonoBehaviour
{
    public enum Inputtype
    {
        HumanDesk,
        HumanMobile,
        Bot
    }    
    public static IMoveMent GetInputControls(Inputtype inputType)
    {
        return inputType switch
        {
            Inputtype.HumanDesk => new DesMoveControl(),
            Inputtype.HumanMobile => null,
            Inputtype.Bot => null,
            _ => throw new ArgumentOutOfRangeException(nameof(inputType), inputType, message:    null)
        };
    }    
}

