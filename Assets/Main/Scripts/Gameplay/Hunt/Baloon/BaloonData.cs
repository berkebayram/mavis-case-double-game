using UnityEngine;

[CreateAssetMenu(menuName = "Game/Hunt/BaloonData")]
public class BaloonData : ScriptableObject
{
    public BaloonLevel Level;
    public int Value;
    public float Scale;
    public float duration;
}
