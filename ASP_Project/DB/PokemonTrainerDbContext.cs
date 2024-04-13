using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ASP_Project;

public partial class PokemonTrainerDbContext : DbContext
{
    public PokemonTrainerDbContext()
    {
    }

    public PokemonTrainerDbContext(DbContextOptions<PokemonTrainerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Pokemon> Pokemons { get; set; }

    public virtual DbSet<PokemonAbility> PokemonAbilities { get; set; }

    public virtual DbSet<PokemonHeldItem> PokemonHeldItems { get; set; }

    public virtual DbSet<PokemonMove> PokemonMoves { get; set; }

    public virtual DbSet<PokemonMoveSet> PokemonMoveSets { get; set; }

    public virtual DbSet<PokemonSprite> PokemonSprites { get; set; }

    public virtual DbSet<PokemonStat> PokemonStats { get; set; }

    public virtual DbSet<PokemonTeam> PokemonTeams { get; set; }

    public virtual DbSet<PokemonType> PokemonTypes { get; set; }

    public virtual DbSet<Trainer> Trainers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(File.ReadAllText(Globals.SQLSERVERCREDENTIALS));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pokemon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pokemon__3214EC27E8401125");

            entity.ToTable("Pokemon");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ChosenAbility).HasMaxLength(50);
            entity.Property(e => e.ChosenTeraType).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.TeamId).HasColumnName("TeamID");

            entity.HasOne(d => d.Team).WithMany(p => p.Pokemons)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pokemon__TeamID__06CD04F7");
        });

        modelBuilder.Entity<PokemonAbility>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PokemonA__3214EC27B1C24632");

            entity.ToTable("PokemonAbility");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PokemonId).HasColumnName("PokemonID");
            entity.Property(e => e.Url)
                .HasMaxLength(50)
                .HasColumnName("URL");

            entity.HasOne(d => d.Pokemon).WithMany(p => p.PokemonAbilities)
                .HasForeignKey(d => d.PokemonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PokemonID_Ability");
        });

        modelBuilder.Entity<PokemonHeldItem>(entity =>
        {
            entity.HasKey(e => e.PokemonId);

            entity.ToTable("PokemonHeldItem");

            entity.Property(e => e.PokemonId)
                .ValueGeneratedNever()
                .HasColumnName("PokemonID");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Url)
                .HasMaxLength(50)
                .HasColumnName("URL");

            entity.HasOne(d => d.Pokemon).WithOne(p => p.PokemonHeldItem)
                .HasForeignKey<PokemonHeldItem>(d => d.PokemonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PokemonID_Item");
        });

        modelBuilder.Entity<PokemonMove>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PokemonM__3214EC27777892D0");

            entity.ToTable("PokemonMove");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PokemonId).HasColumnName("PokemonID");
            entity.Property(e => e.Url)
                .HasMaxLength(50)
                .HasColumnName("URL");

            entity.HasOne(d => d.Pokemon).WithMany(p => p.PokemonMoves)
                .HasForeignKey(d => d.PokemonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PokemonID_Move");
        });

        modelBuilder.Entity<PokemonMoveSet>(entity =>
        {
            entity.HasKey(e => e.PokemonId).HasName("PK_MoveSet");

            entity.ToTable("PokemonMoveSet");

            entity.Property(e => e.PokemonId)
                .ValueGeneratedNever()
                .HasColumnName("PokemonID");
            entity.Property(e => e.Move)
                .HasMaxLength(50)
                .HasColumnName("move_#");
            entity.Property(e => e.Move1)
                .HasMaxLength(50)
                .HasColumnName("move_1");
            entity.Property(e => e.Move2)
                .HasMaxLength(50)
                .HasColumnName("move_2");
            entity.Property(e => e.Move3)
                .HasMaxLength(50)
                .HasColumnName("move_3");

            entity.HasOne(d => d.Pokemon).WithOne(p => p.PokemonMoveSet)
                .HasForeignKey<PokemonMoveSet>(d => d.PokemonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PokemonID_MoveSet");
        });

        modelBuilder.Entity<PokemonSprite>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PokemonSprite");

            entity.Property(e => e.FrontDefault).HasMaxLength(50);
            entity.Property(e => e.FrontFemale).HasMaxLength(50);
            entity.Property(e => e.FrontShiny).HasMaxLength(50);
            entity.Property(e => e.FrontShinyFemale).HasMaxLength(50);
            entity.Property(e => e.PokemonId).HasColumnName("PokemonID");

            entity.HasOne(d => d.Pokemon).WithMany()
                .HasForeignKey(d => d.PokemonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PokemonID_Sprite");
        });

        modelBuilder.Entity<PokemonStat>(entity =>
        {
            entity.ToTable("PokemonStat");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PokemonId).HasColumnName("PokemonID");
            entity.Property(e => e.Url)
                .HasMaxLength(50)
                .HasColumnName("URL");

            entity.HasOne(d => d.Pokemon).WithMany(p => p.PokemonStats)
                .HasForeignKey(d => d.PokemonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PokemonID_Stat");
        });

        modelBuilder.Entity<PokemonTeam>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PokemonT__3214EC27AE892B21");

            entity.ToTable("PokemonTeam");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
            entity.Property(e => e.TrainerId).HasColumnName("TrainerID");

            entity.HasOne(d => d.Trainer).WithMany(p => p.PokemonTeams)
                .HasForeignKey(d => d.TrainerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TrainerID_Team");
        });

        modelBuilder.Entity<PokemonType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PokemonT__3214EC27461BAB9A");

            entity.ToTable("PokemonType");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.PokemonId).HasColumnName("PokemonID");

            entity.HasOne(d => d.Pokemon).WithMany(p => p.PokemonTypes)
                .HasForeignKey(d => d.PokemonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PokemonID_Type");
        });

        modelBuilder.Entity<Trainer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Trainer__3214EC2797527C1D");

            entity.ToTable("Trainer");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
