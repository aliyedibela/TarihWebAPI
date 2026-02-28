using Microsoft.EntityFrameworkCore;
using TarihWebAPI.ApplicationAndDomain.Entities;
using TarihWebAPI.ApplicationAndDomain.Relationships;

namespace TarihWebAPI.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // =============================================
        // 1. TEMEL TANIMLAR VE SINIFLANDIRMALAR
        // =============================================
        public DbSet<Religion> Religions { get; set; }
        public DbSet<Sect> Sects { get; set; }
        public DbSet<Dynasty> Dynasties { get; set; }
        public DbSet<Era> Eras { get; set; }
        public DbSet<Period> Periods { get; set; }

        // =============================================
        // 2. ANA VARLIKLAR
        // =============================================
        public DbSet<State> States { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<HistoricalEvent> HistoricalEvents { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Treaty> Treaties { get; set; }
        public DbSet<TradeRoute> TradeRoutes { get; set; }

        // =============================================
        // 3. İLİŞKİ TABLOLARI
        // =============================================
        public DbSet<PersonStateRelation> PersonStateRelations { get; set; }
        public DbSet<StateDynastyRelation> StateDynastyRelations { get; set; }
        public DbSet<EventStateParticipant> EventStateParticipants { get; set; }
        public DbSet<EventPersonParticipant> EventPersonParticipants { get; set; }
        public DbSet<PersonRelation> PersonRelations { get; set; }
        public DbSet<PersonOccupation> PersonOccupations { get; set; }

        // =============================================
        // 4. DİN DETAY TABLOLARI
        // =============================================
        public DbSet<ReligionHolyBook> ReligionHolyBooks { get; set; }
        public DbSet<ReligionHolyCity> ReligionHolyCities { get; set; }
        public DbSet<ReligionSubBranch> ReligionSubBranches { get; set; }

        // =============================================
        // 5. DEVLET DETAY TABLOLARI
        // =============================================
        public DbSet<StateOfficialLanguage> StateOfficialLanguages { get; set; }
        public DbSet<PopulationData> PopulationData { get; set; }

        // =============================================
        // 6. ANTLAŞMA DETAY TABLOLARI
        // =============================================
        public DbSet<TreatyArticle> TreatyArticles { get; set; }
        public DbSet<TreatySignatory> TreatySignatories { get; set; }

        // =============================================
        // 7. TİCARET YOLU TABLOLARI
        // =============================================
        public DbSet<TradeRouteParticipant> TradeRouteParticipants { get; set; }
        public DbSet<TradeRouteWaypoint> TradeRouteWaypoints { get; set; }

        // =============================================
        // 8. ESER DETAY TABLOLARI
        // =============================================
        public DbSet<WorkContributor> WorkContributors { get; set; }

        // =============================================
        // 9. KULLANICI TABLOLARI
        // =============================================
        public DbSet<User> Users { get; set; }
        public DbSet<UserInteraction> UserInteractions { get; set; }

        // =============================================
        // UpdatedAt otomatik güncelleme
        // =============================================
        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Modified)
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("postgis");
            base.OnModelCreating(modelBuilder);

            // ==========================================
            // 1. LOCATION İLİŞKİLERİ
            // ==========================================

            // Location -> ParentLocation (Self-referencing)
            modelBuilder.Entity<Location>()
                .HasOne(l => l.ParentLocation)
                .WithMany(l => l.SubLocations)
                .HasForeignKey(l => l.ParentLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Location -> BornPersons (One-to-Many)
            modelBuilder.Entity<Person>()
                .HasOne(p => p.BirthLocation)
                .WithMany(l => l.BornPersons)
                .HasForeignKey(p => p.BirthLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Location -> DiedPersons (One-to-Many)
            modelBuilder.Entity<Person>()
                .HasOne(p => p.DeathLocation)
                .WithMany(l => l.DiedPersons)
                .HasForeignKey(p => p.DeathLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Location -> HistoricalEvents (One-to-Many)
            modelBuilder.Entity<HistoricalEvent>()
                .HasOne(he => he.Location)
                .WithMany(l => l.Events)
                .HasForeignKey(he => he.LocationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Location -> Treaties (SigningLocation)
            modelBuilder.Entity<Treaty>()
                .HasOne(t => t.SigningLocation)
                .WithMany()
                .HasForeignKey(t => t.SigningLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Location -> TradeRouteWaypoints (One-to-Many)
            modelBuilder.Entity<TradeRouteWaypoint>()
                .HasOne(w => w.Location)
                .WithMany(l => l.TradeRouteWaypoints)
                .HasForeignKey(w => w.LocationId)
                .OnDelete(DeleteBehavior.Restrict);

            // ==========================================
            // 2. RELIGION İLİŞKİLERİ
            // ==========================================

            // Religion -> Sects (One-to-Many)
            modelBuilder.Entity<Sect>()
                .HasOne(s => s.Religion)
                .WithMany(r => r.Sects)
                .HasForeignKey(s => s.ReligionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Religion -> Persons (One-to-Many)
            modelBuilder.Entity<Person>()
                .HasOne(p => p.Religion)
                .WithMany(r => r.Persons)
                .HasForeignKey(p => p.ReligionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Religion -> Works/ReligiousWorks (One-to-Many)
            modelBuilder.Entity<Work>()
                .HasOne(w => w.Religion)
                .WithMany(r => r.ReligiousWorks)
                .HasForeignKey(w => w.ReligionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Religion -> HolyBooks (One-to-Many)
            modelBuilder.Entity<ReligionHolyBook>()
                .HasOne(hb => hb.Religion)
                .WithMany(r => r.HolyBooks)
                .HasForeignKey(hb => hb.ReligionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Religion -> HolyCities (One-to-Many)
            modelBuilder.Entity<ReligionHolyCity>()
                .HasOne(hc => hc.Religion)
                .WithMany(r => r.HolyCities)
                .HasForeignKey(hc => hc.ReligionId)
                .OnDelete(DeleteBehavior.Cascade);

            // ReligionHolyCity -> Location (nullable FK)
            modelBuilder.Entity<ReligionHolyCity>()
                .HasOne(hc => hc.Location)
                .WithMany()
                .HasForeignKey(hc => hc.LocationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Religion -> SubBranches (One-to-Many)
            modelBuilder.Entity<ReligionSubBranch>()
                .HasOne(sb => sb.Religion)
                .WithMany(r => r.SubBranches)
                .HasForeignKey(sb => sb.ReligionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Religion -> FounderPerson (nullable FK)
            // HATA NOTU: Religion.FounderPersonId → Person; ancak Person.ReligionId → Religion FK'sı da var.
            // Bu çevrimsel bağımlılık oluşturur. Çözüm: bu ilişkiyi WithMany() bırakıp nav prop üzerinden takip etmek.
            modelBuilder.Entity<Religion>()
                .HasOne(r => r.FounderPerson)
                .WithMany()
                .HasForeignKey(r => r.FounderPersonId)
                .OnDelete(DeleteBehavior.Restrict);

            // Religion -> FoundedLocation (nullable FK)
            modelBuilder.Entity<Religion>()
                .HasOne(r => r.FoundedLocation)
                .WithMany()
                .HasForeignKey(r => r.FoundedLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            // ==========================================
            // 3. SECT İLİŞKİLERİ
            // ==========================================

            // Sect -> Persons (One-to-Many)
            modelBuilder.Entity<Person>()
                .HasOne(p => p.Sect)
                .WithMany(s => s.Persons)
                .HasForeignKey(p => p.SectId)
                .OnDelete(DeleteBehavior.Restrict);

            // ==========================================
            // 4. DYNASTY İLİŞKİLERİ
            // ==========================================

            // Dynasty -> Persons (One-to-Many)
            modelBuilder.Entity<Person>()
                .HasOne(p => p.Dynasty)
                .WithMany(d => d.Persons)
                .HasForeignKey(p => p.DynastyId)
                .OnDelete(DeleteBehavior.Restrict);

            // Dynasty -> FounderPerson (nullable FK)
            // Person.DynastyId → Dynasty zaten var; çevrimsel döngüyü önlemek için WithMany() boş bırakılır.
            modelBuilder.Entity<Dynasty>()
                .HasOne(d => d.FounderPerson)
                .WithMany()
                .HasForeignKey(d => d.FounderPersonId)
                .OnDelete(DeleteBehavior.Restrict);

            // Dynasty -> StateDynastyRelations (One-to-Many)
            modelBuilder.Entity<StateDynastyRelation>()
                .HasOne(sdr => sdr.Dynasty)
                .WithMany(d => d.StateDynastyRelations)
                .HasForeignKey(sdr => sdr.DynastyId)
                .OnDelete(DeleteBehavior.Restrict);

            // ==========================================
            // 5. ERA İLİŞKİLERİ
            // ==========================================

            // Era -> Periods (One-to-Many)
            modelBuilder.Entity<Period>()
                .HasOne(p => p.Era)
                .WithMany(e => e.Periods)
                .HasForeignKey(p => p.EraId)
                .OnDelete(DeleteBehavior.Restrict);

            // Era -> HistoricalEvents (One-to-Many)
            modelBuilder.Entity<HistoricalEvent>()
                .HasOne(he => he.Era)
                .WithMany(e => e.Events)
                .HasForeignKey(he => he.EraId)
                .OnDelete(DeleteBehavior.Restrict);

            // Era -> Persons (One-to-Many)
            modelBuilder.Entity<Person>()
                .HasOne(p => p.Era)
                .WithMany(e => e.Persons)
                .HasForeignKey(p => p.EraId)
                .OnDelete(DeleteBehavior.Restrict);

            // Era -> Treaties (One-to-Many)
            modelBuilder.Entity<Treaty>()
                .HasOne(t => t.Era)
                .WithMany(e => e.Treaties)
                .HasForeignKey(t => t.EraId)
                .OnDelete(DeleteBehavior.Restrict);

            // ==========================================
            // 6. PERIOD İLİŞKİLERİ
            // ==========================================

            // Period -> State (Many-to-One)
            modelBuilder.Entity<Period>()
                .HasOne(p => p.State)
                .WithMany(s => s.Periods)
                .HasForeignKey(p => p.StateId)
                .OnDelete(DeleteBehavior.Restrict);

            // Period -> HistoricalEvents (One-to-Many)
            modelBuilder.Entity<HistoricalEvent>()
                .HasOne(he => he.Period)
                .WithMany(p => p.Events)
                .HasForeignKey(he => he.PeriodId)
                .OnDelete(DeleteBehavior.Restrict);

            // Period -> Persons (One-to-Many)
            modelBuilder.Entity<Person>()
                .HasOne(p => p.Period)
                .WithMany(per => per.Persons)
                .HasForeignKey(p => p.PeriodId)
                .OnDelete(DeleteBehavior.Restrict);

            // Period -> Treaties (One-to-Many)
            modelBuilder.Entity<Treaty>()
                .HasOne(t => t.Period)
                .WithMany(p => p.Treaties)
                .HasForeignKey(t => t.PeriodId)
                .OnDelete(DeleteBehavior.Restrict);

            // ==========================================
            // 7. STATE İLİŞKİLERİ
            // ==========================================

            // State -> OfficialReligion (Many-to-One)
            modelBuilder.Entity<State>()
                .HasOne(s => s.OfficialReligion)
                .WithMany()
                .HasForeignKey(s => s.OfficialReligionId)
                .OnDelete(DeleteBehavior.Restrict);

            // State -> CapitalLocation (Many-to-One)
            modelBuilder.Entity<State>()
                .HasOne(s => s.CapitalLocation)
                .WithMany()
                .HasForeignKey(s => s.CapitalLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            // State -> OfficialLanguages (One-to-Many)
            modelBuilder.Entity<StateOfficialLanguage>()
                .HasOne(sl => sl.State)
                .WithMany(s => s.OfficialLanguages)
                .HasForeignKey(sl => sl.StateId)
                .OnDelete(DeleteBehavior.Cascade);

            // State -> PersonStateRelations (One-to-Many)
            modelBuilder.Entity<PersonStateRelation>()
                .HasOne(psr => psr.State)
                .WithMany(s => s.PersonStateRelations)
                .HasForeignKey(psr => psr.StateId)
                .OnDelete(DeleteBehavior.Restrict);

            // State -> EventStateParticipants (One-to-Many)
            modelBuilder.Entity<EventStateParticipant>()
                .HasOne(esp => esp.State)
                .WithMany(s => s.EventParticipations)
                .HasForeignKey(esp => esp.StateId)
                .OnDelete(DeleteBehavior.Restrict);

            // State -> PopulationData (One-to-Many)
            modelBuilder.Entity<PopulationData>()
                .HasOne(pd => pd.State)
                .WithMany(s => s.PopulationData)
                .HasForeignKey(pd => pd.StateId)
                .OnDelete(DeleteBehavior.Restrict);

            // State -> StateDynastyRelations (One-to-Many)
            modelBuilder.Entity<StateDynastyRelation>()
                .HasOne(sdr => sdr.State)
                .WithMany(s => s.StateDynastyRelations)
                .HasForeignKey(sdr => sdr.StateId)
                .OnDelete(DeleteBehavior.Restrict);

            // State -> TreatySignatories (nullable FK — devlet olmayan imzacılar için)
            modelBuilder.Entity<TreatySignatory>()
                .HasOne(ts => ts.State)
                .WithMany()
                .HasForeignKey(ts => ts.StateId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // State -> TradeRouteParticipants (nullable FK — bireysel katılımcılar için)
            modelBuilder.Entity<TradeRouteParticipant>()
                .HasOne(trp => trp.State)
                .WithMany(s => s.TradeRouteParticipations)
                .HasForeignKey(trp => trp.StateId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // ==========================================
            // 8. PERSON İLİŞKİLERİ
            // ==========================================

            // Person -> PersonOccupations (One-to-Many)
            modelBuilder.Entity<PersonOccupation>()
                .HasOne(po => po.Person)
                .WithMany(p => p.Occupations)
                .HasForeignKey(po => po.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            // Person -> PersonRelations (Self-referencing, çift taraflı)
            // "PersonId" tarafı: bu kişinin başlattığı ilişkiler
            modelBuilder.Entity<PersonRelation>()
                .HasOne(pr => pr.Person)
                .WithMany(p => p.Relations)
                .HasForeignKey(pr => pr.PersonId)
                .OnDelete(DeleteBehavior.Restrict);

            // "RelatedPersonId" tarafı: bu kişinin karşı taraf olduğu ilişkiler
            modelBuilder.Entity<PersonRelation>()
                .HasOne(pr => pr.RelatedPerson)
                .WithMany(p => p.InverseRelations)
                .HasForeignKey(pr => pr.RelatedPersonId)
                .OnDelete(DeleteBehavior.Restrict);

            // Person -> PersonStateRelations (One-to-Many)
            modelBuilder.Entity<PersonStateRelation>()
                .HasOne(psr => psr.Person)
                .WithMany(p => p.StateRelations)
                .HasForeignKey(psr => psr.PersonId)
                .OnDelete(DeleteBehavior.Restrict);

            // Person -> EventPersonParticipants (One-to-Many)
            modelBuilder.Entity<EventPersonParticipant>()
                .HasOne(epp => epp.Person)
                .WithMany(p => p.EventParticipations)
                .HasForeignKey(epp => epp.PersonId)
                .OnDelete(DeleteBehavior.Restrict);

            // Person -> Works (Author, One-to-Many)
            // DÜZELTME: Nav prop adı Works'ten AuthoredWorks'e değişti
            modelBuilder.Entity<Work>()
                .HasOne(w => w.Author)
                .WithMany(p => p.AuthoredWorks)
                .HasForeignKey(w => w.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Person -> WorkContributors (One-to-Many)
            modelBuilder.Entity<WorkContributor>()
                .HasOne(wc => wc.Person)
                .WithMany(p => p.WorkContributions)
                .HasForeignKey(wc => wc.PersonId)
                .OnDelete(DeleteBehavior.Restrict);

            // Person -> TreatySignatories (SignatoryPerson, nullable FK)
            modelBuilder.Entity<TreatySignatory>()
                .HasOne(ts => ts.SignatoryPerson)
                .WithMany()
                .HasForeignKey(ts => ts.SignatoryPersonId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // Person -> TradeRouteParticipants (nullable FK)
            modelBuilder.Entity<TradeRouteParticipant>()
                .HasOne(trp => trp.Person)
                .WithMany()
                .HasForeignKey(trp => trp.PersonId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // ==========================================
            // 9. HISTORICAL EVENT İLİŞKİLERİ
            // ==========================================

            // HistoricalEvent -> EventStateParticipants (One-to-Many)
            modelBuilder.Entity<EventStateParticipant>()
                .HasOne(esp => esp.Event)
                .WithMany(he => he.StateParticipants)
                .HasForeignKey(esp => esp.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            // HistoricalEvent -> EventPersonParticipants (One-to-Many)
            modelBuilder.Entity<EventPersonParticipant>()
                .HasOne(epp => epp.Event)
                .WithMany(he => he.PersonParticipants)
                .HasForeignKey(epp => epp.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            // HistoricalEvent -> Treaties (RelatedEvent, nullable FK)
            modelBuilder.Entity<Treaty>()
                .HasOne(t => t.RelatedEvent)
                .WithMany()
                .HasForeignKey(t => t.RelatedEventId)
                .OnDelete(DeleteBehavior.Restrict);

            // HistoricalEvent -> Works (RelatedEvent, nullable FK)
            modelBuilder.Entity<Work>()
                .HasOne(w => w.RelatedEvent)
                .WithMany()
                .HasForeignKey(w => w.RelatedEventId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<HistoricalEvent>()
                .HasOne(e => e.ParentEvent)
                .WithMany(e => e.SubEvents)
                .HasForeignKey(e => e.ParentEventId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict); 

            // Index
            modelBuilder.Entity<HistoricalEvent>()
                .HasIndex(e => e.ParentEventId);

            // ==========================================
            // 10. TREATY İLİŞKİLERİ
            // ==========================================

            // Treaty -> TreatyArticles (One-to-Many, Cascade)
            modelBuilder.Entity<TreatyArticle>()
                .HasOne(ta => ta.Treaty)
                .WithMany(t => t.Articles)
                .HasForeignKey(ta => ta.TreatyId)
                .OnDelete(DeleteBehavior.Cascade);

            // Treaty -> TreatySignatories (One-to-Many, Cascade)
            modelBuilder.Entity<TreatySignatory>()
                .HasOne(ts => ts.Treaty)
                .WithMany(t => t.Signatories)
                .HasForeignKey(ts => ts.TreatyId)
                .OnDelete(DeleteBehavior.Cascade);

            // ==========================================
            // 11. TRADE ROUTE İLİŞKİLERİ
            // ==========================================

            // TradeRoute -> Participants (One-to-Many, Cascade)
            modelBuilder.Entity<TradeRouteParticipant>()
                .HasOne(trp => trp.TradeRoute)
                .WithMany(tr => tr.Participants)
                .HasForeignKey(trp => trp.TradeRouteId)
                .OnDelete(DeleteBehavior.Cascade);

            // TradeRoute -> Waypoints (One-to-Many, Cascade)
            modelBuilder.Entity<TradeRouteWaypoint>()
                .HasOne(w => w.TradeRoute)
                .WithMany(tr => tr.Waypoints)
                .HasForeignKey(w => w.TradeRouteId)
                .OnDelete(DeleteBehavior.Cascade);

            // TradeRoute -> StartLocation (nullable FK)
            modelBuilder.Entity<TradeRoute>()
                .HasOne(tr => tr.StartLocation)
                .WithMany()
                .HasForeignKey(tr => tr.StartLocationId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // TradeRoute -> EndLocation (nullable FK)
            modelBuilder.Entity<TradeRoute>()
                .HasOne(tr => tr.EndLocation)
                .WithMany()
                .HasForeignKey(tr => tr.EndLocationId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // ==========================================
            // 12. WORK İLİŞKİLERİ
            // ==========================================

            // Work -> WorkContributors (One-to-Many, Cascade)
            modelBuilder.Entity<WorkContributor>()
                .HasOne(wc => wc.Work)
                .WithMany(w => w.Contributors)
                .HasForeignKey(wc => wc.WorkId)
                .OnDelete(DeleteBehavior.Cascade);

            // ==========================================
            // 13. USER İLİŞKİLERİ
            // ==========================================

            // User -> UserInteractions (One-to-Many, Cascade)
            modelBuilder.Entity<UserInteraction>()
                .HasOne(ui => ui.User)
                .WithMany(u => u.Interactions)
                .HasForeignKey(ui => ui.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // ==========================================
            // 14. INDEXLER
            // ==========================================

            // --- Religion ---
            modelBuilder.Entity<Religion>()
                .HasIndex(r => r.Name).IsUnique();

            // --- Sect ---
            modelBuilder.Entity<Sect>()
                .HasIndex(s => new { s.ReligionId, s.Name });

            // --- Dynasty ---
            modelBuilder.Entity<Dynasty>()
                .HasIndex(d => d.Name);

            // --- Era ---
            modelBuilder.Entity<Era>()
                .HasIndex(e => e.Name).IsUnique();
            modelBuilder.Entity<Era>()
                .HasIndex(e => e.StartYear);

            // --- Period ---
            modelBuilder.Entity<Period>()
                .HasIndex(p => p.StateId);
            modelBuilder.Entity<Period>()
                .HasIndex(p => p.Name);
            modelBuilder.Entity<Period>()
                .HasIndex(p => p.StartYear);
            modelBuilder.Entity<Period>()
                .HasIndex(p => new { p.StateId, p.OrderNumber });

            // --- Location ---
            modelBuilder.Entity<Location>()
                .HasIndex(l => l.Name);
            modelBuilder.Entity<Location>()
                .HasIndex(l => new { l.Latitude, l.Longitude });
            modelBuilder.Entity<Location>()
                .HasIndex(l => l.ParentLocationId);

            // --- State ---
            modelBuilder.Entity<State>()
                .HasIndex(s => s.Name);
            modelBuilder.Entity<State>()
                .HasIndex(s => s.StartYear);

            // --- StateOfficialLanguage ---
            modelBuilder.Entity<StateOfficialLanguage>()
                .HasIndex(sl => sl.StateId);
            modelBuilder.Entity<StateOfficialLanguage>()
                .HasIndex(sl => new { sl.StateId, sl.Language }).IsUnique();

            // --- Person ---
            modelBuilder.Entity<Person>()
                .HasIndex(p => p.FullName);
            // DÜZELTME: p.Occupation artık yok — PersonOccupations tablosuna taşındı
            // Eski hatalı satır: .HasIndex(p => p.Occupation)  ← SİLİNDİ
            modelBuilder.Entity<Person>()
                .HasIndex(p => p.BirthDate);
            modelBuilder.Entity<Person>()
                .HasIndex(p => p.ReligionId);
            modelBuilder.Entity<Person>()
                .HasIndex(p => p.DynastyId);
            modelBuilder.Entity<Person>()
                .HasIndex(p => p.EraId);
            modelBuilder.Entity<Person>()
                .HasIndex(p => p.PeriodId);

            // --- PersonOccupation ---
            modelBuilder.Entity<PersonOccupation>()
                .HasIndex(po => po.PersonId);
            modelBuilder.Entity<PersonOccupation>()
                .HasIndex(po => po.Occupation);
            modelBuilder.Entity<PersonOccupation>()
                .HasIndex(po => new { po.PersonId, po.Occupation }).IsUnique();

            // --- PersonRelation ---
            modelBuilder.Entity<PersonRelation>()
                .HasIndex(pr => pr.PersonId);
            modelBuilder.Entity<PersonRelation>()
                .HasIndex(pr => pr.RelatedPersonId);
            modelBuilder.Entity<PersonRelation>()
                .HasIndex(pr => pr.RelationType);
            // Aynı iki kişi aynı tür ilişkiyle iki kez eklenemesin
            modelBuilder.Entity<PersonRelation>()
                .HasIndex(pr => new { pr.PersonId, pr.RelatedPersonId, pr.RelationType }).IsUnique();

            // --- PersonStateRelation ---
            modelBuilder.Entity<PersonStateRelation>()
                .HasIndex(psr => psr.PersonId);
            modelBuilder.Entity<PersonStateRelation>()
                .HasIndex(psr => psr.StateId);
            modelBuilder.Entity<PersonStateRelation>()
                .HasIndex(psr => new { psr.StateId, psr.OrderNumber });

            // --- HistoricalEvent ---
            modelBuilder.Entity<HistoricalEvent>()
                .HasIndex(he => he.Title);
            modelBuilder.Entity<HistoricalEvent>()
                .HasIndex(he => he.EventType);
            modelBuilder.Entity<HistoricalEvent>()
                .HasIndex(he => he.StartDate);
            modelBuilder.Entity<HistoricalEvent>()
                .HasIndex(he => he.StartYear);
            modelBuilder.Entity<HistoricalEvent>()
                .HasIndex(he => he.EraId);
            modelBuilder.Entity<HistoricalEvent>()
                .HasIndex(he => he.PeriodId);
            modelBuilder.Entity<HistoricalEvent>()
                .HasIndex(he => he.LocationId);

            // --- Work ---
            modelBuilder.Entity<Work>()
                .HasIndex(w => w.Title);
            modelBuilder.Entity<Work>()
                .HasIndex(w => w.WorkType);
            modelBuilder.Entity<Work>()
                .HasIndex(w => w.AuthorId);
            modelBuilder.Entity<Work>()
                .HasIndex(w => w.IsReligiousText);
            modelBuilder.Entity<Work>()
                .HasIndex(w => w.ReligionId);

            // --- WorkContributor ---
            modelBuilder.Entity<WorkContributor>()
                .HasIndex(wc => wc.WorkId);
            modelBuilder.Entity<WorkContributor>()
                .HasIndex(wc => wc.PersonId);
            modelBuilder.Entity<WorkContributor>()
                .HasIndex(wc => new { wc.WorkId, wc.PersonId, wc.Role }).IsUnique();

            // --- Treaty ---
            modelBuilder.Entity<Treaty>()
                .HasIndex(t => t.Name);
            modelBuilder.Entity<Treaty>()
                .HasIndex(t => t.SigningDate);
            modelBuilder.Entity<Treaty>()
                .HasIndex(t => t.SigningYear);
            modelBuilder.Entity<Treaty>()
                .HasIndex(t => t.TreatyType);
            modelBuilder.Entity<Treaty>()
                .HasIndex(t => t.EraId);
            modelBuilder.Entity<Treaty>()
                .HasIndex(t => t.PeriodId);

            // --- TreatyArticle ---
            modelBuilder.Entity<TreatyArticle>()
                .HasIndex(ta => ta.TreatyId);
            modelBuilder.Entity<TreatyArticle>()
                .HasIndex(ta => new { ta.TreatyId, ta.ArticleNumber }).IsUnique();

            // --- TreatySignatory ---
            modelBuilder.Entity<TreatySignatory>()
                .HasIndex(ts => ts.TreatyId);
            modelBuilder.Entity<TreatySignatory>()
                .HasIndex(ts => ts.StateId);
            modelBuilder.Entity<TreatySignatory>()
                .HasIndex(ts => ts.SignatoryPersonId);

            // --- TradeRoute ---
            modelBuilder.Entity<TradeRoute>()
                .HasIndex(tr => tr.Name);
            modelBuilder.Entity<TradeRoute>()
                .HasIndex(tr => tr.StartYear);

            // --- TradeRouteWaypoint ---
            modelBuilder.Entity<TradeRouteWaypoint>()
                .HasIndex(w => w.TradeRouteId);
            modelBuilder.Entity<TradeRouteWaypoint>()
                .HasIndex(w => new { w.TradeRouteId, w.OrderNumber }).IsUnique();

            // --- TradeRouteParticipant ---
            modelBuilder.Entity<TradeRouteParticipant>()
                .HasIndex(trp => trp.TradeRouteId);
            modelBuilder.Entity<TradeRouteParticipant>()
                .HasIndex(trp => trp.StateId);
            modelBuilder.Entity<TradeRouteParticipant>()
                .HasIndex(trp => trp.PersonId);

            // --- ReligionHolyBook ---
            modelBuilder.Entity<ReligionHolyBook>()
                .HasIndex(hb => hb.ReligionId);

            // --- ReligionHolyCity ---
            modelBuilder.Entity<ReligionHolyCity>()
                .HasIndex(hc => hc.ReligionId);
            modelBuilder.Entity<ReligionHolyCity>()
                .HasIndex(hc => hc.LocationId);

            // --- ReligionSubBranch ---
            modelBuilder.Entity<ReligionSubBranch>()
                .HasIndex(sb => sb.ReligionId);

            // --- PopulationData ---
            modelBuilder.Entity<PopulationData>()
                .HasIndex(pd => pd.StateId);
            modelBuilder.Entity<PopulationData>()
                .HasIndex(pd => pd.Year);
            modelBuilder.Entity<PopulationData>()
                .HasIndex(pd => new { pd.StateId, pd.Year }).IsUnique();

            // --- User ---
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username).IsUnique();

            // --- UserInteraction ---
            modelBuilder.Entity<UserInteraction>()
                .HasIndex(ui => ui.UserId);
            modelBuilder.Entity<UserInteraction>()
                .HasIndex(ui => new { ui.EntityType, ui.EntityId });
            modelBuilder.Entity<UserInteraction>()
                .HasIndex(ui => ui.CreatedAt);
        }
    }
}