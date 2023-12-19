using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackSystem
{
    private GameEvent gameEvent;
    private List<CollisionComponent> collisionComponentList = new List<CollisionComponent>();
    private List<CharacterBaseComponent> characterBaseComponentList = new List<CharacterBaseComponent>();
    private List<DamageComponent> damageComponentList = new List<DamageComponent>();

    public EnemyAttackSystem(GameEvent gameEvent)
    {
        this.gameEvent = gameEvent;
        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
    }

    public void OnUpdate()
    {
        for (int i = 0; i < collisionComponentList.Count; i++)
        {
            if (!collisionComponentList[i].gameObject.activeSelf) continue;

            for (int j = 0; j < collisionComponentList.Count; j++)
            {
                if (!characterBaseComponentList[i].gameObject.activeSelf) continue;
                if (collisionComponentList[i].ObjectTag == collisionComponentList[j].ObjectTag) continue;

                if ((collisionComponentList[i].gameObject.transform.position - collisionComponentList[j].gameObject.transform.position).magnitude > collisionComponentList[i].Radius + collisionComponentList[j].Radius) continue;
                DamageComponent playerDamage;
                CharacterBaseComponent enemyBase;
                if (collisionComponentList[i].ObjectTag == "Player")
                {
                    playerDamage = damageComponentList[i];
                    enemyBase = characterBaseComponentList[j];
                }
                else
                {
                    playerDamage = damageComponentList[j];
                    enemyBase = characterBaseComponentList[i];
                }

                playerDamage.IsDamage = true;
                playerDamage.DamagePoint = enemyBase.AttackPoint;
            }
        }
    }

    private void AddComponentList(GameObject gameObject)
    {
        CollisionComponent collisionComponent = gameObject.GetComponent<CollisionComponent>();
        CharacterBaseComponent characterBaseComponent = gameObject.GetComponent<CharacterBaseComponent>();
        DamageComponent damageComponent = gameObject.GetComponent<DamageComponent>();

        if (collisionComponent == null || characterBaseComponent == null || damageComponent == null) return;

        collisionComponentList.Add(collisionComponent);
        characterBaseComponentList.Add(characterBaseComponent);
        damageComponentList.Add(damageComponent);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        CollisionComponent collisionComponent = gameObject.GetComponent<CollisionComponent>();
        CharacterBaseComponent characterBaseComponent = gameObject.GetComponent<CharacterBaseComponent>();
        DamageComponent damageComponent = gameObject.GetComponent<DamageComponent>();

        if (collisionComponent == null || characterBaseComponent == null || damageComponent == null) return;

        collisionComponentList.Remove(collisionComponent);
        characterBaseComponentList.Remove(characterBaseComponent);
        damageComponentList.Remove(damageComponent);
    }
}
