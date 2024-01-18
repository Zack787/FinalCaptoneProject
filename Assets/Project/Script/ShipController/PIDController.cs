﻿using System;
using UnityEngine;

[Serializable]
public class PIDController
{
    public enum DerivativeMeasurement
    {
        Velocity,
        ErrorRateOfChange
    }

    [Header("PID Parameters")]
    [SerializeField]
    float
        _proportionalGain,
        _integralGain,
        _derivativeGain,
        _minOutput,
        _maxOutput,
        _integralSaturation;

    [Header("PID Parameters")]
    [SerializeField]
    DerivativeMeasurement _derivativeMeasurement;

    [Header("PID Parameters")]
    [SerializeField]
    private bool _enablePid = true;

    public float _lastValue, _lastError, _integrationStored, _velocity;
    private bool _derivativeInitialized;

    public void Reset()
    {
        _derivativeInitialized = false;
    }

    public float Update(float deltaTime, float currentValue, float targetValue)
    {
        if (!_enablePid) return targetValue;
        if (deltaTime <= 0) throw new ArgumentOutOfRangeException(nameof(deltaTime));

        float error = targetValue - currentValue;

        //calculate P term
        float P = _proportionalGain * error;

        //calculate I term
        _integrationStored = Mathf.Clamp(_integrationStored + (error * deltaTime), -_integralSaturation, _integralSaturation);
        float I = _integralGain * _integrationStored;

        //calculate both D terms
        float errorRateOfChange = (error - _lastError) / deltaTime;
        _lastError = error;

        float valueRateOfChange = (currentValue - _lastValue) / deltaTime;
        _lastValue = currentValue;
        _velocity = valueRateOfChange;

        //choose D term to use
        float deriveMeasure = 0;

        if (_derivativeInitialized)
        {
            if (_derivativeMeasurement == DerivativeMeasurement.Velocity)
            {
                deriveMeasure = -valueRateOfChange;
            }
            else
            {
                deriveMeasure = errorRateOfChange;
            }
        }
        else
        {
            _derivativeInitialized = true;
        }

        float D = _derivativeGain * deriveMeasure;

        float result = P + I + D;

        return Mathf.Clamp(result, _minOutput, _maxOutput);
    }
}
