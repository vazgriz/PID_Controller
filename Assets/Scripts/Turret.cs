using System;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Controller {
    [SerializeField]
    PIDController controller;
    [SerializeField]
    float power;
    [SerializeField]
    Transform target;

    new Rigidbody rigidbody;

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
    }

    public override PIDController GetController() {
        return controller;
    }

    public override void SetTarget(int index) {
    }

    void FixedUpdate() {
        var targetPosition = target.position;
        targetPosition.y = rigidbody.position.y;    //ignore difference in Y
        var targetDir = (targetPosition - rigidbody.position).normalized;
        var forwardDir = rigidbody.rotation * Vector3.forward;

        var currentAngle = Vector3.SignedAngle(Vector3.forward, forwardDir, Vector3.up);
        var targetAngle = Vector3.SignedAngle(Vector3.forward, targetDir, Vector3.up);

        float input = controller.UpdateAngle(Time.fixedDeltaTime, currentAngle, targetAngle);
        rigidbody.AddTorque(new Vector3(0, input * power, 0));
    }
}
