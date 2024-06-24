using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameplayConfig", menuName = "DucksSelection/GameplayConfig")]
public class GameplayConfig : ScriptableObject
{
    [field: SerializeField] public int roundCount { get; private set; }
    [field: SerializeField] public float hintWaitTime { get; private set; }
    [field: SerializeField] public float hintMoveTime { get; private set; }
    [field: SerializeField] public Ease hintMoveEase { get; private set; }
}

