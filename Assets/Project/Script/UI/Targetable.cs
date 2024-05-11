using UnityEngine;

public class Targetable : MonoBehaviour
{
    void OnEnable()
    {
        UIManager.Instance.AddTarget(transform);
    }

    void OnDisable()
    {
        RemoveTarget();
    }

    void OnDestroy()
    {
        RemoveTarget();
    }

    void AddTarget()
    {
        if (UIManager.Instance == null)
        {
            UIManager.Instance.AddTarget(transform);
        }
        else
        {
            Debug.LogWarning("UIManager.Instance is null!");
        }
    }

    void RemoveTarget()
    {
        if (UIManager.Instance == null)
        {
            UIManager.Instance.RemoveTarget(transform);
        }
        else
        {
            Debug.LogWarning("UIManager.Instance is null!");
        }
    }
}