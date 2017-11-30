using System;
using System.Reflection;

namespace TwainablePlus
{
    public sealed class PluginSupportInfo : PaintDotNet.IPluginSupportInfo
    {
        public string Author
        {
            get
            {
                return "null54";
            }
        }

        public string Copyright
        {
            get
            {
                return ((AssemblyCopyrightAttribute)typeof(PluginSupportInfo).Assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0]).Copyright;
            }
        }

        public string DisplayName
        {
            get
            {
                return TwainablePlusEffect.StaticName;
            }
        }

        public Version Version
        {
            get
            {
                return typeof(PluginSupportInfo).Assembly.GetName().Version;
            }
        }

        public Uri WebsiteUri
        {
            get
            {
                return new Uri("http://www.getpaint.net/redirect/plugins.html");
            }
        }
    }
}
