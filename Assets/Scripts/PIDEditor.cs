using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PIDEditor : MonoBehaviour {
    [SerializeField]
    Controller controllerSource;
    [SerializeField]
    InputField proportionalInput;
    [SerializeField]
    InputField integralInput;
    [SerializeField]
    InputField derivativeInput;
    [SerializeField]
    InputField integralSaturationInput;
    [SerializeField]
    InputField powerInput;
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

        UpdateInput(proportionalInput, controller.proportionalGain);
        UpdateInput(integralInput, controller.integralGain);
        UpdateInput(derivativeInput, controller.derivativeGain);
        UpdateInput(integralSaturationInput, controller.integralSaturation);
        UpdateInput(powerInput, controllerSource.Power);
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
            controller.proportionalGain = value;
        }
    }

    public void UpdateIntegral(string text) {
        if (TryParse(text, out float value)) {
            integralInput.text = text;
            controller.integralGain = value;
        }
    }

    public void UpdateDerivative(string text) {
        if (TryParse(text, out float value)) {
            derivativeInput.text = text;
            controller.derivativeGain = value;
        }
    }

    public void UpdateIntegralSaturation(string text) {
        if (TryParse(text, out float value)) {
            integralSaturationInput.text = text;
            controller.integralSaturation = value;
        }
    }

    public void UpdatePower(string text) {
        if (TryParse(text, out float value)) {
            powerInput.text = text;
            controllerSource.Power = value;
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
