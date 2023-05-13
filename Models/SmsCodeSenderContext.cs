using Microsoft.EntityFrameworkCore;

namespace WebAPI;

public partial class SmsCodeSenderContext : DbContext
{

    public SmsCodeSenderContext(DbContextOptions<SmsCodeSenderContext> options)
        : base(options)
    {
    }

    public virtual DbSet<SmsMessage> SmsMessages { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SmsMessage>(entity =>
        {
            entity.HasKey(e => e.SmsMessageId).HasName("PK_SmsMessage_SmsMessageId");

            entity.ToTable("SmsMessage");

            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FromPhone).HasMaxLength(255);
            entity.Property(e => e.MessageBody).HasMaxLength(800);
            entity.Property(e => e.MessageSid).HasMaxLength(255);
            entity.Property(e => e.ToPhone).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
