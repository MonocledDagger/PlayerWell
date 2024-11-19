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

    public virtual DbSet<tblFriend> tblFriends { get; set; }

    public virtual DbSet<tblFriendComment> tblFriendComments { get; set; }

    public virtual DbSet<tblGame> tblGames { get; set; }

    public virtual DbSet<tblGuild> tblGuilds { get; set; }

    public virtual DbSet<tblGuildComment> tblGuildComments { get; set; }

    public virtual DbSet<tblPlayerEvent> tblPlayerEvents { get; set; }

    public virtual DbSet<tblPlayerGuild> tblPlayerGuilds { get; set; }

    public virtual DbSet<tblPost> tblPosts { get; set; }

    public virtual DbSet<tblPostComment> tblPostComments { get; set; }

    public virtual DbSet<tblReview> tblReviews { get; set; }

    public virtual DbSet<tblUser> tblUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => //optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=AgileTeamFour.DB;Integrated Security=True");
    optionsBuilder.UseSqlServer("Server=server-101521081-300085063.database.windows.net;Database=bigprojectdb;User Id=300085063db;Password=Test123!");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<tblComment>(entity =>
        {
            entity.HasKey(e => e.CommentID).HasName("PK__tblComme__C3B4DFAA26C84A86");

            entity.Property(e => e.CommentID).ValueGeneratedNever();
            entity.Property(e => e.Text).HasColumnType("text");
            entity.Property(e => e.TimePosted).HasColumnType("datetime");
        });

        modelBuilder.Entity<tblEvent>(entity =>
        {
            entity.HasKey(e => e.EventID).HasName("PK__tblEvent__7944C870901BE220");

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

        modelBuilder.Entity<tblFriend>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__tblFrien__3214EC27A8194990");

            entity.ToTable("tblFriend");

            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<tblFriendComment>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__tblFrien__3214EC27E8795789");

            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.Text).HasColumnType("text");
            entity.Property(e => e.TimePosted).HasColumnType("datetime");
        });

        modelBuilder.Entity<tblGame>(entity =>
        {
            entity.HasKey(e => e.GameID).HasName("PK__tblGames__2AB897DD14D18B50");

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

        modelBuilder.Entity<tblGuild>(entity =>
        {
            entity.HasKey(e => e.GuildId).HasName("PK__tblGuild__3A3F896FAA74AFF4");

            entity.ToTable("tblGuild");

            entity.Property(e => e.GuildId).ValueGeneratedNever();
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.GuildName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<tblGuildComment>(entity =>
        {
            entity.HasKey(e => e.CommentID).HasName("PK__tblGuild__C3B4DFAA0464D866");

            entity.Property(e => e.CommentID).ValueGeneratedNever();
            entity.Property(e => e.Text).HasColumnType("text");
            entity.Property(e => e.TimePosted).HasColumnType("datetime");
        });

        modelBuilder.Entity<tblPlayerEvent>(entity =>
        {
            entity.HasKey(e => e.PlayerEventID).HasName("PK__tblPlaye__B001D16762AEE25B");

            entity.Property(e => e.PlayerEventID).ValueGeneratedNever();
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<tblPlayerGuild>(entity =>
        {
            entity.HasKey(e => e.PlayerGuildID).HasName("PK__tblPlaye__7A1E30061F0C572F");

            entity.ToTable("tblPlayerGuild");

            entity.Property(e => e.PlayerGuildID).ValueGeneratedNever();
            entity.Property(e => e.JoinDate).HasColumnType("datetime");
            entity.Property(e => e.Role)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<tblPost>(entity =>
        {
            entity.HasKey(e => e.PostID).HasName("PK__tblPost__AA1260387C45C3B6");

            entity.ToTable("tblPost");

            entity.Property(e => e.PostID).ValueGeneratedNever();
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Text).HasColumnType("text");
            entity.Property(e => e.TimePosted).HasColumnType("datetime");
        });

        modelBuilder.Entity<tblPostComment>(entity =>
        {
            entity.HasKey(e => e.CommentID).HasName("PK__tblPostC__C3B4DFAA463FC011");

            entity.Property(e => e.CommentID).ValueGeneratedNever();
            entity.Property(e => e.Text).HasColumnType("text");
            entity.Property(e => e.TimePosted).HasColumnType("datetime");
        });

        modelBuilder.Entity<tblReview>(entity =>
        {
            entity.HasKey(e => e.ReviewID).HasName("PK__tblRevie__74BC79AEF9657D1B");

            entity.Property(e => e.ReviewID).ValueGeneratedNever();
            entity.Property(e => e.DateTime).HasColumnType("datetime");
            entity.Property(e => e.ReviewText).HasColumnType("text");
        });

        modelBuilder.Entity<tblUser>(entity =>
        {
            entity.HasKey(e => e.UserID).HasName("PK__tblUsers__1788CCAC201C7053");

            entity.Property(e => e.UserID).ValueGeneratedNever();
            entity.Property(e => e.AccessLevel)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Bio).HasColumnType("text");
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IconPic)
                .HasMaxLength(255)
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
