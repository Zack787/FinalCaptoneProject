    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    public class AnimateCockpitControls : MonoBehaviour
    {
   
        [SerializeField] Transform joystick;
        [SerializeField] Vector3 joystickRange = Vector3.zero;
        [SerializeField] List<Transform> _throttles;
        [SerializeField] float throttleRange = 35f;

   
        [SerializeField] ShipMoveMent _movementInput;

        IMoveMent ControlInput => _movementInput.MovementControls;

        // Update is called once per frame
        void Update()
        {
            joystick.localRotation = Quaternion.Euler(
                ControlInput.PitchAmount * joystickRange.x,
                ControlInput.YawAmount * joystickRange.y,
                ControlInput.RollAmount * joystickRange.z
            );

            Vector3 throttleRotation = _throttles[0].localRotation.eulerAngles;
            throttleRotation.x = ControlInput.ThrustAmount * throttleRange;
            foreach (Transform throttle in _throttles)
            {
                throttle.localRotation = Quaternion.Euler(throttleRotation);
            }
        }
    }