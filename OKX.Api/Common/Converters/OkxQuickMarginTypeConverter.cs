﻿namespace OKX.Api.Common;

internal class OkxQuickMarginTypeConverter(bool quotes) : BaseConverter<OkxQuickMarginType>(quotes)
{
    public OkxQuickMarginTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxQuickMarginType, string>> Mapping =>
    [
        new(OkxQuickMarginType.Manual, "manual"),
        new(OkxQuickMarginType.AutoBorrow, "auto_borrow"),
        new(OkxQuickMarginType.AutoRepay, "auto_repay"),
    ];
}