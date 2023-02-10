//using System;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata;

//namespace HKDataClassLibrary.Model
//{
//    public partial class HKWebContext : DbContext
//    {
//        public HKWebContext()
//        {
//        }

//        public HKWebContext(DbContextOptions<HKWebContext> options)
//            : base(options)
//        {
//        }

//        public DbSet<Member> Members { get; set; }
//        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
//        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
//        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
//        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
//        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
//        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }
//        public virtual DbSet<TAgentCommissions> TAgentCommissions { get; set; }
//        public virtual DbSet<TAgents> TAgents { get; set; }
//        public virtual DbSet<TApplicationRoles> TApplicationRoles { get; set; }
//        public virtual DbSet<TApplicationUserClaims> TApplicationUserClaims { get; set; }
//        public virtual DbSet<TApplicationUserLogins> TApplicationUserLogins { get; set; }
//        public virtual DbSet<TApplicationUserRoles> TApplicationUserRoles { get; set; }
//        public virtual DbSet<TApplicationUsers> TApplicationUsers { get; set; }
//        public virtual DbSet<TAspNetUserSchemes> TAspNetUserSchemes { get; set; }
//        public virtual DbSet<TBeneficiaries> TBeneficiaries { get; set; }
//        public virtual DbSet<TBeneficiaryInsuranceCoverTypes> TBeneficiaryInsuranceCoverTypes { get; set; }
//        public virtual DbSet<TBenefits> TBenefits { get; set; }
//        public virtual DbSet<TBlog> TBlog { get; set; }
//        public virtual DbSet<TClinic> TClinic { get; set; }
//        public virtual DbSet<TClinicEmail> TClinicEmail { get; set; }
//        public virtual DbSet<TClinicPhone> TClinicPhone { get; set; }
//        public virtual DbSet<TClinicService> TClinicService { get; set; }
//        public virtual DbSet<TClinicVisuals> TClinicVisuals { get; set; }
//        public virtual DbSet<TContactType> TContactType { get; set; }
//        public virtual DbSet<TContactUs> TContactUs { get; set; }
//        public virtual DbSet<TCounty> TCounty { get; set; }
//        public virtual DbSet<TGroupLifeBeneficiaries> TGroupLifeBeneficiaries { get; set; }
//        public virtual DbSet<THospital> THospital { get; set; }
//        public virtual DbSet<TInquiry> TInquiry { get; set; }
//        public virtual DbSet<TInsuranceCoverApprovals> TInsuranceCoverApprovals { get; set; }
//        public virtual DbSet<TInsuranceCoverTypeBenefits> TInsuranceCoverTypeBenefits { get; set; }
//        public virtual DbSet<TInsuranceCoverTypes> TInsuranceCoverTypes { get; set; }
//        public virtual DbSet<TInsuranceCovers> TInsuranceCovers { get; set; }
//        public virtual DbSet<TInsurances> TInsurances { get; set; }
//        public virtual DbSet<TJobGroup> TJobGroup { get; set; }
//        public virtual DbSet<TJobGroupLimits> TJobGroupLimits { get; set; }
//        public virtual DbSet<TLastExpenseBeneficiaries> TLastExpenseBeneficiaries { get; set; }
//        public virtual DbSet<TLeads> TLeads { get; set; }
//        public virtual DbSet<TLog> TLog { get; set; }
//        public virtual DbSet<TMedicalCoverProductLimits> TMedicalCoverProductLimits { get; set; }
//        public virtual DbSet<TMedicalCoverProducts> TMedicalCoverProducts { get; set; }
//        public virtual DbSet<TMemberDocuments> TMemberDocuments { get; set; }
//        public virtual DbSet<TMemberInsuranceCovers> TMemberInsuranceCovers { get; set; }
//        public virtual DbSet<TMembers> TMembers { get; set; }
//        public virtual DbSet<TNewsLetter> TNewsLetter { get; set; }
//        public virtual DbSet<TNextOfKins> TNextOfKins { get; set; }
//        public virtual DbSet<TNominee> TNominee { get; set; }
//        public virtual DbSet<TOrderPayments> TOrderPayments { get; set; }
//        public virtual DbSet<TOrders> TOrders { get; set; }
//        public virtual DbSet<TPromotion> TPromotion { get; set; }
//        public virtual DbSet<TRegion> TRegion { get; set; }
//        public virtual DbSet<TRegistration> TRegistration { get; set; }
//        public virtual DbSet<TRelation> TRelation { get; set; }
//        public virtual DbSet<TScheme> TScheme { get; set; }
//        public virtual DbSet<TService> TService { get; set; }
//        public virtual DbSet<TStatus> TStatus { get; set; }
//        public virtual DbSet<TSubCounty> TSubCounty { get; set; }
//        public virtual DbSet<TSurvey> TSurvey { get; set; }
//        public virtual DbSet<TSurveyDetail> TSurveyDetail { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                //optionsBuilder.UseSqlServer("Server=DESKTOP-7KON1NE;Database=HKWeb;Trusted_Connection=True;");
//            }
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<AspNetRoles>(entity =>
//            {
//                entity.Property(e => e.Id).HasMaxLength(128);

//                entity.Property(e => e.Discriminator).HasMaxLength(300);

//                entity.Property(e => e.Name)
//                    .IsRequired()
//                    .HasMaxLength(256);
//            });

//            modelBuilder.Entity<AspNetUserClaims>(entity =>
//            {
//                entity.Property(e => e.UserId)
//                    .IsRequired()
//                    .HasMaxLength(128);

//                entity.HasOne(d => d.User)
//                    .WithMany(p => p.AspNetUserClaims)
//                    .HasForeignKey(d => d.UserId)
//                    .HasConstraintName("FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId");
//            });

//            modelBuilder.Entity<AspNetUserLogins>(entity =>
//            {
//                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey, e.UserId })
//                    .HasName("PK_dbo.AspNetUserLogins");

//                entity.Property(e => e.LoginProvider).HasMaxLength(128);

//                entity.Property(e => e.ProviderKey).HasMaxLength(128);

//                entity.Property(e => e.UserId).HasMaxLength(128);

//                entity.HasOne(d => d.User)
//                    .WithMany(p => p.AspNetUserLogins)
//                    .HasForeignKey(d => d.UserId)
//                    .HasConstraintName("FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId");
//            });

//            modelBuilder.Entity<AspNetUserRoles>(entity =>
//            {
//                entity.HasKey(e => new { e.UserId, e.RoleId })
//                    .HasName("PK_dbo.AspNetUserRoles");

//                entity.Property(e => e.UserId).HasMaxLength(128);

//                entity.Property(e => e.RoleId).HasMaxLength(128);

//                entity.HasOne(d => d.Role)
//                    .WithMany(p => p.AspNetUserRoles)
//                    .HasForeignKey(d => d.RoleId)
//                    .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId");

//                entity.HasOne(d => d.User)
//                    .WithMany(p => p.AspNetUserRoles)
//                    .HasForeignKey(d => d.UserId)
//                    .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId");
//            });

//            modelBuilder.Entity<AspNetUsers>(entity =>
//            {
//                entity.Property(e => e.Id).HasMaxLength(128);

//                entity.Property(e => e.DateCreated).HasColumnType("datetime");

//                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

//                entity.Property(e => e.Email).HasMaxLength(256);

//                entity.Property(e => e.LastLoginDate).HasColumnType("datetime");

//                entity.Property(e => e.LockoutEndDateUtc).HasColumnType("datetime");

//                entity.Property(e => e.PasswordChangeDate).HasColumnType("datetime");

//                entity.Property(e => e.UserName)
//                    .IsRequired()
//                    .HasMaxLength(256);

//                entity.HasOne(d => d.Agent)
//                    .WithMany(p => p.AspNetUsers)
//                    .HasForeignKey(d => d.AgentId)
//                    .HasConstraintName("FK_AspNetUsers_t_Agents");
//            });

//            modelBuilder.Entity<MigrationHistory>(entity =>
//            {
//                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
//                    .HasName("PK_dbo.__MigrationHistory");

//                entity.ToTable("__MigrationHistory");

//                entity.Property(e => e.MigrationId).HasMaxLength(150);

//                entity.Property(e => e.ContextKey).HasMaxLength(300);

//                entity.Property(e => e.Model).IsRequired();

//                entity.Property(e => e.ProductVersion)
//                    .IsRequired()
//                    .HasMaxLength(32);
//            });

//            modelBuilder.Entity<TAgentCommissions>(entity =>
//            {
//                entity.ToTable("t_AgentCommissions");

//                entity.Property(e => e.Id).ValueGeneratedNever();

//                entity.Property(e => e.AgentCommission).HasColumnType("decimal(18, 2)");

//                entity.Property(e => e.CommissionDate).HasColumnType("datetime");

//                entity.Property(e => e.ParentCommission).HasColumnType("decimal(18, 2)");

//                entity.HasOne(d => d.Agent)
//                    .WithMany(p => p.TAgentCommissions)
//                    .HasForeignKey(d => d.AgentId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_t_AgentCommissions_t_Agents");

//                entity.HasOne(d => d.InsuranceCover)
//                    .WithMany(p => p.TAgentCommissions)
//                    .HasForeignKey(d => d.InsuranceCoverId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_t_AgentCommissions_t_InsuranceCovers");
//            });

//            modelBuilder.Entity<TAgents>(entity =>
//            {
//                entity.ToTable("t_Agents");

//                entity.HasIndex(e => e.AgentNumber)
//                    .HasName("IX_t_Agents")
//                    .IsUnique();

//                entity.Property(e => e.Id).ValueGeneratedNever();

//                entity.Property(e => e.AgentNumber).HasMaxLength(50);

//                entity.Property(e => e.AgentType).HasMaxLength(50);

//                entity.Property(e => e.CertificateOfInCorporation).HasMaxLength(300);

//                entity.Property(e => e.ComissionPercentage).HasColumnType("decimal(3, 2)");

//                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

//                entity.Property(e => e.EmailAddress).HasMaxLength(50);

//                entity.Property(e => e.Gender)
//                    .HasMaxLength(50)
//                    .IsUnicode(false);

//                entity.Property(e => e.IdentificationCard).HasMaxLength(300);

//                entity.Property(e => e.Krapin)
//                    .HasColumnName("KRAPin")
//                    .HasMaxLength(50);

//                entity.Property(e => e.KrapinCertificate)
//                    .HasColumnName("KRAPinCertificate")
//                    .HasMaxLength(300);

//                entity.Property(e => e.MpesaAccountNumber)
//                    .HasColumnName("MPesaAccountNumber")
//                    .HasMaxLength(50);

//                entity.Property(e => e.Name).HasMaxLength(300);

//                entity.Property(e => e.NationalId)
//                    .HasColumnName("NationalID")
//                    .HasMaxLength(50);

//                entity.Property(e => e.ParentAgentCommissionPercentage).HasColumnType("decimal(3, 2)");

//                entity.Property(e => e.PhoneNumber).HasMaxLength(50);

//                entity.Property(e => e.PhysicalAddress).HasMaxLength(50);

//                entity.Property(e => e.PostalCode).HasMaxLength(50);
//            });

//            modelBuilder.Entity<TApplicationRoles>(entity =>
//            {
//                entity.ToTable("t_ApplicationRoles");

//                entity.HasIndex(e => e.Name)
//                    .HasName("RoleNameIndex")
//                    .IsUnique();

//                entity.Property(e => e.Id).HasMaxLength(300);

//                entity.Property(e => e.Name)
//                    .IsRequired()
//                    .HasMaxLength(256);
//            });

//            modelBuilder.Entity<TApplicationUserClaims>(entity =>
//            {
//                entity.ToTable("t_ApplicationUserClaims");

//                entity.HasIndex(e => e.UserId)
//                    .HasName("IX_UserId");

//                entity.Property(e => e.UserId)
//                    .IsRequired()
//                    .HasMaxLength(300);

//                entity.HasOne(d => d.User)
//                    .WithMany(p => p.TApplicationUserClaims)
//                    .HasForeignKey(d => d.UserId)
//                    .HasConstraintName("FK_ApplicationUserClaims_ApplicationUsers_UserId");
//            });

//            modelBuilder.Entity<TApplicationUserLogins>(entity =>
//            {
//                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey, e.UserId })
//                    .HasName("PK_dbo.ApplicationUserLogins");

//                entity.ToTable("t_ApplicationUserLogins");

//                entity.HasIndex(e => e.UserId)
//                    .HasName("IX_UserId");

//                entity.Property(e => e.LoginProvider).HasMaxLength(128);

//                entity.Property(e => e.ProviderKey).HasMaxLength(128);

//                entity.Property(e => e.UserId).HasMaxLength(300);

//                entity.HasOne(d => d.User)
//                    .WithMany(p => p.TApplicationUserLogins)
//                    .HasForeignKey(d => d.UserId)
//                    .HasConstraintName("FK_dbo.ApplicationUserLogins_dbo.ApplicationUsers_Id");
//            });

//            modelBuilder.Entity<TApplicationUserRoles>(entity =>
//            {
//                entity.HasNoKey();

//                entity.ToTable("t_ApplicationUserRoles");

//                entity.HasIndex(e => e.RoleId)
//                    .HasName("IX_RoleId");

//                entity.HasIndex(e => e.UserId)
//                    .HasName("IX_UserId");

//                entity.Property(e => e.RoleId)
//                    .IsRequired()
//                    .HasMaxLength(300);

//                entity.Property(e => e.UserId)
//                    .IsRequired()
//                    .HasMaxLength(300);

//                entity.HasOne(d => d.Role)
//                    .WithMany()
//                    .HasForeignKey(d => d.RoleId)
//                    .HasConstraintName("FK_dbo.ApplicationUserRoles_dbo.ApplicationRoles_RoleId");

//                entity.HasOne(d => d.User)
//                    .WithMany()
//                    .HasForeignKey(d => d.UserId)
//                    .HasConstraintName("FK_dbo.ApplicationUserRoles_dbo.ApplicationUsers_UserId");
//            });

//            modelBuilder.Entity<TApplicationUsers>(entity =>
//            {
//                entity.ToTable("t_ApplicationUsers");

//                entity.HasIndex(e => e.Email)
//                    .HasName("UQ_ApplicationUsers_Email")
//                    .IsUnique();

//                entity.HasIndex(e => e.UserName)
//                    .HasName("UQ_ApplicationUsers_UserName")
//                    .IsUnique();

//                entity.Property(e => e.Id).HasMaxLength(300);

//                entity.Property(e => e.DateCreated).HasColumnType("datetime");

//                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

//                entity.Property(e => e.Email).HasMaxLength(256);

//                entity.Property(e => e.FirstName).HasMaxLength(256);

//                entity.Property(e => e.LastLoginDate).HasColumnType("datetime");

//                entity.Property(e => e.LastName).HasMaxLength(256);

//                entity.Property(e => e.LockoutEndDateUtc).HasColumnType("datetime");

//                entity.Property(e => e.PasswordChangeDate).HasColumnType("datetime");

//                entity.Property(e => e.PasswordHash).HasMaxLength(512);

//                entity.Property(e => e.PhoneNumber).HasMaxLength(128);

//                entity.Property(e => e.SecurityStamp).HasMaxLength(512);

//                entity.Property(e => e.UserName)
//                    .IsRequired()
//                    .HasMaxLength(256);
//            });

//            modelBuilder.Entity<TAspNetUserSchemes>(entity =>
//            {
//                entity.ToTable("t_AspNetUserSchemes");

//                entity.Property(e => e.Id).ValueGeneratedNever();

//                entity.Property(e => e.UserId)
//                    .IsRequired()
//                    .HasMaxLength(128);

//                entity.HasOne(d => d.Scheme)
//                    .WithMany(p => p.TAspNetUserSchemes)
//                    .HasForeignKey(d => d.SchemeId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_t_AspNetUserSchemes_t_InsuranceCovers");

//                entity.HasOne(d => d.User)
//                    .WithMany(p => p.TAspNetUserSchemes)
//                    .HasForeignKey(d => d.UserId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_t_AspNetUserSchemes_t_AspNetUserSchemes");
//            });

//            modelBuilder.Entity<TBeneficiaries>(entity =>
//            {
//                entity.ToTable("t_Beneficiaries");

//                entity.Property(e => e.Id).ValueGeneratedNever();

//                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

//                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

//                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

//                entity.HasOne(d => d.MedicalCoverProduct)
//                    .WithMany(p => p.TBeneficiaries)
//                    .HasForeignKey(d => d.MedicalCoverProductId)
//                    .HasConstraintName("FK_t_Beneficiaries_t_MedicalCoverProducts");
//            });

//            modelBuilder.Entity<TBeneficiaryInsuranceCoverTypes>(entity =>
//            {
//                entity.ToTable("t_BeneficiaryInsuranceCoverTypes");

//                entity.Property(e => e.Id).ValueGeneratedNever();

//                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

//                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

//                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

//                entity.HasOne(d => d.Beneficiary)
//                    .WithMany(p => p.TBeneficiaryInsuranceCoverTypes)
//                    .HasForeignKey(d => d.BeneficiaryId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_dbo.t_BeneficiaryInsuranceCoverTypes_dbo.t_Beneficiaries_BeneficiaryId");

//                entity.HasOne(d => d.InsuranceCoverType)
//                    .WithMany(p => p.TBeneficiaryInsuranceCoverTypes)
//                    .HasForeignKey(d => d.InsuranceCoverTypeId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_dbo.t_BeneficiaryInsuranceCoverTypes_dbo.t_InsuranceCoverTypes_InsuranceCoverTypeId");
//            });

//            modelBuilder.Entity<TBenefits>(entity =>
//            {
//                entity.ToTable("t_Benefits");

//                entity.Property(e => e.Id).ValueGeneratedNever();

//                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

//                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
//            });

//            modelBuilder.Entity<TBlog>(entity =>
//            {
//                entity.HasKey(e => e.Pkid)
//                    .HasName("PK__t_Blog__5E0282726FAF6873");

//                entity.ToTable("t_Blog");

//                entity.Property(e => e.Pkid).HasColumnName("PKID");

//                entity.Property(e => e.Author).IsRequired();

//                entity.Property(e => e.CreateDate).HasColumnType("datetime");

//                entity.Property(e => e.Description).IsRequired();

//                entity.Property(e => e.Title).IsRequired();
//            });

//            modelBuilder.Entity<TClinic>(entity =>
//            {
//                entity.HasKey(e => e.Pkid);

//                entity.ToTable("t_Clinic");

//                entity.Property(e => e.Pkid).HasColumnName("PKID");

//                entity.Property(e => e.Name).IsRequired();

//                entity.HasOne(d => d.CountyNavigation)
//                    .WithMany(p => p.TClinic)
//                    .HasForeignKey(d => d.County)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_t_Clinic_t_County");

//                entity.HasOne(d => d.RegionNavigation)
//                    .WithMany(p => p.TClinic)
//                    .HasForeignKey(d => d.Region)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_t_Clinic_t_Region");

//                entity.HasOne(d => d.SubCountyNavigation)
//                    .WithMany(p => p.TClinic)
//                    .HasForeignKey(d => d.SubCounty)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_t_Clinic_t_SubCounty");
//            });

//            modelBuilder.Entity<TClinicEmail>(entity =>
//            {
//                entity.HasKey(e => e.Pkid);

//                entity.ToTable("t_ClinicEmail");

//                entity.Property(e => e.Pkid).HasColumnName("PKID");

//                entity.Property(e => e.ClinicId).HasColumnName("ClinicID");

//                entity.Property(e => e.EmailId).HasColumnName("EmailID");

//                entity.HasOne(d => d.Clinic)
//                    .WithMany(p => p.TClinicEmail)
//                    .HasForeignKey(d => d.ClinicId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_t_ClinicEmail_t_Clinic");
//            });

//            modelBuilder.Entity<TClinicPhone>(entity =>
//            {
//                entity.HasKey(e => e.Pkid);

//                entity.ToTable("t_ClinicPhone");

//                entity.Property(e => e.Pkid).HasColumnName("PKID");

//                entity.Property(e => e.ClinicId).HasColumnName("ClinicID");

//                entity.HasOne(d => d.Clinic)
//                    .WithMany(p => p.TClinicPhone)
//                    .HasForeignKey(d => d.ClinicId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_t_ClinicPhone_t_Clinic");
//            });

//            modelBuilder.Entity<TClinicService>(entity =>
//            {
//                entity.HasKey(e => e.Pkid)
//                    .HasName("PK_t_HospitalService");

//                entity.ToTable("t_ClinicService");

//                entity.Property(e => e.Pkid).HasColumnName("PKID");

//                entity.Property(e => e.ClinicId).HasColumnName("ClinicID");

//                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

//                entity.HasOne(d => d.Clinic)
//                    .WithMany(p => p.TClinicService)
//                    .HasForeignKey(d => d.ClinicId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_t_ClinicService_t_Clinic");

//                entity.HasOne(d => d.Service)
//                    .WithMany(p => p.TClinicService)
//                    .HasForeignKey(d => d.ServiceId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_t_ClinicService_t_Service");
//            });

//            modelBuilder.Entity<TClinicVisuals>(entity =>
//            {
//                entity.HasKey(e => e.Pkid);

//                entity.ToTable("t_ClinicVisuals");

//                entity.Property(e => e.Pkid).HasColumnName("PKID");

//                entity.Property(e => e.ClinicId).HasColumnName("ClinicID");

//                entity.Property(e => e.Extension).IsRequired();

//                entity.Property(e => e.ImageUri)
//                    .IsRequired()
//                    .HasColumnName("ImageURI");

//                entity.Property(e => e.Name).IsRequired();

//                entity.HasOne(d => d.Clinic)
//                    .WithMany(p => p.TClinicVisuals)
//                    .HasForeignKey(d => d.ClinicId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_t_ClinicVisuals_t_Clinic");
//            });

//            modelBuilder.Entity<TContactType>(entity =>
//            {
//                entity.HasKey(e => e.Pkid);

//                entity.ToTable("t_ContactType");

//                entity.Property(e => e.Pkid).HasColumnName("PKID");

//                entity.Property(e => e.ContactType).IsRequired();

//                entity.Property(e => e.CreateDate).HasColumnType("datetime");
//            });

//            modelBuilder.Entity<TContactUs>(entity =>
//            {
//                entity.HasKey(e => e.Pkid);

//                entity.ToTable("t_ContactUs");

//                entity.Property(e => e.Pkid).HasColumnName("PKID");

//                entity.Property(e => e.CreateDate).HasColumnType("datetime");

//                entity.Property(e => e.FirstName).IsRequired();

//                entity.HasOne(d => d.ContactTypeNavigation)
//                    .WithMany(p => p.TContactUs)
//                    .HasForeignKey(d => d.ContactType)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_t_ContactUs_t_ContactType");

//                entity.HasOne(d => d.InquiryTypeNavigation)
//                    .WithMany(p => p.TContactUs)
//                    .HasForeignKey(d => d.InquiryType)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_t_ContactUs_t_Inquiry");

//                entity.HasOne(d => d.StatusNavigation)
//                    .WithMany(p => p.TContactUs)
//                    .HasForeignKey(d => d.Status)
//                    .HasConstraintName("FK_t_ContactUs_t_Status");
//            });

//            modelBuilder.Entity<TCounty>(entity =>
//            {
//                entity.HasKey(e => e.Pkid);

//                entity.ToTable("t_County");

//                entity.Property(e => e.Pkid).HasColumnName("PKID");

//                entity.Property(e => e.RegionId).HasColumnName("RegionID");
//            });

//            modelBuilder.Entity<TGroupLifeBeneficiaries>(entity =>
//            {
//                entity.ToTable("t_GroupLifeBeneficiaries");

//                entity.Property(e => e.Id).ValueGeneratedNever();

//                entity.Property(e => e.EmailAddress)
//                    .HasMaxLength(300)
//                    .IsUnicode(false);

//                entity.Property(e => e.FirstName)
//                    .HasMaxLength(300)
//                    .IsUnicode(false);

//                entity.Property(e => e.LastName)
//                    .HasMaxLength(300)
//                    .IsUnicode(false);

//                entity.Property(e => e.PhoneNumber)
//                    .HasMaxLength(300)
//                    .IsUnicode(false);

//                entity.Property(e => e.Relationship)
//                    .HasMaxLength(300)
//                    .IsUnicode(false);

//                entity.HasOne(d => d.InsuranceCover)
//                    .WithMany(p => p.TGroupLifeBeneficiaries)
//                    .HasForeignKey(d => d.InsuranceCoverId)
//                    .HasConstraintName("FK_t_GroupLifeBeneficiaries_t_InsuranceCovers");
//            });

//            modelBuilder.Entity<THospital>(entity =>
//            {
//                entity.HasKey(e => e.Pkid);

//                entity.ToTable("t_Hospital");

//                entity.Property(e => e.Pkid)
//                    .HasColumnName("PKID")
//                    .ValueGeneratedNever();

//                entity.Property(e => e.Name).IsRequired();
//            });

//            modelBuilder.Entity<TInquiry>(entity =>
//            {
//                entity.HasKey(e => e.Pkid);

//                entity.ToTable("t_Inquiry");

//                entity.Property(e => e.Pkid).HasColumnName("PKID");

//                entity.Property(e => e.CreateDate).HasColumnType("datetime");

//                entity.Property(e => e.InquiryDesc).IsRequired();
//            });

//            modelBuilder.Entity<TInsuranceCoverApprovals>(entity =>
//            {
//                entity.ToTable("t_InsuranceCoverApprovals");

//                entity.Property(e => e.Id).ValueGeneratedNever();

//                entity.Property(e => e.ApprovalDate).HasColumnType("datetime");

//                entity.Property(e => e.Remarks).HasMaxLength(300);

//                entity.Property(e => e.RequestDate).HasColumnType("datetime");

//                entity.HasOne(d => d.InsuranceCover)
//                    .WithMany(p => p.TInsuranceCoverApprovals)
//                    .HasForeignKey(d => d.InsuranceCoverId)
//                    .HasConstraintName("FK_t_InsuranceCoverApprovals_t_InsuranceCovers");
//            });

//            modelBuilder.Entity<TInsuranceCoverTypeBenefits>(entity =>
//            {
//                entity.ToTable("t_InsuranceCoverTypeBenefits");

//                entity.Property(e => e.Id).ValueGeneratedNever();

//                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

//                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

//                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

//                entity.HasOne(d => d.Benefit)
//                    .WithMany(p => p.TInsuranceCoverTypeBenefits)
//                    .HasForeignKey(d => d.BenefitId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_dbo.t_InsuranceCoverTypeBenefits_dbo.t_Benefits_BenefitId");

//                entity.HasOne(d => d.InsuranceCoverType)
//                    .WithMany(p => p.TInsuranceCoverTypeBenefits)
//                    .HasForeignKey(d => d.InsuranceCoverTypeId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_dbo.t_InsuranceCoverTypeBenefits_dbo.t_InsuranceCoverTypes_InsuranceCoverTypeId");
//            });

//            modelBuilder.Entity<TInsuranceCoverTypes>(entity =>
//            {
//                entity.ToTable("t_InsuranceCoverTypes");

//                entity.Property(e => e.Id).ValueGeneratedNever();

//                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

//                entity.Property(e => e.Status).HasColumnName("status");

//                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
//            });

//            modelBuilder.Entity<TInsuranceCovers>(entity =>
//            {
//                entity.ToTable("t_InsuranceCovers");

//                entity.Property(e => e.Id).ValueGeneratedNever();

//                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

//                entity.Property(e => e.Authorizer).HasMaxLength(50);

//                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

//                entity.Property(e => e.EmailAddress)
//                    .HasMaxLength(300)
//                    .IsUnicode(false);

//                entity.Property(e => e.EndDate).HasColumnType("datetime");

//                entity.Property(e => e.InsuranceCompany).HasMaxLength(300);

//                entity.Property(e => e.InsurancePolicyDocument).HasMaxLength(300);

//                entity.Property(e => e.Name)
//                    .HasMaxLength(300)
//                    .IsUnicode(false);

//                entity.Property(e => e.PhoneNumber)
//                    .HasMaxLength(300)
//                    .IsUnicode(false);

//                entity.Property(e => e.PolicyNumber).HasMaxLength(50);

//                entity.Property(e => e.StartDate).HasColumnType("datetime");

//                entity.HasOne(d => d.Agent)
//                    .WithMany(p => p.TInsuranceCovers)
//                    .HasForeignKey(d => d.AgentId)
//                    .HasConstraintName("FK_t_InsuranceCovers_t_Agents");

//                entity.HasOne(d => d.Beneficiary)
//                    .WithMany(p => p.TInsuranceCovers)
//                    .HasForeignKey(d => d.BeneficiaryId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_t_InsuranceCovers_t_Beneficiaries");

//                entity.HasOne(d => d.InsuranceCoverType)
//                    .WithMany(p => p.TInsuranceCovers)
//                    .HasForeignKey(d => d.InsuranceCoverTypeId)
//                    .HasConstraintName("FK_t_InsuranceCovers_t_InsuranceCoverTypes");

//                entity.HasOne(d => d.Insurance)
//                    .WithMany(p => p.TInsuranceCovers)
//                    .HasForeignKey(d => d.InsuranceId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_t_InsuranceCovers_t_InsuranceCovers");

//                entity.HasOne(d => d.Scheme)
//                    .WithMany(p => p.TInsuranceCovers)
//                    .HasForeignKey(d => d.SchemeId)
//                    .HasConstraintName("FK_t_InsuranceCovers_t_Scheme");
//            });

//            modelBuilder.Entity<TInsurances>(entity =>
//            {
//                entity.ToTable("t_Insurances");

//                entity.Property(e => e.Id).ValueGeneratedNever();

//                entity.Property(e => e.Description)
//                    .HasMaxLength(300)
//                    .IsUnicode(false);

//                entity.Property(e => e.Name)
//                    .IsRequired()
//                    .HasMaxLength(300)
//                    .IsUnicode(false);
//            });

//            modelBuilder.Entity<TJobGroup>(entity =>
//            {
//                entity.HasKey(e => e.Pkid);

//                entity.ToTable("t_JobGroup");

//                entity.Property(e => e.Pkid)
//                    .HasColumnName("PKID")
//                    .ValueGeneratedNever();

//                entity.Property(e => e.Name).IsRequired();
//            });

//            modelBuilder.Entity<TJobGroupLimits>(entity =>
//            {
//                entity.HasKey(e => e.Pkid);

//                entity.ToTable("t_JobGroupLimits");

//                entity.Property(e => e.Pkid).HasColumnName("PKID");

//                entity.Property(e => e.JobGroupId).HasColumnName("JobGroupID");
//            });

//            modelBuilder.Entity<TLastExpenseBeneficiaries>(entity =>
//            {
//                entity.ToTable("t_LastExpenseBeneficiaries");

//                entity.Property(e => e.Id).ValueGeneratedNever();

//                entity.Property(e => e.EmailAddress)
//                    .HasMaxLength(300)
//                    .IsUnicode(false);

//                entity.Property(e => e.FirstName)
//                    .HasMaxLength(300)
//                    .IsUnicode(false);

//                entity.Property(e => e.LastName)
//                    .HasMaxLength(300)
//                    .IsUnicode(false);

//                entity.Property(e => e.PhoneNumber)
//                    .HasMaxLength(300)
//                    .IsUnicode(false);

//                entity.Property(e => e.Relationship)
//                    .HasMaxLength(300)
//                    .IsUnicode(false);

//                entity.HasOne(d => d.InsuranceCover)
//                    .WithMany(p => p.TLastExpenseBeneficiaries)
//                    .HasForeignKey(d => d.InsuranceCoverId)
//                    .HasConstraintName("FK_t_LastExpenseBeneficiaries_t_InsuranceCovers");
//            });

//            modelBuilder.Entity<TLeads>(entity =>
//            {
//                entity.ToTable("t_Leads");

//                entity.Property(e => e.Id).ValueGeneratedNever();

//                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

//                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

//                entity.Property(e => e.EmailAddress).HasMaxLength(50);

//                entity.Property(e => e.FirstName).HasMaxLength(50);

//                entity.Property(e => e.Gender).HasMaxLength(50);

//                entity.Property(e => e.LastName).HasMaxLength(50);

//                entity.Property(e => e.NationalId)
//                    .HasColumnName("NationalID")
//                    .HasMaxLength(50);

//                entity.Property(e => e.PhoneNumber).HasMaxLength(50);

//                entity.Property(e => e.PostalAddress).HasMaxLength(50);

//                entity.Property(e => e.PostalCode).HasMaxLength(50);
//            });

//            modelBuilder.Entity<TLog>(entity =>
//            {
//                entity.HasKey(e => e.Pkid);

//                entity.ToTable("t_Log");

//                entity.Property(e => e.Pkid).HasColumnName("PKID");

//                entity.Property(e => e.Browser).IsRequired();

//                entity.Property(e => e.BrowserType).IsRequired();

//                entity.Property(e => e.Crawler).IsRequired();

//                entity.Property(e => e.Method).IsRequired();

//                entity.Property(e => e.Path).IsRequired();

//                entity.Property(e => e.RawUrl).IsRequired();

//                entity.Property(e => e.Referrer).IsRequired();

//                entity.Property(e => e.SessionId)
//                    .IsRequired()
//                    .HasColumnName("SessionID")
//                    .HasMaxLength(255);

//                entity.Property(e => e.Time).IsRequired();

//                entity.Property(e => e.UserAgent).IsRequired();

//                entity.Property(e => e.UserId)
//                    .IsRequired()
//                    .HasColumnName("UserID")
//                    .HasMaxLength(500);

//                entity.Property(e => e.Version).IsRequired();
//            });

//            modelBuilder.Entity<TMedicalCoverProductLimits>(entity =>
//            {
//                entity.ToTable("t_MedicalCoverProductLimits");

//                entity.Property(e => e.Id).ValueGeneratedNever();

//                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

//                entity.HasOne(d => d.Beneficiary)
//                    .WithMany(p => p.TMedicalCoverProductLimits)
//                    .HasForeignKey(d => d.BeneficiaryId)
//                    .HasConstraintName("FK_t_MedicalCoverProductLimits_t_Beneficiaries");

//                entity.HasOne(d => d.Benefit)
//                    .WithMany(p => p.TMedicalCoverProductLimits)
//                    .HasForeignKey(d => d.BenefitId)
//                    .HasConstraintName("FK_t_MedicalCoverProductLimits_t_Benefits");

//                entity.HasOne(d => d.InsuranceCoverType)
//                    .WithMany(p => p.TMedicalCoverProductLimits)
//                    .HasForeignKey(d => d.InsuranceCoverTypeId)
//                    .HasConstraintName("FK_t_MedicalCoverProductLimits_t_InsuranceCoverTypes");

//                entity.HasOne(d => d.MedicalCoverProduct)
//                    .WithMany(p => p.TMedicalCoverProductLimits)
//                    .HasForeignKey(d => d.MedicalCoverProductId)
//                    .HasConstraintName("FK_t_MedicalCoverProductLimits_t_MedicalCoverProducts");
//            });

//            modelBuilder.Entity<TMedicalCoverProducts>(entity =>
//            {
//                entity.ToTable("t_MedicalCoverProducts");

//                entity.Property(e => e.Id).ValueGeneratedNever();

//                entity.Property(e => e.Code).HasMaxLength(50);

//                entity.Property(e => e.Description).HasMaxLength(50);

//                entity.Property(e => e.Name)
//                    .IsRequired()
//                    .HasMaxLength(50);
//            });

//            modelBuilder.Entity<TMemberDocuments>(entity =>
//            {
//                entity.ToTable("t_MemberDocuments");

//                entity.Property(e => e.Id).ValueGeneratedNever();

//                entity.Property(e => e.FileAttachment)
//                    .HasMaxLength(300)
//                    .IsUnicode(false);

//                entity.HasOne(d => d.Member)
//                    .WithMany(p => p.TMemberDocuments)
//                    .HasForeignKey(d => d.MemberId)
//                    .HasConstraintName("FK_t_MemberDocuments_t_Members");
//            });

//            modelBuilder.Entity<TMemberInsuranceCovers>(entity =>
//            {
//                entity.ToTable("t_MemberInsuranceCovers");

//                entity.Property(e => e.Id).ValueGeneratedNever();

//                entity.HasOne(d => d.InsuranceCover)
//                    .WithMany(p => p.TMemberInsuranceCovers)
//                    .HasForeignKey(d => d.InsuranceCoverId)
//                    .HasConstraintName("FK_t_MemberInsuranceCovers_t_InsuranceCovers");

//                entity.HasOne(d => d.Member)
//                    .WithMany(p => p.TMemberInsuranceCovers)
//                    .HasForeignKey(d => d.MemberId)
//                    .HasConstraintName("FK_t_MemberInsuranceCovers_t_Members");
//            });

//            modelBuilder.Entity<TMembers>(entity =>
//            {
//                entity.ToTable("t_Members");

//                entity.Property(e => e.Id).ValueGeneratedNever();

//                entity.Property(e => e.AddressCity).HasColumnName("Address_City");

//                entity.Property(e => e.AddressEmail).HasColumnName("Address_Email");

//                entity.Property(e => e.AddressLandLine).HasColumnName("Address_LandLine");

//                entity.Property(e => e.AddressMobileNumber).HasColumnName("Address_MobileNumber");

//                entity.Property(e => e.AddressPhysical).HasColumnName("Address_Physical");

//                entity.Property(e => e.AddressPostal).HasColumnName("Address_Postal");

//                entity.Property(e => e.AddressPostalCode).HasColumnName("Address_PostalCode");

//                entity.Property(e => e.AddressStreet).HasColumnName("Address_Street");

//                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

//                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

//                entity.Property(e => e.Gender)
//                    .HasMaxLength(50)
//                    .IsUnicode(false);

//                entity.Property(e => e.IdNumber)
//                    .HasMaxLength(50)
//                    .IsUnicode(false);

//                entity.Property(e => e.Relationship).HasMaxLength(50);

//                entity.Property(e => e.Remarks)
//                    .HasMaxLength(300)
//                    .IsUnicode(false);

//                entity.Property(e => e.SuspensionReasons)
//                    .HasMaxLength(3000)
//                    .IsUnicode(false);

//                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

//                entity.HasOne(d => d.Agent)
//                    .WithMany(p => p.TMembers)
//                    .HasForeignKey(d => d.AgentId)
//                    .HasConstraintName("FK_t_Members_t_Agents");

//                entity.HasOne(d => d.Parent)
//                    .WithMany(p => p.InverseParent)
//                    .HasForeignKey(d => d.ParentId)
//                    .HasConstraintName("FK_dbo.t_Members_dbo.t_Members_ParentId");
//            });

//            modelBuilder.Entity<TNewsLetter>(entity =>
//            {
//                entity.HasKey(e => e.Pkid);

//                entity.ToTable("t_NewsLetter");

//                entity.Property(e => e.Pkid).HasColumnName("PKID");

//                entity.Property(e => e.CreateDate).HasColumnType("datetime");

//                entity.Property(e => e.Email).IsRequired();
//            });

//            modelBuilder.Entity<TNextOfKins>(entity =>
//            {
//                entity.ToTable("t_NextOfKins");

//                entity.Property(e => e.Id).ValueGeneratedNever();

//                entity.Property(e => e.EmailAddress).HasMaxLength(50);

//                entity.Property(e => e.Name).HasMaxLength(50);

//                entity.Property(e => e.PhoneNumber).HasMaxLength(50);

//                entity.Property(e => e.Relationship).HasMaxLength(50);

//                entity.HasOne(d => d.InsuranceCover)
//                    .WithMany(p => p.TNextOfKins)
//                    .HasForeignKey(d => d.InsuranceCoverId)
//                    .HasConstraintName("FK_t_NextOfKins_t_InsuranceCovers");
//            });

//            modelBuilder.Entity<TNominee>(entity =>
//            {
//                entity.HasKey(e => e.Pkid);

//                entity.ToTable("t_Nominee");

//                entity.Property(e => e.Pkid).HasColumnName("PKID");

//                entity.Property(e => e.CreateDate).HasColumnType("datetime");

//                entity.Property(e => e.Dob)
//                    .HasColumnName("DOB")
//                    .HasColumnType("datetime");

//                entity.Property(e => e.NationalId).IsRequired();

//                entity.Property(e => e.NomineeName).IsRequired();

//                entity.Property(e => e.RegistrationId).HasColumnName("RegistrationID");

//                entity.Property(e => e.RelationshipId).HasColumnName("RelationshipID");
//            });

//            modelBuilder.Entity<TOrderPayments>(entity =>
//            {
//                entity.ToTable("t_OrderPayments");

//                entity.Property(e => e.Id).ValueGeneratedNever();

//                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

//                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

//                entity.Property(e => e.TransactionReference)
//                    .IsRequired()
//                    .HasMaxLength(300)
//                    .IsUnicode(false);

//                entity.HasOne(d => d.Order)
//                    .WithMany(p => p.TOrderPayments)
//                    .HasForeignKey(d => d.OrderId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_t_OrderPayments_t_Orders");
//            });

//            modelBuilder.Entity<TOrders>(entity =>
//            {
//                entity.ToTable("t_Orders");

//                entity.Property(e => e.Id).ValueGeneratedNever();

//                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

//                entity.Property(e => e.OrderDate).HasColumnType("datetime");

//                entity.Property(e => e.OrderNumber)
//                    .IsRequired()
//                    .HasMaxLength(300)
//                    .IsUnicode(false);

//                entity.Property(e => e.UserId).HasMaxLength(300);

//                entity.HasOne(d => d.Agent)
//                    .WithMany(p => p.TOrders)
//                    .HasForeignKey(d => d.AgentId)
//                    .HasConstraintName("FK_t_Orders_t_Agents");

//                entity.HasOne(d => d.InsuranceCover)
//                    .WithMany(p => p.TOrders)
//                    .HasForeignKey(d => d.InsuranceCoverId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_t_Orders_t_InsuranceCovers");
//            });

//            modelBuilder.Entity<TPromotion>(entity =>
//            {
//                entity.HasKey(e => e.Pkid);

//                entity.ToTable("t_Promotion");

//                entity.Property(e => e.Pkid).HasColumnName("PKID");

//                entity.Property(e => e.ClinicId).HasColumnName("ClinicID");

//                entity.Property(e => e.Date).HasColumnType("datetime");

//                entity.Property(e => e.PromotionCode).HasMaxLength(50);
//            });

//            modelBuilder.Entity<TRegion>(entity =>
//            {
//                entity.HasKey(e => e.Pkid);

//                entity.ToTable("t_Region");

//                entity.Property(e => e.Pkid).HasColumnName("PKID");

//                entity.Property(e => e.Region).IsRequired();
//            });

//            modelBuilder.Entity<TRegistration>(entity =>
//            {
//                entity.HasKey(e => e.Pkid);

//                entity.ToTable("t_Registration");

//                entity.Property(e => e.Pkid).HasColumnName("PKID");

//                entity.Property(e => e.Date).HasColumnType("datetime");

//                entity.Property(e => e.Email)
//                    .IsRequired()
//                    .HasMaxLength(500);

//                entity.Property(e => e.MemberNo)
//                    .IsRequired()
//                    .HasMaxLength(250);

//                entity.Property(e => e.Phone)
//                    .IsRequired()
//                    .HasMaxLength(250);

//                entity.Property(e => e.SchemeId).HasMaxLength(50);
//            });

//            modelBuilder.Entity<TRelation>(entity =>
//            {
//                entity.HasKey(e => e.Pkid);

//                entity.ToTable("t_Relation");

//                entity.Property(e => e.Pkid)
//                    .HasColumnName("PKID")
//                    .ValueGeneratedNever();

//                entity.Property(e => e.Relation).IsRequired();
//            });

//            modelBuilder.Entity<TScheme>(entity =>
//            {
//                entity.HasKey(e => e.Pkid);

//                entity.ToTable("t_Scheme");

//                entity.Property(e => e.Pkid).HasColumnName("PKID");

//                entity.Property(e => e.SchemeId).HasColumnName("SchemeID");
//            });

//            modelBuilder.Entity<TService>(entity =>
//            {
//                entity.HasKey(e => e.Pkid);

//                entity.ToTable("t_Service");

//                entity.Property(e => e.Pkid).HasColumnName("PKID");

//                entity.Property(e => e.Service).IsRequired();
//            });

//            modelBuilder.Entity<TStatus>(entity =>
//            {
//                entity.HasKey(e => e.Pkid);

//                entity.ToTable("t_Status");

//                entity.Property(e => e.Pkid).HasColumnName("PKID");

//                entity.Property(e => e.CreateDate).HasColumnType("datetime");

//                entity.Property(e => e.Status)
//                    .IsRequired()
//                    .HasMaxLength(250);
//            });

//            modelBuilder.Entity<TSubCounty>(entity =>
//            {
//                entity.HasKey(e => e.Pkid);

//                entity.ToTable("t_SubCounty");

//                entity.Property(e => e.Pkid).HasColumnName("PKID");

//                entity.Property(e => e.SubCounty).IsRequired();
//            });

//            modelBuilder.Entity<TSurvey>(entity =>
//            {
//                entity.HasKey(e => e.Pkid);

//                entity.ToTable("t_Survey");

//                entity.Property(e => e.Pkid).HasColumnName("PKID");

//                entity.Property(e => e.CreateDate).HasColumnType("datetime");

//                entity.Property(e => e.Email).HasMaxLength(250);

//                entity.Property(e => e.Name).HasMaxLength(500);

//                entity.Property(e => e.Phone).HasMaxLength(100);

//                entity.Property(e => e.Result)
//                    .IsRequired()
//                    .HasMaxLength(150);
//            });

//            modelBuilder.Entity<TSurveyDetail>(entity =>
//            {
//                entity.HasKey(e => e.Pkid);

//                entity.ToTable("t_SurveyDetail");

//                entity.Property(e => e.Pkid).HasColumnName("PKID");

//                entity.Property(e => e.AnswerId).HasColumnName("AnswerID");

//                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

//                entity.Property(e => e.SurveyId).HasColumnName("SurveyID");

//                entity.HasOne(d => d.Survey)
//                    .WithMany(p => p.TSurveyDetail)
//                    .HasForeignKey(d => d.SurveyId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK_t_SurveyDetail_t_Survey");
//            });

//            OnModelCreatingPartial(modelBuilder);
//        }

//        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//    }
//}
