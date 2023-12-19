using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackComponent : MonoBehaviour
{
    private float attackTimer;
    [SerializeField] private float attackInterval;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int split;

    public float AttackTimer { get => attackTimer; set => attackTimer = value; }
    public float AttackInterval { get => attackInterval; set => attackInterval = value; }
    public GameObject BulletPrefab { get => bulletPrefab; set => bulletPrefab = value; }
    public int Split { get => split; set => split = value; }
}
