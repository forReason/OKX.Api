﻿namespace OKX.Api.Grid;

internal class OkxGridInvestmentTypeConverter(bool quotes) : BaseConverter<OkxGridInvestmentType>(quotes)
{
    public OkxGridInvestmentTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxGridInvestmentType, string>> Mapping =>
    [
        new(OkxGridInvestmentType.Grid, "grid"),
        new(OkxGridInvestmentType.Quote, "quote"),
        new(OkxGridInvestmentType.Base, "base"),
        new(OkxGridInvestmentType.Dual, "dual"),
    ];
}