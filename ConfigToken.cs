////////////////////////////////////////////////////////////////////////
//
// This file is part of pdn-twainable-plus, an Effect plugin for
// Paint.NET that imports images from TWAIN devices.
//
// Copyright (c) 2014, 2017, 2018 Nicholas Hayes
//
// This file is licensed under the MIT License.
// See LICENSE.txt for complete licensing and attribution information.
//
////////////////////////////////////////////////////////////////////////

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
