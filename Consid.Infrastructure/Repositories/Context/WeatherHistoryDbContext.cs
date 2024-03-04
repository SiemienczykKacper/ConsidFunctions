using Consid.Domain.Entities;
using Consid.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consid.Infrastructure.Repositories.Context;

public class WeatherHistoryDbContext : DbContext
{
    private readonly ILogger<WeatherHistoryDbContext> _logger;
    private readonly IConfiguration _configuration;

    public WeatherHistoryDbContext(ILogger<WeatherHistoryDbContext> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public DbSet<City> Cities { get; set; }
    public DbSet<WeatherHistory> WeatherHistories { get; set; }

    public DbSet<CountryMinTempMaxWindSpeed> CountryMinTempMaxWindSpeeds { get; set; }

    public DbSet<CountryMaxWindSpeed> CountryMaxWindSpeeds { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.GetConnectionString("WeatherHistoryDbConnectionString");
        optionsBuilder.UseSqlServer(connectionString,
            sqlServerOptionsAction: sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 2,
                maxRetryDelay: TimeSpan.FromSeconds(3),
                errorNumbersToAdd: null);
            }
            );        
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CountryMaxWindSpeed>(entity =>
        {
            entity.HasNoKey();           
        });

        modelBuilder.Entity<CountryMinTempMaxWindSpeed>(entity =>
        {
            entity.HasNoKey();
        });        

        modelBuilder.Entity<City>(entity =>
        {
            entity.ToTable("City");
            entity.HasKey(e => e.CityId);
            entity.Property(e => e.CityName).IsRequired().HasMaxLength(128);
            entity.Property(e => e.Country).IsRequired().HasMaxLength(128);
            entity.Property(e => e.Latitude).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.Longitude).HasColumnType("decimal(9, 6)");
        });

        modelBuilder.Entity<WeatherHistory>(entity =>
        {
            entity.ToTable("WeatherHistory");
            entity.HasKey(e => e.WeatherDataId);
            entity.Property(e => e.Time).IsRequired();
            entity.Property(e => e.Temperature).HasColumnType("decimal(6, 3)");
            entity.Property(e => e.CloudCoverage).HasColumnType("decimal(6, 3)");
            entity.Property(e => e.WindSpeed).HasColumnType("decimal(6, 3)");
            
            entity.HasOne(w => w.City)
                  .WithMany(c => c.WeatherHistories)
                  .HasForeignKey(w => w.CityId)
                  .OnDelete(DeleteBehavior.Restrict); 
        });
    }
}

