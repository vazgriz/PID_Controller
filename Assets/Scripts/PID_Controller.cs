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
    public float proportional;
    public float integral;
    public float derivative;

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

        float P = proportional * error;

        integrationStored = Mathf.Clamp(integrationStored + (error * dt), -integralSaturation, integralSaturation);
        float I = integral * integrationStored;

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

        float D = derivative * deriveMeasure;

        float result = P + I + D;

        return Mathf.Clamp(result, outputMin, outputMax);
    }
}
