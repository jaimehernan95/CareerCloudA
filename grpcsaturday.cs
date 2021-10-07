Milad Ghafoori
10:03 AM

using GrpcService.Interfaces;

using System.Collections.Concurrent;



namespace GrpcService.Services

{

  public class CacheService : ICache

  {



    private readonly ConcurrentDictionary<string, string> _cache = new ConcurrentDictionary<string, string>();



    public bool Add(string key, string value)

    {

      if (_cache.ContainsKey(key))

      {

        return false;

      }

      return _cache.TryAdd(key, value);

    }



    public bool Delete(string key)

    {

      return _cache.TryRemove(key, out _);

    }



    public string Get(string key)

    {

      if (_cache.TryGetValue(key, out var value))

      {

        return value;

      }

      return null;

    }



    public bool Update(string key, string value)

    {

      var oldValue = Get(key);

      if (oldValue == null)

      {

        return false;

      }

      return _cache.TryUpdate(key, value, oldValue);

    }



  }

}





Milad Ghafoori
10:30 AM

syntax = "proto3";



option csharp_namespace = "GrpcService.Services";



service CacheServiceProvider {

	rpc Get		(GetRequest)	returns (GetReply);

	rpc Add		(AddRequest)	returns (AddReply);

	rpc Update	(UpdateRequest) returns (UpdateReply);

	rpc Delete	(DeleteRequest) returns (DeleteReply);

}



message GetRequest {

	string key = 1;

}



message GetReply {

	bool result = 1;

	string key = 2;

	string value = 3;

}



message AddRequest {

	string key = 1;

	string value = 2;

}



message AddReply {

	bool result = 1;

}



message UpdateRequest {

	string key = 1;

	string value = 2;

}



message UpdateReply {

	bool result = 1;

}



message DeleteRequest {

	string key = 1;

}



message DeleteReply {

	bool result = 1;

}









Milad Ghafoori
11:31 AM

using Grpc.Core;

using GrpcService.Interfaces;

using System.Threading.Tasks;



namespace GrpcService.Services

{

  public class CacheGrpcService : CacheServiceProvider.CacheServiceProviderBase

  {

    private readonly ICache _cacheService;



    public CacheGrpcService(ICache cacheService) 

      => _cacheService = cacheService;



    public override Task<AddReply> Add(AddRequest request, ServerCallContext context)

    {

      var addResult = _cacheService.Add(request.Key, request.Value);

      var addReply = new AddReply { Result = addResult };

      var taskResult = Task.FromResult(addReply);

      return taskResult;

    }



    public override Task<DeleteReply> Delete(DeleteRequest request, ServerCallContext context)

    {

      var deleteResult = _cacheService.Delete(request.Key);

      var deleteReply = new DeleteReply { Result = deleteResult };

      var taskResult = Task.FromResult(deleteReply);

      return taskResult;

    }



    public override Task<GetReply> Get(GetRequest request, ServerCallContext context)

    {

      var getResult = _cacheService.Get(request.Key);

      var getReply = new GetReply { Result = getResult != null, Key = request.Key, Value = getResult };

      var taskResult = Task.FromResult(getReply);

      return taskResult;

    }



    public override Task<UpdateReply> Update(UpdateRequest request, ServerCallContext context)

    {

      var updateResult = _cacheService.Update(request.Key, request.Value);

      var updateReply = new UpdateReply { Result = updateResult };

      var taskResult = Task.FromResult(updateReply);

      return taskResult;

    }

  }

}


monday

Milad Ghafoori
6:25 PM

using Grpc.Net.Client;

using Microsoft.Extensions.Logging;

using System;

using System.Threading.Tasks;

using WebApi.Clients;

using static WebApi.Clients.CacheServiceProvider;



namespace WebApi.Services

{

  public class CacheGrpcClientService

  {



    private readonly CacheServiceProviderClient _client;

    private readonly ILogger<CacheGrpcClientService> _logger;



    public CacheGrpcClientService(ILogger<CacheGrpcClientService> logger)

    {

      var grpcChannel = GrpcChannel.ForAddress("localhost:5000");

      _client = new CacheServiceProvider.CacheServiceProviderClient(grpcChannel);

      _logger = logger;

    }



    public async Task AddAsync()

    {

      var addRequest = new AddRequest { Key = "City", Value = "Toronto" };

      var addReply = await _client.AddAsync(addRequest);

      _logger.LogInformation($"Add result: {addReply.Result}");

    }



    public async Task GetAsync()

    {

      var getRequest = new GetRequest { Key = "City" };

      var getReply = await _client.GetAsync(getRequest);

      _logger.LogInformation($"Get result: {getReply.Result}, Key: {getReply.Key}, Value: {getReply.Value}");

    }

  }

}

6:26 PM
namespace WebApi.Model

{

  public class GetResponseModel

  {

    public bool Result { get; set; }

    public string Key { get; set; }

    public string Value { get; set; }

  }

}


//


Milad Ghafoori
6:33 PM

using Grpc.Net.Client;

using Microsoft.Extensions.Logging;

using System.Threading.Tasks;

using WebApi.Clients;

using WebApi.Model;

using static WebApi.Clients.CacheServiceProvider;



namespace WebApi.Services

{

  public class CacheGrpcClientService

  {



    private readonly CacheServiceProviderClient _client;

    private readonly ILogger<CacheGrpcClientService> _logger;



    public CacheGrpcClientService(ILogger<CacheGrpcClientService> logger)

    {

      var grpcChannel = GrpcChannel.ForAddress("localhost:5000");

      _client = new CacheServiceProviderClient(grpcChannel);

      _logger = logger;

    }



    public async Task<bool> AddAsync(string key, string value)

    {

      var addRequest = new AddRequest { Key = key, Value = value };

      var addReply = await _client.AddAsync(addRequest);

      _logger.LogInformation($"Add result: {addReply.Result}");

      return addReply.Result;

    }



    public async Task<GetResponseModel> GetAsync(string key)

    {

      var getRequest = new GetRequest { Key = key };

      var getReply = await _client.GetAsync(getRequest);

      _logger.LogInformation($"Get result: {getReply.Result}, Key: {getReply.Key}, Value: {getReply.Value}");

      var model = new GetResponseModel

      {

        Result = getReply.Result,

        Key = getReply.Key,

        Value = getReply.Value

      };

      return model;

    }

  }

}


:39 PM
namespace WebApi.Model

{

  public class AddModel

  {

    public string Key { get; set; }

    public string Value { get; set; }

  }

}