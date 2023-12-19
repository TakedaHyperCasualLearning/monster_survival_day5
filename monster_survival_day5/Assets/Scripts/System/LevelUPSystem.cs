using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUPSystem
{
    private GameEvent gameEven;
    private List<LevelUPComponent> levelUPComponentList = new List<LevelUPComponent>();
    private List<CharacterBaseComponent> characterBaseComponentList = new List<CharacterBaseComponent>();
    private List<PlayerAttackComponent> playerAttackComponentList = new List<PlayerAttackComponent>();

    public LevelUPSystem(GameEvent gameEvent)
    {
        gameEven = gameEvent;
        gameEven.AddComponentList += AddComponentList;
        gameEven.RemoveComponentList += RemoveComponentList;
        gameEven.LevelUp += UpdateLevelUpStatus;
    }

    public void OnUpdate()
    {
        for (int i = 0; i < levelUPComponentList.Count; i++)
        {
            LevelUPComponent levelUPComponent = levelUPComponentList[i];
            if (!levelUPComponentList[i].gameObject.activeSelf) continue;

            if (levelUPComponent.ExperiencePoint < levelUPComponent.ExperiencePointBorder) continue;
            levelUPComponent.ExperiencePoint -= levelUPComponent.ExperiencePointBorder;
            levelUPComponent.IsLevelUP = true;
        }
    }

    private void UpdateLevelUpStatus(GameObject gameObject)
    {
        LevelUPComponent levelUPComponent = gameObject.GetComponent<LevelUPComponent>();
        CharacterBaseComponent characterBaseComponent = gameObject.GetComponent<CharacterBaseComponent>();
        PlayerAttackComponent playerAttackComponent = gameObject.GetComponent<PlayerAttackComponent>();

        if (levelUPComponent == null || characterBaseComponent == null || playerAttackComponent == null) return;

        if (levelUPComponent.AttackLevelOld != levelUPComponent.AttackLevel)
        {
            characterBaseComponent.AttackPoint = levelUPComponent.AttackBase + levelUPComponent.AttackLevel * levelUPComponent.AttackRaiseValue;
        }
        if (levelUPComponent.HitPointLevelOld != levelUPComponent.HitPointLevel)
        {
            characterBaseComponent.HitPoint = characterBaseComponent.HitPoint + levelUPComponent.HitPointRaiseValue;
            characterBaseComponent.HitPointMax = levelUPComponent.HitPointBase + levelUPComponent.HitPointLevel * levelUPComponent.HitPointRaiseValue;
            gameObject.GetComponent<HitPointUIComponent>().IsChange = true;
        }
        if (levelUPComponent.AttackSpeedLevelOld != levelUPComponent.AttackSpeedLevel)
        {
            playerAttackComponent.AttackInterval = levelUPComponent.AttackSpeedBase - levelUPComponent.AttackSpeedLevel * levelUPComponent.AttackSpeedRaiseValue;
        }
        if (levelUPComponent.SplitLevelOld != levelUPComponent.SplitLevel)
        {
            playerAttackComponent.Split = levelUPComponent.SplitBase + levelUPComponent.SplitLevel * levelUPComponent.SplitRaiseValue;
        }

        levelUPComponent.AttackLevelOld = levelUPComponent.AttackLevel;
        levelUPComponent.HitPointLevelOld = levelUPComponent.HitPointLevel;
        levelUPComponent.AttackSpeedLevelOld = levelUPComponent.AttackSpeedLevel;
    }

    private void AddComponentList(GameObject gameObject)
    {
        LevelUPComponent levelUPComponent = gameObject.GetComponent<LevelUPComponent>();
        CharacterBaseComponent characterBaseComponent = gameObject.GetComponent<CharacterBaseComponent>();
        PlayerAttackComponent playerAttackComponent = gameObject.GetComponent<PlayerAttackComponent>();

        if (levelUPComponent == null || characterBaseComponent == null || playerAttackComponent == null) return;

        levelUPComponentList.Add(levelUPComponent);
        characterBaseComponentList.Add(characterBaseComponent);
        playerAttackComponentList.Add(playerAttackComponent);

        levelUPComponent.AttackBase = characterBaseComponent.AttackPoint;
        levelUPComponent.HitPointBase = characterBaseComponent.HitPoint;
        levelUPComponent.AttackSpeedBase = playerAttackComponent.AttackInterval;
        levelUPComponent.SplitBase = playerAttackComponent.Split;
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        LevelUPComponent levelUPComponent = gameObject.GetComponent<LevelUPComponent>();
        CharacterBaseComponent characterBaseComponent = gameObject.GetComponent<CharacterBaseComponent>();
        PlayerAttackComponent playerAttackComponent = gameObject.GetComponent<PlayerAttackComponent>();

        if (levelUPComponent == null || characterBaseComponent == null || playerAttackComponent == null) return;

        levelUPComponentList.Remove(levelUPComponent);
        characterBaseComponentList.Remove(characterBaseComponent);
        playerAttackComponentList.Remove(playerAttackComponent);
    }

    public bool IsLevelUP(GameObject gameObject)
    {
        LevelUPComponent levelUPComponent = gameObject.GetComponent<LevelUPComponent>();

        if (levelUPComponent == null) return false;

        return levelUPComponent.IsLevelUP;
    }
}
