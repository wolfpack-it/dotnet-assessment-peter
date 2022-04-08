using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Wolfpack.Data.Database.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Gender
{
    [EnumMember(Value = "male")]
    Male = 100,

    [EnumMember(Value = "female")]
    Female = 200,

    [EnumMember(Value = "other")]
    Other = 300
}