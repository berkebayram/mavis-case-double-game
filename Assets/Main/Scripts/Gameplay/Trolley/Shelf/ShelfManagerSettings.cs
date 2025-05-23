using System;
using UnityEngine;

namespace Main.Gameplay
{
    [Serializable]
    public struct ShelfManagerSettings
    {
        public int ShelfCount;
        public int ProductCount;
        public Vector2 FirstShelfPos;
        public Vector2 PrefabGap;

        public ShelfManagerSettings(
                int shelfCount,
                int productCount,
                Vector2 firstShelfPos,
                Vector2 prefabGap
                )
        {
            ShelfCount = shelfCount;
            ProductCount = productCount;
            FirstShelfPos= firstShelfPos;
            PrefabGap = prefabGap;
        }
    }
}
