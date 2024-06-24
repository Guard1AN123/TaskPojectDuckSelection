using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DucksConfig", menuName = "DucksSelection/DucksConfig")]
public class DucksConfig : ScriptableObject
{
    [field: SerializeField] public Duck duck { get; private set; }
    [field: SerializeField] public float duckMoveInTime { get; private set; }
    [field: SerializeField] public Ease duckMoveInEase { get; private set; }
}
