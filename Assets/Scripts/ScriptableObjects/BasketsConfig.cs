using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "BasketsConfig",menuName = "DucksSelection/BasketsConfig")]
public class BasketsConfig : ScriptableObject
{
    [field: SerializeField] public Basket basket{ get; private set; }
    [field: SerializeField] public float basketAppearTime { get; private set; }
    [field: SerializeField] public Ease basketAppearEase { get; private set; }

}
