using System.Linq;
using Windows.UI.Xaml;
using Forms;
using UWP;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ResolutionGroupName("Forms")]
[assembly: ExportEffect(typeof(UwpLifecycleEffect), "LifecycleEffect")]
namespace UWP
{
    public class UwpLifecycleEffect : PlatformEffect
    {
        private FrameworkElement _nativeView;
        private LifecycleEffect _lifecycleEffect;

        protected override void OnAttached()
        {
            _lifecycleEffect = Element.Effects.OfType<LifecycleEffect>().FirstOrDefault();
            _nativeView = Control ?? Container;

            _nativeView.Loaded += NativeViewOnLoaded;
            _nativeView.Unloaded += NativeViewOnUnloaded;
        }

        protected override void OnDetached()
        {
            _lifecycleEffect?.RaiseUnloaded(Element);
            _nativeView.Loaded -= NativeViewOnLoaded;
            _nativeView.Unloaded -= NativeViewOnUnloaded;
        }

        private void NativeViewOnLoaded(object sender, RoutedEventArgs e) => 
            _lifecycleEffect?.RaiseLoaded(Element);

        private void NativeViewOnUnloaded(object sender, RoutedEventArgs e) => 
            _lifecycleEffect?.RaiseUnloaded(Element);
    }
}