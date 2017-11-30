////////////////////////////////////////////////////////////////////////
//
// This file is part of pdn-twainable-plus, an Effect plugin for
// Paint.NET that imports images from TWAIN devices.
//
// Copyright (c) 2014, 2017 Nicholas Hayes
//
// This file is licensed under the MIT License.
// See LICENSE.txt for complete licensing and attribution information.
//
////////////////////////////////////////////////////////////////////////

using PaintDotNet.Effects;

namespace TwainablePlus
{
    [PaintDotNet.PluginSupportInfo(typeof(PluginSupportInfo))]
    public sealed class TwainablePlusEffect : Effect
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
                return new System.Drawing.Bitmap(typeof(TwainablePlusEffect), "icon.png");
            }
        }

        public TwainablePlusEffect() : base(TwainablePlusEffect.StaticName, TwainablePlusEffect.StaticIcon, "Tools", EffectFlags.Configurable)
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
