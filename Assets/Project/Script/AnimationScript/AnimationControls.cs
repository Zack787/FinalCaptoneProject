using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCockpitControls : MonoBehaviour
{  
    [SerializeField] Transform joystick;
    [SerializeField] Vector3 joystickRange = Vector3.zero;
    [SerializeField] List<Transform> _throttles;
    [SerializeField] float throttleRange = 35f;

    private IMoveMent _movementInput;
    // Update is called once per frame
    void Update()
    {
        if (_movementInput == null) return;
        joystick.localRotation = Quaternion.Euler(
            _movementInput.PitchAmount * joystickRange.x,
            _movementInput.YawAmount * joystickRange.y,
            _movementInput.RollAmount * joystickRange.z
         );

        Vector3 throttleRotation = _throttles[0].localRotation.eulerAngles;
        throttleRotation.x = _movementInput.ThrustAmount * throttleRange;
        foreach (Transform throttle in _throttles)
        {
            throttle.localRotation = Quaternion.Euler(throttleRotation);
        }
    }
    public void Init(IMoveMent movementControls)
    {
        _movementInput = movementControls;
    }
}