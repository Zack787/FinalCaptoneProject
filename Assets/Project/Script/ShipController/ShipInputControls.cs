using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShipInputControls : MonoBehaviour
{
    [SerializeField] ShipManagerInput.Inputtype _inputType = ShipManagerInput.Inputtype.HumanDesk;

    public IMoveMent MovementControls { get; private set; }
    public IWeaponControls WeaponControls { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        MovementControls = ShipManagerInput.GetMoveMentControls(_inputType);
        WeaponControls = ShipManagerInput.GetWeaponControls(_inputType);

    }

    private void OnDestroy()
    {
        MovementControls = null;
    }
}
