using TrainTrackChecker.Models;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.FluentUI.AspNetCore.Components;
using System.ComponentModel;
using System.Net.Mime;


namespace TrainTrackChecker.Services {


    public interface IApiService<TEntity> {
        public Task<IQueryable<TEntity>?> GetAllAsync();
        public Task<TEntity?> GetByIdAsync(Guid? id);
        public Task<TEntity?> SaveAsync(TEntity entity, Guid? itemId = null);
        public Task<bool> DeleteAsync(Guid? id);
    }

    public class ApiService<TEntity>(HttpClient httpClient)
        : IApiService<TEntity> {

        private readonly HttpClient _httpClient = httpClient;
        private string endpointName {
            get {
                switch (typeof(TEntity).Name) {
                    case "Hardware": { return "/api/Hardwares"; }
                    case "Trecho": { return "/api/Trechos"; }
                    case "Trecho_Registro": { return "/api/Trechos_Registros"; }
                    case "OrdemManutencao": { return "/api/OrdensManutencao"; }
                    case "OrdemManutencao_Local": { return "/api/OrdensManutencao_Locais"; }
                    default: throw new NotImplementedException();
                }
            }
        }

        private static readonly JsonSerializerOptions _serializerOptions = new() {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        
        public async Task<IQueryable<TEntity>?> GetAllAsync() {

            HttpResponseMessage response = await _httpClient.GetAsync(endpointName);
            if (response.IsSuccessStatusCode) {
                string content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<TEntity>>(content, _serializerOptions)?.AsQueryable();
            } else {
                response.EnsureSuccessStatusCode();
            }

            return default;
        }

        public async Task<TEntity?> GetByIdAsync(Guid? id) {

            HttpResponseMessage response = await _httpClient.GetAsync($"{endpointName}/{id}");
            if (response.IsSuccessStatusCode) {
                string content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<TEntity>(content, _serializerOptions);
            } else {
                response.EnsureSuccessStatusCode();
            }

            return default;
        }

        public async Task<TEntity?> SaveAsync(TEntity entity, Guid? existingItemId = null) //bool isNewItem = false
        {   
            string json = JsonSerializer.Serialize(entity, _serializerOptions);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);

            HttpResponseMessage response;
            if (existingItemId == null) {
                response = await _httpClient.PostAsync(endpointName, stringContent);
                if (response.IsSuccessStatusCode) {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<TEntity>(content, _serializerOptions);
                } else {
                    response.EnsureSuccessStatusCode();
                }
            } else {
                response = await _httpClient.PutAsync($"{endpointName}/{existingItemId}", stringContent);
                if (response.IsSuccessStatusCode == false) {
                    response.EnsureSuccessStatusCode();
                }
            }

            return default;
        }

        public async Task<bool> DeleteAsync(Guid? id) {

            HttpResponseMessage response = await _httpClient.DeleteAsync($"{endpointName}/{id}");
            if (response.IsSuccessStatusCode) {
                Debug.WriteLine($@"\{typeof(TEntity).Name} successfully deleted.");
                return true;
            } else {
                response.EnsureSuccessStatusCode();
            }

            return false;
        }

    }

}











#region Old



//public interface IApiService<TEntity> {
//    public Task<IQueryable<TEntity>?> GetAllAsync();
//    public Task<TEntity?> GetByIdAsync(Guid? id);
//    public Task<TEntity?> SaveAsync(TEntity entity, Guid? itemId = null);
//    public Task<bool> DeleteAsync(Guid? id);
//}

//public class ApiService<TEntity>(HttpClient httpClient)
//: ApiServiceBase<TEntity>(httpClient), IApiService<TEntity> {
//    //, ILogger<HardwareRepository> logger
//    //private readonly ILogger<HardwareRepository> _logger = logger;

//    public async Task<IQueryable<TEntity>?> GetAllAsync() {
//        return await GetEntitiesAllAsync();
//    }
//    public async Task<TEntity?> GetByIdAsync(Guid? id) {
//        return await GetEntityByIdAsync(id);
//    }
//    public async Task<TEntity?> SaveAsync(TEntity entity, Guid? itemId = null) {
//        return await SaveEntityAsync(entity, itemId);
//    }
//    public async Task<bool> DeleteAsync(Guid? id) {
//        return await DeleteEntityAsync(id);
//    }

//}

//    public class ApiServiceBase<TEntity>(HttpClient httpClient) {
//        //, ILogger logger
//        //: IRepository<TEntity> where TEntity : Entity {

//        private readonly HttpClient _httpClient = httpClient;
//        private string endpointName { 
//            get {
//                switch (typeof(TEntity).Name) {
//                    case "Hardware": { return "/api/Hardwares"; }
//                    case "Trecho": { return "/api/Trechos"; }
//                    case "Trecho_Registro": { return "/api/Trechos_Registros"; }
//                    case "OrdemManutencao": { return "/api/OrdensManutencao"; } 
//                    case "OrdemManutencao_Local": { return "/api/OrdensManutencao_Locais"; }
//                    default: throw new NotImplementedException();
//                }
//            }
//        }
//        //private readonly ILogger _logger = logger; 
//        //_logger.LogInformation("Add concert to artist");

//        private static readonly JsonSerializerOptions _serializerOptions = new() {
//            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
//            WriteIndented = true
//        };

//        protected async Task<IQueryable<TEntity>?> GetEntitiesAllAsync() {

//            HttpResponseMessage response = await _httpClient.GetAsync(endpointName);
//            if (response.IsSuccessStatusCode) {
//                string content = await response.Content.ReadAsStringAsync();
//                return JsonSerializer.Deserialize<List<TEntity>>(content, _serializerOptions)?.AsQueryable();
//            }

//            return default;
//        }

//        protected async Task<TEntity?> GetEntityByIdAsync(Guid? id) {

//            HttpResponseMessage response = await _httpClient.GetAsync($"{endpointName}/{id}");
//            if (response.IsSuccessStatusCode) {
//                string content = await response.Content.ReadAsStringAsync();
//                return JsonSerializer.Deserialize<TEntity>(content, _serializerOptions);
//            }

//            return default;
//        }

//        protected async Task<TEntity?> SaveEntityAsync(TEntity entity, Guid? existingItemId = null) //bool isNewItem = false
//        {
//            string json = JsonSerializer.Serialize(entity, _serializerOptions);
//            StringContent stringContent = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);

//            HttpResponseMessage response;
//            if (existingItemId == null) {
//                response = await _httpClient.PostAsync(endpointName, stringContent);
//                if (response.IsSuccessStatusCode) {
//                    string content = await response.Content.ReadAsStringAsync();
//                    return JsonSerializer.Deserialize<TEntity>(content, _serializerOptions);
//                }
//            } else {
//                response = await _httpClient.PutAsync($"{endpointName}/{existingItemId}", stringContent);
//                if (response.IsSuccessStatusCode == false) {
//                    throw new HttpRequestException("Error: ", null, response.StatusCode);
//                }
//            }

//            return default;
//        }

//        protected async Task<bool> DeleteEntityAsync(Guid? id) {

//                HttpResponseMessage response = await _httpClient.DeleteAsync($"{endpointName}/{id}");
//                if (response.IsSuccessStatusCode) {
//                    Debug.WriteLine($@"\{typeof(TEntity).Name} successfully deleted.");
//                    return true;
//                }

//            return false;
//        }

//        //private string formatError(Exception ex) {
//        //    return string.Format(@"\tError {0}: {1}", typeof(TEntity).Name, ex.Message);
//        //}

//    }


#endregion