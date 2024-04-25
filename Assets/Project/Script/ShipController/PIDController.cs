using System;
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

    public float Update(float currentValue, float targetValue, float v)
    {
        if (!_enablePid) return targetValue;

        float deltaTime = Time.deltaTime;
        // Check if deltaTime is almost zero, set it to a very small value to avoid division by zero
        if (Mathf.Approximately(deltaTime, 0))
        {
            deltaTime = 0.0001f; // Or any other small non-zero value you prefer
        }

        float error = targetValue - currentValue;

        // Calculate P term
        float P = _proportionalGain * error;

        // Calculate I term
        _integrationStored = Mathf.Clamp(_integrationStored + (error * deltaTime), -_integralSaturation, _integralSaturation);
        float I = _integralGain * _integrationStored;

        // Calculate both D terms
        float errorRateOfChange = (error - _lastError) / deltaTime;
        _lastError = error;

        float valueRateOfChange = (currentValue - _lastValue) / deltaTime;
        _lastValue = currentValue;
        _velocity = valueRateOfChange;

        // Choose D term to use
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
