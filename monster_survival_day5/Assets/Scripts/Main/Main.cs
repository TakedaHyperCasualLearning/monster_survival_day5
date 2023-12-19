using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject enemySpawner;
    [SerializeField] GameObject hitPointTextParent;
    [SerializeField] GameObject LevelUpUI;
    [SerializeField] GameObject cameraObject;
    [SerializeField] GameObject gameOverUI;

    private GameObject player;
    private GameEvent gameEvent;
    private ObjectPool objectPool;

    private CharacterMoveSystem characterMoveSystem;

    private PlayerInputSystem playerInputSystem;
    private PlayerAttackSystem playerAttackSystem;

    private EnemySpawnSystem enemySpawnSystem;
    private EnemyAttackSystem enemyAttackSystem;

    private BulletMoveSystem bulletMoveSystem;
    private BulletHitSystem bulletHitSystem;

    private DamageSystem damageSystem;
    private HitPointUISystem hitPointUISystem;

    private LevelUPSystem levelUPSystem;
    private LevelUpUISystem levelUpUISystem;

    private CameraMoveSystem cameraMoveSystem;
    private GameOverSystem gameOverSystem;

    void Start()
    {
        player = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        gameEvent = new GameEvent();
        objectPool = new ObjectPool(gameEvent);

        characterMoveSystem = new CharacterMoveSystem(gameEvent);

        playerInputSystem = new PlayerInputSystem(gameEvent);
        playerAttackSystem = new PlayerAttackSystem(gameEvent, objectPool);

        enemySpawnSystem = new EnemySpawnSystem(gameEvent, objectPool, player);
        enemyAttackSystem = new EnemyAttackSystem(gameEvent);

        bulletMoveSystem = new BulletMoveSystem(gameEvent, player);
        bulletHitSystem = new BulletHitSystem(gameEvent, objectPool, enemyPrefab);

        damageSystem = new DamageSystem(gameEvent, player);
        hitPointUISystem = new HitPointUISystem(gameEvent, hitPointTextParent);

        levelUPSystem = new LevelUPSystem(gameEvent);
        levelUpUISystem = new LevelUpUISystem(gameEvent, player);

        cameraMoveSystem = new CameraMoveSystem(gameEvent, player);
        gameOverSystem = new GameOverSystem(gameEvent);

        gameEvent.AddComponentList?.Invoke(player);
        gameEvent.AddComponentList?.Invoke(enemySpawner);
        gameEvent.AddComponentList?.Invoke(LevelUpUI);
        gameEvent.AddComponentList?.Invoke(cameraObject);
        gameEvent.AddComponentList?.Invoke(gameOverUI);
    }

    void Update()
    {

        levelUPSystem.OnUpdate();
        levelUpUISystem.OnUpdate();

        if (levelUPSystem.IsLevelUP(player)) return;

        playerInputSystem.OnUpdate();
        characterMoveSystem.OnUpdate();
        playerAttackSystem.OnUpdate();

        enemySpawnSystem.OnUpdate();
        enemyAttackSystem.OnUpdate();

        bulletMoveSystem.OnUpdate();
        bulletHitSystem.OnUpdate();

        damageSystem.OnUpdate();
        hitPointUISystem.OnUpdate();

        cameraMoveSystem.OnUpdate();
        gameOverSystem.OnUpdate();
    }
}
