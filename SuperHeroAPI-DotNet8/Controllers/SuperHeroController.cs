using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI_DotNet8.Data;
using SuperHeroAPI_DotNet8.Entities;

namespace SuperHeroAPI_DotNet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetAllHeros()
        {
            var heroes = await _context.SuperHeroes.ToListAsync();

            return Ok(heroes);
        }


        [HttpGet("id")]
        public async Task<ActionResult<SuperHero>> GetAllHeros(int id)
        {
            var heroes = await _context.SuperHeroes.FindAsync(id);
            if (heroes is null)
                return NotFound("Hero not found");

            return Ok(heroes);
        }


        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHeroes(SuperHero superHero)
        {
            _context.SuperHeroes.Add(superHero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero hero)
        {
            var heroes = await _context.SuperHeroes.FindAsync(hero.Id);
            if (heroes is null)
                return NotFound("Hero not found");

            heroes.Name=hero.Name;
            heroes.FirstName=hero.FirstName;
            heroes.LastName=hero.LastName;
            heroes.Place=hero.Place;

            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }


        [HttpDelete]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var heroes = await _context.SuperHeroes.FindAsync(id);
            if (heroes is null)
                return NotFound("Hero not found");

            _context.SuperHeroes.Remove(heroes);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }
    }
}
