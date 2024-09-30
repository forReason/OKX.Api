﻿namespace OKX.Api.Account;

/// <summary>
/// OKX Account Bill
/// </summary>
public class OkxAccountBill
{
    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType")]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Bill ID
    /// </summary>
    [JsonProperty("billId")]
    public long BillId { get; set; }

    /// <summary>
    /// Bill type
    /// </summary>
    [JsonProperty("type")]
    public OkxAccountBillType BillType { get; set; }

    /// <summary>
    /// Bill subtype
    /// </summary>
    [JsonProperty("subType")]
    public OkxAccountBillSubType BillSubType { get; set; }

    /// <summary>
    /// Creation time, Unix timestamp format in milliseconds, e.g.1597026383085
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Creation time
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Change in balance amount at the account level
    /// </summary>
    [JsonProperty("balChg")]
    public decimal? AccountBalanceChange { get; set; }

    /// <summary>
    /// Change in balance amount at the position level
    /// </summary>
    [JsonProperty("posBalChg")]
    public decimal? PositionBalanceChange { get; set; }

    /// <summary>
    /// Balance at the account level
    /// </summary>
    [JsonProperty("bal")]
    public decimal? AccountBalance { get; set; }

    /// <summary>
    /// Balance at the position level
    /// </summary>
    [JsonProperty("posBal")]
    public decimal? PositionBalance { get; set; }

    /// <summary>
    /// Quantity
    /// </summary>
    [JsonProperty("sz")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Price. It is related to subType.
    /// </summary>
    /// <remarks>
    /// Trade filled price for 1: Buy 2: Sell 3: Open long 4: Open short 5: Close long 6: Close short 204: block trade buy 205: block trade sell 206: block trade open long 207: block trade open short 208: block trade close open209: block trade close short 114: Auto buy 115: Auto sell
    /// Liquidation Price:100: Partial liquidation close long 101: Partial liquidation close short 102: Partial liquidation buy 103: Partial liquidation sell 104: Liquidation long 105: Liquidation short 106: Liquidation buy 107: Liquidation sell 16: Repay forcibly 17: Repay interest by borrowing forcibly 110: Liquidation transfer in 111: Liquidation transfer out
    /// Delivery price for 112: Delivery long 113: Delivery short
    /// Exercise price for 170: Exercised 171: Counterparty exercised 172: Expired OTM
    /// Mark price for 173: Funding fee expense 174: Funding fee income
    /// </remarks>
    [JsonProperty("px")]
    public decimal? Price { get; set; }

    /// <summary>
    /// Account balance currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Profit and loss
    /// </summary>
    [JsonProperty("pnl")]
    public decimal? ProfitLoss { get; set; }

    /// <summary>
    /// Fee
    /// Negative number represents the user transaction fee charged by the platform.
    /// Positive number represents rebate.
    /// </summary>
    [JsonProperty("fee")]
    public decimal? Fee { get; set; }

    /// <summary>
    /// Margin mode
    /// isolated cross
    /// When bills are not generated by position changes, the field returns ""
    /// </summary>
    [JsonProperty("mgnMode")]
    public OkxAccountMarginMode? MarginMode { get; set; }

    /// <summary>
    /// Instrument ID, e.g. BTC-USD-190927-5000-C
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Order ID
    /// When bill type is not trade, the field returns ""
    /// </summary>
    [JsonProperty("ordId")]
    public long? OrderId { get; set; }

    /// <summary>
    /// Liquidity taker or maker, T: taker M: maker
    /// </summary>
    [JsonProperty("execType")]
    public OkxTradeOrderRole? TradeRole { get; set; }

    /// <summary>
    /// The remitting account
    /// 6: FUNDING 18: Trading account
    /// Only applicable to transfer.When bill type is not transfer, the field returns "".
    /// </summary>
    [JsonProperty("from")]
    public OkxAccount? FromAccount { get; set; }

    /// <summary>
    /// The beneficiary account
    /// 6: FUNDING 18: Trading account
    /// Only applicable to transfer.When bill type is not transfer, the field returns "".
    /// </summary>
    [JsonProperty("to")]
    public OkxAccount? ToAccount { get; set; }

    /// <summary>
    /// Notes
    /// Only applicable to transfer.When bill type is not transfer, the field returns "".
    /// </summary>
    [JsonProperty("notes")]
    public string Notes { get; set; } = string.Empty;

    /// <summary>
    /// Interest
    /// </summary>
    [JsonProperty("interest")]
    public decimal? Interest { get; set; }

    /// <summary>
    /// Last filled time
    /// </summary>
    [JsonProperty("fillTime")]
    public long? FillTimestamp { get; set; }

    /// <summary>
    /// Last filled time
    /// </summary>
    [JsonIgnore]
    public DateTime? FillTime => FillTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Last traded ID
    /// </summary>
    [JsonProperty("tradeId")]
    public long? TradeId { get; set; }

    /// <summary>
    /// Client Order ID as assigned by the client
    /// A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.
    /// </summary>
    [JsonProperty("clOrdId")]
    public string ClientOrderId { get; set; } = string.Empty;

    /// <summary>
    /// Index price at the moment of trade execution
    /// For cross currency spot pairs, it returns baseCcy-USDT index price.For example, for LTC-ETH, this field returns the index price of LTC-USDT.
    /// </summary>
    [JsonProperty("fillIdxPx")]
    public decimal? FillIndexPrice { get; set; }

    /// <summary>
    /// Mark price when filled
    /// Applicable to FUTURES/SWAP/OPTIONS, return "" for other instrument types
    /// </summary>
    [JsonProperty("fillMarkPx")]
    public decimal? FillMarkPrice { get; set; }

    /// <summary>
    /// Implied volatility when filled
    /// Only applicable to options; return "" for other instrument types
    /// </summary>
    [JsonProperty("fillPxVol")]
    public decimal? FillImpliedVolatility { get; set; }

    /// <summary>
    /// Options price when filled, in the unit of USD
    /// Only applicable to options; return "" for other instrument types
    /// </summary>
    [JsonProperty("fillPxUsd")]
    public decimal? FillUsdPrice { get; set; }

    /// <summary>
    /// Mark volatility when filled
    /// Only applicable to options; return "" for other instrument types
    /// </summary>
    [JsonProperty("fillMarkVol")]
    public decimal? FillMarkVolatility { get; set; }

    /// <summary>
    /// Forward price when filled
    /// Only applicable to options; return "" for other instrument types
    /// </summary>
    [JsonProperty("fillFwdPx")]
    public decimal? FillForwardPrice { get; set; }
}
