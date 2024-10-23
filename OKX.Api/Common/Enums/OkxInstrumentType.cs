using Newtonsoft.Json.Converters;

namespace OKX.Api.Common;

/// <summary>
/// OKX Instrument Type
/// </summary>
[JsonConverter(typeof(UppercaseEnumConverter))]
public enum OkxInstrumentType
{
    /// <summary>
    /// Any
    /// </summary>
    [Map("ANY")]
    Any = 0,

    /// <summary>
    /// Spot
    /// </summary>
    [Map("SPOT")]
    Spot = 1,

    /// <summary>
    /// Margin
    /// </summary>
    [Map("MARGIN")]
    Margin = 2,

    /// <summary>
    /// Swap
    /// </summary>
    [Map("SWAP")]
    Swap = 3,

    /// <summary>
    /// Futures
    /// </summary>
    [Map("FUTURES")]
    Futures = 4,

    /// <summary>
    /// Option
    /// </summary>
    [Map("OPTION")]
    Option = 5,

    /// <summary>
    /// Contracts
    /// </summary>
    [Map("CONTRACTS")]
    Contracts = 6,
}
public class UppercaseEnumConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value is Enum enumValue)
        {
            // Convert the enum value to uppercase
            writer.WriteValue(enumValue.ToString().ToUpper());
        }
        else
        {
            writer.WriteValue(value?.ToString());
        }
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        // Read the value as a string
        var enumText = reader.Value?.ToString()?.ToUpper();
        if (enumText == null)
            return existingValue;

        // Try to parse the uppercase string back to the corresponding enum
        try
        {
            return Enum.Parse(objectType, enumText, ignoreCase: true);
        }
        catch (ArgumentException)
        {
            // Return the default value if parsing fails
            return existingValue;
        }
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType.IsEnum;
    }
}