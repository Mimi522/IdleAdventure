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

    protected virtual int Hp { get { return _baseStats.MaxHp; } }
    protected virtual int Damage { get { return _baseStats.DamageDealt; } }
    protected virtual int Cooldown { get { return _baseStats.AttackCooldown; } }

    private int _currentHp;
    public int CurrentHp {
        get { return _currentHp; }
    }

    protected EntityStats _baseStats;

    // The Hp is settle on OnEnbale to make sure the it resets when reusing 
    // the entity
    void OnEnable()
    {
        _baseStats = GetComponent<EntityStats>();
        _currentHp = Hp;
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

        OnHealthChange?.Invoke((float)_currentHp / Hp);
    }

    public void Heal(int amount)
    {
        if (_currentHp <= 0) {
            return;
        }

        _currentHp += amount;

        if (_currentHp > Hp) {
            _currentHp = Hp;
        }

        OnHealthChange?.Invoke((float)_currentHp / Hp);
    }

    private IEnumerator Attack()
    {
        while (_currentHp > 0) {
            yield return new WaitForSeconds(Cooldown);
            OnAttack?.Invoke(DamageInfo.Single(Damage));
        }
    }
}
