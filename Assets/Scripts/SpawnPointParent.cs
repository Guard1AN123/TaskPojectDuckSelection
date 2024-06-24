using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointParent : MonoBehaviour
{
    [field: SerializeField] public List<RectTransform> spawnPoints {  get; private set; }
}
