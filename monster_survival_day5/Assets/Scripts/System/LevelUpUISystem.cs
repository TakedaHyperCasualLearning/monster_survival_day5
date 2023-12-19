using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpUISystem
{
    private GameEvent gameEvent;
    private GameObject playerObject;
    private List<LevelUpUIComponent> levelUpUIComponentList = new List<LevelUpUIComponent>();

    public LevelUpUISystem(GameEvent gameEvent, GameObject playerObject)
    {
        this.gameEvent = gameEvent;
        this.playerObject = playerObject;
        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
    }

    public void Initialize(GameObject gameObject)
    {
        LevelUpUIComponent levelUpUIComponent = gameObject.GetComponent<LevelUpUIComponent>();

        if (levelUpUIComponent == null) return;

        levelUpUIComponent.AttackButton.onClick.AddListener(OnClickAttackButton);
        levelUpUIComponent.HitPointButton.onClick.AddListener(OnClickHitPointButton);
        levelUpUIComponent.ShotSpeedButton.onClick.AddListener(OnClickAttackSpeedButton);
    }

    public void OnUpdate()
    {
        for (int i = 0; i < levelUpUIComponentList.Count; i++)
        {
            LevelUpUIComponent levelUpUIComponent = levelUpUIComponentList[i];
            LevelUPComponent levelUPComponent = playerObject.gameObject.GetComponent<LevelUPComponent>();

            if (levelUPComponent.IsLevelUP) levelUpUIComponent.gameObject.SetActive(true);
            else levelUpUIComponent.gameObject.SetActive(false);
        }
    }

    public void OnClickAttackButton()
    {
        LevelUPComponent levelUPComponent = playerObject.gameObject.GetComponent<LevelUPComponent>();

        if (levelUPComponent == null) return;

        levelUPComponent.AttackLevel += 1;
        gameEvent.LevelUp?.Invoke(playerObject);
        levelUPComponent.IsLevelUP = false;
    }

    public void OnClickHitPointButton()
    {
        LevelUPComponent levelUPComponent = playerObject.gameObject.GetComponent<LevelUPComponent>();

        if (levelUPComponent == null) return;

        levelUPComponent.HitPointLevel += 1;
        gameEvent.LevelUp?.Invoke(playerObject);
        levelUPComponent.IsLevelUP = false;
    }

    public void OnClickAttackSpeedButton()
    {
        LevelUPComponent levelUPComponent = playerObject.gameObject.GetComponent<LevelUPComponent>();

        if (levelUPComponent == null) return;

        levelUPComponent.AttackSpeedLevel += 1;
        gameEvent.LevelUp?.Invoke(playerObject);
        levelUPComponent.IsLevelUP = false;
    }



    private void AddComponentList(GameObject gameObject)
    {
        LevelUpUIComponent levelUpUIComponent = gameObject.GetComponent<LevelUpUIComponent>();

        if (levelUpUIComponent == null) return;

        levelUpUIComponentList.Add(levelUpUIComponent);
        Initialize(gameObject);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        LevelUpUIComponent levelUpUIComponent = gameObject.GetComponent<LevelUpUIComponent>();

        if (levelUpUIComponent == null) return;

        levelUpUIComponentList.Remove(levelUpUIComponent);
    }
}
