namespace Api.Models;
using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Relationship
{
    None,
    Spouse,
    DomesticPartner,
    Child
}

