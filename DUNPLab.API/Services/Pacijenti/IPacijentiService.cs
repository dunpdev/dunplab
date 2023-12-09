using DUNPLab.API.Dtos;
using DUNPLab.API.Models;

namespace DUNPLab.API.Services.Pacijenti
{
    public interface IPacijentiService
    {
        Task Seed();
        Task<List<PacijentDto>> ReadPatientsFromFileAsync();

        Task<bool> CheckIfExistsBasedOnJmbg(string jmbg);
    }
}
