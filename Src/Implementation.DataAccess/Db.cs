using Core;
using Implementation.DataAccess.DataModel;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Implementation.DataAccess;

public sealed class Db : DbContext
{
    private static readonly JsonSerializerSettings SerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All,
        TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
    };

    private readonly string _connectionString;

    public Db(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DbSet<FigureInfo> FigureInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FigureInfo>()
            .Property(i => i.Figure)
            .HasConversion(v => JsonConvert.SerializeObject(v, SerializerSettings),
                s => JsonConvert.DeserializeObject<IFigure>(s, SerializerSettings));
    }
}