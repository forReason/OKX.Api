﻿namespace OKX.Api.Account;

/// <summary>
/// OKX Trading Account Rest Api Client
/// </summary>
public class OkxAccountRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    // Endpoints
    private const string v5AccountInstruments = "api/v5/account/instruments";
    private const string v5AccountBalance = "api/v5/account/balance";
    private const string v5AccountPositions = "api/v5/account/positions";
    private const string v5AccountPositionsHistory = "api/v5/account/positions-history";
    private const string v5AccountPositionRisk = "api/v5/account/account-position-risk";
    private const string v5AccountBills = "api/v5/account/bills";
    private const string v5AccountBillsArchive = "api/v5/account/bills-archive";
    private const string v5AccountBillsHistoryArchive = "api/v5/account/bills-history-archive";
    private const string v5AccountConfig = "api/v5/account/config";
    private const string v5AccountSetPositionMode = "api/v5/account/set-position-mode";
    private const string v5AccountSetLeverage = "api/v5/account/set-leverage";
    private const string v5AccountMaxSize = "api/v5/account/max-size";
    private const string v5AccountMaxAvailSize = "api/v5/account/max-avail-size";
    private const string v5AccountPositionMarginBalance = "api/v5/account/position/margin-balance";
    private const string v5AccountLeverageInfo = "api/v5/account/leverage-info";
    private const string v5AccountAdjustLeverageInfo = "api/v5/account/adjust-leverage-info";
    private const string v5AccountMaxLoan = "api/v5/account/max-loan";
    private const string v5AccountTradeFee = "api/v5/account/trade-fee";
    private const string v5AccountInterestAccrued = "api/v5/account/interest-accrued";
    private const string v5AccountInterestRate = "api/v5/account/interest-rate";
    private const string v5AccountSetGreeks = "api/v5/account/set-greeks";
    private const string v5AccountSetIsolatedMode = "api/v5/account/set-isolated-mode";
    private const string v5AccountMaxWithdrawal = "api/v5/account/max-withdrawal";
    private const string v5AccountRiskState = "api/v5/account/risk-state";
    private const string v5AccountBorrowRepay = "api/v5/account/borrow-repay";
    private const string v5AccountBorrowRepayHistory = "api/v5/account/borrow-repay-history";
    private const string v5AccountVipInterestAccrued = "api/v5/account/vip-interest-accrued";
    private const string v5AccountVipInterestDeducted = "api/v5/account/vip-interest-deducted";
    private const string v5AccountVipLoanOrderList = "api/v5/account/vip-loan-order-list";
    private const string v5AccountVipLoanOrderDetail = "api/v5/account/vip-loan-order-detail";
    private const string v5AccountInterestLimits = "api/v5/account/interest-limits";
    private const string v5AccountFixedLoanBorrowingLimit = "api/v5/account/fixed-loan/borrowing-limit";
    private const string v5AccountFixedLoanBorrowingQuote = "api/v5/account/fixed-loan/borrowing-quote";
    private const string v5AccountFixedLoanBorrowingOrder = "api/v5/account/fixed-loan/borrowing-order";
    private const string v5AccountFixedLoanAmendBorrowingOrder = "api/v5/account/fixed-loan/amend-borrowing-order";
    private const string v5AccountFixedLoanManualReborrow = "api/v5/account/fixed-loan/manual-reborrow";
    private const string v5AccountFixedLoanRepayBorrowingOrder = "api/v5/account/fixed-loan/repay-borrowing-order";
    private const string v5AccountFixedLoanBorrowingOrdersList = "api/v5/account/fixed-loan/borrowing-orders-list";
    private const string v5AccountPositionBuilder = "api/v5/account/position-builder";
    private const string v5AccountSetRiskOffsetAmount = "api/v5/account/set-riskOffset-amt";
    private const string v5AccountGreeks = "api/v5/account/greeks";
    private const string v5AccountPositionTiers = "api/v5/account/position-tiers";
    private const string v5AccountSetRiskOffsetType = "api/v5/account/set-riskOffset-type";
    private const string v5AccountActivateOption = "api/v5/account/activate-option";
    private const string v5AccountSetAutoLoan = "api/v5/account/set-auto-loan";
    private const string v5AccountSetAccountLevel = "api/v5/account/set-account-level";
    private const string v5AccountMmpReset = "api/v5/account/mmp-reset";
    private const string v5AccountMmpConfig = "api/v5/account/mmp-config";

    /// <summary>
    /// Retrieve available instruments info of current account.
    /// </summary>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxInstrument>>> GetInstrumentsAsync(
       OkxInstrumentType instrumentType,
       string instrumentFamily = null,
       string instrumentId = null,
       string underlying = null,
       CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("instFamily", instrumentFamily);
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("uly", underlying);

        return ProcessListRequestAsync<OkxInstrument>(GetUri(v5AccountInstruments), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve a list of assets (with non-zero balance), remaining balance, and available amount in the account.
    /// </summary>
    /// <param name="currencies">Currencies</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAccountBalance>> GetBalancesAsync(IEnumerable<string> currencies = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        if (currencies is not null && currencies.Count() > 0)
            parameters.AddOptionalParameter("ccy", string.Join(",", currencies));

        return ProcessOneRequestAsync<OkxAccountBalance>(GetUri(v5AccountBalance), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve information on your positions. When the account is in net mode, net positions will be displayed, and when the account is in long/short mode, long or short positions will be displayed.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="positionId">Position ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountPosition>>> GetPositionsAsync(
        OkxInstrumentType? instrumentType = null,
        string instrumentId = null,
        string positionId = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("posId", positionId);

        return ProcessListRequestAsync<OkxAccountPosition>(GetUri(v5AccountPositions), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve the updated position data for the last 3 months. Return in reverse chronological order using utime.
    /// </summary>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="marginMode">Margin mode</param>
    /// <param name="type">The type of closing position. It is the latest type if there are several types for the same position.</param>
    /// <param name="positionId">Position ID</param>
    /// <param name="after">Pagination of data to return records earlier than the requested uTime, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records earlier than the requested uTime, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountPositionHistory>>> GetPositionsHistoryAsync(
        OkxInstrumentType? instrumentType = null,
        string instrumentId = null,
        OkxAccountMarginMode? marginMode = null,
        OkxClosingPositionType? type = null,
        string positionId = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("mgnMode", JsonConvert.SerializeObject(marginMode, new OkxAccountMarginModeConverter(false)));
        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new OkxClosingPositionTypeConverter(false)));
        parameters.AddOptionalParameter("posId", positionId);
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxAccountPositionHistory>(GetUri(v5AccountPositionsHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get account and position risk
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountPositionBalance>>> GetPositionRiskAsync(OkxInstrumentType? instrumentType = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));

        return ProcessListRequestAsync<OkxAccountPositionBalance>(GetUri(v5AccountPositionRisk), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve the bills of the account. The bill refers to all transaction records that result in changing the balance of an account. Pagination is supported, and the response is sorted with the most recent first. This endpoint can retrieve data from the last 7 days.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID, e.g. BTC-USDT</param>
    /// <param name="currency">Currency</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="contractType">Contract Type</param>
    /// <param name="billType">Bill Type</param>
    /// <param name="billSubType">Bill Sub Type</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="begin">Filter with a begin timestamp. Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="end">Filter with an end timestamp. Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountBill>>> GetBillHistoryAsync(
        OkxInstrumentType? instrumentType = null,
        string instrumentId = null,
        string currency = null,
        OkxAccountMarginMode? marginMode = null,
        OkxContractType? contractType = null,
        OkxAccountBillType? billType = null,
        OkxAccountBillSubType? billSubType = null,
        long? after = null,
        long? before = null,
        long? begin = null,
        long? end = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("mgnMode", JsonConvert.SerializeObject(marginMode, new OkxAccountMarginModeConverter(false)));
        parameters.AddOptionalParameter("ctType", JsonConvert.SerializeObject(contractType, new OkxContractTypeConverter(false)));
        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(billType, new OkxAccountBillTypeConverter(false)));
        parameters.AddOptionalParameter("subType", JsonConvert.SerializeObject(billSubType, new OkxAccountBillSubTypeConverter(false)));
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("begin", begin?.ToOkxString());
        parameters.AddOptionalParameter("end", end?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxAccountBill>(GetUri(v5AccountBills), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve the account’s bills. The bill refers to all transaction records that result in changing the balance of an account. Pagination is supported, and the response is sorted with most recent first. This endpoint can retrieve data from the last 3 months.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID, e.g. BTC-USDT</param>
    /// <param name="currency">Currency</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="contractType">Contract Type</param>
    /// <param name="billType">Bill Type</param>
    /// <param name="billSubType">Bill Sub Type</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="begin">Filter with a begin timestamp. Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="end">Filter with an end timestamp. Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountBill>>> GetBillArchiveAsync(
        OkxInstrumentType? instrumentType = null,
        string instrumentId = null,
        string currency = null,
        OkxAccountMarginMode? marginMode = null,
        OkxContractType? contractType = null,
        OkxAccountBillType? billType = null,
        OkxAccountBillSubType? billSubType = null,
        long? after = null,
        long? before = null,
        long? begin = null,
        long? end = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("mgnMode", JsonConvert.SerializeObject(marginMode, new OkxAccountMarginModeConverter(false)));
        parameters.AddOptionalParameter("ctType", JsonConvert.SerializeObject(contractType, new OkxContractTypeConverter(false)));
        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(billType, new OkxAccountBillTypeConverter(false)));
        parameters.AddOptionalParameter("subType", JsonConvert.SerializeObject(billSubType, new OkxAccountBillSubTypeConverter(false)));
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("begin", begin?.ToOkxString());
        parameters.AddOptionalParameter("end", end?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxAccountBill>(GetUri(v5AccountBillsArchive), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Apply for bill data since 1 February, 2021 except for the current quarter.
    /// </summary>
    /// <param name="year">4 digits year</param>
    /// <param name="quarter">Quarter, valid value is Q1, Q2, Q3, Q4</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxDownloadApplication>> ApplyBillDataAsync(int year, OkxQuarter quarter, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"year", year.ToOkxString() },
            {"quarter", JsonConvert.SerializeObject(quarter, new OkxQuarterConverter(false)) },
        };

        return ProcessOneRequestAsync<OkxDownloadApplication>(GetUri(v5AccountBillsHistoryArchive), HttpMethod.Post, ct, true, bodyParameters: parameters);
    }

    /// <summary>
    /// Apply for bill data since 1 February, 2021 except for the current quarter.
    /// </summary>
    /// <param name="year">4 digits year</param>
    /// <param name="quarter">Quarter, valid value is Q1, Q2, Q3, Q4</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxDownloadLink>> GetBillDataAsync(int year, OkxQuarter quarter, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"year", year.ToOkxString() },
            {"quarter", JsonConvert.SerializeObject(quarter, new OkxQuarterConverter(false)) },
        };

        return ProcessOneRequestAsync<OkxDownloadLink>(GetUri(v5AccountBillsHistoryArchive), HttpMethod.Get, ct, true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve current account configuration.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAccountConfiguration>> GetConfigurationAsync(CancellationToken ct = default)
    {
        return ProcessOneRequestAsync<OkxAccountConfiguration>(GetUri(v5AccountConfig), HttpMethod.Get, ct, true);
    }

    /// <summary>
    /// FUTURES and SWAP support both long/short mode and net mode. In net mode, users can only have positions in one direction; In long/short mode, users can hold positions in long and short directions.
    /// </summary>
    /// <param name="positionMode"></param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAccountPositionMode>> SetPositionModeAsync(OkxTradePositionMode positionMode, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"posMode", JsonConvert.SerializeObject(positionMode, new OkxTradePositionModeConverter(false)) },
        };

        return ProcessOneRequestAsync<OkxAccountPositionMode>(GetUri(v5AccountSetPositionMode), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// There are 10 different scenarios for leverage setting:
    /// 1. Set leverage for MARGIN instruments under isolated-margin trade mode at pairs level.
    /// 2. Set leverage for MARGIN instruments under cross-margin trade mode and Single-currency margin account mode at pairs level.
    /// 3. Set leverage for MARGIN instruments under cross-margin trade mode and Multi-currency margin at currency level.
    /// 4. Set leverage for MARGIN instruments under cross-margin trade mode and Portfolio margin at currency level.
    /// 5. Set leverage for FUTURES instruments under cross-margin trade mode at underlying level.
    /// 6. Set leverage for FUTURES instruments under isolated-margin trade mode and buy/sell position mode at contract level.
    /// 7. Set leverage for FUTURES instruments under isolated-margin trade mode and long/short position mode at contract and position side level.
    /// 8. Set leverage for SWAP instruments under cross-margin trade at contract level.
    /// 9. Set leverage for SWAP instruments under isolated-margin trade mode and buy/sell position mode at contract level.
    /// 10. Set leverage for SWAP instruments under isolated-margin trade mode and long/short position mode at contract and position side level.
    /// 
    /// Note that the request parameter posSide is only required when margin mode is isolated in long/short position mode for FUTURES/SWAP instruments (see scenario 7 and 10 above).
    /// Please refer to the request examples on the right for each case.
    /// </summary>
    /// <param name="leverage">Leverage</param>
    /// <param name="currency">Currency</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="positionSide">Position Side</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountLeverage>>> SetLeverageAsync(
        decimal leverage,
        string currency = null,
        string instrumentId = null,
        OkxAccountMarginMode? marginMode = null,
        OkxTradePositionSide? positionSide = null,
        CancellationToken ct = default)
    {
        if (leverage < 0.01m)
            throw new ArgumentException("Invalid Leverage");

        if (string.IsNullOrEmpty(currency) && string.IsNullOrEmpty(instrumentId))
            throw new ArgumentException("Either instId or ccy is required; if both are passed, instId will be used by default.");

        if (marginMode is null)
            throw new ArgumentException("marginMode is required");

        var parameters = new Dictionary<string, object> {
            {"lever", leverage.ToOkxString() },
            {"mgnMode", JsonConvert.SerializeObject(marginMode, new OkxAccountMarginModeConverter(false)) },
        };
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("posSide", JsonConvert.SerializeObject(positionSide, new OkxTradePositionSideConverter(false)));

        return ProcessListRequestAsync<OkxAccountLeverage>(GetUri(v5AccountSetLeverage), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// The maximum quantity to buy or sell. It corresponds to the "sz" from placement.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="tradeMode">Trade Mode</param>
    /// <param name="currency">Currency</param>
    /// <param name="price">Price</param>
    /// <param name="leverage">Leverage for instrument</param>
    /// <param name="unSpotOffset">Spot-Derivatives risk offset</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountMaximumOrderQuantity>>> GetMaximumOrderQuantityAsync(
        string instrumentId,
        OkxTradeMode tradeMode,
        string currency = null,
        decimal? price = null,
        decimal? leverage = null,
        bool? unSpotOffset = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "instId", instrumentId },
            { "tdMode", JsonConvert.SerializeObject(tradeMode, new OkxTradeModeConverter(false)) },
        };
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("px", price?.ToOkxString());
        parameters.AddOptionalParameter("leverage", leverage?.ToOkxString());
        parameters.AddOptionalParameter("unSpotOffset", unSpotOffset);

        return ProcessListRequestAsync<OkxAccountMaximumOrderQuantity>(GetUri(v5AccountMaxSize), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get Maximum Available Tradable Amount
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="tradeMode">Trade Mode</param>
    /// <param name="currency">Currency</param>
    /// <param name="price">The available amount corresponds to price of close position. Only applicable to reduceOnly MARGIN.</param>
    /// <param name="reduceOnly">Reduce Only</param>
    /// <param name="unSpotOffset">Spot-Derivatives risk offset</param>
    /// <param name="quickMgnType">Quick Margin type. Only applicable to Quick Margin Mode of isolated margin</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountMaximumAvailableAmount>>> GetMaximumAvailableAmountAsync(
        string instrumentId,
        OkxTradeMode tradeMode,
        string currency = null,
        decimal? price = null,
        bool? reduceOnly = null,
        bool? unSpotOffset = null,
        OkxQuickMarginType? quickMgnType = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "instId", instrumentId },
            { "tdMode", JsonConvert.SerializeObject(tradeMode, new OkxTradeModeConverter(false)) },
        };
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("reduceOnly", reduceOnly);
        parameters.AddOptionalParameter("px", price?.ToOkxString());
        parameters.AddOptionalParameter("unSpotOffset", unSpotOffset);
        parameters.AddOptionalParameter("quickMgnType", JsonConvert.SerializeObject(quickMgnType, new OkxQuickMarginTypeConverter(false)));

        return ProcessListRequestAsync<OkxAccountMaximumAvailableAmount>(GetUri(v5AccountMaxAvailSize), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Increase or decrease the margin of the isolated position.
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="positionSide">Position Side</param>
    /// <param name="type">Type</param>
    /// <param name="amount">Amount</param>
    /// <param name="currency">Currency, only applicable to MARGIN（Manual transfers and Quick Margin Mode</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountMarginBalance>>> SetMarginAmountAsync(
        string instrumentId,
        OkxTradePositionSide positionSide,
        OkxAccountMarginAddReduce type,
        decimal amount,
        string currency = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "instId", instrumentId },
            { "posSide", JsonConvert.SerializeObject(positionSide, new OkxTradePositionSideConverter(false)) },
            { "type", JsonConvert.SerializeObject(type, new OkxAccountMarginAddReduceConverter(false)) },
            { "amt", amount.ToOkxString() },
        };
        parameters.AddOptionalParameter("ccy", currency);

        return ProcessListRequestAsync<OkxAccountMarginBalance>(GetUri(v5AccountPositionMarginBalance), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Get Leverage
    /// </summary>
    /// <param name="instrumentIds">ingle instrument ID or multiple instrument IDs (no more than 20) separated with comma</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountLeverage>>> GetLeverageAsync(
        string instrumentIds,
        OkxAccountMarginMode marginMode,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"instId", instrumentIds },
            {"mgnMode", JsonConvert.SerializeObject(marginMode, new OkxAccountMarginModeConverter(false)) },
        };

        return ProcessListRequestAsync<OkxAccountLeverage>(GetUri(v5AccountLeverageInfo), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get leverage estimated info
    /// </summary>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="marginMode">Margin mode</param>
    /// <param name="leverage">Leverage</param>
    /// <param name="instrumentId">Instrument ID，e.g. BTC-USDT</param>
    /// <param name="currency">Currency used for margin, e.g. BTC</param>
    /// <param name="positionSide">posSide</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountLeverageEstimatedInformation>>> GetLeverageEstimatedInformationAsync(
        OkxInstrumentType instrumentType,
        OkxAccountMarginMode marginMode,
        decimal leverage,
        string instrumentId = null,
        string currency = null,
        OkxTradePositionSide? positionSide = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)) },
            {"mgnMode", JsonConvert.SerializeObject(marginMode, new OkxAccountMarginModeConverter(false)) },
            {"lever", leverage.ToOkxString() },
        };
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("posSide", JsonConvert.SerializeObject(positionSide, new OkxTradePositionSideConverter(false)));

        return ProcessListRequestAsync<OkxAccountLeverageEstimatedInformation>(GetUri(v5AccountAdjustLeverageInfo), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get the maximum loan of instrument
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="marginCurrency">Margin Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountMaximumLoanAmount>>> GetMaximumLoanAmountAsync(
        string instrumentId,
        OkxAccountMarginMode marginMode,
        string marginCurrency = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"instId", instrumentId },
            {"mgnMode", JsonConvert.SerializeObject(marginMode, new OkxAccountMarginModeConverter(false)) },
        };
        parameters.AddOptionalParameter("mgnCcy", marginCurrency);

        return ProcessListRequestAsync<OkxAccountMaximumLoanAmount>(GetUri(v5AccountMaxLoan), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get Fee Rates
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="ruleType">Trading rule types</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAccountFeeRate>> GetFeeRatesAsync(
        OkxInstrumentType instrumentType,
        OkxInstrumentRuleType ruleType,
        string instrumentId = null,
        string underlying = null,
        string instrumentFamily = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            {"instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)) },
            {"ruleType", JsonConvert.SerializeObject(ruleType, new OkxInstrumentRuleTypeConverter(false)) },
        };
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        return ProcessOneRequestAsync<OkxAccountFeeRate>(GetUri(v5AccountTradeFee), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get interest-accrued
    /// </summary>
    /// <param name="type">Loan type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="currency">Currency</param>
    /// <param name="marginMode">Margin Mode</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested ts, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100; the default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountInterestAccrued>>> GetInterestAccruedAsync(
        OkxAccountLoanType? type = null,
        string currency = null,
        string instrumentId = null,
        OkxAccountMarginMode? marginMode = null,
        long? after = null,
        long? before = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new OkxAccountLoanTypeConverter(false)));
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("instId", instrumentId);
        parameters.AddOptionalParameter("mgnMode", JsonConvert.SerializeObject(marginMode, new OkxAccountMarginModeConverter(false)));
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxAccountInterestAccrued>(GetUri(v5AccountInterestAccrued), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get the user's current leveraged currency borrowing interest rate
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountInterestRate>>> GetInterestRateAsync(
        string currency = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);

        return ProcessListRequestAsync<OkxAccountInterestRate>(GetUri(v5AccountInterestRate), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Set the display type of Greeks.
    /// </summary>
    /// <param name="greeksType">Display type of Greeks.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAccountGreeksTypeResponse>> SetGreeksAsync(OkxAccountGreeksType greeksType, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { "greeksType", JsonConvert.SerializeObject(greeksType, new OkxAccountGreeksTypeConverter(false)) },
        };

        return ProcessOneRequestAsync<OkxAccountGreeksTypeResponse>(GetUri(v5AccountSetGreeks), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// You can set the currency margin and futures/perpetual Isolated margin trading mode
    /// </summary>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="marginMode">Isolated margin trading settings</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public Task<RestCallResult<OkxAccountIsolatedMarginModeSettings>> SetIsolatedMarginModeAsync(OkxInstrumentType instrumentType, OkxAccountIsolatedMarginMode marginMode, CancellationToken ct = default)
    {
        if (!instrumentType.IsIn(OkxInstrumentType.Margin, OkxInstrumentType.Contracts)) throw new ArgumentException("Only Margin and Contracts allowed", nameof(instrumentType));
        var parameters = new Dictionary<string, object> {
            { "isoMode", JsonConvert.SerializeObject(marginMode, new OkxAccountIsolatedMarginModeConverter(false)) },
            { "type", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)) },
        };

        return ProcessOneRequestAsync<OkxAccountIsolatedMarginModeSettings>(GetUri(v5AccountSetIsolatedMode), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Retrieve the maximum transferable amount.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountWithdrawalAmount>>> GetMaximumWithdrawalsAsync(
        string currency = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);

        return ProcessListRequestAsync<OkxAccountWithdrawalAmount>(GetUri(v5AccountMaxWithdrawal), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get account risk state
    /// Only applicable to Portfolio margin account
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAccountRiskState>> GetRiskStateAsync(CancellationToken ct = default)
    {
        return ProcessOneRequestAsync<OkxAccountRiskState>(GetUri(v5AccountRiskState), HttpMethod.Get, ct, signed: true);
    }

    /// <summary>
    /// VIP loans borrow and repay
    /// Maximum number of borrowing orders for a single currency is 20
    /// </summary>
    /// <param name="currency">Loan currency, e.g. BTC</param>
    /// <param name="amount">Borrow Amount</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAccountVipLoanBorrowRepay>> VipLoanBorrowAsync(string currency, decimal amount, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "ccy", currency },
            { "side", "borrow" },
            { "amt", amount.ToOkxString() }
        };

        return ProcessOneRequestAsync<OkxAccountVipLoanBorrowRepay>(GetUri(v5AccountBorrowRepay), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// VIP loans borrow and repay
    /// Maximum number of borrowing orders for a single currency is 20
    /// </summary>
    /// <param name="currency">Loan currency, e.g. BTC</param>
    /// <param name="amount">Repay Amount</param>
    /// <param name="orderId">Order ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAccountVipLoanBorrowRepay>> VipLoanRepayAsync(string currency, decimal amount, long orderId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "ccy", currency },
            { "side", "repay" },
            { "ordId", orderId.ToOkxString() },
            { "amt", amount.ToOkxString() }
        };

        return ProcessOneRequestAsync<OkxAccountVipLoanBorrowRepay>(GetUri(v5AccountBorrowRepay), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Get borrow and repay history for VIP loans
    /// </summary>
    /// <param name="currency">Loan currency, e.g. BTC</param>
    /// <param name="after">Pagination of data to return records earlier than the requested timestamp, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountVipLoanBorrowRepayHistory>>> GetVipLoanBorrowRepayHistoryAsync(
    string currency = null,
    long? after = null,
    long? before = null,
    int limit = 100,
    CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxAccountVipLoanBorrowRepayHistory>(GetUri(v5AccountBorrowRepayHistory), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get VIP interest accrued data
    /// </summary>
    /// <param name="currency">Loan currency, e.g. BTC. Only applicable toMARGIN</param>
    /// <param name="orderId">Order ID of borrowing</param>
    /// <param name="after">Pagination of data to return records earlier than the requested timestamp, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountVipInterestAccruedData>>> GetVipInterestAccruedDataAsync(
    string currency = null,
    long? orderId = null,
    long? after = null,
    long? before = null,
    int limit = 100,
    CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("ordId", orderId?.ToOkxString());
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxAccountVipInterestAccruedData>(GetUri(v5AccountVipInterestAccrued), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get VIP interest deducted data
    /// </summary>
    /// <param name="currency">Loan currency, e.g. BTC. Only applicable toMARGIN</param>
    /// <param name="orderId">Order ID of borrowing</param>
    /// <param name="after">Pagination of data to return records earlier than the requested timestamp, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="before">Pagination of data to return records newer than the requested, Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountVipInterestDeductedData>>> GetVipInterestDeductedDataAsync(
    string currency = null,
    long? orderId = null,
    long? after = null,
    long? before = null,
    int limit = 100,
    CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("ordId", orderId?.ToOkxString());
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxAccountVipInterestDeductedData>(GetUri(v5AccountVipInterestDeducted), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get VIP loan order list
    /// </summary>
    /// <param name="orderId">Order ID of borrowing</param>
    /// <param name="state">State 1:Borrowing 2:Borrowed 3:Repaying 4:Repaid 5:Borrow failed</param>
    /// <param name="currency">Loan currency, e.g. BTC</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ordId</param>
    /// <param name="before">Pagination of data to return records newer than the requested ordId</param>
    /// <param name="limit">Number of results per request. The maximum is 100; The default is 100</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountVipLoanOrder>>> GetVipLoanOrdersAsync(
    long? orderId = null,
    OkxAccountVipLoanState? state = null,
    string currency = null,
    long? after = null,
    long? before = null,
    int limit = 100,
    CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ordId", orderId?.ToOkxString());
        parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(state, new OkxAccountVipLoanStateConverter(false)));
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxAccountVipLoanOrder>(GetUri(v5AccountVipLoanOrderList), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get VIP loan order detail
    /// </summary>
    /// <param name="orderId">Order ID of borrowing</param>
    /// <param name="currency">Loan currency, e.g. BTC</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ordId</param>
    /// <param name="before">Pagination of data to return records newer than the requested ordId</param>
    /// <param name="limit">Number of results per request. The maximum is 100; The default is 100</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountVipLoanOrderDetails>>> GetVipLoanOrderDetailsAsync(
    long? orderId = null,
    string currency = null,
    long? after = null,
    long? before = null,
    int limit = 100,
    CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ordId", orderId?.ToOkxString());
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxAccountVipLoanOrderDetails>(GetUri(v5AccountVipLoanOrderDetail), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get borrow interest and limit
    /// </summary>
    /// <param name="type">Loan type</param>
    /// <param name="currency">Loan currency, e.g. BTC</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountInterestLimits>>> GetInterestLimitsAsync(
    OkxAccountLoanType? type = null,
    string currency = null,
    CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new OkxAccountLoanTypeConverter(false)));
        parameters.AddOptionalParameter("ccy", currency);

        return ProcessListRequestAsync<OkxAccountInterestLimits>(GetUri(v5AccountInterestLimits), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get fixed loan borrow limit
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAccountFixedLoanBorrowingLimit>> GetFixedLoanBorrowingLimitAsync(CancellationToken ct = default)
    {
        return ProcessOneRequestAsync<OkxAccountFixedLoanBorrowingLimit>(GetUri(v5AccountFixedLoanBorrowingLimit), HttpMethod.Get, ct, signed: true);
    }

    /// <summary>
    /// Get fixed loan borrow quote
    /// </summary>
    /// <param name="type">Type</param>
    /// <param name="currency">Borrowing currency, e.g. BTC. if type=normal, the parameter is required.</param>
    /// <param name="amount">Borrowing amount. if type=normal, the parameter is required.</param>
    /// <param name="maximumRate">Maximum acceptable borrow rate, in decimal. e.g. 0.01 represents 1%. if type=normal, the parameter is required.</param>
    /// <param name="term">Fixed term for borrowing order. 30D：30 Days. if type=normal, the parameter is required.</param>
    /// <param name="orderId">Order ID. if type=reborrow, the parameter is required.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public Task<RestCallResult<OkxAccountFixedLoanBorrowingQuote>> GetFixedLoanBorrowingQuoteAsync(
    OkxAccountFixedLoanBorrowingType type,
    string currency = null,
    decimal? amount = null,
    decimal? maximumRate = null,
    string term = null,
    long? orderId = null,
    CancellationToken ct = default)
    {
        if (type == OkxAccountFixedLoanBorrowingType.Normal)
        {
            if (string.IsNullOrEmpty(currency)) throw new ArgumentNullException($"{nameof(currency)} is required for Normal Borrows");
            if (amount is null) throw new ArgumentNullException($"{nameof(amount)} is required for Normal Borrows");
            if (maximumRate is null) throw new ArgumentNullException($"{nameof(maximumRate)} is required for Normal Borrows");
            if (string.IsNullOrEmpty(term)) throw new ArgumentNullException($"{nameof(term)} is required for Normal Borrows");
        }
        else if (type == OkxAccountFixedLoanBorrowingType.Reborrow)
        {
            if (orderId is null) throw new ArgumentNullException($"{nameof(orderId)} is required for Reborrows");
        }

        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new OkxAccountFixedLoanBorrowingTypeConverter(false)));
        parameters.AddOptionalParameter("ccy", currency);
        parameters.AddOptionalParameter("amt", amount.ToOkxString());
        parameters.AddOptionalParameter("maxRate", maximumRate.ToOkxString());
        parameters.AddOptionalParameter("term", term);
        parameters.AddOptionalParameter("ordId", orderId.ToOkxString());

        return ProcessOneRequestAsync<OkxAccountFixedLoanBorrowingQuote>(GetUri(v5AccountFixedLoanBorrowingQuote), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Place fixed loan borrowing order
    /// For new borrowing orders, they belong to the IOC (immediately close and cancel the remaining) type. For renewal orders, they belong to the FOK (Fill-or-kill) type.
    /// Order book may refer to Get lending volume (public).
    /// </summary>
    /// <param name="currency">Borrowing currency, e.g. BTC</param>
    /// <param name="amount">Borrowing amount</param>
    /// <param name="maximumRate">Maximum acceptable borrow rate, in decimal. e.g. 0.01 represents 1%.</param>
    /// <param name="term">Fixed term for borrowing order. 30D：30 Days</param>
    /// <param name="reborrow">Whether or not auto-renew when the term is due</param>
    /// <param name="reborrowRate">Auto-renew borrowing rate, in decimal. e.g. 0.01 represents 1%. If reborrow is true, the parameter is required.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAccountFixedLoanBorrowingOrder>> PlaceFixedLoanBorrowingOrderAsync(
    string currency,
    decimal amount,
    decimal maximumRate,
    string term,
    bool reborrow = false,
    decimal? reborrowRate = null,
    CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "ccy", currency },
            { "amt", amount.ToOkxString() },
            { "maxRate", maximumRate.ToOkxString() },
            { "term", term },
        };
        parameters.AddOptionalParameter("reborrow", reborrow);
        parameters.AddOptionalParameter("reborrowRate", reborrowRate?.ToOkxString());

        return ProcessOneRequestAsync<OkxAccountFixedLoanBorrowingOrder>(GetUri(v5AccountFixedLoanBorrowingOrder), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Amend fixed loan borrowing order
    /// </summary>
    /// <param name="orderId">Borrowing order ID, e.g. BTC</param>
    /// <param name="reborrow">Whether or not reborrowing when the term is due. Default is false.</param>
    /// <param name="maximumRate">Maximum acceptable auto-renew borrow rate for borrowing order, in decimal. e.g. 0.01 represents 1%. If reborrow is true, the parameter is required.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAccountFixedLoanBorrowingOrder>> AmendFixedLoanBorrowingOrderAsync(
    long orderId,
    bool? reborrow = null,
    decimal? maximumRate = null,
    CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "ordId", orderId },
        };
        parameters.AddOptionalParameter("reborrow", reborrow);
        parameters.AddOptionalParameter("renewMaxRate", maximumRate?.ToOkxString());

        return ProcessOneRequestAsync<OkxAccountFixedLoanBorrowingOrder>(GetUri(v5AccountFixedLoanAmendBorrowingOrder), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Manual renew fixed loan borrowing order
    /// </summary>
    /// <param name="orderId">Borrowing order ID</param>
    /// <param name="maximumRate">Maximum acceptable auto-renew borrow rate for borrowing order, in decimal. e.g. 0.01 represents 1%.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAccountFixedLoanBorrowingOrder>> RenewFixedLoanBorrowingOrderAsync(
    long orderId,
    decimal? maximumRate = null,
    CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "ordId", orderId },
            { "maxRate", maximumRate?.ToOkxString() },
        };

        return ProcessOneRequestAsync<OkxAccountFixedLoanBorrowingOrder>(GetUri(v5AccountFixedLoanManualReborrow), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Repay fixed loan borrowing order
    /// </summary>
    /// <param name="orderId">Borrowing order ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAccountFixedLoanBorrowingOrder>> RepayFixedLoanBorrowingOrderAsync(
    long orderId,
    CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "ordId", orderId },
        };

        return ProcessOneRequestAsync<OkxAccountFixedLoanBorrowingOrder>(GetUri(v5AccountFixedLoanRepayBorrowingOrder), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Get fixed loan borrow order list
    /// </summary>
    /// <param name="orderId">Borrowing order ID</param>
    /// <param name="currency">Borrowing currency, e.g. BTC</param>
    /// <param name="state">State</param>
    /// <param name="after">Pagination of data to return records earlier than the requested ordId</param>
    /// <param name="before">Pagination of data to return records newer than the requested ordId</param>
    /// <param name="limit">Number of results per request. The maximum is 100. The default is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAccountFixedLoanBorrowingOrderDetails>> GetFixedLoanBorrowingOrdersAsync(
    long? orderId = null,
    string currency = null,
    OkxAccountFixedLoanBorrowingOrderState? state = null,
    long? after = null,
    long? before = null,
    int limit = 100,
    CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "ordId", orderId },
        };
        parameters.AddOptionalParameter("after", currency);
        parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(state, new OkxAccountFixedLoanBorrowingOrderStateConverter(false)));
        parameters.AddOptionalParameter("after", after?.ToOkxString());
        parameters.AddOptionalParameter("before", before?.ToOkxString());
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessOneRequestAsync<OkxAccountFixedLoanBorrowingOrderDetails>(GetUri(v5AccountFixedLoanBorrowingOrdersList), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Calculates portfolio margin information for virtual position/assets or current position of the user.
    /// You can add up to 200 virtual positions and 200 virtual assets in one request.
    /// </summary>
    /// <param name="importExistingPositionsAndAssets">Whether import existing positions and assets. The default is true</param>
    /// <param name="spotOffsetType">Spot-derivatives risk offset mode</param>
    /// <param name="simulatedPositions">List of simulated positions</param>
    /// <param name="simulatedAssets">List of simulated assets</param>
    /// <param name="greeksType">Greeks type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountPositionBuilder>>> PositionBuilderAsync(
    bool importExistingPositionsAndAssets = true,
    OkxAccountRiskOffsetType spotOffsetType = OkxAccountRiskOffsetType.DerivativesOnly,
    IEnumerable<OkxAccountSimulatedPosition> simulatedPositions = null,
    IEnumerable<OkxAccountSimulatedAsset> simulatedAssets = null,
    OkxAccountGreeksType? greeksType = OkxAccountGreeksType.BlackScholesGreeksInDollars,
    CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("inclRealPosAndEq", importExistingPositionsAndAssets);
        parameters.AddOptionalParameter("spotOffsetType", JsonConvert.SerializeObject(spotOffsetType, new OkxAccountRiskOffsetTypeConverter(false)));
        parameters.AddOptionalParameter("simPos", simulatedPositions);
        parameters.AddOptionalParameter("simAsset", simulatedAssets);
        parameters.AddOptionalParameter("greeksType", JsonConvert.SerializeObject(greeksType, new OkxAccountGreeksTypeConverter(false)));

        return ProcessListRequestAsync<OkxAccountPositionBuilder>(GetUri(v5AccountPositionBuilder), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Set risk offset amount. This does not represent the actual spot risk offset amount. Only applicable to Portfolio Margin Mode.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <param name="spotRiskOffsetAmount">Spot risk offset amount defined by users</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAccountRiskOffsetAmount>> SetRiskOffsetAmountAsync(
        string currency,
        decimal spotRiskOffsetAmount,
    CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "ccy", currency },
            { "clSpotInUseAmt", spotRiskOffsetAmount.ToOkxString() }
        };

        return ProcessOneRequestAsync<OkxAccountRiskOffsetAmount>(GetUri(v5AccountSetRiskOffsetAmount), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Retrieve a greeks list of all assets in the account.
    /// </summary>
    /// <param name="currency">Single currency, e.g. BTC.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxAccountGreeks>>> GetGreeksAsync(
    string currency = null,
    CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ccy", currency);

        return ProcessListRequestAsync<OkxAccountGreeks>(GetUri(v5AccountGreeks), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve cross position limitation of SWAP/FUTURES/OPTION under Portfolio margin mode.
    /// </summary>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="underlying">Single underlying or multiple underlyings (no more than 3) separated with comma. Either uly or instFamily is required. If both are passed, instFamily will be used.</param>
    /// <param name="instrumentFamily">Single instrument family or instrument families (no more than 5) separated with comma. Either uly or instFamily is required. If both are passed, instFamily will be used.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAccountPositionTiers>> GetPositionTiersAsync(
    OkxInstrumentType instrumentType,
    string underlying = null,
    string instrumentFamily = null,
    CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)));
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        return ProcessOneRequestAsync<OkxAccountPositionTiers>(GetUri(v5AccountPositionTiers), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Configure the risk offset type in portfolio margin mode.
    /// </summary>
    /// <param name="type">Risk offset type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAccountRiskOffsetTypeModel>> SetRiskOffsetTypeAsync(
    OkxAccountRiskOffsetType type,
    CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("type", JsonConvert.SerializeObject(type, new OkxAccountRiskOffsetTypeConverter(false)));

        return ProcessOneRequestAsync<OkxAccountRiskOffsetTypeModel>(GetUri(v5AccountSetRiskOffsetType), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Activate option
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxTimestamp>> ActivateOptionAsync(CancellationToken ct = default)
    {
        return ProcessOneRequestAsync<OkxTimestamp>(GetUri(v5AccountActivateOption), HttpMethod.Post, ct, signed: true);
    }

    /// <summary>
    /// Set auto loan
    /// Only applicable to Multi-currency margin and Portfolio margin
    /// </summary>
    /// <param name="autoLoan">Whether to automatically make loans. Valid values are true, false. The default is true</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAccountAutoLoan>> SetAutoLoanAsync(bool autoLoan, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "autoLoan", autoLoan }
        };

        return ProcessOneRequestAsync<OkxAccountAutoLoan>(GetUri(v5AccountSetAutoLoan), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Set account mode
    /// You need to set on the Web/App for the first set of every account mode.
    /// </summary>
    /// <param name="accountLevel">Account mode</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAccountLevelData>> SetLevelAsync(OkxAccountLevel accountLevel, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("acctLv", JsonConvert.SerializeObject(accountLevel, new OkxAccountLevelConverter(false)));

        return ProcessOneRequestAsync<OkxAccountLevelData>(GetUri(v5AccountSetAccountLevel), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Reset MMP Status
    /// You can unfreeze by this endpoint once MMP is triggered.
    /// Only applicable to Option in Portfolio Margin mode, and MMP privilege is required.
    /// </summary>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxBooleanResponse>> ResetMmpAsync(string instrumentFamily, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instType", "OPTION" },
            { "instFamily", instrumentFamily }
        };

        return ProcessOneRequestAsync<OkxBooleanResponse>(GetUri(v5AccountMmpReset), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Set MMP
    /// This endpoint is used to set MMP configure
    /// Only applicable to Option in Portfolio Margin mode, and MMP privilege is required.
    /// 
    /// What is MMP?
    /// Market Maker Protection (MMP) is an automated mechanism for market makers to pull their quotes when their executions exceed a certain threshold(`qtyLimit`) within a certain time frame(`timeInterval`). Once mmp is triggered, any pre-existing mmp pending orders(`mmp` and `mmp_and_post_only` orders) will be automatically canceled, and new orders tagged as MMP will be rejected for a specific duration(`frozenInterval`), or until manual reset by makers.
    /// 
    /// How to enable MMP?
    /// Please send an email to institutional@okx.com or contact your business development (BD) manager to apply for MMP. The initial threshold will be upon your request.
    /// </summary>
    /// <param name="instrumentFamily">Instrument family</param>
    /// <param name="timeInterval">Time window (ms). MMP interval where monitoring is done. "0" means disable MMP</param>
    /// <param name="frozenInterval">Frozen period (ms). "0" means the trade will remain frozen until you request "Reset MMP Status" to unfrozen</param>
    /// <param name="quantityLimit">Trade qty limit in number of contracts. Must be > 0</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAccountMmpConfiguration>> SetMmpAsync(string instrumentFamily, int timeInterval, int frozenInterval, int quantityLimit, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instFamily", instrumentFamily },
            { "timeInterval", timeInterval.ToOkxString() },
            { "frozenInterval", frozenInterval.ToOkxString() },
            { "qtyLimit", quantityLimit.ToOkxString() },
        };

        return ProcessOneRequestAsync<OkxAccountMmpConfiguration>(GetUri(v5AccountMmpConfig), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// GET MMP Config
    /// This endpoint is used to get MMP configure information
    /// Only applicable to Option in Portfolio Margin mode, and MMP privilege is required.
    /// </summary>
    /// <param name="instrumentFamily">Instrument Family</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxAccountMmpConfigurationData>> GetMmpAsync(string instrumentFamily, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instFamily", instrumentFamily },
        };

        return ProcessOneRequestAsync<OkxAccountMmpConfigurationData>(GetUri(v5AccountMmpConfig), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

}
