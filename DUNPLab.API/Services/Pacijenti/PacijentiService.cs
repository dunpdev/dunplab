using DUNPLab.API.Dtos;
using DUNPLab.API.Dtos.Mapping;
using DUNPLab.API.Infrastructure;
using DUNPLab.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DUNPLab.API.Services.Pacijenti
{
    public class PacijentiService : IPacijentiService
    {
        public DunpContext _context { get; set; }
        public PacijentiService(DunpContext context)
        {
           _context= context;
        }
        public async Task Seed()
        {
            List<PacijentDto> pacijenti = await ReadPatientsFromFileAsync(); //Procitamo sve pacijente
            Random random = new Random();
            List<int> pickedIds = new List<int>();  //Napravimo listu odabranih pacijenata
            for (int i = 0; i < 10; i++)  //For testing purpose we used 10, should be 50!
            {
                //Check if the patient already exists in database based on jmbg
                //Check if we already picked in this cycle the same patient.
                bool selectedFlag = false;
                while (selectedFlag == false)
                {
                    var randomNumber = random.Next(60);
                    var doesExist = await CheckIfExistsBasedOnJmbg(pacijenti[randomNumber].JMBG);
                    if (doesExist == false)
                    {
                        if (pickedIds.Contains(randomNumber) == false)
                        {
                            selectedFlag = true;
                            pickedIds.Add(randomNumber);
                        }
                    }
                }
                Pacijent pacijent = new Pacijent();
                pacijenti[pickedIds.Last()].CopyProperties(pacijent);
                await _context.Pacijenti.AddAsync(pacijent);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<List<PacijentDto>> ReadPatientsFromFileAsync()
        {
            List<PacijentDto> patients = new List<PacijentDto>();

            try
            {
                using (StreamReader reader = new StreamReader("wwwroot/spisak.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = await reader.ReadLineAsync();
                        string[] fields = line.Split(',');

                        PacijentDto patient = new PacijentDto
                        {
                            Ime = fields[0],
                            Prezime = fields[1],
                            Email = fields[2],
                            DatumRodjenja = DateTime.Parse(fields[3]),
                            Adresa = fields[4],
                            Grad = fields[5],
                            Telefon = fields[6],
                            Pol = fields[7],
                            JMBG = fields[8],
                            BrojDokumenta = fields[9],
                            DatumIstekaDokumenta = DateTime.Parse(fields[10])
                        };

                        patients.Add(patient);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it)
                Console.WriteLine($"Error reading patients from file: {ex.Message}");
            }

            return patients;
        }

        public async Task<bool> CheckIfExistsBasedOnJmbg(string jmbg)
        {
            var result = await _context.Pacijenti.Where(pacijent => pacijent.JMBG == jmbg).FirstOrDefaultAsync();
            if(result == null)
                return false;
            return true;
        }
    }
}
