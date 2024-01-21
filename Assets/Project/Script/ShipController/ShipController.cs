#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections.Generic;
using System;

public class ShipController : MonoBehaviour
{
    [Header("Ship Input Controls")]
    [SerializeField] MoveMentControlBase _movementControls;
    [SerializeField] WeaponControlsBase _weaponControls;
   

    Rigidbody rig;
    DamageHandler _damageHandler;


    [Header("Ship Components")]
    [SerializeField] List<ShipEngine> _shipEngine;
    [SerializeField] AnimateCockpitControls _animatonControls;
    [SerializeField] List<Blaster> blasters;
    
    [SerializeField]
    ShipDataSo _shipData;

  /*  [SerializeField]
    [Range(-1f, 1f)]
    float thrustAmount = 2f;*/

    [SerializeField]
    [Range(-1f, 1f)]
    float pitchAmount = 0f;

    [SerializeField]
    [Range(-1f, 1f)]
    float rollAmount = 0f;

    [SerializeField]
    [Range(-1f, 1f)]
    float yawAmount = 0f;

    IMoveMent MoveMentInput => _movementControls;
    IWeaponControls WeaponInput => _weaponControls;
    void Awake()
    {
        rig = GetComponent<Rigidbody>();
        _damageHandler = GetComponent<DamageHandler>();
    }

    void Start()
    {
        foreach (ShipEngine engine in _shipEngine)
        {
            engine.Init(MoveMentInput, rig, _shipData.ThrustForce / _shipEngine.Count);
        }
    
        foreach (Blaster blaster in blasters)
        {
            blaster.Init(WeaponInput, _shipData.BlasterCooldown, _shipData.BlasterLaunchForce, _shipData.BlasterProjectileDuration, _shipData.BlasterDamage);
        }
        if(_animatonControls!= null)
        {
            _animatonControls.Init(MoveMentInput);
        }
    }

    void OnEnable()
    {
        if (_damageHandler == null) return;
        _damageHandler.Init(_shipData.MaxHealth);
        _damageHandler.HealthChanged.AddListener(OnHealthChanged);
        _damageHandler.ObjectDestroyed.AddListener(DestroyShip);
    }

   

    private void Update()
    {
        //thrustAmount = MoveMentInput.ThrustAmount;
        rollAmount = MoveMentInput.RollAmount;
        yawAmount = MoveMentInput.YawAmount;
        pitchAmount = MoveMentInput.PitchAmount;
    }
    private void FixedUpdate()
    {
        if (!Mathf.Approximately(a: 0f, b: pitchAmount))
        {
            rig.AddTorque(transform.right * (_shipData.PitchForce * pitchAmount * Time.fixedDeltaTime));
        }
        if (!Mathf.Approximately(0f, rollAmount))
        {
            rig.AddTorque(transform.forward * (_shipData.RollForce * rollAmount * Time.fixedDeltaTime));
        }

        if (!Mathf.Approximately(0f, yawAmount))
        {
            rig.AddTorque(transform.up * (yawAmount * _shipData.YawForce * Time.fixedDeltaTime));
        }

        /*if (!Mathf.Approximately(0f, thrustAmount))
        {
            rig.AddForce(transform.forward * (thrustForce * thrustAmount * Time.fixedDeltaTime));
        }*/

    }
    private void DestroyShip()
    {
        gameObject.SetActive(false);
    }

    private void OnHealthChanged()
    {
        Debug.Log($"{gameObject.name} health is {_damageHandler.Health} / {_damageHandler.Maxhealth}");    
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
