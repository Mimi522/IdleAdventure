using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EntityStats))]
public class Entity : MonoBehaviour
{
    private EntityStats _entityStats;
    public EntityStats EntityStats {
        get { return _entityStats; }
    }

    private int _currentHp;
    public int CurrentHp {
        get { return _currentHp; }
    }

    public UnityEvent<float> OnHealthChange;

    void OnEnable()
    {
        _entityStats = GetComponent<EntityStats>();
        _currentHp = _entityStats.Hp;
    }

    public void Dispose()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int value)
    {
        _currentHp -= value;
        OnHealthChange?.Invoke((float)_currentHp / _entityStats.Hp);
    }
}
