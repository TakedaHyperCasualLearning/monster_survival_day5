using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSystem
{
    private GameEvent gameEvent;
    private ObjectPool objectPool;
    private List<PlayerAttackComponent> playerAttackComponentList = new List<PlayerAttackComponent>();
    private List<InputComponent> inputComponentList = new List<InputComponent>();
    private List<CharacterBaseComponent> characterBaseComponentList = new List<CharacterBaseComponent>();


    public PlayerAttackSystem(GameEvent gameEvent, ObjectPool objectPool)
    {
        this.gameEvent = gameEvent;
        this.objectPool = objectPool;
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
            Attack(playerAttackComponent, characterBaseComponentList[i]);
        }
    }

    public void Attack(PlayerAttackComponent playerAttackComponent, CharacterBaseComponent characterBaseComponent)
    {
        GameObject bullet = objectPool.GetGameObject(playerAttackComponent.BulletPrefab);
        bullet.transform.position = playerAttackComponent.gameObject.transform.position;
        bullet.GetComponent<BulletMoveComponent>().Direction = playerAttackComponent.gameObject.transform.forward;
        bullet.GetComponent<BulletBaseComponent>().AttackPoint = characterBaseComponent.AttackPoint;
        if (!objectPool.IsNewGenerate) return;
        gameEvent.AddComponentList?.Invoke(bullet);
        objectPool.IsNewGenerate = false;
    }

    private void AddComponentList(GameObject gameObject)
    {
        PlayerAttackComponent playerAttackComponent = gameObject.GetComponent<PlayerAttackComponent>();
        InputComponent inputComponent = gameObject.GetComponent<InputComponent>();
        CharacterBaseComponent characterBaseComponent = gameObject.GetComponent<CharacterBaseComponent>();

        if (playerAttackComponent == null || inputComponent == null || characterBaseComponent == null) return;

        playerAttackComponentList.Add(playerAttackComponent);
        inputComponentList.Add(inputComponent);
        characterBaseComponentList.Add(characterBaseComponent);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        PlayerAttackComponent playerAttackComponent = gameObject.GetComponent<PlayerAttackComponent>();
        InputComponent inputComponent = gameObject.GetComponent<InputComponent>();
        CharacterBaseComponent characterBaseComponent = gameObject.GetComponent<CharacterBaseComponent>();

        if (playerAttackComponent == null || inputComponent == null || characterBaseComponent == null) return;

        playerAttackComponentList.Remove(playerAttackComponent);
        inputComponentList.Remove(inputComponent);
        characterBaseComponentList.Remove(characterBaseComponent);
    }
}
