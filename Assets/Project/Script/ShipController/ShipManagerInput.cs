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
    public static IMoveMent GetMoveMentControls(Inputtype inputType)
    {
        return inputType switch
        {
            Inputtype.HumanDesk => new DesMoveControl(),
            Inputtype.HumanMobile => null,
            Inputtype.Bot => null,
            _ => throw new ArgumentOutOfRangeException(nameof(inputType), inputType, message:    null)
        };
    }

    public static IWeaponControls GetWeaponControls(Inputtype inputType)
    {
        return inputType switch
        {
            Inputtype.HumanDesk => new DesWeaponControls(),
            Inputtype.HumanMobile => null,
            Inputtype.Bot => null,
            _ => throw new ArgumentOutOfRangeException(nameof(inputType), inputType, message: null)
        };
    }
}

