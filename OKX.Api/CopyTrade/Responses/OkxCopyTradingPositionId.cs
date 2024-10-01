﻿namespace OKX.Api.CopyTrade;

/// <summary>
/// OkxLeadingPositionId
/// </summary>
internal record OkxCopyTradingPositionId
{
    /// <summary>
    /// Leading position ID
    /// </summary>
    [JsonProperty("subPosId")]
    public long? Data { get; set; }
}
