using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace CRUD.Models;

public class Carro {

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("modelo")]
    public string Modelo { get; set; } = null!;

    [BsonElement("plate")]
    public string Plate { get; set; } = null!;

    [BsonElement("yearManufacture")]
    public int YearManufacture { get; set; } = 0;

    [BsonElement("accessory")]
    public List<string> Accessory { get; set; } = new List<string>();
}