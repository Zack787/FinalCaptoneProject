using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissileUI : MonoBehaviour
{
    [SerializeField] MissileLauncher[] _missileLaunchers;
    [SerializeField] Transform[] _missileAmmo;

    [SerializeField] GameObject _missileDisplayPrefab;
    [SerializeField] GameObject _reloadedDisplay;
    [SerializeField] Image _reloadingBar;
    [SerializeField] TMP_Text _reloadsRemaining;

    void OnEnable()
    {
        foreach (var launcher in _missileLaunchers)
        {
            launcher.MissileFired.AddListener(UpdateMissileDisplay);
            launcher.MissilesReloaded.AddListener(OnReloadCompleted);
        }
        UpdateMissileDisplay();
        OnReloadCompleted();
    }

    void OnDisable()
    {
        foreach (var launcher in _missileLaunchers)
        {
            launcher.MissileFired.RemoveListener(UpdateMissileDisplay);
            launcher.MissilesReloaded.RemoveListener(OnReloadCompleted);
        }
    }

    void LateUpdate()
    {
        if (_reloadingBar == null || _reloadedDisplay == null || _missileLaunchers == null || _missileLaunchers.Length == 0)
        {
            // Log thông báo hoặc xử lý lỗi nếu cần thiết
            Debug.LogWarning("Some UI elements or references are missing.");
            return;
        }

        if (!_missileLaunchers[0].Reloading)
        {
            if (!_reloadedDisplay.activeSelf) return;
            _reloadingBar.fillAmount = 0;
            _reloadedDisplay.SetActive(false);
            return;
        }

        _reloadingBar.fillAmount = Mathf.Lerp(_reloadingBar.fillAmount,
            _missileLaunchers[0].ReloadPercent, 10f * Time.deltaTime);

        if (!_reloadedDisplay.activeSelf)
        {
            _reloadedDisplay.SetActive(true);
        }

    }

    void UpdateMissileDisplay()
    {
        if (_missileAmmo == null || _missileAmmo.Length != _missileLaunchers.Length)
        {
            Debug.LogWarning("Missile ammo array is not properly initialized or does not match the number of missile launchers.");
            return;
        }

        for (int i = 0; i < _missileAmmo.Length; ++i)
        {
            if (_missileAmmo[i] == null || _missileLaunchers[i] == null) continue;

            while (_missileAmmo[i].childCount < _missileLaunchers[i].MissileCapacity)
            {
                Instantiate(_missileDisplayPrefab, _missileAmmo[i]);
            }

            for (int m = 0; m < _missileAmmo[i].childCount; ++m)
            {
                _missileAmmo[i].GetChild(m).gameObject.SetActive(m < _missileLaunchers[i].Missiles);
            }
        }
    }

    void OnReloadCompleted()
    {
        UpdateMissileDisplay();
        if (_reloadsRemaining != null)
        {
            _reloadsRemaining.text = $"Reloads: {_missileLaunchers[0].Reloads}";
        }
    }

}
