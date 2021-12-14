using System;
using System.Collections.Generic;
using UnityEngine;

public class Horizontal : Controller {
    [SerializeField]
    PIDController controller;
    [SerializeField]
    float power;
    [SerializeField]
    Transform[] targets;
    [SerializeField]
    GameObject flameRight;
    [SerializeField]
    GameObject flameLeft;
    [SerializeField]
    float flameSize;

    new Rigidbody rigidbody;
    List<Vector3> targetPositions;
    Vector3 targetPosition;

    public override float Power {
        get {
            return power;
        }
        set {
            power = value;
        }
    }

    void Start() {
        rigidbody = GetComponent<Rigidbody>();

        targetPositions = new List<Vector3>();
        foreach (var target in targets) {
            targetPositions.Add(target.position);
        }
    }

    public override PIDController GetController() {
        return controller;
    }

    public override void SetTarget(int index) {
        targetPosition = targetPositions[index];
    }

    void SetScale(GameObject go, float scale) {
        scale = Mathf.Clamp(scale, 0, 1);

        if (scale < 0.1f) {
            go.SetActive(false);
        } else {
            go.SetActive(true);
            go.GetComponent<Transform>().localScale = new Vector3(scale, scale, scale) * flameSize;
        }
    }

    void FixedUpdate() {
        float throttle = controller.Update(Time.fixedDeltaTime, rigidbody.position.x, targetPosition.x);
        rigidbody.AddForce(new Vector3(throttle * power, 0, 0));

        SetScale(flameRight, -throttle);
        SetScale(flameLeft, throttle);
    }
}
