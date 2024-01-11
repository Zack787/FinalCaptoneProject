using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipEngine : MonoBehaviour
{
    [SerializeField] GameObject _thruster;

    IMoveMent _shipMovementControls;
    Rigidbody _rigidbody;
    float _thrustForce;
    float _thrustAmount;

    bool ThrustersEnabled => !Mathf.Approximately(0f, _shipMovementControls.ThrustAmount);

    void Update()
    {
        ActivateThrusters();
    }

    void FixedUpdate()
    {
        if (!ThrustersEnabled) return;
        _rigidbody.AddForce(transform.forward * _thrustAmount * Time.fixedDeltaTime);
    }

    public void Init(IMoveMent movementControls, Rigidbody rb, float thrustForce)
    {
        _shipMovementControls = movementControls;
        _rigidbody = rb;
        _thrustForce = thrustForce;
    }

    void ActivateThrusters()
    {
        if (_thruster != null)
            _thruster.SetActive(ThrustersEnabled);

        if (!ThrustersEnabled) return;
        _thrustAmount = _thrustForce * _shipMovementControls.ThrustAmount;
    }
}
