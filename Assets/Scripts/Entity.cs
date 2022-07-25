using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EntityStats))]
public class Entity : MonoBehaviour
{
    public event Action<Entity> OnAttack;
    public event Action<Entity> OnDeath;
    public UnityEvent<float> OnHealthChange;

    private EntityStats _entityStats;
    public EntityStats EntityStats {
        get { return _entityStats; }
    }

    private int _currentHp;
    public int CurrentHp {
        get { return _currentHp; }
    }

    void OnEnable()
    {
        _entityStats = GetComponent<EntityStats>();
        _currentHp = _entityStats.Hp;
    }

    public void Dispose()
    {
        OnAttack += null;
        OnDeath += null;
        Destroy(gameObject);
    }

    public void StartAttacking()
    {
        StartCoroutine(Attack());
    }

    public void StopAttacking()
    {
        StopCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        while (_currentHp > 0) {
            // Turn into protected variable on base
            yield return new WaitForSeconds(EntityStats.AttackCooldown);
            OnAttack?.Invoke(this);
        }
    }

    public void TakeDamage(int value)
    {
        if (_currentHp <= 0)
            return;

        _currentHp -= value;

        if (_currentHp <= 0) {
            _currentHp = 0;
            StopAttacking();
            OnDeath?.Invoke(this);
        }

        OnHealthChange?.Invoke((float)_currentHp / _entityStats.Hp);
    }
}
