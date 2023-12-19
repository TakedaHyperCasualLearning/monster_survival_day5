using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionComponent : MonoBehaviour
{
    [SerializeField] private string objectTag;
    private Vector3 position;
    [SerializeField] private float radius;

    public string ObjectTag { get => objectTag; set => objectTag = value; }
    public Vector3 Position { get => Position; set => Position = value; }
    public float Radius { get => radius; set => radius = value; }
}
