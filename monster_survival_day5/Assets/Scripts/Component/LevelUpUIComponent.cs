using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpUIComponent : MonoBehaviour
{
    [SerializeField] Button attackButton;
    [SerializeField] Button hitPointButton;
    [SerializeField] Button shotSpeedButton;

    public Button AttackButton { get => attackButton; set => attackButton = value; }
    public Button HitPointButton { get => hitPointButton; set => hitPointButton = value; }
    public Button ShotSpeedButton { get => shotSpeedButton; set => shotSpeedButton = value; }
}
