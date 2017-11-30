using System;

namespace TwainablePlus
{
    [Serializable]
	public sealed class ConfigToken : PaintDotNet.Effects.EffectConfigToken
	{
		public ConfigToken()
		{
		}

		private ConfigToken(ConfigToken copyMe)
		{
		}

		public override object Clone()
		{
			return new ConfigToken(this);
		}
	}
}
