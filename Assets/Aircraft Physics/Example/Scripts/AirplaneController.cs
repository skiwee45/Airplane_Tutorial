using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AirplaneController : MonoBehaviour
{
    [SerializeField]
    float rollControlSensitivity = 0.2f;
    [SerializeField]
    float pitchControlSensitivity = 0.2f;
    [SerializeField]
    float yawControlSensitivity = 0.2f;
    [SerializeField]
    float thrustControlSensitivity = 0.01f;
    [SerializeField]
    float flapControlSensitivity = 0.15f;


    float pitch;
    float yaw;
    float roll;
    float flap;

    float thrustPercent;
    bool brake = false;

    AircraftPhysics aircraftPhysics;
    Rotator propeller;

    private void Start()
    {
        aircraftPhysics = GetComponent<AircraftPhysics>();
        propeller = FindObjectOfType<Rotator>();
        SetThrust(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            SetThrust(thrustPercent + thrustControlSensitivity);
        }
        propeller.speed = thrustPercent * 1500f;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            thrustControlSensitivity *= -1;
            flapControlSensitivity *= -1;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            brake = !brake;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            flap += flapControlSensitivity;
            //clamp
            flap = Mathf.Clamp(flap, 0f, Mathf.Deg2Rad * 40);
        }

        pitch = pitchControlSensitivity * Input.GetAxis("Vertical");
        roll = rollControlSensitivity * Input.GetAxis("Horizontal");
        yaw = yawControlSensitivity * Input.GetAxis("Yaw");
    }

    private void SetThrust(float percent)
    {
        thrustPercent = Mathf.Clamp01(percent);
    }

    private void FixedUpdate()
    {
        aircraftPhysics.SetControlSurfecesAngles(pitch, roll, yaw, flap);
        aircraftPhysics.SetThrustPercent(thrustPercent);
        aircraftPhysics.Brake(brake);
    }
}
