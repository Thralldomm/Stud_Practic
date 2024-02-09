using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Sample.Models;

public partial class MasterContext : DbContext
{
    public MasterContext()
    {
    }

    public MasterContext(DbContextOptions<MasterContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Micropost> Microposts { get; set; }

    public virtual DbSet<Relation> Relations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-QOTDBB6;Initial Catalog=master;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Micropost>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Micropos__3214EC07B6B5129E");

            entity.Property(e => e.Content).HasColumnType("text");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.Microposts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Microposts_ToUsers");
        });

        modelBuilder.Entity<Relation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Relation__3214EC0711EF589C");

            entity.HasIndex(e => new { e.FollowerId, e.FollowedId }, "UniqPairFollowedFollower").IsUnique();

            entity.HasOne(d => d.Followed).WithMany(p => p.RelationFolloweds)
                .HasForeignKey(d => d.FollowedId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Followed");

            entity.HasOne(d => d.Follower).WithMany(p => p.RelationFollowers)
                .HasForeignKey(d => d.FollowerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Follower");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07AD2B83C3");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PasswordConfirmation)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Password_Confirmation");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
