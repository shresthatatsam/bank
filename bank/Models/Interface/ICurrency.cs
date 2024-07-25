using Microsoft.AspNetCore.Mvc;

namespace bank.Models.Interface
{
    public interface ICurrency
    {
        IActionResult Create();
        Task<List<CurrencyViewModel>> GetAllViewModelsAsync();
        public CurrencyViewModel Model { get; set; }
    }
}
