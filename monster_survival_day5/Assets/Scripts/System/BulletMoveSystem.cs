using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveSystem : MonoBehaviour
{
    private GameEvent gameEvent;
    private GameObject player;
    private List<BulletMoveComponent> bulletMoveComponentList = new List<BulletMoveComponent>();

    public BulletMoveSystem(GameEvent gameEvent, GameObject gameObject)
    {
        this.gameEvent = gameEvent;
        this.player = gameObject;

        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
    }

    public void OnUpdate()
    {
        for (int i = 0; i < bulletMoveComponentList.Count; i++)
        {
            BulletMoveComponent bulletMoveComponent = bulletMoveComponentList[i];
            if (!bulletMoveComponent.gameObject.activeSelf) continue;

            bulletMoveComponent.gameObject.transform.Translate(bulletMoveComponent.Direction * bulletMoveComponent.Speed * Time.deltaTime);

            if ((bulletMoveComponent.gameObject.transform.position - player.transform.position).magnitude < bulletMoveComponent.DeleteRange) continue;
            gameEvent.ReleaseObject?.Invoke(bulletMoveComponent.gameObject);
        }
    }

    private void AddComponentList(GameObject gameObject)
    {
        BulletMoveComponent bulletMoveComponent = gameObject.GetComponent<BulletMoveComponent>();

        if (bulletMoveComponent == null) return;

        bulletMoveComponentList.Add(bulletMoveComponent);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        BulletMoveComponent bulletMoveComponent = gameObject.GetComponent<BulletMoveComponent>();

        if (bulletMoveComponent == null) return;

        bulletMoveComponentList.Remove(bulletMoveComponent);
    }
}
