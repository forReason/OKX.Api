﻿namespace OKX.Api.Public;

/// <summary>
/// OKX Exchange Rate
/// </summary>
public class OkxExchangeRate
{
    /// <summary>
    /// USD to CNY
    /// </summary>
    [JsonProperty("usdCny")]
    public decimal UsdCny { get; set; }
}
