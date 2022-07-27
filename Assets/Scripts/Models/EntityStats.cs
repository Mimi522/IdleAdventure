using UnityEngine;

/// <summary>
/// Class containing base data refering to an entity in game being that
/// a player or monster.
/// </summary>
public class EntityStats : MonoBehaviour
{
    [SerializeField] private int _maxHp;
    public int MaxHp {
        get { return _maxHp; }
    }

    [SerializeField] private int _damageDealt;
    public int DamageDealt {
        get { return _damageDealt; }
    }

    [SerializeField] private int _attackCooldown;
    public int AttackCooldown {
        get { return _attackCooldown; }
    }

    void OnValidate()
    {
        if (_maxHp <= 0) {
            Debug.LogError("Max hp can't be lower or equal to zero.");
            _maxHp = 1;
        }

        if (_damageDealt < 0) {
            Debug.LogError("Damage can't be negative. Don't heal your enemies!");
            _damageDealt = 0;
        }

        if (_attackCooldown <= 0) {
            Debug.LogError("Attack cooldown can't be lower or equal to zero.");
            _attackCooldown = 1;
        }
    }
}
