using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PIDController {
    public enum DerivativeMeasurement {
        Measurement,
        Error
    }

    //PID coefficients
    public float proportionalGain;
    public float integralGain;
    public float derivativeGain;

    public float outputMin = -1;
    public float outputMax = 1;
    public float integralSaturation;
    public DerivativeMeasurement derivativeMeasurement;

    public float valueLast;
    public float errorLast;
    public float integrationStored;
    public float velocity;

    public float Update(float dt, float currentValue, float targetValue) {
        if (dt <= 0) throw new ArgumentOutOfRangeException(nameof(dt));

        float error = targetValue - currentValue;

        float P = proportionalGain * error;

        integrationStored = Mathf.Clamp(integrationStored + (error * dt), -integralSaturation, integralSaturation);
        float I = integralGain * integrationStored;

        float errorRateOfChange = (error - errorLast) / dt;
        errorLast = error;

        float valueRateOfChange = (currentValue - valueLast) / dt;
        valueLast = currentValue;
        velocity = valueRateOfChange;

        float deriveMeasure;
        if (derivativeMeasurement == DerivativeMeasurement.Measurement) {
            deriveMeasure = -valueRateOfChange;
        } else {
            deriveMeasure = errorRateOfChange;
        }

        float D = derivativeGain * deriveMeasure;

        float result = P + I + D;

        return Mathf.Clamp(result, outputMin, outputMax);
    }
}
