using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Anim.UnityBindings
{
    public class TweenRunner : Singleton<TweenRunner>
    {
        private Dictionary<int, IPlayable> _playablesDict;
        private List<IPlayable> _playableList;
        private static int idCounter;
        public static int GetId()
        {
            idCounter++;
            return idCounter - 1;
        }

        void Awake()
        {
            gameObject.hideFlags = HideFlags.HideInHierarchy | HideFlags.DontSave;
        }

        void Update()
        {
            if (_playableList == null)
                return;

            foreach (var playable in _playableList)
            {
                if (!playable.IsAnimating || playable.IsFinished)
                    continue;
                playable.Step(Time.deltaTime);
            }
        }

        public void Register(IPlayable anim)
        {
            _playablesDict ??= new();
            _playablesDict.TryAdd(anim.Id, anim);
            _playableList = _playablesDict.Values.ToList();
        }

        public void Destroy(int id)
        {
            _playablesDict ??= new();
            if (_playablesDict.ContainsKey(id))
                _playablesDict.Remove(id);
            _playableList = _playablesDict.Values.ToList();
        }
    }
}
