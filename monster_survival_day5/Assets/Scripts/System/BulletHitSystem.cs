using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHitSystem
{
    private GameEvent gameEvent;
    private GameObject enemyPrefab;
    private ObjectPool objectPool;
    private List<BulletBaseComponent> bulletBaseComponentList = new List<BulletBaseComponent>();
    private List<CollisionComponent> collisionComponentList = new List<CollisionComponent>();

    public BulletHitSystem(GameEvent gameEvent, ObjectPool objectPool, GameObject enemyPrefab)
    {
        this.gameEvent = gameEvent;
        this.objectPool = objectPool;
        this.enemyPrefab = enemyPrefab;
        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
    }

    public void OnUpdate()
    {
        for (int i = 0; i < bulletBaseComponentList.Count; i++)
        {
            BulletBaseComponent bulletBaseComponent = bulletBaseComponentList[i];
            if (!bulletBaseComponent.gameObject.activeSelf) continue;

            List<GameObject> tempList = objectPool.GetObjectList(enemyPrefab);
            for (int j = 0; j < tempList.Count; j++)
            {
                GameObject enemyObject = tempList[j];
                if (!enemyObject.activeSelf) continue;

                CollisionComponent collisionComponent = enemyObject.GetComponent<CollisionComponent>();
                if ((enemyObject.transform.position - bulletBaseComponent.gameObject.transform.position).magnitude < collisionComponent.Radius + collisionComponentList[i].Radius)
                {
                    DamageComponent damageComponent = enemyObject.GetComponent<DamageComponent>();
                    damageComponent.IsDamage = true;
                    damageComponent.DamagePoint = bulletBaseComponent.AttackPoint;
                    gameEvent.ReleaseObject?.Invoke(bulletBaseComponent.gameObject);
                }
            }
        }
    }

    private void AddComponentList(GameObject gameObject)
    {
        BulletBaseComponent bulletBaseComponent = gameObject.GetComponent<BulletBaseComponent>();
        CollisionComponent collisionComponent = gameObject.GetComponent<CollisionComponent>();

        if (bulletBaseComponent == null || collisionComponent == null) return;

        bulletBaseComponentList.Add(bulletBaseComponent);
        collisionComponentList.Add(collisionComponent);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        BulletBaseComponent bulletBaseComponent = gameObject.GetComponent<BulletBaseComponent>();
        CollisionComponent collisionComponent = gameObject.GetComponent<CollisionComponent>();

        if (bulletBaseComponent == null || collisionComponent == null) return;

        bulletBaseComponentList.Remove(bulletBaseComponent);
        collisionComponentList.Remove(collisionComponent);
    }
}

