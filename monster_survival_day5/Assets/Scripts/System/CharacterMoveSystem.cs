using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterMoveSystem
{
    private GameEvent gameEvent;
    private List<CharacterMoveComponent> characterMoveComponentList = new List<CharacterMoveComponent>();

    public CharacterMoveSystem(GameEvent gameEvent)
    {
        this.gameEvent = gameEvent;

        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
    }

    public void OnUpdate()
    {
        for (int i = 0; i < characterMoveComponentList.Count; i++)
        {
            CharacterMoveComponent characterMoveComponent = characterMoveComponentList[i];
            if (!characterMoveComponent.gameObject.activeSelf) continue;

            if (characterMoveComponent.IsLookAt)
            {
                characterMoveComponent.transform.LookAt(characterMoveComponent.TargetPosition);
            }
            if (characterMoveComponent.IsChase)
            {
                characterMoveComponent.Direction = characterMoveComponent.gameObject.transform.forward;
            }

            characterMoveComponent.transform.Translate(characterMoveComponent.Direction * characterMoveComponent.Speed * Time.deltaTime, Space.Self);
        }
    }

    public void AddComponentList(GameObject gameObject)
    {
        CharacterMoveComponent characterMoveComponent = gameObject.GetComponent<CharacterMoveComponent>();

        if (characterMoveComponent == null) return;

        characterMoveComponentList.Add(characterMoveComponent);
    }

    public void RemoveComponentList(GameObject gameObject)
    {
        CharacterMoveComponent characterMoveComponent = gameObject.GetComponent<CharacterMoveComponent>();

        if (characterMoveComponent == null) return;

        characterMoveComponentList.Remove(characterMoveComponent);
    }
}
