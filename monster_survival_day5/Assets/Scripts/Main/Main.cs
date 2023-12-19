using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    private GameObject player;
    private GameEvent gameEvent;

    private CharacterMoveSystem characterMoveSystem;

    private PlayerInputSystem playerInputSystem;

    void Start()
    {
        player = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        gameEvent = new GameEvent();

        characterMoveSystem = new CharacterMoveSystem(gameEvent);

        playerInputSystem = new PlayerInputSystem(gameEvent);

        gameEvent.AddComponentList?.Invoke(player);
    }

    void Update()
    {
        playerInputSystem.OnUpdate();
        characterMoveSystem.OnUpdate();
    }
}
