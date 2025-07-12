using DexApi.Parsers;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DexApi.Services;

public class DexService
{
    private readonly IConfiguration _config;

    public DexService(IConfiguration config)
    {
        _config = config;
    }

    /// <summary>
    /// Processes a DEX file by parsing its contents and saving the data to the database.
    /// </summary>
    /// <param name="dexText">Raw text content of the DEX file</param>
    /// <param name="machine">Machine identifier ("A" or "B")</param>
    public async Task ProcessDexAsync(string dexText, string machine)
    {
        DexParser parser = new();
        var dexMeter = parser.ParseDexMeter(dexText, machine);
        var laneMeters = parser.ParseLaneMeters(dexText);

        using var conn = new SqlConnection(_config.GetConnectionString("DexDb"));
        await conn.OpenAsync();

        int meterId;

        // Save DEXMeter using strongly-typed parameters
        using (var cmd = new SqlCommand("SaveDEXMeter", conn))
        {
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Machine", SqlDbType.Char, 1).Value = dexMeter.Machine;
            cmd.Parameters.Add("@DEXDateTime", SqlDbType.DateTime).Value = dexMeter.DEXDateTime;
            cmd.Parameters.Add("@MachineSerialNumber", SqlDbType.VarChar, 50).Value =
                dexMeter.MachineSerialNumber ?? (object)DBNull.Value;
            cmd.Parameters.Add("@ValueOfPaidVends", SqlDbType.Decimal).Value = dexMeter.ValueOfPaidVends;

            meterId = Convert.ToInt32(await cmd.ExecuteScalarAsync());
        }

        // Reuse SqlCommand instance for saving lane meters
        using var laneCmd = new SqlCommand("SaveDEXLaneMeter", conn)
        {
            CommandType = CommandType.StoredProcedure
        };

        laneCmd.Parameters.Add("@DexMeterId", SqlDbType.Int);
        laneCmd.Parameters.Add("@ProductIdentifier", SqlDbType.VarChar, 20);
        laneCmd.Parameters.Add("@Price", SqlDbType.Decimal).Value = 0;
        laneCmd.Parameters["@Price"].Precision = 10;
        laneCmd.Parameters["@Price"].Scale = 2;
        laneCmd.Parameters.Add("@NumberOfVends", SqlDbType.Int);
        laneCmd.Parameters.Add("@ValueOfPaidSales", SqlDbType.Decimal).Value = 0;
        laneCmd.Parameters["@ValueOfPaidSales"].Precision = 10;
        laneCmd.Parameters["@ValueOfPaidSales"].Scale = 2;

        foreach (var lane in laneMeters)
        {
            laneCmd.Parameters["@DexMeterId"].Value = meterId;
            laneCmd.Parameters["@ProductIdentifier"].Value = lane.ProductIdentifier ?? (object)DBNull.Value;
            laneCmd.Parameters["@Price"].Value = lane.Price;
            laneCmd.Parameters["@NumberOfVends"].Value = lane.NumberOfVends;
            laneCmd.Parameters["@ValueOfPaidSales"].Value = lane.ValueOfPaidSales;

            await laneCmd.ExecuteNonQueryAsync();
        }
    }
}
