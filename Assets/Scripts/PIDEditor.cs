﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PIDEditor : MonoBehaviour {
    [SerializeField]
    Horizontal controllerSource;
    [SerializeField]
    InputField proportionalInput;
    [SerializeField]
    InputField integralInput;
    [SerializeField]
    InputField derivativeInput;
    [SerializeField]
    InputField integralSaturationInput;
    [SerializeField]
    Text valueText;
    [SerializeField]
    Text errorText;
    [SerializeField]
    Text velocityText;
    [SerializeField]
    Text saturationText;

    PIDController controller;

    void Start() {
        controller = controllerSource.GetController();

        UpdateInput(proportionalInput, controller.proportional);
        UpdateInput(integralInput, controller.integral);
        UpdateInput(derivativeInput, controller.derivative);
        UpdateInput(integralSaturationInput, controller.integralSaturation);
    }

    bool TryParse(string text, out float value) {
        if (string.IsNullOrEmpty(text)) {
            value = 0;
            return true;
        }

        return float.TryParse(text, out value);
    }

    public void UpdateProportional(string text) {
        if (TryParse(text, out float value)) {
            proportionalInput.text = text;
            controller.proportional = value;
        }
    }

    public void UpdateIntegral(string text) {
        if (TryParse(text, out float value)) {
            integralInput.text = text;
            controller.integral = value;
        }
    }

    public void UpdateDerivative(string text) {
        if (TryParse(text, out float value)) {
            derivativeInput.text = text;
            controller.derivative = value;
        }
    }

    public void UpdateIntegralSaturation(string text) {
        if (TryParse(text, out float value)) {
            integralSaturationInput.text = text;
            controller.integralSaturation = value;
        }
    }

    void UpdateInput(InputField input, float value) {
        if (input.isFocused) return;
        input.text = value.ToString();
    }

    void Update() {
        valueText.text = string.Format("{0:0.00}", controller.valueLast);
        errorText.text = string.Format("{0:0.00}", controller.errorLast);
        velocityText.text = string.Format("{0:0.00}", controller.velocity);
        saturationText.text = string.Format("{0:0.00}", controller.integrationStored);
    }
}