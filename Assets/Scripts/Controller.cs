using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour {
    public abstract PIDController GetController();
    public abstract void SetTarget(int index);
    public abstract float Power { get; set; }
}
