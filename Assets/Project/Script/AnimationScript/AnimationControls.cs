using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCockpitControls : MonoBehaviour
{  
    [SerializeField] Transform joystick;
    [SerializeField] Vector3 joystickRange = Vector3.zero;
    [SerializeField] List<Transform> _throttles;
    [SerializeField] float throttleRange = 35f;

    IMoveMent _movementControls; 

    /* IMoveMent ControlInput => _movementInput.MovementControls;*/

    // Update is called once per frame
    void Update()
    {
        if (_movementControls == null) return;
        joystick.localRotation = Quaternion.Euler(
            _movementControls.PitchAmount * joystickRange.x,
            _movementControls.YawAmount * joystickRange.y,
            _movementControls.RollAmount * joystickRange.z
         );

        Vector3 throttleRotation = _throttles[0].localRotation.eulerAngles;
        throttleRotation.x = _movementControls.ThrustAmount * throttleRange;
        foreach (Transform throttle in _throttles)
        {
            throttle.localRotation = Quaternion.Euler(throttleRotation);
        }
    }
    public void Init(IMoveMent movementControls)
    {
        _movementControls = movementControls;
    }
}