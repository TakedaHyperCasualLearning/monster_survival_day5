using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUPComponent : MonoBehaviour
{
    private int experiencePoint;
    [SerializeField] private int experiencePointBorder;
    private int attackLevel;
    private int attackLevelOld;
    private int attackBase;
    [SerializeField] private int attackRaiseValue;
    private int hitPointLevel;
    private int hitPointLevelOld;
    private int hitPointBase;
    [SerializeField] private int hitPointRaiseValue;
    private int attackSpeedLevel;
    private int attackSpeedLevelOld;
    private float attackSpeedBase;
    [SerializeField] private float attackSpeedRaiseValue;
    private bool isLevelUP;

    public int ExperiencePoint { get => experiencePoint; set => experiencePoint = value; }
    public int ExperiencePointBorder { get => experiencePointBorder; set => experiencePointBorder = value; }
    public int AttackLevel { get => attackLevel; set => attackLevel = value; }
    public int AttackLevelOld { get => attackLevelOld; set => attackLevelOld = value; }
    public int AttackBase { get => attackBase; set => attackBase = value; }
    public int AttackRaiseValue { get => attackRaiseValue; set => attackRaiseValue = value; }
    public int HitPointLevel { get => hitPointLevel; set => hitPointLevel = value; }
    public int HitPointLevelOld { get => hitPointLevelOld; set => hitPointLevelOld = value; }
    public int HitPointBase { get => hitPointBase; set => hitPointBase = value; }
    public int HitPointRaiseValue { get => hitPointRaiseValue; set => hitPointRaiseValue = value; }
    public int AttackSpeedLevel { get => attackSpeedLevel; set => attackSpeedLevel = value; }
    public int AttackSpeedLevelOld { get => attackSpeedLevelOld; set => attackSpeedLevelOld = value; }
    public float AttackSpeedBase { get => attackSpeedBase; set => attackSpeedBase = value; }
    public float AttackSpeedRaiseValue { get => attackSpeedRaiseValue; set => attackSpeedRaiseValue = value; }
    public bool IsLevelUP { get => isLevelUP; set => isLevelUP = value; }
}
