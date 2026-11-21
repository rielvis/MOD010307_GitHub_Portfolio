using UnityEngine;

namespace MicrogameCourse.Framework
{
    public abstract class MicrogameBehaviour : MonoBehaviour
    {
        // Microgame session
        public bool IsRunning;
        protected MicrogameSession Session {get; private set;}

        void Start()
        {
            IsRunning = false;
        }

        public virtual void Begin(MicrogameSession session)
        {
            Session = session;
            IsRunning = true;
        }

        public virtual void End()
        {
            IsRunning = false;
        }

        protected void Win()
        {
            if (IsRunning == true) Session.Finish(true);
        }

        protected void Lose()
        {
            if (IsRunning == true) Session.Finish(false);
        }
    }
}

