using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent
{
    public Action<GameObject> AddComponentList;
    public Action<GameObject> RemoveComponentList;
    public Action<GameObject> ReleaseObject;
    public Action<GameObject> LevelUp;
    public Action GameOver;
}
