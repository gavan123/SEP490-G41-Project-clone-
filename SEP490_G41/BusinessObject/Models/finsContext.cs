using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace BusinessObject.Models
{
    public partial class finsContext : DbContext
    {
        public finsContext()
        {
        }

        public finsContext(DbContextOptions<finsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Building> Buildings { get; set; } = null!;
        public virtual DbSet<Facility> Facilities { get; set; } = null!;
        public virtual DbSet<Floor> Floors { get; set; } = null!;
        public virtual DbSet<Map> Maps { get; set; } = null!;
        public virtual DbSet<Mapmanage> Mapmanages { get; set; } = null!;
        public virtual DbSet<Mappoint> Mappoints { get; set; } = null!;
        public virtual DbSet<Mappointroute> Mappointroutes { get; set; } = null!;
        public virtual DbSet<Member> Members { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Route> Routes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                IConfigurationRoot configuration = builder.Build();

                optionsBuilder.UseMySql(configuration.GetConnectionString("Project"), ServerVersion.AutoDetect(configuration.GetConnectionString("Project")),
                    mysqlOptions => mysqlOptions.UseNetTopologySuite()); // Enable NetTopologySuite for MySQL
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Building>(entity =>
            {
                entity.ToTable("building");

                entity.HasIndex(e => e.FacilityId, "FK_Facility_Building_idx");

                entity.Property(e => e.BuildingName)
                    .HasMaxLength(50)
                    .UseCollation("utf8mb3_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.Status).HasColumnType("enum('active','deactive')");

                entity.HasOne(d => d.Facility)
                    .WithMany(p => p.Buildings)
                    .HasForeignKey(d => d.FacilityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Facility_Building");
            });

            modelBuilder.Entity<Facility>(entity =>
            {
                entity.ToTable("facilities");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .UseCollation("utf8mb3_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.FacilityName)
                    .HasMaxLength(100)
                    .UseCollation("utf8mb3_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.Status).HasColumnType("enum('active','deactive')");
            });

            modelBuilder.Entity<Floor>(entity =>
            {
                entity.ToTable("floor");

                entity.HasIndex(e => e.BuildingId, "FK_Building_Floor_idx");

                entity.Property(e => e.FloorName)
                    .HasMaxLength(50)
                    .UseCollation("utf8mb3_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.Greeting).HasMaxLength(100);

                entity.Property(e => e.Status).HasColumnType("enum('active','deactive')");

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.Floors)
                    .HasForeignKey(d => d.BuildingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Building_Floor");
            });

            modelBuilder.Entity<Map>(entity =>
            {
                entity.ToTable("map");

                entity.HasIndex(e => e.FloorId, "FK_Floor_Map_idx");

                entity.Property(e => e.MapName).HasMaxLength(50);

                entity.HasOne(d => d.Floor)
                    .WithMany(p => p.Maps)
                    .HasForeignKey(d => d.FloorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Floor_Map");
            });

            modelBuilder.Entity<Mapmanage>(entity =>
            {
                entity.HasKey(e => new { e.MapId, e.MemberId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("mapmanage");

                entity.HasIndex(e => e.MapId, "FK_Map_MapManage_idx");

                entity.HasIndex(e => e.MemberId, "FK_Member_MapManage_idx");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.Map)
                    .WithMany(p => p.Mapmanages)
                    .HasForeignKey(d => d.MapId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Map_MapManage");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Mapmanages)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Member_MapManage");
            });

            modelBuilder.Entity<Mappoint>(entity =>
            {
                entity.ToTable("mappoint");

                entity.HasIndex(e => e.BuildingId, "FK_Building_BuildingId_idx");

                entity.HasIndex(e => e.FloorId, "FK_Floor_FloorId_idx");

                entity.HasIndex(e => e.MapId, "FK_Map_MapPoint");

                entity.HasIndex(e => e.MapPointId, "FK_Map_MapPoint_idx");

                entity.Property(e => e.Image).HasMaxLength(100);

                entity.Property(e => e.LocationGps).HasColumnName("LocationGPS");

                entity.Property(e => e.MappointName)
                    .HasMaxLength(50)
                    .UseCollation("utf8mb3_general_ci")
                    .HasCharSet("utf8mb3");

                entity.HasOne(d => d.Map)
                    .WithMany(p => p.Mappoints)
                    .HasForeignKey(d => d.MapId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Map_MapPoint");
            });

            modelBuilder.Entity<Mappointroute>(entity =>
            {
                entity.HasKey(e => e.MprId)
                    .HasName("PRIMARY");

                entity.ToTable("mappointroute");

                entity.HasIndex(e => e.MapPointId, "FK_MapPoint_MR_idx");

                entity.HasIndex(e => e.RouteId, "FK_Route_MPR_idx");

                entity.Property(e => e.MprId).HasColumnName("MPR_ID");

                entity.Property(e => e.MapPointId).HasColumnName("MapPointID");

                entity.Property(e => e.RouteId).HasColumnName("RouteID");

                entity.HasOne(d => d.MapPoint)
                    .WithMany(p => p.Mappointroutes)
                    .HasForeignKey(d => d.MapPointId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MapPoint_MR");

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.Mappointroutes)
                    .HasForeignKey(d => d.RouteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Route_MR");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("member");

                entity.HasIndex(e => e.RoleId, "FK_Role_Member_idx");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.Avatar).HasMaxLength(45);

                entity.Property(e => e.Country).HasMaxLength(45);

                entity.Property(e => e.DoB).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(1000);

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsFixedLength()
                    .UseCollation("utf8mb3_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.Status).HasColumnType("enum('active','deactive')");

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Role_Member");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Description).HasColumnType("mediumtext");

                entity.Property(e => e.RoleName).HasMaxLength(45);
            });

            modelBuilder.Entity<Route>(entity =>
            {
                entity.ToTable("routes");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.RouteName).HasMaxLength(45);

                entity.Property(e => e.StartTime).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
