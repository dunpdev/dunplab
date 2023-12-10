using DUNPLab.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DUNPLab.API.Infrastructure
{
    public class DunpContext : DbContext
    {
        public DunpContext(DbContextOptions<DunpContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data for Pacijent
            modelBuilder.Entity<Pacijent>().HasData(
                new Pacijent { Id = 1, Ime = "John", Prezime = "Doe", Email = "john.doe@example.com", DatumRodjenja = new DateTime(1980, 1, 1), Adresa = "123 Main St", Grad = "Belgrade", Telefon = "1234567890", Pol = "M", JMBG = "1234567890123", BrojDokumenta = "AB123456", DatumIstekaDokumenta = new DateTime(2025, 1, 1) },
                new Pacijent { Id = 2, Ime = "Jane", Prezime = "Doe", Email = "jane.doe@example.com", DatumRodjenja = new DateTime(1985, 1, 1), Adresa = "456 Elm St", Grad = "Novi Sad", Telefon = "0987654321", Pol = "F", JMBG = "9876543210987", BrojDokumenta = "CD987654", DatumIstekaDokumenta = new DateTime(2026, 1, 1) }
            );

            // Seed data for Supstanca
            modelBuilder.Entity<Supstanca>().HasData(
                new Supstanca { Id = 1, Naziv = "Substance 1", Oznaka = "S1", Opis = "Description for Substance 1", Tip = "Type 1", DonjaGranica = 0.1, GornjaGranica = 1.0, MetodTestiranja = "Method 1", Cena = 100.0 },
                new Supstanca { Id = 2, Naziv = "Substance 2", Oznaka = "S2", Opis = "Description for Substance 2", Tip = "Type 2", DonjaGranica = 0.2, GornjaGranica = 2.0, MetodTestiranja = "Method 2", Cena = 200.0 }
            );

            // Seed data for Zahtev
            modelBuilder.Entity<Zahtev>().HasData(
                new Zahtev { Id = 1, DatumTestiranja = new DateTime(2023, 1, 1), TestiranjeId = 1, PacijentId = 1, JeLiObradjen = false },
                new Zahtev { Id = 2, DatumTestiranja = new DateTime(2023, 2, 1), TestiranjeId = 2, PacijentId = 2, JeLiObradjen = false }
            );

            // Seed data for Testiranje
            modelBuilder.Entity<Testiranje>().HasData(
                new Testiranje { Id = 1, Naziv = "Test 1", ZahtevId = 1, UkupnaCena = 300.0, NacinPlacanja = "Card", TestOdradio = "Tester 1", JesuLiPotvrdjeniSviUzorci = false, DatumVremeTestiranja = new DateTime(2023, 1, 1, 10, 0, 0), DatumVremeRezultata = null, Status = "In Progress", BrojSobe = "101", Izmenio = "Modifier 1", IzmenioDatumVreme = new DateTime(2023, 1, 1, 11, 0, 0) },
                new Testiranje { Id = 2, Naziv = "Test 2", ZahtevId = 2, UkupnaCena = 400.0, NacinPlacanja = "Cash", TestOdradio = "Tester 2", JesuLiPotvrdjeniSviUzorci = false, DatumVremeTestiranja = new DateTime(2023, 2, 1, 10, 0, 0), DatumVremeRezultata = null, Status = "In Progress", BrojSobe = "102", Izmenio = "Modifier 2", IzmenioDatumVreme = new DateTime(2023, 2, 1, 11, 0, 0) }
            );

            modelBuilder.Entity<ZahtevSubstanca>().HasData(
                new ZahtevSubstanca { Id = 1, ZahtevId = 1, SubstancaId = 1 },
                new ZahtevSubstanca { Id = 2, ZahtevId = 1, SubstancaId = 2 },
                new ZahtevSubstanca { Id = 3, ZahtevId = 2, SubstancaId = 1 },
                new ZahtevSubstanca { Id = 4, ZahtevId = 2, SubstancaId = 2 }
            );

            modelBuilder.Entity<Uzorak>().HasData(
                new Uzorak { Id = 1, Naziv = "Sample 1", KodEpruvete = "E1", MetodTestiranja = "Method 1", KonacanRezultat = "Result 1", Komentar = "Comment 1", Cena = 100.0, Kutija = "BX20230101000001", IdTestiranja = 1, Izmenio = "Modifier 1", IzmenioDatumVreme = new DateTime(2023, 1, 1, 12, 0, 0) },
                new Uzorak { Id = 2, Naziv = "Sample 2", KodEpruvete = "E2", MetodTestiranja = "Method 2", KonacanRezultat = "Result 2", Komentar = "Comment 2", Cena = 200.0, Kutija = "BX20230201000002", IdTestiranja = 2, Izmenio = "Modifier 2", IzmenioDatumVreme = new DateTime(2023, 2, 1, 12, 0, 0) }
            );
        }


        public DbSet<Pacijent> Pacijenti { get; set; }
        public DbSet<Uzorak> Uzorci { get; set; }
        public DbSet<Testiranje> Testiranja { get; set; }
        public DbSet<Supstanca> Supstance { get; set; }
        public DbSet<Rezultat> Rezultati { get; set; }
        public DbSet<Zahtev> Zahtevi { get; set;}
        public DbSet<ZahtevSubstanca> ZahtevSubstance { get; set; }
        public DbSet<RezultatOdMasine> RezultatiOdMasine { get; set; }
        public DbSet<VrednostOdMasine> VrednostiOdMasine { get; set; }
        public DbSet<ATNotification> Notifications { get; set; }
        public DbSet<NotificationRecipient> Recipients { get; set; }
        public DbSet<Zahtev> Zahtevi { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<DUNPLab.API.Models.File> Files { get; set; }
        public DbSet<Report> Reports { get; set; }
    }
}
