using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CharacterMoveComponent : MonoBehaviour
{
    private Vector3 direction;
    [SerializeField] private float speed;
    [SerializeField] private bool isLookAt;
    [SerializeField] private bool isChase;
    private Vector3 targetPosition;

    public Vector3 Direction { get => direction; set => direction = value; }
    public float Speed { get => speed; set => speed = value; }
    public bool IsLookAt { get => isLookAt; set => isLookAt = value; }
    public bool IsChase { get => isChase; set => isChase = value; }
    public Vector3 TargetPosition { get => targetPosition; set => targetPosition = value; }
}
