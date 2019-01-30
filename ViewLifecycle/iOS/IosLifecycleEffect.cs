using System;
using System.Linq;
using Forms;
using Foundation;
using iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("Forms")]
[assembly: ExportEffect(typeof(IosLifecycleEffect), "LifecycleEffect")]
namespace iOS
{
    public class IosLifecycleEffect : PlatformEffect
    {
        private const NSKeyValueObservingOptions ObservingOptions =
                                                    NSKeyValueObservingOptions.Initial | 
                                                    NSKeyValueObservingOptions.OldNew | 
                                                    NSKeyValueObservingOptions.Prior;

        private LifecycleEffect _lifecycleEffect;
        private IDisposable _isLoadedObserverDisposable;

        protected override void OnAttached()
        {
            _lifecycleEffect = Element.Effects.OfType<LifecycleEffect>().FirstOrDefault();
            UIView nativeView = Control ?? Container;
            _isLoadedObserverDisposable = nativeView?.AddObserver("superview", ObservingOptions, IsViewLoadedObserver);
        }

        protected override void OnDetached()
        {
            _lifecycleEffect.RaiseUnloaded(Element);
            _isLoadedObserverDisposable.Dispose();
        }

        public void IsViewLoadedObserver(NSObservedChange nsObservedChange)
        {
            if (!nsObservedChange.NewValue.Equals(NSNull.Null))
                _lifecycleEffect?.RaiseLoaded(Element);
            else if (!nsObservedChange.OldValue.Equals(NSNull.Null))
                _lifecycleEffect?.RaiseUnloaded(Element);
        }
    }
}