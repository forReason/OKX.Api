﻿namespace OKX.Api.Block;

/// <summary>
/// OKX Block Trading Cancel All After Response
/// </summary>
public class OkxBlockCancelAllAfterResponse
{
    /// <summary>
    /// The time the cancellation is triggered.
    /// triggerTime=0 means Cancel All After is disabled.
    /// </summary>
    [JsonProperty("triggerTime")]
    public long TriggerTimestamp { get; set; }

    /// <summary>
    /// The time the cancellation is triggered.
    /// triggerTime=0 means Cancel All After is disabled.
    /// </summary>
    [JsonIgnore]
    public DateTime TriggerTime { get { return TriggerTimestamp.ConvertFromMilliseconds(); } }

    /// <summary>
    /// The time the request is received.
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// The time the request is received.
    /// </summary>
    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }

}