using System;
using System.Collections.Generic;

namespace Main.Scripts.SaveSystem
{
    [Serializable]
    public class SaveWrapper
    {
        public List<SaveContainer> Data;
        public SaveWrapper(List<SaveContainer> data )
        {
            Data = data;
        }
    }
}
