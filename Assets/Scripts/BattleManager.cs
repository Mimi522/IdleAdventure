using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public partial class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    public event Action OnBattleEnded;

    public GameObject Arena;

    public SpawnLocationProvider[] SpawnPositions;

    private Entity _player;

    private List<Entity> _enemies;
    private List<Coroutine> _enemiesActions;

    private Coroutine _playerAction;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        _enemies = new List<Entity>();
        _enemiesActions = new List<Coroutine>();
        _player = Arena.transform.Find("Player").GetComponent<Entity>();
        _player.OnAttack += PlayerAttack;
        _player.OnDeath += PlayerDeath;
    }

    public void StartBattle(GameObject[] enemiesPrefab)
    {
        Debug.Log("Battle in motion!");
        Arena.SetActive(true);
        SpawnEnemies(enemiesPrefab);
        _player.StartAttacking();
        foreach (Entity enemy in _enemies) {
            enemy.StartAttacking();
        }
    }

    private void PlayerAttack(Entity player)
    {
        if (_enemies.Count > 0) {
            _enemies[0].TakeDamage(player.EntityStats.DamageDealt);
            Debug.Log(string.Format("Player inflicts {0} damage on enemy!", player.EntityStats.DamageDealt));
        }
    }

    private void EnemyAttack(Entity enemyStats)
    {
        if (_player.CurrentHp > 0) {
            _player.TakeDamage(enemyStats.EntityStats.DamageDealt);
            Debug.Log(String.Format("Enemy inflicts {0} damage on player!", enemyStats.EntityStats.DamageDealt));
        }
    }

    private void SpawnEnemies(GameObject[] enemiesPrefab)
    {
        List<Vector3> positions = SpawnPositions[enemiesPrefab.Length - 1].GetLocations();

        for (int i = 0; i < positions.Count; i++) {
            var go = Instantiate(enemiesPrefab[i], positions[i], Quaternion.identity);
            _enemies.Add(go.GetComponent<Entity>());
            _enemies[i].OnAttack += EnemyAttack;
            _enemies[i].OnDeath += EnemyDeath;
        }
    }

    private void EnemyDeath(Entity enemy)
    {
        Debug.Log(enemy.name + " killed!");
        enemy.Dispose();
        _enemies.RemoveAt(0);

        if (_enemies.Count == 0) {
            OnBattleEnded?.Invoke();
            Debug.Log("Battle ended!");
            Arena.SetActive(false);
        }
    }

    private void PlayerDeath(Entity player)
    {
        player.Dispose();
        Debug.Log("GAME OVER");
    }
}