using UnityEngine;

/*
-- Class containing base data refering to an entity in game being that
-- a player or monster.
*/
public class EntityStats : MonoBehaviour
{
    [SerializeField] private int _hp;
    public int Hp {
        get { return _hp; }
    }

    [SerializeField] private int _damageDealt;
    public int DamageDealt {
        get { return _damageDealt; }
    }

    [SerializeField] private int _attackCooldown;
    public int AttackCooldown {
        get { return _attackCooldown; }
    }
}
