using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ClickSpawnEssence : MonoBehaviour
{
    [Inject] protected InputHandler inputHandler;

    // Update is called once per frame
    void Update()
    {
        Debug.Log(inputHandler.GetMouseLeftClicked());

        Debug.Log(inputHandler.GetMouseLeftHeld());
    }
}
