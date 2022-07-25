using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStatus : MonoBehaviour
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
