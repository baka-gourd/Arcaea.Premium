using System;
using System.Collections.Generic;

using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;
using J = System.Text.Json.Serialization.JsonPropertyNameAttribute;
using N = System.Text.Json.Serialization.JsonIgnoreCondition;

public class WikiResp
{
    [J("batchcomplete")] public string? BatchComplete { get; set; }
    [J("warnings")] public Warnings? Warnings { get; set; }
    [J("query")] public Query? Query { get; set; }
}

public class Query
{
    [J("pages")] public Dictionary<string,PageData>? Pages { get; set; }
}

public class PageData
{
    [J("pageid")] public long PageId { get; set; }
    [J("ns")] public long Ns { get; set; }
    [J("title")] public string? Title { get; set; }
    [J("revisions")] public Revision[]? Revisions { get; set; }
}

public class Revision
{
    [J("contentformat")] public string? ContentFormat { get; set; }
    [J("contentmodel")] public string? ContentModel { get; set; }
    [J("*")] public string? Result { get; set; }
}

public class Warnings
{
    [J("main")] public Main? Main { get; set; }
    [J("revisions")] public Main? Revisions { get; set; }
}

public class Main
{
    [J("*")] public string? Result { get; set; }
}


[JsonSerializable(typeof(WikiResp))]
[JsonSourceGenerationOptions(WriteIndented = true)]
public partial class WikiRespContext : JsonSerializerContext
{

}