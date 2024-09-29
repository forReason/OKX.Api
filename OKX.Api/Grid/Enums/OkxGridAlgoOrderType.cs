﻿namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid Algo Order Type
/// </summary>
public enum OkxGridAlgoOrderType
{
    /// <summary>
    /// SpotGrid
    /// </summary>
    SpotGrid,

    /// <summary>
    /// ContractGrid
    /// </summary>
    ContractGrid,

    /// <summary>
    /// MoonGrid
    /// </summary>
    [Obsolete]
    MoonGrid,
}