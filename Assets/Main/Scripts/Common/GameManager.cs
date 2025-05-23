using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [Inject] private IGameLogic _logic;
    void OnEnable()
    {
        _logic.Setup();
    }

    void OnDisable()
    {
        _logic.Dispose();
    }
}
