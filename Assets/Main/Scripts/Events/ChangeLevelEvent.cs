using System;
namespace Main.Scripts.Events
{
    [Serializable]
    public struct ChangeLevelEvent
    {
        public int level;
        public float cooldown;
        public int productPrize;
    }
}
