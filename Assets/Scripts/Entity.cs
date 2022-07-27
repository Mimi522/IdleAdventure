using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Class responsible for handling the state of an entity.
/// </summary>
[RequireComponent(typeof(EntityStats))]
public class Entity : MonoBehaviour
{
    public event Action<DamageInfo> OnAttack;
    public event Action<Entity> OnDeath;
    public UnityEvent<float> OnHealthChange;

    private EntityStats _entityStats;

    private int _currentHp;
    public int CurrentHp {
        get { return _currentHp; }
    }

    // The Hp is settle on OnEnbale to make sure the it resets when reusing 
    // the entity
    void OnEnable()
    {
        _entityStats = GetComponent<EntityStats>();
        _currentHp = _entityStats.MaxHp;
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

    public void TakeDamage(int amount)
    {
        if (_currentHp <= 0) {
            return;
        }

        _currentHp -= amount;

        if (_currentHp <= 0) {
            _currentHp = 0;
            StopAttacking();
            OnDeath?.Invoke(this);
        }

        OnHealthChange?.Invoke((float)_currentHp / _entityStats.MaxHp);
    }

    public void Heal(int amount)
    {
        if (_currentHp <= 0) {
            return;
        }

        _currentHp += amount;

        if (_currentHp > _entityStats.MaxHp) {
            _currentHp = _entityStats.MaxHp;
        }

        OnHealthChange?.Invoke((float)_currentHp / _entityStats.MaxHp);
    }

    private IEnumerator Attack()
    {
        while (_currentHp > 0) {
            yield return new WaitForSeconds(_entityStats.AttackCooldown);
            OnAttack?.Invoke(DamageInfo.Single(_entityStats.DamageDealt));
        }
    }
}
