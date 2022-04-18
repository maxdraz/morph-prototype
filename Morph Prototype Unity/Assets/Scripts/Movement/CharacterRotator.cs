using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterRotationMode
{
    Velocity,
    CameraForward
}

[RequireComponent(typeof(Velocity))]
public class CharacterRotator : MonoBehaviour
{
    [SerializeField] private CharacterRotationMode characterRotationMode;

    private CharacterRotationMode initialRotationMode;
    private CharacterRotationMode finishRotationMode;

    private Timer rotationTimer;
    private Velocity velocity;

    // Start is called before the first frame update
    void Awake()
    {
        velocity = GetComponent<Velocity>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        rotationTimer.Update(Time.deltaTime);

        if (rotationTimer.JustStarted)
        {
            
            characterRotationMode = initialRotationMode;
        }

        if (rotationTimer.JustCompleted)
        {
            characterRotationMode = finishRotationMode;
        }
        
        Rotate();
    }

    private void Rotate()
    {
        switch (characterRotationMode)
        {
            case CharacterRotationMode.Velocity:
                RotateToVelocity();
                break;
            case CharacterRotationMode.CameraForward:
                RotateToCameraForward();
                break;
        }
    }

    private void RotateToVelocity()
    {
        if (velocity)
        {
            var newForwardDir = new Vector3(velocity.CurrentVelocity.x, 0, velocity.CurrentVelocity.z).normalized;
            if (newForwardDir != Vector3.zero)
            {
                transform.forward = newForwardDir;
            }
        }
    }

    private void RotateToCameraForward()
    {
        var newForwardDir = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up).normalized;
        transform.forward = newForwardDir;
    }

    public void StartRotating(CharacterRotationMode initial, CharacterRotationMode finish, Timer durationTimer)
    {
        initialRotationMode = initial;
        finishRotationMode = finish;
        rotationTimer = durationTimer;

        rotationTimer.RestartIfCompleted();
    }
}
