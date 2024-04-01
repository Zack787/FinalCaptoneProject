using UnityEngine;

using UnityEngine.AI;

public class AiShipWeaponControls : WeaponControlsBase
{
    public override bool PrimaryFired => _firePrimary;
    public override bool SecondaryFired => _fireSecondary;

    bool _firePrimary, _fireSecondary;
    Transform _transform, _target;
    float _attackRange;
    int _layerMask;

    void Awake()
    {
        _transform = transform;
    }

    void Update()
    {
        _firePrimary = CanFirePrimary();
        _fireSecondary = CanFireSecondary();
        // Debug.Log để kiểm tra giá trị của _firePrimary và _fireSecondary
        Debug.Log($"Primary fired: {_firePrimary}, Secondary fired: {_fireSecondary}");
    }

    bool CanFirePrimary()
    {
        if (!_target) return false;
        return Physics.Raycast(_transform.position, _transform.forward, out var hit, _attackRange * 0.5f, _layerMask);
    }

    bool CanFireSecondary()
    {
        if (!_target) return false;
        if (!Physics.SphereCast(_transform.position, 3f, _transform.forward, out var hit,
                _attackRange, _layerMask)) return false;
        return (_target.transform.forward - _transform.forward).magnitude < 0.5f;
    }

    public void SetTarget(Transform target, float attackRange, int targetMask)
    {
        _target = target;
        _attackRange = attackRange;
        _layerMask = targetMask;
        // Debug.Log để kiểm tra liệu SetTarget đã được gọi chưa
        Debug.Log("Target set: " + target.name);
    }
}
