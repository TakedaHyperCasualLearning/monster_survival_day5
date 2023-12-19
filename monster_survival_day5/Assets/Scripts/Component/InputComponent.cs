using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputComponent : MonoBehaviour
{
    private bool isClick;
    private Vector3 clickPosition;

    public bool IsClick { get => isClick; set => isClick = value; }
    public Vector3 ClickPosition { get => clickPosition; set => clickPosition = value; }
}
