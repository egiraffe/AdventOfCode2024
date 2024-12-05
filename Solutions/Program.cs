﻿using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Solutions;

var services = new ServiceCollection();
using var loggerFactory = LoggerFactory.Create(b => b.AddConsole());
var logger = loggerFactory.CreateLogger<Program>();

foreach (var type in Assembly.GetExecutingAssembly().GetTypes()
             .Where(t => typeof(ISolution).IsAssignableFrom(t) && !t.IsInterface))
{
    services.AddSingleton(typeof(ISolution), type);
}

services.AddHttpClient();

await using var sp = services.BuildServiceProvider();
var solutions = sp.GetServices<ISolution>().OrderBy(o => o.GetType().Name);
foreach (var solution in solutions)
{
    try
    {
        logger.LogInformation(@"{solutionName}: {solutionAnswer}", solution.GetType().Name, await solution.ExecuteAsync());
    }
    catch (Exception e)
    {
        logger.LogError(@"{solutionName}: {@ex}", solution.GetType().Name, e);
    }
    
}