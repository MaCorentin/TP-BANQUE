using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using Models;
namespace ORM
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Compte> Comptes { get; set; }
        public virtual DbSet<Transact> Transacts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .Property(e => e.Nom)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Prenom)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.adresse)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Comptes)
                .WithMany(e => e.Clients)
                .Map(m => m.ToTable("Posseder").MapLeftKey("IdClient").MapRightKey("IdCompte"));

            modelBuilder.Entity<Compte>()
                .Property(e => e.Libelle)
                .IsUnicode(false);

            modelBuilder.Entity<Compte>()
                .Property(e => e.Solde)
                .HasPrecision(15, 2);

            modelBuilder.Entity<Compte>()
                .HasMany(e => e.Transacts)
                .WithRequired(e => e.Compte)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Transact>()
                .Property(e => e.TypeTransaction)
                .IsUnicode(false);

            modelBuilder.Entity<Transact>()
                .Property(e => e.Montant)
                .HasPrecision(15, 2);
        }
    }
}
