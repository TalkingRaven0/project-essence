using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UniRx;
using UnityEngine;
using Zenject;

public class ClickSpawnEssence : MonoBehaviour
{
    [Inject] protected EssencePooler essencePool;
    [Inject] protected InputHandler inputHandler;

    private Subject<float> test = new();

    private void Awake()
    {
        var clickStream = Observable.EveryUpdate()
            .Where(_ => inputHandler.GetMouseLeftClicked());

        clickStream.Subscribe(_ => Debug.Log(_));

        test.AsObservable().Subscribe(value => Debug.Log(value));
    }

    // Update is called once per frame
    void Update()
    {
        if (inputHandler.GetMouseLeftHeld() > 0)
        {
            test.OnNext(inputHandler.GetMouseLeftHeld());
        }
    }
}
