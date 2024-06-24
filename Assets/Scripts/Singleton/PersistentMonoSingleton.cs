using System.Collections;
using System.Collections.Generic;


using UnityEngine;


public abstract class PersistentMonoSingleton<T> : MonoSingleton<T> where T : MonoSingleton<T>
{

    #region Protected Methods

    protected override void OnInitializing()
    {
        base.OnInitializing();
        if (Application.isPlaying)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    #endregion

}