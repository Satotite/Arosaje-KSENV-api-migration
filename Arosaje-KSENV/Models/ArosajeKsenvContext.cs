using Microsoft.EntityFrameworkCore;

namespace Arosaje_KSENV.Models
{
    public partial class ArosajeKsenvContext : DbContext
    {
        public ArosajeKsenvContext()
        {
        }

        public ArosajeKsenvContext(DbContextOptions<ArosajeKsenvContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Botaniste> Botanistes { get; set; }
        public virtual DbSet<DateTip> DateTips { get; set; }
        public virtual DbSet<EnvoyerRecevoir> EnvoyerRecevoirs { get; set; }
        public virtual DbSet<Membre> Membres { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<Plante> Plantes { get; set; }
        public virtual DbSet<Proprio> Proprios { get; set; }
        public virtual DbSet<Utilisateur> Utilisateurs { get; set; }
        public virtual DbSet<Ville> Villes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Database=Arosaje_Database_MySql;Username=postgres;Password=epsi");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Botaniste>(entity =>
            {
                entity.HasKey(e => e.IdUtilisateur).HasName("PK__Botanist__63A5C1FFBCC7A728");

                entity.ToTable("Botaniste");

                entity.Property(e => e.IdUtilisateur)
                    .ValueGeneratedNever()
                    .HasColumnName("Id_Utilisateur");

                entity.HasOne(d => d.IdUtilisateurNavigation).WithOne(p => p.Botaniste)
                    .HasForeignKey<Botaniste>(d => d.IdUtilisateur)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Botaniste__Id_Ut__3C69FB99");

                entity.HasMany(d => d.IdTips).WithMany(p => p.IdUtilisateurs)
                    .UsingEntity<Dictionary<string, object>>(
                        "Conseiller",
                        r => r.HasOne<DateTip>().WithMany()
                            .HasForeignKey("IdTips")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK__Conseille__Id_Ti__59FA5E80"),
                        l => l.HasOne<Botaniste>().WithMany()
                            .HasForeignKey("IdUtilisateur")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK__Conseille__Id_Ut__59063A47"),
                        j =>
                        {
                            j.HasKey("IdUtilisateur", "IdTips").HasName("PK__Conseill__93C1D7C6ECFA37EC");
                            j.ToTable("Conseiller");
                            j.IndexerProperty<int>("IdUtilisateur").HasColumnName("Id_Utilisateur");
                            j.IndexerProperty<int>("IdTips").HasColumnName("Id_Tips");
                        });
            });

            modelBuilder.Entity<DateTip>(entity =>
            {
                entity.HasKey(e => e.IdTips).HasName("PK__dateTips__0641639EAF8D1B48");

                entity.ToTable("dateTips");

                entity.Property(e => e.IdTips).HasColumnName("Id_Tips");
                entity.Property(e => e.Contenu)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("contenu");
            });

            modelBuilder.Entity<EnvoyerRecevoir>(entity =>
            {
                entity.HasKey(e => new { e.IdUtilisateur, e.IdUtilisateur1, e.IdUtilisateur2, e.IdMessage }).HasName("PK__Envoyer___97E4F897273813C5");

                entity.ToTable("Envoyer_recevoir");

                entity.Property(e => e.IdUtilisateur).HasColumnName("Id_Utilisateur");
                entity.Property(e => e.IdUtilisateur1).HasColumnName("Id_Utilisateur_1");
                entity.Property(e => e.IdUtilisateur2).HasColumnName("Id_Utilisateur_2");
                entity.Property(e => e.IdMessage).HasColumnName("Id_Message");

                entity.HasOne(d => d.IdMessageNavigation).WithMany(p => p.EnvoyerRecevoirs)
                    .HasForeignKey(d => d.IdMessage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Envoyer_r__Id_Me__5629CD9C");

                entity.HasOne(d => d.IdUtilisateurNavigation).WithMany(p => p.EnvoyerRecevoirs)
                    .HasForeignKey(d => d.IdUtilisateur)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Envoyer_r__Id_Ut__534D60F1");

                entity.HasOne(d => d.IdUtilisateur1Navigation).WithMany(p => p.EnvoyerRecevoirs)
                    .HasForeignKey(d => d.IdUtilisateur1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Envoyer_r__Id_Ut__5441852A");

                entity.HasOne(d => d.IdUtilisateur2Navigation).WithMany(p => p.EnvoyerRecevoirs)
                    .HasForeignKey(d => d.IdUtilisateur2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Envoyer_r__Id_Ut__5535A963");
            });

            modelBuilder.Entity<Membre>(entity =>
            {
                entity.HasKey(e => e.IdUtilisateur).HasName("PK__Membre__63A5C1FFA80227E1");

                entity.ToTable("Membre");

                entity.Property(e => e.IdUtilisateur)
                    .ValueGeneratedNever()
                    .HasColumnName("Id_Utilisateur");

                entity.HasOne(d => d.IdUtilisateurNavigation).WithOne(p => p.Membre)
                    .HasForeignKey<Membre>(d => d.IdUtilisateur)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Membre__Id_Utili__398D8EEE");

                entity.HasMany(d => d.IdPhotos).WithMany(p => p.IdUtilisateurs)
                    .UsingEntity<Dictionary<string, object>>(
                        "Prendre",
                        r => r.HasOne<Photo>().WithMany()
                            .HasForeignKey("IdPhoto")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK__Prendre__Id_Phot__5070F446"),
                        l => l.HasOne<Membre>().WithMany()
                            .HasForeignKey("IdUtilisateur")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK__Prendre__Id_Util__4F7CD00D"),
                        j =>
                        {
                            j.HasKey("IdUtilisateur", "IdPhoto").HasName("PK__Prendre__4AC92A48BE6E6FB2");
                            j.ToTable("Prendre");
                            j.IndexerProperty<int>("IdUtilisateur").HasColumnName("Id_Utilisateur");
                            j.IndexerProperty<int>("IdPhoto").HasColumnName("Id_Photo");
                        });
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.IdMessage).HasName("PK__Message__A33138E696D85CD1");

                entity.ToTable("Message");

                entity.Property(e => e.IdMessage).HasColumnName("Id_Message");
                entity.Property(e => e.Contenu)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("contenu");
                entity.Property(e => e.DateMessage)
                    .HasColumnType("date")
                    .HasColumnName("dateMessage");
            });

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.HasKey(e => e.IdPhoto).HasName("PK__Photo__96CEBB73EF77EAA1");

                entity.ToTable("Photo");

                entity.Property(e => e.IdPhoto).HasColumnName("Id_Photo");
                entity.Property(e => e.Image)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("image");
            });

            modelBuilder.Entity<Plante>(entity =>
            {
                entity.HasKey(e => e.IdPlante).HasName("PK__Plante__8E9A9EE58E900046");

                entity.ToTable("Plante");

                entity.Property(e => e.IdPlante).HasColumnName("Id_Plante");
                entity.Property(e => e.Categorie)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Espece)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Etat)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.IdPhoto).HasColumnName("Id_Photo");
                entity.Property(e => e.IdUtilisateur).HasColumnName("Id_Utilisateur");
                entity.Property(e => e.IdUtilisateur1).HasColumnName("Id_Utilisateur_1");
                entity.Property(e => e.IdVille).HasColumnName("Id_Ville");
                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPhotoNavigation).WithMany(p => p.Plantes)
                    .HasForeignKey(d => d.IdPhoto)
                    .HasConstraintName("FK__Plante__Id_Photo__4AB81AF0");

