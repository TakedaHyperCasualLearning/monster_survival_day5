using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnSystem
{
    private GameEvent gameEvent;
    private ObjectPool objectPool;
    private GameObject playerObject;
    private Vector3 screenSize;
    private List<EnemySpawnComponent> enemySpawnComponentList = new List<EnemySpawnComponent>();

    public EnemySpawnSystem(GameEvent gameEvent, ObjectPool objectPool, GameObject player)
    {
        this.gameEvent = gameEvent;
        this.objectPool = objectPool;
        this.playerObject = player;

        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;

        screenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 10));
    }

    public void OnUpdate()
    {
        for (int i = 0; i < enemySpawnComponentList.Count; i++)
        {
            EnemySpawnComponent enemySpawnComponent = enemySpawnComponentList[i];
            if (!enemySpawnComponent.gameObject.activeSelf) continue;

            if (enemySpawnComponent.SpawnTimer < enemySpawnComponent.SpawnInterval)
            {
                enemySpawnComponent.SpawnTimer += Time.deltaTime;
                continue;
            }

            enemySpawnComponent.SpawnTimer = 0;
            Spawn(enemySpawnComponent);
        }
    }

    private void Spawn(EnemySpawnComponent enemySpawnComponent)
    {
        GameObject enemy = objectPool.GetGameObject(enemySpawnComponent.EnemyPrefab);
        Vector3 spawnPosition = new Vector3(Random.Range(screenSize.x, screenSize.x + enemySpawnComponent.SpawnPositionOffset.x), 0.0f, Random.Range(screenSize.y, screenSize.y + enemySpawnComponent.SpawnPositionOffset.z));
        spawnPosition *= Random.Range(0, 2) == 0 ? 1 : -1;
        enemy.transform.position = playerObject.transform.position + spawnPosition;
        if (!objectPool.IsNewGenerate) return;
        gameEvent.AddComponentList?.Invoke(enemy);
        objectPool.IsNewGenerate = false;
    }

    private void AddComponentList(GameObject gameObject)
    {
        EnemySpawnComponent enemySpawnComponent = gameObject.GetComponent<EnemySpawnComponent>();

        if (enemySpawnComponent == null) return;

        enemySpawnComponentList.Add(enemySpawnComponent);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        EnemySpawnComponent enemySpawnComponent = gameObject.GetComponent<EnemySpawnComponent>();

        if (enemySpawnComponent == null) return;

        enemySpawnComponentList.Remove(enemySpawnComponent);
    }
}
