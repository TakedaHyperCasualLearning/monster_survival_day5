using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveSystem
{
    private GameEvent gameEvent;
    private GameObject player;

    private List<CameraMoveComponent> cameraMoveComponentList = new List<CameraMoveComponent>();

    public CameraMoveSystem(GameEvent gameEvent, GameObject player)
    {
        this.gameEvent = gameEvent;
        this.player = player;

        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
    }

    public void OnUpdate()
    {
        for (int i = 0; i < cameraMoveComponentList.Count; i++)
        {
            CameraMoveComponent cameraMoveComponent = cameraMoveComponentList[i];
            if (!cameraMoveComponent.gameObject.activeSelf) continue;

            cameraMoveComponent.gameObject.transform.position = player.transform.position + cameraMoveComponent.PositionOffset;
        }
    }

    private void AddComponentList(GameObject gameObject)
    {
        CameraMoveComponent cameraMoveComponent = gameObject.GetComponent<CameraMoveComponent>();

        if (cameraMoveComponent == null) return;

        cameraMoveComponentList.Add(cameraMoveComponent);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        CameraMoveComponent cameraMoveComponent = gameObject.GetComponent<CameraMoveComponent>();

        if (cameraMoveComponent == null) return;

        cameraMoveComponentList.Remove(cameraMoveComponent);
    }
}
