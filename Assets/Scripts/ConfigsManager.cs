using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigsManager : MonoSingleton<ConfigsManager>
{
    [field: SerializeField] public BasketsConfig basketsConfig { get; private set; }
    [field: SerializeField] public DucksConfig ducksConfig { get; private set; }
    [field: SerializeField] public GameplayConfig gameplayConfig { get; private set; }
}
