using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HitPointUIComponent : MonoBehaviour
{
    [SerializeField] private GameObject hitPointPrefab;
    private GameObject hitPointParent;
    private TextMeshPro hitPointText;
    [SerializeField] private Vector3 positionOffset;
    private bool isChange;

    public GameObject HitPointPrefab { get => hitPointPrefab; set => hitPointPrefab = value; }
    public GameObject HitPointParent { get => hitPointParent; set => hitPointParent = value; }
    public TextMeshPro HitPointText { get => hitPointText; set => hitPointText = value; }
    public Vector3 PositionOffset { get => positionOffset; set => positionOffset = value; }
    public bool IsChange { get => isChange; set => isChange = value; }
}
