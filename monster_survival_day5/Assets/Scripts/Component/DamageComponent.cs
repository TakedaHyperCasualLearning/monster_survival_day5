using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageComponent : MonoBehaviour
{
    private int damagePoint;
    private bool isDamage;
    [SerializeField] private bool isInterval;
    private float damageTimer;
    [SerializeField] private float damageInterval;

    public int DamagePoint { get => damagePoint; set => damagePoint = value; }
    public bool IsDamage { get => isDamage; set => isDamage = value; }
    public bool IsInterval { get => isInterval; set => isInterval = value; }
    public float DamageTimer { get => damageTimer; set => damageTimer = value; }
    public float DamageInterval { get => damageInterval; set => damageInterval = value; }
}
