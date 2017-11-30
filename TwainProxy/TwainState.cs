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

namespace TwainProxy
{
    internal enum TwainState
    {
        /// <summary>
        /// S1
        /// </summary>
        PreSession = 0,
        /// <summary>
        /// S2
        /// </summary>
        SourceManagerLoaded,
        /// <summary>
        /// S3
        /// </summary>
        SourceManagerOpen,
        /// <summary>
        /// S4
        /// </summary>
        SourceOpen,
        /// <summary>
        /// S5
        /// </summary>
        SourceEnabled,
        /// <summary>
        /// S6
        /// </summary>
        TransferReady,
        /// <summary>
        /// S7
        /// </summary>
        Transfering
    }
}