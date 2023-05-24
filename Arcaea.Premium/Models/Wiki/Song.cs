using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using static System.Text.Json.Serialization.JsonIgnoreCondition;

namespace Arcaea.Premium.Models.Wiki;

using System.Text.Json.Serialization;
using J = System.Text.Json.Serialization.JsonPropertyNameAttribute;

public class SongWrapper
{
    [J("songs")] public Song[]? Songs { get; set; }
}

public class Song
{
    [J("idx")] public long Idx { get; set; }
    [J("id")] public string? Id { get; set; }
    [J("title_localized")] public Dictionary<string,string>? TitleLocalizationDict { get; set; }
    [J("artist")] public string? Artist { get; set; }
    [J("bpm")] public string? Bpm { get; set; }
    [J("bpm_base")] public double BpmBase { get; set; }
    [J("set")] public string? Set { get; set; }
    [J("purchase")] public string? Purchase { get; set; }
    [J("audioPreview")] public long AudioPreview { get; set; }
    [J("audioPreviewEnd")] public long AudioPreviewEnd { get; set; }
    [J("side")] public long Side { get; set; }
    [J("bg")] public string? Bg { get; set; }
    [J("bg_inverse")] public string? BgInverse { get; set; }
    [J("date")] public long Date { get; set; }
    [J("version")] public string? Version { get; set; }
    [J("difficulties")] public Difficulty[]? Difficulties { get; set; }
    [JsonIgnore(Condition = WhenWritingNull)][J("world_unlock")] public bool? WorldUnlock { get; set; }
    [JsonIgnore(Condition = WhenWritingNull)][J("byd_local_unlock")] public bool? BeyondUnLock { get; set; }
    [JsonIgnore(Condition = WhenWritingNull)][J("remote_dl")] public bool? RemoteDownload { get; set; }
}

public class Difficulty
{
    [JsonIgnore]
    public long Id { get; set; }
    [J("ratingClass")] public long RatingClass { get; set; }
    [J("chartDesigner")] public string? ChartDesigner { get; set; }
    [J("jacketDesigner")] public string? JacketDesigner { get; set; }
    [J("rating")] public long Rating { get; set; }
    [JsonIgnore(Condition = WhenWritingNull)][J("jacketOverride")] public bool? JacketOverride { get; set; }
    [JsonIgnore(Condition = WhenWritingNull)][J("ratingPlus")] public bool? RatingPlus { get; set; }
    [JsonIgnore(Condition = WhenWritingNull)][J("date")] public long? Date { get; set; }
    [JsonIgnore(Condition = WhenWritingNull)][J("version")] public string? Version { get; set; }
}

[JsonSerializable(typeof(SongWrapper[]))]
[JsonSourceGenerationOptions(WriteIndented = true)]
public partial class SongContext:JsonSerializerContext
{
    
}