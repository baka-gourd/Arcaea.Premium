using System.Text.Json.Serialization;

using J = System.Text.Json.Serialization.JsonPropertyNameAttribute;

namespace Arcaea.Premium.Models.Wiki;

public class SongConstantValues
{
    public Dictionary<string, SongConstantValueTuple?[]>? ConstantValueTuplesDict { get; set; }
}

public class SongConstantValueTuple
{
    [JsonIgnore]
    public long Id { get; set; }
    [J("constant")] public double Constant { get; set; }
    [J("old")] public bool Old { get; set; }
}

[JsonSerializable(typeof(SongConstantValues))]
[JsonSourceGenerationOptions(WriteIndented = true)]
public partial class SongConstantValuesContext : JsonSerializerContext
{
}