using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinMover : MonoBehaviour {
    [SerializeField]
    float amplitude;
    [SerializeField]
    float frequency;

    new Transform transform;
    Vector3 startPosition;

    void Start() {
        transform = GetComponent<Transform>();
        startPosition = transform.position;
    }

    void Update() {
        transform.position = startPosition + new Vector3(Mathf.Sin(Time.time * frequency) * amplitude, 0, 0);
    }
}
