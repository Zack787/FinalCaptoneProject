#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] ShipMoveMent movemetShip;
    [SerializeField]
    [Range(1000f, 10000f)]
    float thrustForce = 7500f,
          pitchForce = 6000f,
          rollForce = 1000f,
          yawForce = 2000f;

    Rigidbody rig;

    [SerializeField]
    [Range(-1f, 1f)]
    float thrustAmount = 2f;

    [SerializeField]
    [Range(-1f, 1f)]
    float pitchAmount = 0f;

    [SerializeField]
    [Range(-1f, 1f)]
    float rollAmount = 0f;

    [SerializeField]
    [Range(-1f, 1f)]
    float yawAmount = 0f;

    IMoveMent MoveMentInput => movemetShip.MovementControls;
    void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        thrustAmount = MoveMentInput.ThrustAmount;
        rollAmount = MoveMentInput.RollAmount;
        yawAmount = MoveMentInput.YawAmount;
        pitchAmount = MoveMentInput.PitchAmount;
    }
    private void FixedUpdate()
    {
        if (!Mathf.Approximately(a: 0f, b: pitchAmount))
        {
            rig.AddTorque(transform.right * (pitchForce * pitchAmount * Time.fixedDeltaTime));
        }
        if (!Mathf.Approximately(0f, rollAmount))
        {
            rig.AddTorque(transform.forward * (rollForce * rollAmount * Time.fixedDeltaTime));
        }

        if (!Mathf.Approximately(0f, yawAmount))
        {
            rig.AddTorque(transform.up * (yawAmount * yawForce * Time.fixedDeltaTime));
        }

        if (!Mathf.Approximately(0f, thrustAmount))
        {
            rig.AddForce(transform.forward * (thrustForce * thrustAmount * Time.fixedDeltaTime));
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(ShipController))]
    public class ShipControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            ShipController shipController = (ShipController)target;

            // Add custom editor fields or functionality here if needed
        }
    }
#endif
}
