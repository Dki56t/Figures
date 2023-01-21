using Core;
using Implementation.DataAccess.DataModel;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Implementation.DataAccess
{
    public sealed class Db : DbContext
    {
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
        };

        public DbSet<FigureInfo> FigureInfos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // TODO extract as appsetting
            options.UseSqlite("Data Source=figures.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FigureInfo>()
                .Property(i => i.Figure)
                .HasConversion(v => JsonConvert.SerializeObject(v, SerializerSettings),
                    s => JsonConvert.DeserializeObject<IFigure>(s, SerializerSettings));
        }
    }
}