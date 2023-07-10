using System.Data;
using System.Globalization;
using Dapper;
using electric_report.Models;
using Microsoft.AspNetCore.Mvc;

namespace electric_report;

public static class Endpoints
{
    public static void MapEndpoints(this WebApplication application)
    {
        application.MapGet("/api/consumption/history/byline",
            async (string startDate, string endDate, [FromServices] IDbConnection db) =>
            {
                // Check if date has format yyyy-MM-dd
                if (!DateTime.TryParseExact(startDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None,
                        out var start))
                {
                    return Results.BadRequest("Invalid start date");
                }

                if (!DateTime.TryParseExact(endDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None,
                        out var end))
                {
                    return Results.BadRequest("Invalid end date");
                }

                var history = await db.QueryAsync<ConsumptionHistoryByLine>(
                    "GetConsumptionHistoryByLine",
                    new {startDate, endDate},
                    commandType: CommandType.StoredProcedure
                );

                return Results.Ok(history);
            });

        application.MapGet("/api/consumption/history/bycustomer",
            async (string startDate, string endDate, [FromServices] IDbConnection db) =>
            {
                // Check if date has format yyyy-MM-dd
                if (!DateTime.TryParseExact(startDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None,
                        out var start))
                {
                    return Results.BadRequest("Invalid start date");
                }

                if (!DateTime.TryParseExact(endDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None,
                        out var end))
                {
                    return Results.BadRequest("Invalid end date");
                }

                var history = await db.QueryAsync<ConsumptionHistoryByCustomerType>(
                    "GetConsumptionHistoryByCustomerType",
                    new {startDate, endDate},
                    commandType: CommandType.StoredProcedure
                );

                return Results.Ok(history);
            });

        application.MapGet("/api/worstlines/bycustomer",
            async (string startDate, string endDate, [FromServices] IDbConnection db) =>
            {
                // Check if date has format yyyy-MM-dd
                if (!DateTime.TryParseExact(startDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None,
                        out var start))
                {
                    return Results.BadRequest("Invalid start date");
                }

                if (!DateTime.TryParseExact(endDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None,
                        out var end))
                {
                    return Results.BadRequest("Invalid end date");
                }

                var history = await db.QueryAsync<TopWorstLinesByCustomer>(
                    "GetTop20WorstLinesByCustomer",
                    new {startDate, endDate},
                    commandType: CommandType.StoredProcedure
                );

                return Results.Ok(history);
            });
    }
}
