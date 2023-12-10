﻿using DUNPLab.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DUNPLab.API.Infrastructure
{
    public class DunpContext : DbContext
    {
        public DunpContext(DbContextOptions<DunpContext> options) : base(options)
        {

        }
        public DbSet<Pacijent> Pacijenti { get; set; }
        public DbSet<Uzorak> Uzorci { get; set; }
        public DbSet<Testiranje> Testiranja { get; set; }
        public DbSet<Supstanca> Supstance { get; set; }
        public DbSet<Rezultat> Rezultati { get; set; }
        public DbSet<RezultatOdMasine> RezultatiOdMasine { get; set; }
        public DbSet<VrednostOdMasine> VrednostiOdMasine { get; set; }
        public DbSet<ATNotification> Notifications { get; set; }
        public DbSet<NotificationRecipient> Recipients { get; set; }
        public DbSet<Report> Reports { get; set; }

        public DbSet<Models.File> Files { get; set; }

        public DbSet<Zahtev> Zahtevi { get; set; }


    }
}
