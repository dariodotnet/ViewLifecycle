using System.Linq;
using Android;
using Forms;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("Forms")]
[assembly: ExportEffect(typeof(AndroidLifecycleEffect), "LifecycleEffect")]
namespace Android
{
    public class AndroidLifecycleEffect : PlatformEffect
    {
        private Android.Views.View _nativeView;
        private LifecycleEffect _lifecycleEffect;

        protected override void OnAttached()
        {
            _lifecycleEffect = Element.Effects.OfType<LifecycleEffect>().FirstOrDefault();
            _nativeView = Control ?? Container;
            _nativeView.ViewAttachedToWindow += NativeViewOnViewAttachedToWindow;
            _nativeView.ViewDetachedFromWindow += NativeViewOnViewDetachedFromWindow;
        }

        private void NativeViewOnViewDetachedFromWindow(object sender,
            Android.Views.View.ViewDetachedFromWindowEventArgs e) =>
            _lifecycleEffect.RaiseUnloaded(Element);

        private void NativeViewOnViewAttachedToWindow(object sender,
            Android.Views.View.ViewAttachedToWindowEventArgs e) =>
            _lifecycleEffect.RaiseLoaded(Element);

        protected override void OnDetached()
        {
            _lifecycleEffect.RaiseUnloaded(Element);
            _nativeView.ViewAttachedToWindow -= NativeViewOnViewAttachedToWindow;
            _nativeView.ViewDetachedFromWindow -= NativeViewOnViewDetachedFromWindow;
        }
    }
}
