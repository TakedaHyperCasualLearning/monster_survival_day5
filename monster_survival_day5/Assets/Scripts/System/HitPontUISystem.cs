using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HitPointUISystem
{
    private GameEvent gameEvent;
    private GameObject hitPointUIParent;
    private List<HitPointUIComponent> hitPointUIComponentList = new List<HitPointUIComponent>();
    private List<CharacterBaseComponent> characterBaseComponentList = new List<CharacterBaseComponent>();


    public HitPointUISystem(GameEvent gameEvent, GameObject hitPointUIParent)
    {
        this.gameEvent = gameEvent;
        this.hitPointUIParent = hitPointUIParent;
        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
    }

    public void Initialize(GameObject gameObject)
    {
        HitPointUIComponent hitPointUIComponent = gameObject.GetComponent<HitPointUIComponent>();
        CharacterBaseComponent characterBaseComponent = gameObject.GetComponent<CharacterBaseComponent>();

        if (hitPointUIComponent == null || characterBaseComponent == null) return;

        GameObject obj = GameObject.Instantiate(hitPointUIComponent.HitPointPrefab, hitPointUIComponent.gameObject.transform);
        hitPointUIComponent.HitPointText = obj.GetComponent<TextMeshPro>();
        hitPointUIComponent.HitPointText.transform.SetParent(hitPointUIParent.transform);
        characterBaseComponent.HitPointMax = characterBaseComponent.HitPoint;
        hitPointUIComponent.HitPointText.text = "HP:" + characterBaseComponent.HitPoint.ToString() + "/" + characterBaseComponent.HitPointMax.ToString();
    }

    public void OnUpdate()
    {
        for (int i = 0; i < hitPointUIComponentList.Count; i++)
        {
            HitPointUIComponent hitPointUIComponent = hitPointUIComponentList[i];
            if (!hitPointUIComponent.gameObject.activeSelf) continue;

            hitPointUIComponent.HitPointText.transform.position = characterBaseComponentList[i].gameObject.transform.position + hitPointUIComponent.PositionOffset;

            if (!hitPointUIComponent.IsChange) continue;
            UpdateHitPoint(hitPointUIComponent, characterBaseComponentList[i]);
        }
    }

    public void UpdateHitPoint(HitPointUIComponent hitPointUIComponent, CharacterBaseComponent characterBaseComponent)
    {
        hitPointUIComponent.HitPointText.text = "HP:" + characterBaseComponent.HitPoint.ToString() + "/" + characterBaseComponent.HitPointMax.ToString();
    }

    private void AddComponentList(GameObject gameObject)
    {
        HitPointUIComponent hitPointUIComponent = gameObject.GetComponent<HitPointUIComponent>();
        CharacterBaseComponent characterBaseComponent = gameObject.GetComponent<CharacterBaseComponent>();

        if (hitPointUIComponent == null || characterBaseComponent == null) return;

        hitPointUIComponentList.Add(hitPointUIComponent);
        characterBaseComponentList.Add(characterBaseComponent);

        Initialize(gameObject);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        HitPointUIComponent hitPointUIComponent = gameObject.GetComponent<HitPointUIComponent>();
        CharacterBaseComponent characterBaseComponent = gameObject.GetComponent<CharacterBaseComponent>();

        if (hitPointUIComponent == null || characterBaseComponent == null) return;

        hitPointUIComponentList.Remove(hitPointUIComponent);
        characterBaseComponentList.Remove(characterBaseComponent);
    }
}