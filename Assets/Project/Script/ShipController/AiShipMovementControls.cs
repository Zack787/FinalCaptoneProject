using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiShipMovementControls : MoveMentControlBase
{
    [Header("AI Ship Controls")]
    [SerializeField] Transform _target;
    [SerializeField] bool _enableYaw = true;
    [SerializeField] PIDController _yawPidController;
    [SerializeField] bool _enablePitch = true;
    [SerializeField] PIDController _pitchPidController;



    public override float YawAmount => GetYawAmount();
    public override float PitchAmount => GetPitchAmount();
    public override float RollAmount => GetRollhAmount();
    public override float ThrustAmount => GetThrustAmount();

    float DistanceToTarget => _target ? Vector3.Distance(_target.position, _transform.position) : 0f;

    public Vector3 _localDirection;
    public float _distanceToTaget;
    public float _yaw, _pitch, _thrustAmount;
    Transform _transform;


    void Awake()
    {
        _transform = transform;
    }

    void Update()
    {
        if (!_target) return;
        _distanceToTaget = DistanceToTarget;
        _localDirection = Quaternion.Inverse(_transform.rotation) * (_target.position - _transform.position);
    }

    float GetYawAmount()
    {
        if (!_target || !_enableYaw) return 0f;
        _yaw = Mathf.Atan2(_localDirection.x, _localDirection.z) * Mathf.Rad2Deg;
        if (Mathf.Approximately(0, _yaw)) return 0f;
        return _yawPidController.Update(Time.deltaTime, _yaw, 0f) * -1f;
    }

    float GetPitchAmount()
    {
        if (!_target || !_enablePitch) return 0f;
        _pitch = Vector3.Angle(Vector3.down, _localDirection) - 90f;
        return _pitchPidController.Update(Time.deltaTime, _pitch, 0f);
    }

    float GetRollhAmount()
    {
        if (!_target) return 0f;
        return Mathf.Abs(_yaw) > 0.25f ? _yaw * -1 : 0f;
    }

    float GetThrustAmount()
    {
        _thrustAmount = Mathf.Lerp(_thrustAmount, _distanceToTaget > 100f ? 1f : 0f, Time.deltaTime);
        return _thrustAmount;
    }
}
