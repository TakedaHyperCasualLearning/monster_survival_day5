using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private GameEvent gameEvent;
    private Dictionary<int, List<GameObject>> objectPoolList = new Dictionary<int, List<GameObject>>();

    private bool isNewGenerate = false;

    public ObjectPool(GameEvent gameEvent)
    {
        this.gameEvent = gameEvent;

        gameEvent.ReleaseObject += ReleaseGameObject;
    }

    public GameObject GetGameObject(GameObject prefab)
    {
        int hash = prefab.GetHashCode();

        if (objectPoolList.ContainsKey(hash))
        {
            List<GameObject> tempList = objectPoolList[hash];

            for (int i = 0; i < tempList.Count; i++)
            {
                GameObject gameObject = tempList[i];

                if (!gameObject.activeSelf)
                {
                    gameObject.SetActive(true);
                    return gameObject;
                }
            }

            GameObject newGameObject = GameObject.Instantiate(prefab);
            newGameObject.SetActive(true);
            tempList.Add(newGameObject);
            isNewGenerate = true;
            return newGameObject;
        }
        else
        {
            List<GameObject> tempList = new List<GameObject>();
            GameObject newGameObject = GameObject.Instantiate(prefab);
            newGameObject.SetActive(true);
            tempList.Add(newGameObject);
            objectPoolList.Add(hash, tempList);
            isNewGenerate = true;
            return newGameObject;
        }
    }

    public void ReleaseGameObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    public bool IsNewGenerate { get => isNewGenerate; set => isNewGenerate = value; }
}
