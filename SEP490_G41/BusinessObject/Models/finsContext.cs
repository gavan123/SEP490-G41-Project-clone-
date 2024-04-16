﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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

<<<<<<< HEAD
        public virtual DbSet<Building> Buildings { get; set; }
        public virtual DbSet<Facility> Facilities { get; set; }
        public virtual DbSet<Floor> Floors { get; set; }
        public virtual DbSet<Map> Maps { get; set; }
        public virtual DbSet<Mapmanage> Mapmanages { get; set; }
        public virtual DbSet<Mappoint> Mappoints { get; set; }
        public virtual DbSet<Mappointroute> Mappointroutes { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
=======
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
>>>>>>> main

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
<<<<<<< HEAD
                optionsBuilder.UseMySql("server=localhost;database=fins;uid=root;pwd=12345", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.33-mysql"), x => x.UseNetTopologySuite());
=======
                optionsBuilder.UseMySQL("Server=localhost;Database=fins;User=root;Password=Klmnop123#;");
>>>>>>> main
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Building>(entity =>
            {
                entity.ToTable("building");

                entity.HasIndex(e => e.FacilityId, "FK_Facility_Building_idx");

<<<<<<< HEAD
                entity.Property(e => e.BuildingName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .UseCollation("utf8mb3_general_ci")
                    .HasCharSet("utf8mb3");
=======
                entity.Property(e => e.BuildingName).HasMaxLength(50);
>>>>>>> main

                entity.Property(e => e.Image).IsRequired();

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnType("enum('active','deactive')");

                entity.HasOne(d => d.Facility)
                    .WithMany(p => p.Buildings)
                    .HasForeignKey(d => d.FacilityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Facility_Building");
            });

            modelBuilder.Entity<Facility>(entity =>
            {
                entity.ToTable("facilities");

<<<<<<< HEAD
                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("utf8mb3_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.FacilityName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .UseCollation("utf8mb3_general_ci")
                    .HasCharSet("utf8mb3");
=======
                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.FacilityName).HasMaxLength(100);
>>>>>>> main

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnType("enum('active','deactive')");
            });

            modelBuilder.Entity<Floor>(entity =>
            {
                entity.ToTable("floor");

                entity.HasIndex(e => e.BuildingId, "FK_Building_Floor_idx");

<<<<<<< HEAD
                entity.Property(e => e.FloorName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .UseCollation("utf8mb3_general_ci")
                    .HasCharSet("utf8mb3");
=======
                entity.Property(e => e.FloorName).HasMaxLength(50);
>>>>>>> main

                entity.Property(e => e.Greeting)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnType("enum('active','deactive')");

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

                entity.Property(e => e.Image2D).IsRequired();

                entity.Property(e => e.MapName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Floor)
                    .WithMany(p => p.Maps)
                    .HasForeignKey(d => d.FloorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Floor_Map");
            });

            modelBuilder.Entity<Mapmanage>(entity =>
            {
                entity.HasKey(e => new { e.MapId, e.MemberId })
                    .HasName("PRIMARY");

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

<<<<<<< HEAD
                entity.Property(e => e.LocationGps).HasColumnName("LocationGPS");

                entity.Property(e => e.LocationWeb).IsRequired();

                entity.Property(e => e.MappointName)
                    .HasMaxLength(50)
                    .UseCollation("utf8mb3_general_ci")
                    .HasCharSet("utf8mb3");
=======
                entity.Property(e => e.MappointName).HasMaxLength(50);
>>>>>>> main

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

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Avatar).HasMaxLength(45);
<<<<<<< HEAD
=======

                entity.Property(e => e.Country).HasMaxLength(45);

                entity.Property(e => e.DoB).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(50);
>>>>>>> main

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(45);

<<<<<<< HEAD
                entity.Property(e => e.DoB).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(1000);
=======
                entity.Property(e => e.Password).HasMaxLength(1000);
>>>>>>> main

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnType("enum('active','deactive')");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Role_Member");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("mediumtext");

<<<<<<< HEAD
                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(45);
=======
                entity.Property(e => e.RoleName).HasMaxLength(45);
>>>>>>> main
            });

            modelBuilder.Entity<Route>(entity =>
            {
                entity.ToTable("routes");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

<<<<<<< HEAD
                entity.Property(e => e.RouteName)
                    .IsRequired()
                    .HasMaxLength(45);
=======
                entity.Property(e => e.RouteName).HasMaxLength(45);
>>>>>>> main

                entity.Property(e => e.StartTime).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
