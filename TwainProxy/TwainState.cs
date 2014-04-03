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