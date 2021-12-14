using System;
using System.Collections.Generic;
using UnityEngine;

public class Vertical : Controller {
    [SerializeField]
    PIDController controller;
    [SerializeField]
    float power;
    [SerializeField]
    Transform[] targets;
    [SerializeField]
    GameObject flame;
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

        SetTarget(0);
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
        float throttle = controller.Update(Time.fixedDeltaTime, rigidbody.position.y, targetPosition.y);
        rigidbody.AddForce(new Vector3(0, throttle * power, 0));

        SetScale(flame, throttle);
    }
}
