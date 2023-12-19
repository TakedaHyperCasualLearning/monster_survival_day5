using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystem
{
    private GameEvent gameEvent;
    private GameObject player;
    private List<DamageComponent> damageComponentList = new List<DamageComponent>();
    private List<CharacterBaseComponent> characterBaseComponentList = new List<CharacterBaseComponent>();
    private List<HitPointUIComponent> hitPointUIComponentList = new List<HitPointUIComponent>();

    public DamageSystem(GameEvent gameEvent, GameObject player)
    {
        this.gameEvent = gameEvent;
        this.player = player;
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
            hitPointUIComponentList[i].IsChange = true;

            if (characterBaseComponentList[i].HitPoint <= 0)
            {
                damageComponent.gameObject.GetComponent<HitPointUIComponent>().HitPointText.gameObject.SetActive(false);
                gameEvent.ReleaseObject?.Invoke(damageComponent.gameObject);
                if (damageComponent.gameObject != player)
                    player.GetComponent<LevelUPComponent>().ExperiencePoint += 1;
                if (damageComponent.gameObject == player)
                    gameEvent.GameOver?.Invoke();
            }
        }
    }

    private void AddComponentList(GameObject gameObject)
    {
        DamageComponent damageComponent = gameObject.GetComponent<DamageComponent>();
        CharacterBaseComponent characterBaseComponent = gameObject.GetComponent<CharacterBaseComponent>();
        HitPointUIComponent hitPointUIComponent = gameObject.GetComponent<HitPointUIComponent>();

        if (damageComponent == null || characterBaseComponent == null || hitPointUIComponent == null) return;

        damageComponentList.Add(damageComponent);
        characterBaseComponentList.Add(characterBaseComponent);
        hitPointUIComponentList.Add(hitPointUIComponent);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        DamageComponent damageComponent = gameObject.GetComponent<DamageComponent>();
        CharacterBaseComponent characterBaseComponent = gameObject.GetComponent<CharacterBaseComponent>();
        HitPointUIComponent hitPointUIComponent = gameObject.GetComponent<HitPointUIComponent>();

        if (damageComponent == null || characterBaseComponent == null || hitPointUIComponent == null) return;

        damageComponentList.Remove(damageComponent);
        characterBaseComponentList.Remove(characterBaseComponent);
        hitPointUIComponentList.Remove(hitPointUIComponent);
    }
}
