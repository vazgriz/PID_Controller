using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalButton : MonoBehaviour {
    [SerializeField]
    Horizontal controller;
    [SerializeField]
    int value;

    public void SetValue() {
        controller.SetTarget(value);
    }
}
