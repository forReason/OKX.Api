﻿namespace OKX.Api.Account;

/// <summary>
/// OkxPositionHistory
/// </summary>
public class OkxAccountPositionHistory
{
    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(OkxInstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Margin mode
    /// </summary>
    [JsonProperty("mgnMode"), JsonConverter(typeof(OkxAccountMarginModeConverter))]
    public OkxAccountMarginMode MarginMode { get; set; }

    /// <summary>
    /// The type of closing position
    /// </summary>
    [JsonProperty("type"), JsonConverter(typeof(OkxClosingPositionTypeConverter))]
    public OkxClosingPositionType Type { get; set; }

    /// <summary>
    /// Created time of position
    /// </summary>
    [JsonProperty("cTime")]
    public long CreateTimestamp { get; set; }

    /// <summary>
    /// Created time of position
    /// </summary>
    [JsonIgnore]
    public DateTime CreateTime => CreateTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Updated time of position
    /// </summary>
    [JsonProperty("uTime")]
    public long? UpdateTimestamp { get; set; }

    /// <summary>
    /// Updated time of position
    /// </summary>
    [JsonIgnore]
    public DateTime? UpdateTime => UpdateTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Average price of opening position
    /// </summary>
    [JsonProperty("openAvgPx")]
    public decimal OpenAveragePrice { get; set; }

    /// <summary>
    /// Average price of closing position
    /// </summary>
    [JsonProperty("closeAvgPx")]
    public decimal CloseAveragePrice { get; set; }

    /// <summary>
    /// Position ID
    /// </summary>
    [JsonProperty("posId")]
    public long PositionId { get; set; }

    /// <summary>
    /// Max quantity of position
    /// </summary>
    [JsonProperty("openMaxPos")]
    public decimal OpenMaxPos { get; set; }

    /// <summary>
    /// Position's cumulative closed volume
    /// </summary>
    [JsonProperty("closeTotalPos")]
    public decimal CloseTotalPos { get; set; }

    /// <summary>
    /// Realized profit and loss
    /// </summary>
    [JsonProperty("realizedPnl")]
    public decimal RealizedProfitLoss { get; set; }

    /// <summary>
    /// Accumulated fee
    /// Negative number represents the user transaction fee charged by the platform.Positive number represents rebate.
    /// </summary>
    [JsonProperty("fee")]
    public decimal Fee { get; set; }

    /// <summary>
    /// Accumulated funding fee
    /// </summary>
    [JsonProperty("fundingFee")]
    public decimal FundingFee { get; set; }

    /// <summary>
    /// Accumulated liquidation penalty. It is negative when there is a value.
    /// </summary>
    [JsonProperty("liqPenalty")]
    public decimal LiquidationPenalty { get; set; }

    /// <summary>
    /// Profit and loss
    /// </summary>
    [JsonProperty("pnl")]
    public decimal ProfitLoss { get; set; }

    /// <summary>
    /// P&amp;L ratio
    /// </summary>
    [JsonProperty("pnlRatio")]
    public decimal ProfitLossRatio { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("lever")]
    public decimal Leverage { get; set; }

    /// <summary>
    /// Direction: long short
    /// Only applicable to MARGIN/FUTURES/SWAP/OPTION
    /// </summary>
    [JsonProperty("direction"), JsonConverter(typeof(OkxTradePositionDirectionConverter))]
    public OkxTradePositionDirection Direction { get; set; }

    /// <summary>
    /// trigger mark price
    /// </summary>
    [JsonProperty("triggerPx")]
    public decimal? TriggerMarkPrice { get; set; }

    /// <summary>
    /// Underlying
    /// </summary>
    [JsonProperty("uly")]
    public string Underlying { get; set; } = string.Empty;

    /// <summary>
    /// Currency used for margin
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;
}