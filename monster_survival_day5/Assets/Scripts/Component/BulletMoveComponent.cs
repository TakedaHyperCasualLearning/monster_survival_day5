using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveComponent : MonoBehaviour
{
    private Vector3 direction;
    [SerializeField] private float speed;
    [SerializeField] private float deleteRange;

    public Vector3 Direction { get => direction; set => direction = value; }
    public float Speed { get => speed; set => speed = value; }
    public float DeleteRange { get => deleteRange; set => deleteRange = value; }
}
