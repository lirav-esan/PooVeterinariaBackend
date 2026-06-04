using Dapper;
using SmallChangeDAW.CORE.Core.Interfaces;
using SmallChangeDAW.CORE.Infrastructure.Data;
using SmallChangeDAW.CORE.Models;

namespace SmallChangeDAW.CORE.Infrastructure.Repositories;

public class OfertasRepository : IOfertasRepository
{
    private readonly DbConnectionFactory _connectionFactory;

    public OfertasRepository(DbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Oferta>> GetAllAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<Oferta>("SELECT * FROM Ofertas");
    }

    public async Task<Oferta?> GetByIdAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Oferta>(
            "SELECT * FROM Ofertas WHERE id = @Id", new { Id = id });
    }

    public async Task<int> AddAsync(Oferta oferta)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"INSERT INTO Ofertas (cliente_id, moneda_a_enviar, moneda_a_recibir, tipo_cambio)
                    VALUES (@ClienteId, @MonedaAEnviar, @MonedaARecibir, @TipoCambio);
                    SELECT CAST(SCOPE_IDENTITY() AS INT);";
        return await connection.QuerySingleAsync<int>(sql, oferta);
    }

    public async Task<bool> UpdateAsync(Oferta oferta)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"UPDATE Ofertas SET
                    cliente_id = @cliente_id,
                    moneda_a_enviar = @moneda_a_enviar,
                    moneda_a_recibir = @moneda_a_recibir,
                    tipo_cambio = @tipo_cambio,
                    estado = @estado
                WHERE id = @id";

        var rowsAffected = await connection.ExecuteAsync(sql, oferta);
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "DELETE FROM Ofertas WHERE id = @Id";
        var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });
        return rowsAffected > 0;
    }
}
