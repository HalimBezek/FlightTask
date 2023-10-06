﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FlightTaskProject.Models;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Flight> Flights { get; set; }

    public virtual DbSet<Route> Routes { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost; Database=postgres; Username=postgres;Password=1234;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pg_catalog", "adminpack");

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.FlightId).HasName("flights_pk");

            entity.ToTable("flights");

            entity.Property(e => e.FlightId)
                .ValueGeneratedNever()
                .HasColumnName("flight_id");
            entity.Property(e => e.AirlineId).HasColumnName("airline_id");
            entity.Property(e => e.ArrivalTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("arrival_time");
            entity.Property(e => e.DepartureTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("departure_time");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.RouteId).HasColumnName("route_id");

            entity.HasOne(d => d.Route).WithMany(p => p.Flights)
                .HasForeignKey(d => d.RouteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("flights_router_fk");
        });

        modelBuilder.Entity<Route>(entity =>
        {
            entity.HasKey(e => e.RouteId).HasName("routes_pk");

            entity.ToTable("routes");

            entity.HasIndex(e => e.DepartureDate, "routes_departure_date_idx");

            entity.HasIndex(e => e.DestinationCityId, "routes_destination_city_id_idx");

            entity.HasIndex(e => e.OriginCityId, "routes_origin_city_id_idx");

            entity.HasIndex(e => e.RouteId, "routes_route_id_idx");

            entity.Property(e => e.RouteId)
                .ValueGeneratedNever()
                .HasColumnName("route_id");
            entity.Property(e => e.DepartureDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("departure_date");
            entity.Property(e => e.DestinationCityId).HasColumnName("destination_city_id");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.OriginCityId).HasColumnName("origin_city_id");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("subscriptions_pk");

            entity.ToTable("subscriptions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AgencyId).HasColumnName("agency_id");
            entity.Property(e => e.DestinationCityId).HasColumnName("destination_city_id");
            entity.Property(e => e.OriginCityId).HasColumnName("origin_city_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}