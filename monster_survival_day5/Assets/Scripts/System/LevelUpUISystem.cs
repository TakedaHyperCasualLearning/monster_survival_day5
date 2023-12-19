using System.Collections;
using System.Collections.Generic;
using TMPro;
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

        levelUpUIComponent.AttackButton.onClick.AddListener(TopButtonFunction);
        levelUpUIComponent.HitPointButton.onClick.AddListener(MiddleButtonFunction);
        levelUpUIComponent.ShotSpeedButton.onClick.AddListener(BottomButtonFunction);
    }

    public void OnUpdate()
    {
        for (int i = 0; i < levelUpUIComponentList.Count; i++)
        {
            LevelUpUIComponent levelUpUIComponent = levelUpUIComponentList[i];
            LevelUPComponent levelUPComponent = playerObject.gameObject.GetComponent<LevelUPComponent>();

            if (levelUPComponent.IsLevelUP && !levelUpUIComponent.gameObject.activeSelf)
            {
                levelUpUIComponent.gameObject.SetActive(true);
                RandomButtonFunction(levelUpUIComponent);
                continue;
            }

            if (levelUPComponent.IsLevelUP) return;
            else levelUpUIComponent.gameObject.SetActive(false);
        }
    }

    public void TopButtonFunction()
    {
        for (int i = 0; i < levelUpUIComponentList.Count; i++)
        {
            LevelUpUIComponent levelUpUIComponent = levelUpUIComponentList[i];
            levelUpUIComponent.AttackButton.onClick.RemoveAllListeners();

            switch (levelUpUIComponent.LevelUpCostList[0])
            {
                case 0:
                    levelUpUIComponent.AttackButton.onClick.AddListener(OnClickAttackButton);
                    break;
                case 1:
                    levelUpUIComponent.AttackButton.onClick.AddListener(OnClickHitPointButton);
                    break;
                case 2:
                    levelUpUIComponent.AttackButton.onClick.AddListener(OnClickAttackSpeedButton);
                    break;
                case 3:
                    levelUpUIComponent.AttackButton.onClick.AddListener(OnClickSplitButton);
                    break;
            }
        }
    }

    public void MiddleButtonFunction()
    {
        for (int i = 0; i < levelUpUIComponentList.Count; i++)
        {
            LevelUpUIComponent levelUpUIComponent = levelUpUIComponentList[i];
            levelUpUIComponent.HitPointButton.onClick.RemoveAllListeners();

            switch (levelUpUIComponent.LevelUpCostList[1])
            {
                case 0:
                    levelUpUIComponent.HitPointButton.onClick.AddListener(OnClickAttackButton);
                    break;
                case 1:
                    levelUpUIComponent.HitPointButton.onClick.AddListener(OnClickHitPointButton);
                    break;
                case 2:
                    levelUpUIComponent.HitPointButton.onClick.AddListener(OnClickAttackSpeedButton);
                    break;
                case 3:
                    levelUpUIComponent.HitPointButton.onClick.AddListener(OnClickSplitButton);
                    break;
            }
        }
    }

    public void BottomButtonFunction()
    {
        for (int i = 0; i < levelUpUIComponentList.Count; i++)
        {
            LevelUpUIComponent levelUpUIComponent = levelUpUIComponentList[i];
            levelUpUIComponent.ShotSpeedButton.onClick.RemoveAllListeners();

            switch (levelUpUIComponent.LevelUpCostList[2])
            {
                case 0:
                    levelUpUIComponent.ShotSpeedButton.onClick.AddListener(OnClickAttackButton);
                    break;
                case 1:
                    levelUpUIComponent.ShotSpeedButton.onClick.AddListener(OnClickHitPointButton);
                    break;
                case 2:
                    levelUpUIComponent.ShotSpeedButton.onClick.AddListener(OnClickAttackSpeedButton);
                    break;
                case 3:
                    levelUpUIComponent.ShotSpeedButton.onClick.AddListener(OnClickSplitButton);
                    break;
            }
        }
    }

    private void RandomButtonFunction(LevelUpUIComponent levelUpUIComponent)
    {
        List<int> randomList = new List<int>();
        for (int i = 0; i < levelUpUIComponent.LevelUpTypCount; i++)
        {
            randomList.Add(Random.Range(0, levelUpUIComponent.LevelUpTypCount));
        }
        levelUpUIComponent.LevelUpCostList = randomList;
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

    public void OnClickSplitButton()
    {
        LevelUPComponent levelUPComponent = playerObject.gameObject.GetComponent<LevelUPComponent>();

        if (levelUPComponent == null) return;

        levelUPComponent.SplitLevel += 1;
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
