namespace Forms
{
    using System;
    using Xamarin.Forms;

    public class LifecycleEffect : RoutingEffect
    {
        public event EventHandler<EventArgs> Loaded;
        public event EventHandler<EventArgs> Unloaded;

        public LifecycleEffect() : base("Forms.LifecycleEffect") { }

        public void RaiseLoaded(Element element) => Loaded?.Invoke(element, EventArgs.Empty);
        public void RaiseUnloaded(Element element) => Unloaded?.Invoke(element, EventArgs.Empty);
    }
}