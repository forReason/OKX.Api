﻿namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid Investment Add Response
/// </summary>
internal record OkxGridInvestmentAddResponse
{
    /// <summary>
    /// Algo Order Id
    /// </summary>
    [JsonProperty("algoId")]
    public long? Data { get; set; }
}