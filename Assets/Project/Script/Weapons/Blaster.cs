using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour
{
    [SerializeField] private Projectile _projectilePrefab;

    [SerializeField] private Transform _muzzle;
    [SerializeField] [Range(0f, 5f)] float _coolDownTime = 0.25f;
    private IWeaponControls _weaponInput;
    int _launchForce, _damage;
    float _duration;
    float _coolDown;
    Rigidbody _rig;


    private bool CanFire
    {
        get
        {
            _coolDown -= Time.deltaTime;
            return _coolDown <= 0f;
        }
    }

   
    // Update is called once per frame
    void Update()
    {
        if (_weaponInput == null) return;
        if (CanFire && _weaponInput.PrimaryFired)
        {
            FireProjectile();
        }
    }
    public void Init(IWeaponControls weaponInput, float coolDown, int launchForce, float duration, int damage, Rigidbody rig)
    {
        Debug.Log($"Blaster.Init({weaponInput}, {coolDown}, launchForce, {duration}");
        _weaponInput = weaponInput;
        _coolDownTime = coolDown;
        _launchForce = launchForce;
        _duration = duration;
        _damage = damage;
        _rig = rig;
    }

    void FireProjectile()
    {
        _coolDown = _coolDownTime;
        Projectile projectile = Instantiate(_projectilePrefab, _muzzle.position, transform.rotation);
        projectile.gameObject.SetActive(false);
        projectile.Init(_launchForce, _damage, _duration, _rig.velocity, _rig.angularVelocity);
        projectile.gameObject.SetActive(true);
    }
}