                entity.HasOne(d => d.IdUtilisateurNavigation).WithMany(p => p.PlanteIdUtilisateurNavigations)
                    .HasForeignKey(d => d.IdUtilisateur)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Plante__Id_Utili__4BAC3F29");

                entity.HasOne(d => d.IdUtilisateur1Navigation).WithMany(p => p.PlanteIdUtilisateur1Navigations)
                    .HasForeignKey(d => d.IdUtilisateur1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Plante__Id_Utili__4CA06362");

                entity.HasOne(d => d.IdVilleNavigation).WithMany(p => p.Plantes)
                    .HasForeignKey(d => d.IdVille)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Plante__Id_Ville__49C3F6B7");

                entity.HasMany(d => d.IdTips).WithMany(p => p.IdPlantes)
                    .UsingEntity<Dictionary<string, object>>(
                        "HasTip",
                        r => r.HasOne<DateTip>().WithMany()
                            .HasForeignKey("IdTips")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK__HasTips__Id_Tips__5DCAEF64"),
                        l => l.HasOne<Plante>().WithMany()
                            .HasForeignKey("IdPlante")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK__HasTips__Id_Plan__5CD6CB2B"),
                        j =>
                        {
                            j.HasKey("IdPlante", "IdTips").HasName("PK__HasTips__7EFE88DC09331A83");
                            j.ToTable("HasTips");
                            j.IndexerProperty<int>("IdPlante").HasColumnName("Id_Plante");
                            j.IndexerProperty<int>("IdTips").HasColumnName("Id_Tips");
                        });
            });

            modelBuilder.Entity<Proprio>(entity =>
            {
                entity.HasKey(e => e.IdUtilisateur).HasName("PK__Proprio__63A5C1FFDE3A6090");

                entity.ToTable("Proprio");

                entity.Property(e => e.IdUtilisateur)
                    .ValueGeneratedNever()
                    .HasColumnName("Id_Utilisateur");

                entity.HasOne(d => d.IdUtilisateurNavigation).WithOne(p => p.Proprio)
                    .HasForeignKey<Proprio>(d => d.IdUtilisateur)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Proprio__Id_Util__46E78A0C");
            });

            modelBuilder.Entity<Utilisateur>(entity =>
            {
                entity.HasKey(e => e.IdUtilisateur).HasName("PK__Utilisat__63A5C1FF9ADAA4AE");

                entity.ToTable("Utilisateur");

                entity.Property(e => e.IdUtilisateur).HasColumnName("Id_Utilisateur");
                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Mdp)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Prenom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasMany(d => d.IdVilles).WithMany(p => p.IdUtilisateurs)
                    .UsingEntity<Dictionary<string, object>>(
                        "Habite",
                        r => r.HasOne<Ville>().WithMany()
                            .HasForeignKey("IdVille")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK__Habite__Id_Ville__619B8048"),
                        l => l.HasOne<Utilisateur>().WithMany()
                            .HasForeignKey("IdUtilisateur")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK__Habite__Id_Utili__60A75C0F"),
                        j =>
                        {
                            j.HasKey("IdUtilisateur", "IdVille").HasName("PK__Habite__BD550270470E1D44");
                            j.ToTable("Habite");
                            j.IndexerProperty<int>("IdUtilisateur").HasColumnName("Id_Utilisateur");
                            j.IndexerProperty<int>("IdVille").HasColumnName("Id_Ville");
                        });
            });

            modelBuilder.Entity<Ville>(entity =>
            {
                entity.HasKey(e => e.IdVille).HasName("PK__Ville__EF0C38FCB16C0CA0");

                entity.ToTable("Ville");

                entity.Property(e => e.IdVille).HasColumnName("Id_Ville");
                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
