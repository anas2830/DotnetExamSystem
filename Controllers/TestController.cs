using Microsoft.AspNetCore.Mvc;
using DotnetExamSystem.Api.DataAccessLayer;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotnetExamSystem.Api.Controllers;

[ApiController]
[Route("api/test")]
public class TestController : ControllerBase
{
    private readonly MongoDbContext _context;

    public TestController(MongoDbContext context)
    {
        _context = context;
    }

    [HttpGet("ping")]
    public async Task<IActionResult> Ping()
    {
        try
        {
            // Cursor from MongoDB
            var cursor = await _context.Database.ListCollectionNamesAsync();
            var collections = new List<string>();

            // Manual async iteration
            while (await cursor.MoveNextAsync())
            {
                foreach (var name in cursor.Current)
                {
                    collections.Add(name);
                }
            }

            return Ok(new
            {
                message = "MongoDB Connected Successfully!",
                collections = collections
            });
        }
        catch (System.Exception ex)
        {
            return BadRequest(new
            {
                message = "MongoDB Connection Failed",
                error = ex.Message
            });
        }
    }
}
