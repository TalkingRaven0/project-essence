using UniRx;
using UnityEngine;
using Zenject;

public class ClickSpawnEssence : MonoBehaviour
{
    [Inject] protected EssencePooler essencePool;
    [Inject] protected InputHandler inputHandler;

    private void Start()
    {
        var subscription = inputHandler.MouseClicked.Subscribe(_ => {
            
        });

        subscription.Dispose();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
