using System;

namespace LockpickersGuide.EventArgs
{
    public class PreloadCompleteEventArgs : System.EventArgs
    {
        public TimeSpan Duration { get; private set; }
        public PreloadCompleteEventArgs(TimeSpan duration) : base()
        {
            this.Duration = duration;
        }
        public PreloadCompleteEventArgs() : base()
        {
        }
    }
}
