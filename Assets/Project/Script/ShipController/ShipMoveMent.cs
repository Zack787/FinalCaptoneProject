using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShipMoveMent : MonoBehaviour
{
    [SerializeField] ShipManagerInput.Inputtype _inputType = ShipManagerInput.Inputtype.HumanDesk;

    public IMoveMent MovementControls { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        MovementControls = ShipManagerInput.GetInputControls(_inputType);
    }

    private void OnDestroy()
    {
        MovementControls = null;
    }
}
