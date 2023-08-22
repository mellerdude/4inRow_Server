using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Final_Project_4inRow_Server.Data;
using Final_Project_4inRow_Server.Models;
using static Azure.Core.HttpHeader;
using System.Xml.Linq;

namespace Final_Project_4inRow_Server.Pages.GameData
{
    public class IndexModel : PageModel
    {
        private readonly Final_Project_4inRow_Server.Data.UserContext _context;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public IndexModel(Final_Project_4inRow_Server.Data.UserContext context)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            _context = context;
            Names = _context.Scores.Select(p => p.Name).Distinct().OrderBy(s => s).ToList();
        }

        public List<string> Names { get; set; } = default!;

        [BindProperty]
        public string Name { get; set; } = default!;


        public IList<Scores> Scores { get;set; } = default!;

        public IList<Credentials> Creds { get; set; } = default!;

        [BindProperty]
        public bool ShowNameAndDateColumns { get; set; } = false;

        public bool ShowCategorizedNames { get; set; }

        public List<(string Label, List<string> Names)> CategorizedNames { get; set; }
        public bool ShowCategorizedPlayersByCountry { get; set; }
        public List<(string Label, List<string> Players)> CategorizedPlayersByCountry { get; set; }



        public async Task OnGetAsync()
        {
            if (_context.Scores != null && _context.Credentials!= null)
            {
                Scores = await _context.Scores.ToListAsync();
                Creds = await _context.Credentials.ToListAsync();
            }

        }

        public async Task OnPostAsync()
        {
            ShowCategorizedNames = false;
            ShowNameAndDateColumns = false;
            if (_context.Scores != null)
            {
                Scores = await _context.Scores.Where(p => p.Name == Name).ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostOrderByNamesAsync()
        {
            ShowCategorizedNames = false;
            ShowNameAndDateColumns = false;

            Scores = await _context.Scores.OrderBy(p => p.Name).ToListAsync();
            Scores = Scores.OrderBy(p => p.Name, StringComparer.OrdinalIgnoreCase).ToList();

            return Page();
        }



        public async Task<IActionResult> OnPostNameAndDateAsync()
        {
            ShowCategorizedNames = false;
            ShowNameAndDateColumns = true;

            Scores = await _context.Scores.OrderBy(p => p.Name).ToListAsync();
            Scores = Scores.OrderBy(p => p.Name, StringComparer.Ordinal).ToList();

            return Page();
        }



        public async Task<IActionResult> OnPostShowAllGamesAsync()
        {
            ShowCategorizedNames = false;

            ShowNameAndDateColumns = false;
            Scores = await _context.Scores.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostShowDistinctAsync()
        {
            ShowCategorizedNames = false;

            ShowNameAndDateColumns = false;
            Scores = await _context.Scores
            .Where(p => !string.IsNullOrEmpty(p.Name))
            .GroupBy(p => p.Name)
            .Select(group => group.First())
            .ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostShowPlayersByGamesAsync()
        {

            ShowCategorizedNames = true;
            CategorizedNames = await GetCategorizedNamesAsync();
            return Page();
        }


        private async Task<List<(string Label, List<string> Names)>> GetCategorizedNamesAsync()
        {
            var nameCounts = await _context.Scores
                .GroupBy(p => p.Name)
                .Select(group => new
                {
                    Name = group.Key,
                    Count = group.Count()
                })
                .OrderByDescending(item => item.Count)
                .ToListAsync();

            var groupedNameCounts = nameCounts
                .GroupBy(item => item.Count)
                .OrderByDescending(group => group.Key)
                .ToList();

            var categorizedNames = groupedNameCounts
                .Select(group =>
                {
                    var label = group.Key == 1 ? "One Game" : $"{group.Key} Games";
                    var names = group.Select(item => item.Name).ToList();
                    return (label, names);
                })
                .ToList();

            return categorizedNames;
        }

        public async Task<IActionResult> OnPostShowPlayersByCountryAsync()
        {
            ShowCategorizedPlayersByCountry = true;
            CategorizedPlayersByCountry = await GetCategorizedPlayersByCountryAsync();
            return Page();
        }


        private async Task<List<(string Label, List<string> Players)>> GetCategorizedPlayersByCountryAsync()
        {
            var players = await _context.Credentials.ToListAsync();

            var playersByCountry = players
                .GroupBy(c => c.SelectedCountry)
                .OrderBy(group => group.Key)
                .ToList();

            var categorizedPlayersByCountry = playersByCountry
                .Select(group =>
                {
                    var label = group.Key.ToString();
                    var players = group.Select(item => item.Name).ToList();
                    return (label, players);
                })
                .ToList();

            return categorizedPlayersByCountry;
        }




    }
}
