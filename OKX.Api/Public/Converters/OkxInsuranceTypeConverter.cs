﻿namespace OKX.Api.Public;

internal class OkxInsuranceTypeConverter(bool quotes) : BaseConverter<OkxInsuranceType>(quotes)
{
    public OkxInsuranceTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxInsuranceType, string>> Mapping =>
    [
        new(OkxInsuranceType.All, "all"),
        new(OkxInsuranceType.LiquidationBalanceDeposit, "liquidation_balance_deposit"),
        new(OkxInsuranceType.BankruptcyLoss, "bankruptcy_loss"),
        new(OkxInsuranceType.PlatformRevenue, "platform_revenue"),
    ];
}