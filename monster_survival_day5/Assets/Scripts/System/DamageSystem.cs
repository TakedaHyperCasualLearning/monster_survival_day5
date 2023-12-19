using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystem
{
    private GameEvent gameEvent;
    private List<DamageComponent> damageComponentList = new List<DamageComponent>();
    private List<CharacterBaseComponent> characterBaseComponentList = new List<CharacterBaseComponent>();

    public DamageSystem(GameEvent gameEvent)
    {
        this.gameEvent = gameEvent;
        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
    }

    public void OnUpdate()
    {
        for (int i = 0; i < damageComponentList.Count; i++)
        {
            DamageComponent damageComponent = damageComponentList[i];
            if (!damageComponent.gameObject.activeSelf) continue;

            if (damageComponent.DamageTimer < damageComponent.DamageInterval)
            {
                damageComponent.DamageTimer += Time.deltaTime;
                continue;
            }

            if (!damageComponent.IsDamage) continue;

            characterBaseComponentList[i].HitPoint -= damageComponent.DamagePoint;
            damageComponent.DamageTimer = 0;
            damageComponent.DamagePoint = 0;
            damageComponent.IsDamage = false;

            if (characterBaseComponentList[i].HitPoint <= 0)
            {
                gameEvent.ReleaseObject?.Invoke(damageComponent.gameObject);
            }
        }
    }

    private void AddComponentList(GameObject gameObject)
    {
        DamageComponent damageComponent = gameObject.GetComponent<DamageComponent>();
        CharacterBaseComponent characterBaseComponent = gameObject.GetComponent<CharacterBaseComponent>();

        if (damageComponent == null || characterBaseComponent == null) return;

        damageComponentList.Add(damageComponent);
        characterBaseComponentList.Add(characterBaseComponent);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        DamageComponent damageComponent = gameObject.GetComponent<DamageComponent>();
        CharacterBaseComponent characterBaseComponent = gameObject.GetComponent<CharacterBaseComponent>();

        if (damageComponent == null || characterBaseComponent == null) return;

        damageComponentList.Remove(damageComponent);
        characterBaseComponentList.Remove(characterBaseComponent);
    }
}
