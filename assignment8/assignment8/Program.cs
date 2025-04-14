using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseTransactionSystem
{
    public class ClientTransaction
    {
        [Key]
        public int TransactionId { get; set; }

        [Required]
        [StringLength(50)]
        public string ClientIdentifier { get; set; } = null!;

        public List<TransactionEntry> Entries { get; set; } = new();

        [NotMapped]
        public decimal TotalSum => Entries.Sum(e => e.ItemValue);
    }

    public class TransactionEntry
    {
        [Key]
        public int EntryId { get; set; }

        [Required]
        [StringLength(50)]
        public string ArticleIdentifier { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ItemValue { get; set; }

        public int TransactionId { get; set; }
        public ClientTransaction ParentTransaction { get; set; } = null!;
    }

    public class TransactionDbContext : DbContext
    {
        public DbSet<ClientTransaction> ClientTransactions { get; set; }
        public DbSet<TransactionEntry> TransactionEntries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "server=localhost;port=3306;database=EnterpriseTransactionDB;user=root;password=112233aabb;";
            optionsBuilder.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString),
                options => options.EnableRetryOnFailure(3)
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientTransaction>(entity =>
            {
                entity.HasMany(t => t.Entries)
                    .WithOne(e => e.ParentTransaction)
                    .HasForeignKey(e => e.TransactionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });
        }
    }

    public class TransactionProcessor : IDisposable
    {
        private readonly TransactionDbContext _context;

        public TransactionProcessor()
        {
            _context = new TransactionDbContext();
            _context.Database.EnsureCreated();
        }

        public List<ClientTransaction> GetAllTransactions() =>
            _context.ClientTransactions
                .Include(t => t.Entries)
                .AsNoTracking()
                .ToList();

        public List<ClientTransaction> FilterTransactions(string clientQuery) =>
            _context.ClientTransactions
                .Include(t => t.Entries)
                .Where(t => t.ClientIdentifier.Contains(clientQuery))
                .AsNoTracking()
                .ToList();

        public void RegisterTransaction(ClientTransaction transaction)
        {
            ValidateTransaction(transaction);
            _context.ClientTransactions.Add(transaction);
            _context.SaveChanges();
        }

        public bool UpdateTransaction(int id, Action<ClientTransaction> modification)
        {
            var target = _context.ClientTransactions
                .Include(t => t.Entries)
                .FirstOrDefault(t => t.TransactionId == id);

            if (target == null) return false;

            modification(target);
            ValidateTransaction(target);

            _context.SaveChanges();
            return true;
        }

        public bool RemoveTransaction(int id)
        {
            var target = _context.ClientTransactions.Find(id);
            if (target == null) return false;

            _context.ClientTransactions.Remove(target);
            _context.SaveChanges();
            return true;
        }

        private void ValidateTransaction(ClientTransaction transaction)
        {
            if (string.IsNullOrWhiteSpace(transaction.ClientIdentifier))
                throw new ArgumentException("客户信息必须填写");

            if (!transaction.Entries.Any())
                throw new ArgumentException("交易条目不能为空");
        }

        public void Dispose() => _context.Dispose();
    }

    internal static class SystemBootstrapper
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}