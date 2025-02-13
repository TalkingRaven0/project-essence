using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ReferenceManager : MonoInstaller
{
    public GameObject PlayerObject;
    public Camera mainCamera;
    public InputHandler inputHandler;

    public override void InstallBindings()
    {
        Container.Bind<Camera>().FromInstance(mainCamera);
        Container.Bind<GameObject>().FromInstance(PlayerObject);
        Container.Bind<InputHandler>().FromInstance(inputHandler);
    }
}
