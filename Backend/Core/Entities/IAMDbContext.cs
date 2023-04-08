using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Core.Entities
{
    public partial class IAMDbContext : DbContext
    {
        public IAMDbContext()
        {
        }

        public IAMDbContext(DbContextOptions<IAMDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Rolesmenu> Rolesmenus { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Usersrole> Usersroles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Database=ing.IAM;Username=postgres;Password=password");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "French_France.1252");

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(e => e.MenId)
                    .HasName("menus_pkey");

                entity.ToTable("menus");

                entity.Property(e => e.MenId)
                    .ValueGeneratedNever()
                    .HasColumnName("men_id");

                entity.Property(e => e.MemHref)
                    .HasColumnType("character varying")
                    .HasColumnName("mem_href");

                entity.Property(e => e.MemIcon)
                    .HasColumnType("character varying")
                    .HasColumnName("mem_icon");

                entity.Property(e => e.MemRouterlink)
                    .HasColumnType("character varying")
                    .HasColumnName("mem_routerlink");

                entity.Property(e => e.MemTarget)
                    .HasColumnType("character varying")
                    .HasColumnName("mem_target");

                entity.Property(e => e.MenDescription)
                    .HasColumnType("character varying")
                    .HasColumnName("men_description");

                entity.Property(e => e.MenHassubmenu).HasColumnName("men_hassubmenu");

                entity.Property(e => e.MenModuleid).HasColumnName("men_moduleid");

                entity.Property(e => e.MenParentid).HasColumnName("men_parentid");

                entity.Property(e => e.MenTitre)
                    .HasColumnType("character varying")
                    .HasColumnName("men_titre");

                entity.HasOne(d => d.MenModule)
                    .WithMany(p => p.Menus)
                    .HasForeignKey(d => d.MenModuleid)
                    .HasConstraintName("menus_module_fkey_id");

                entity.HasOne(d => d.MenParent)
                    .WithMany(p => p.InverseMenParent)
                    .HasForeignKey(d => d.MenParentid)
                    .HasConstraintName("men_parentid_fkey");
            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.HasKey(e => e.ModId)
                    .HasName("mods_pkey");

                entity.ToTable("modules");

                entity.HasIndex(e => e.ModName, "mod_name_unique")
                    .IsUnique();

                entity.Property(e => e.ModId).HasColumnName("mod_id");

                entity.Property(e => e.ModActive).HasColumnName("mod_active");

                entity.Property(e => e.ModCreatedbyuser).HasColumnName("mod_createdbyuser");

                entity.Property(e => e.ModCreatedondate).HasColumnName("mod_createdondate");

                entity.Property(e => e.ModDescription)
                    .HasColumnType("character varying")
                    .HasColumnName("mod_description");

                entity.Property(e => e.ModIdMenu).HasColumnName("mod_id_menu");

                entity.Property(e => e.ModName)
                    .HasColumnType("character varying")
                    .HasColumnName("mod_name");

                entity.Property(e => e.ModUpdatedbyuser).HasColumnName("mod_updatedbyuser");

                entity.Property(e => e.ModUpdatedondate).HasColumnName("mod_updatedondate");

                entity.HasOne(d => d.ModIdMenuNavigation)
                    .WithMany(p => p.Modules)
                    .HasForeignKey(d => d.ModIdMenu)
                    .HasConstraintName("module_menu_fkey");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");

                entity.HasIndex(e => e.RoleName, "rolename_unique")
                    .IsUnique();

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.RoleActive).HasColumnName("role_active");

                entity.Property(e => e.RoleCreatedbyuser).HasColumnName("role_createdbyuser");

                entity.Property(e => e.RoleCreatedondate).HasColumnName("role_createdondate");

                entity.Property(e => e.RoleDescription)
                    .HasColumnType("character varying")
                    .HasColumnName("role_description");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("role_name");

                entity.Property(e => e.RoleParenteid).HasColumnName("role_parenteid");

                entity.Property(e => e.RoleUpdatedbyuser).HasColumnName("role_updatedbyuser");

                entity.Property(e => e.RoleUpdatedondate).HasColumnName("role_updatedondate");

                entity.HasOne(d => d.RoleParente)
                    .WithMany(p => p.InverseRoleParente)
                    .HasForeignKey(d => d.RoleParenteid)
                    .HasConstraintName("fk_roles_parentid");
            });

            modelBuilder.Entity<Rolesmenu>(entity =>
            {
                entity.HasKey(e => new { e.RmRoleid, e.RmMenuid })
                    .HasName("rm_id_pk");

                entity.ToTable("rolesmenus");

                entity.Property(e => e.RmRoleid).HasColumnName("rm_roleid");

                entity.Property(e => e.RmMenuid).HasColumnName("rm_menuid");

                entity.Property(e => e.RmActive).HasColumnName("rm_active");

                entity.Property(e => e.RmAjout).HasColumnName("rm_ajout");

                entity.Property(e => e.RmCreatedbyapp).HasColumnName("rm_createdbyapp");

                entity.Property(e => e.RmCreatedondate).HasColumnName("rm_createdondate");

                entity.Property(e => e.RmModif).HasColumnName("rm_modif");

                entity.Property(e => e.RmName)
                    .HasColumnType("character varying")
                    .HasColumnName("rm_name");

                entity.Property(e => e.RmSelection).HasColumnName("rm_selection");

                entity.Property(e => e.RmSupprime).HasColumnName("rm_supprime");

                entity.Property(e => e.RmUpdatedbyapp).HasColumnName("rm_updatedbyapp");

                entity.Property(e => e.RmUpdatedondate).HasColumnName("rm_updatedondate");

                entity.HasOne(d => d.RmMenu)
                    .WithMany(p => p.Rolesmenus)
                    .HasForeignKey(d => d.RmMenuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rolesmenu_menuid_fkey");

                entity.HasOne(d => d.RmRole)
                    .WithMany(p => p.Rolesmenus)
                    .HasForeignKey(d => d.RmRoleid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rolesmenu_roleid_fkey");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.UserUsername, "login_unique")
                    .IsUnique();

                entity.HasIndex(e => e.UserGuid, "uguid_unique")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.UserActive).HasColumnName("user_active");

                entity.Property(e => e.UserAdresse)
                    .HasColumnType("character varying")
                    .HasColumnName("user_adresse");

                entity.Property(e => e.UserCreatedbyuser).HasColumnName("user_createdbyuser");

                entity.Property(e => e.UserCreatedondate).HasColumnName("user_createdondate");

                entity.Property(e => e.UserEmail)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("user_email");

                entity.Property(e => e.UserGuid)
                    .HasColumnType("character varying")
                    .HasColumnName("user_guid");

                entity.Property(e => e.UserNom)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("user_nom");

                entity.Property(e => e.UserPasswordhash)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("user_passwordhash");

                entity.Property(e => e.UserPhoto).HasColumnName("user_photo");

                entity.Property(e => e.UserPrenom)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("user_prenom");

                entity.Property(e => e.UserTel)
                    .HasColumnType("character varying")
                    .HasColumnName("user_tel");

                entity.Property(e => e.UserUpdatedbyuser).HasColumnName("user_updatedbyuser");

                entity.Property(e => e.UserUpdatedondate).HasColumnName("user_updatedondate");

                entity.Property(e => e.UserUsername)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("user_username");
            });

            modelBuilder.Entity<Usersrole>(entity =>
            {
                entity.HasKey(e => new { e.UrUserid, e.UrRoleid })
                    .HasName("ur_id_pk");

                entity.ToTable("usersroles");

                entity.Property(e => e.UrUserid).HasColumnName("ur_userid");

                entity.Property(e => e.UrRoleid).HasColumnName("ur_roleid");

                entity.Property(e => e.UrActive).HasColumnName("ur_active");

                entity.Property(e => e.UrCreatedbyuser).HasColumnName("ur_createdbyuser");

                entity.Property(e => e.UrCreatedondate).HasColumnName("ur_createdondate");

                entity.Property(e => e.UrUpdatedbyuser).HasColumnName("ur_updatedbyuser");

                entity.Property(e => e.UrUpdatedondate).HasColumnName("ur_updatedondate");

                entity.HasOne(d => d.UrRole)
                    .WithMany(p => p.Usersroles)
                    .HasForeignKey(d => d.UrRoleid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("usersroles_roleid_fkey");

                entity.HasOne(d => d.UrUser)
                    .WithMany(p => p.Usersroles)
                    .HasForeignKey(d => d.UrUserid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("usersroles_userid_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
