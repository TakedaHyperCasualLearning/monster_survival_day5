using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputSystem : MonoBehaviour
{
    private GameEvent gameEvent;
    private List<InputComponent> inputComponentList = new List<InputComponent>();
    private List<CharacterMoveComponent> characterMoveComponentList = new List<CharacterMoveComponent>();

    public PlayerInputSystem(GameEvent gameEvent)
    {
        this.gameEvent = gameEvent;

        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
    }

    public void OnUpdate()
    {
        for (int i = 0; i < inputComponentList.Count; i++)
        {
            InputComponent inputComponent = inputComponentList[i];
            if (!inputComponent.gameObject.activeSelf) continue;

            CharacterMoveComponent characterMoveComponent = characterMoveComponentList[i];
            InputMove(characterMoveComponent);
            InputLookAt(characterMoveComponent);
            InputClick(inputComponent);
        }
    }

    private void InputMove(CharacterMoveComponent characterMoveComponent)
    {
        Vector3 direction = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) direction += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) direction += Vector3.back;
        if (Input.GetKey(KeyCode.A)) direction += Vector3.left;
        if (Input.GetKey(KeyCode.D)) direction += Vector3.right;

        characterMoveComponent.Direction = direction;
    }

    private void InputLookAt(CharacterMoveComponent characterMoveComponent)
    {
        Vector3 playerPoint = Camera.main.WorldToScreenPoint(characterMoveComponent.gameObject.transform.position);
        Vector3 rotationDirection = Input.mousePosition - playerPoint;
        rotationDirection = rotationDirection.normalized;
        rotationDirection.z = 0.0f;
        characterMoveComponent.TargetPosition = Camera.main.ScreenToWorldPoint(playerPoint + rotationDirection);
    }

    private void InputClick(InputComponent inputComponent)
    {
        if (Input.GetMouseButton(0))
        {
            inputComponent.IsClick = true;
            return;
        }

        inputComponent.IsClick = false;
    }

    public void AddComponentList(GameObject gameObject)
    {
        InputComponent inputComponent = gameObject.GetComponent<InputComponent>();
        CharacterMoveComponent characterMoveComponent = gameObject.GetComponent<CharacterMoveComponent>();

        if (inputComponent == null || characterMoveComponent == null) return;

        inputComponentList.Add(inputComponent);
        characterMoveComponentList.Add(characterMoveComponent);
    }

    public void RemoveComponentList(GameObject gameObject)
    {
        InputComponent inputComponent = gameObject.GetComponent<InputComponent>();
        CharacterMoveComponent characterMoveComponent = gameObject.GetComponent<CharacterMoveComponent>();

        if (inputComponent == null || characterMoveComponent == null) return;

        inputComponentList.Remove(inputComponent);
        characterMoveComponentList.Remove(characterMoveComponent);
    }
}

