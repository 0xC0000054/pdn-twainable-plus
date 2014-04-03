using PaintDotNet.Effects;

namespace TwainablePlus
{
    [PaintDotNet.PluginSupportInfo(typeof(PluginSupportInfo))]
    public sealed class TwainablePlus : Effect
    {
        internal static string StaticName
        {
            get
            {
                return "Twainable+";
            }
        }

        internal static System.Drawing.Bitmap StaticIcon
        {
            get
            {
                return new System.Drawing.Bitmap(typeof(TwainablePlus), "icon.png");
            }
        }

        public TwainablePlus() : base(TwainablePlus.StaticName, TwainablePlus.StaticIcon, "Tools", EffectFlags.Configurable)
        {

        }

        public override EffectConfigDialog CreateConfigDialog()
        {
            return new ConfigDialog();
        }


        public override void Render(PaintDotNet.Effects.EffectConfigToken parameters, PaintDotNet.RenderArgs dstArgs, PaintDotNet.RenderArgs srcArgs, System.Drawing.Rectangle[] rois, int startIndex, int length)
        {
        }
    }
}
