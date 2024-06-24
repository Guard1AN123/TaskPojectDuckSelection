using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Initializing,
    Interactable,
    Finished
}
public enum DuckState
{
    Dragable,
    NotDragable
}
public enum SoundType
{
    Correct,
    Wrong,
    Duck,
    Pop,
    Music,
    WaterSplash
}