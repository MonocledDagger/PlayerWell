using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AgileTeamFour.PL;

public partial class AgileTeamFourEntities : DbContext
{
    public AgileTeamFourEntities()
    {
    }

    public AgileTeamFourEntities(DbContextOptions<AgileTeamFourEntities> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<PlayerEvent> PlayerEvents { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=AgileTeamFour.DB;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentID).HasName("PK__Comments__C3B4DFAABFBFACEF");

            entity.Property(e => e.CommentID).ValueGeneratedNever();
            entity.Property(e => e.AuthorName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Text).HasColumnType("text");
            entity.Property(e => e.TimePosted).HasColumnType("datetime");

            entity.HasOne(d => d.Author).WithMany(p => p.Comments)
                .HasForeignKey(d => d.AuthorID)
                .HasConstraintName("FK__Comments__Author__2D27B809");

            entity.HasOne(d => d.Event).WithMany(p => p.Comments)
                .HasForeignKey(d => d.EventID)
                .HasConstraintName("FK__Comments__EventI__2C3393D0");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventID).HasName("PK__Events__7944C8701E12B890");

            entity.Property(e => e.EventID).ValueGeneratedNever();
            entity.Property(e => e.DateTime).HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.EventName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EventType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Platform)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Server)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Game).WithMany(p => p.Events)
                .HasForeignKey(d => d.GameID)
                .HasConstraintName("FK__Events__GameID__2E1BDC42");
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.GameID).HasName("PK__Games__2AB897DD19D6D8B7");

            entity.Property(e => e.GameID).ValueGeneratedNever();
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.GameName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Genre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Picture)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Platform)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.PlayerID).HasName("PK__Players__4A4E74A8D48316AB");

            entity.Property(e => e.PlayerID).ValueGeneratedNever();
            entity.Property(e => e.Bio).HasColumnType("text");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IconPic)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PlayerEvent>(entity =>
        {
            entity.HasKey(e => e.PlayerEventID).HasName("PK__PlayerEv__B001D167F543EF19");

            entity.ToTable("PlayerEvent");

            entity.Property(e => e.PlayerEventID).ValueGeneratedNever();
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Event).WithMany(p => p.PlayerEvents)
                .HasForeignKey(d => d.EventID)
                .HasConstraintName("FK__PlayerEve__Event__300424B4");

            entity.HasOne(d => d.Player).WithMany(p => p.PlayerEvents)
                .HasForeignKey(d => d.PlayerID)
                .HasConstraintName("FK__PlayerEve__Playe__2F10007B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
