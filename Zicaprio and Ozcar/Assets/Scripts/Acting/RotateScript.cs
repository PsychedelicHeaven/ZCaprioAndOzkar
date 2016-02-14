using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour {

    public float _Angle;
    public float _Period;

    private float _Time;

    public float current_angle;

    // Update is called noce per frame
    void Update()
    {
        _Time = _Time + Time.deltaTime;
        float phase = Mathf.Sin(_Time / _Period);

        current_angle = -phase * _Angle + 90;
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, phase * _Angle));
    }
}
