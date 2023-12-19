using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnComponent : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval;
    private float spawnTimer;
    [SerializeField] private Vector3 spawnPositionOffset;

    public GameObject EnemyPrefab { get => enemyPrefab; set => enemyPrefab = value; }
    public float SpawnInterval { get => spawnInterval; set => spawnInterval = value; }
    public float SpawnTimer { get => spawnTimer; set => spawnTimer = value; }
    public Vector3 SpawnPositionOffset { get => spawnPositionOffset; set => spawnPositionOffset = value; }
}
