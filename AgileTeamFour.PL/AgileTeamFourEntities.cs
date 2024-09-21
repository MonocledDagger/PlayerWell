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

    public virtual DbSet<tblComment> tblComments { get; set; }

    public virtual DbSet<tblEvent> tblEvents { get; set; }

    public virtual DbSet<tblGame> tblGames { get; set; }

    public virtual DbSet<tblPlayer> tblPlayers { get; set; }

    public virtual DbSet<tblPlayerEvent> tblPlayerEvents { get; set; }

    public virtual DbSet<tblReview> tblReviews { get; set; }

    public virtual DbSet<tblUser> tblUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => 
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=AgileTeamFour.DB;Integrated Security=True");
    //optionsBuilder.UseSqlServer("Server=server-101521081-300085063.database.windows.net;Database=bigprojectdb;User Id=300085063db;Password=Test123!");
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<tblComment>(entity =>
        {
            entity.HasKey(e => e.CommentID).HasName("PK__tblComme__C3B4DFAAC3D616DC");

            entity.Property(e => e.CommentID).ValueGeneratedNever();
            entity.Property(e => e.Text).HasColumnType("text");
            entity.Property(e => e.TimePosted).HasColumnType("datetime");
        });

        modelBuilder.Entity<tblEvent>(entity =>
        {
            entity.HasKey(e => e.EventID).HasName("PK__tblEvent__7944C8702A81E715");

            entity.Property(e => e.EventID).ValueGeneratedNever();
            entity.Property(e => e.DateTime).HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.EventName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Platform)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Server)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<tblGame>(entity =>
        {
            entity.HasKey(e => e.GameID).HasName("PK__tblGames__2AB897DDAD08C9A6");

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

        modelBuilder.Entity<tblPlayer>(entity =>
        {
            entity.HasKey(e => e.PlayerID).HasName("PK__tblPlaye__4A4E74A89A6E850F");

            entity.Property(e => e.PlayerID).ValueGeneratedNever();
            entity.Property(e => e.Bio).HasColumnType("text");
            entity.Property(e => e.DateTime).HasColumnType("datetime");
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

        modelBuilder.Entity<tblPlayerEvent>(entity =>
        {
            entity.HasKey(e => e.PlayerEventID).HasName("PK__tblPlaye__B001D1674ACDFACE");

            entity.Property(e => e.PlayerEventID).ValueGeneratedNever();
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<tblReview>(entity =>
        {
            entity.HasKey(e => e.ReviewID).HasName("PK__tblRevie__74BC79AE58ED253A");

            entity.Property(e => e.ReviewID).ValueGeneratedNever();
            entity.Property(e => e.DateTime).HasColumnType("datetime");
            entity.Property(e => e.ReviewText).HasColumnType("text");
        });

        modelBuilder.Entity<tblUser>(entity =>
        {
            entity.HasKey(e => e.UserID).HasName("PK__tblUsers__1788CCAC8961CDFC");

            entity.Property(e => e.UserID).ValueGeneratedNever();
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(28)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
