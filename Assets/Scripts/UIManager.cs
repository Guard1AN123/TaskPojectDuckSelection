using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [field: SerializeField] public Canvas mainCanvas {  get; private set; }
    [field: SerializeField] public List<Stage> stages { get; private set; }

    public void advanceStage(int index)
    {
        stages[index].CompleteStage();
    }
}
