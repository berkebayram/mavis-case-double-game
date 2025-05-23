using System;

namespace Anim
{
    public interface IAnimationEventHandler 
    {
        public Action OnComplete {get;}
        public Action OnStart {get;}
        public void SetOnComplete(Action action);
        public void SetOnStart(Action action);
    }
}
