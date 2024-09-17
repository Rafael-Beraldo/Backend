using CRUD.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace CRUD.Services;

public class MongoDBService {

    private readonly IMongoCollection<Carro> _carroCollection;

    public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings) {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _carroCollection = database.GetCollection<Carro>(mongoDBSettings.Value.CollectionName);
    }

    public async Task CreateAsync(Carro carro){
        if (carro == null)
        {
            throw new ArgumentNullException(nameof(carro));
        }
        await _carroCollection.InsertOneAsync(carro);
        return;
    }

    public async Task<List<Carro>> GetAsync() {
        return await _carroCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task AddToCarroAsync(string id, string modelo, string plate, int yearManufacture, List<string> accessory) {
        var filter = Builders<Carro>.Filter.Eq(c => c.Id, id);
        var update = Builders<Carro>.Update
            .Set(c => c.Modelo, modelo)
            .Set(c => c.Plate, plate)
            .Set(c => c.YearManufacture, yearManufacture)
            .Set(c => c.Accessory, accessory);

        await _carroCollection.UpdateOneAsync(filter, update);
    }

    public async Task DeleteAsync(string id) {
        FilterDefinition<Carro> filter = Builders<Carro>.Filter.Eq("Id", id);
        await _carroCollection.DeleteOneAsync(filter);
        return;
    }

    internal async Task GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }
}