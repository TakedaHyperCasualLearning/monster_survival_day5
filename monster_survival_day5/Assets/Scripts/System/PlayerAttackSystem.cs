using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSystem
{
    private GameEvent gameEvent;
    private List<PlayerAttackComponent> playerAttackComponentList = new List<PlayerAttackComponent>();
    private List<InputComponent> inputComponentList = new List<InputComponent>();


    public PlayerAttackSystem(GameEvent gameEvent)
    {
        this.gameEvent = gameEvent;
        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
    }

    public void OnUpdate()
    {
        for (int i = 0; i < playerAttackComponentList.Count; i++)
        {
            PlayerAttackComponent playerAttackComponent = playerAttackComponentList[i];
            if (!playerAttackComponent.gameObject.activeSelf) continue;

            if (playerAttackComponent.AttackTimer < playerAttackComponent.AttackInterval)
            {
                playerAttackComponent.AttackTimer += Time.deltaTime;
                continue;
            }

            if (!inputComponentList[i].IsClick) continue;
            playerAttackComponent.AttackTimer = 0;
            Attack();
        }
    }

    public void Attack()
    {
        Debug.Log("Attack");
    }

    private void AddComponentList(GameObject gameObject)
    {
        PlayerAttackComponent playerAttackComponent = gameObject.GetComponent<PlayerAttackComponent>();
        InputComponent inputComponent = gameObject.GetComponent<InputComponent>();

        if (playerAttackComponent == null || inputComponent == null) return;

        playerAttackComponentList.Add(playerAttackComponent);
        inputComponentList.Add(inputComponent);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        PlayerAttackComponent playerAttackComponent = gameObject.GetComponent<PlayerAttackComponent>();
        InputComponent inputComponent = gameObject.GetComponent<InputComponent>();

        if (playerAttackComponent == null || inputComponent == null) return;

        playerAttackComponentList.Remove(playerAttackComponent);
        inputComponentList.Remove(inputComponent);
    }
}
