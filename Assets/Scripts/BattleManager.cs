using System.Collections.Generic;
using System;
using UnityEngine;

/*
-- Class that controls the battle between player and monster in a monster
-- camp tile.
*/
public partial class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    public event Action OnBattleEnded;

    public SpawnLocationProvider[] SpawnPositions;

    [SerializeField] private GameObject _arena;

    [SerializeField] private Entity _player;
    public Entity Player {
        get { return _player; }
    }

    private List<Entity> _enemies;

    void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
        _enemies = new List<Entity>();

        _player.OnAttack += PlayerAttack;
        _player.OnDeath += PlayerDeath;
    }

    public void StartBattle(GameObject[] enemiesPrefab)
    {
        Debug.Log("Battle in motion!");
        _arena.SetActive(true);

        _player.StartAttacking();

        SpawnEnemies(enemiesPrefab);

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
        if (enemiesPrefab.Length > SpawnPositions.Length) {
            Debug.LogError("Number of enemies not supported.");
            return;
        }

        // Enemies positions varies accord to the amount of enmies in battle
        List<Vector3> positions = SpawnPositions[enemiesPrefab.Length - 1].GetLocations();

        for (int i = 0; i < positions.Count; i++) {
            GameObject enemy = Instantiate(enemiesPrefab[i], positions[i], Quaternion.identity);
            _enemies.Add(enemy.GetComponent<Entity>());
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
            _arena.SetActive(false);
        }
    }

    private void PlayerDeath(Entity player)
    {
        player.Dispose();
        Debug.Log("GAME OVER");
    }
}
