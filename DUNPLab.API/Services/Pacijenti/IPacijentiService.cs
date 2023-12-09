using DUNPLab.API.Dtos;
using DUNPLab.API.Models;
using System.Text.RegularExpressions;

namespace DUNPLab.API.Services.Pacijenti
{
    public interface IPacijentiService
    {
        Task Seed();
        Task<List<PacijentDto>> ReadPatientsFromFileAsync();

        Task<bool> CheckIfExistsBasedOnJmbg(string jmbg);
        bool IsValidEmail(string email);

    }
}
