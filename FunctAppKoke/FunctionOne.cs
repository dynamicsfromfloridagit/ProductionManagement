using FunctAppKoke.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace FunctAppKoke;

public class FunctionOne
{
    private readonly ILogger<FunctionOne> _logger;
    private readonly ApplicationDbContext _dbContext;

    public FunctionOne(
        ILogger<FunctionOne> logger,
        ApplicationDbContext dbContext
        
        )
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [Function("FunctionOne")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        _logger.LogInformation($"requested ID: {req.Query["id"]}");
        var str = req.Query["id"];
        try
        {
            if (!StringValues.IsNullOrEmpty(str))
            {
                if (int.TryParse(str, out int identification))
                {
                    _logger.LogInformation($"requested integer id is: {identification}");

                    var entity = await _dbContext.Jobskus.FindAsync(identification);
                    if (entity != null)
                    {
                        _dbContext.Jobskus.Remove(entity);
                        await _dbContext.SaveChangesAsync();
                    }
                }
                else
                {
                    _logger.LogWarning($"Invalid id format: {str}");
                }


            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error parsing id: {ex.Message}");
        }
        
        return new OkObjectResult("Welcome to Azure Function The ONE!!");
    }
}