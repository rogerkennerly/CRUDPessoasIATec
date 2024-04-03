using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Text.RegularExpressions;
using TesteIATec.Data;
using TesteIATec.Models;

namespace TesteIATec.Controllers
{
    [Route("pessoas")]
    public class PessoasController : Controller
    {
        private readonly AppDbContext _context;

        public PessoasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Pessoas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pessoas.ToListAsync());
        }

        [Route("detalhes/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var pessoa = await _context.Pessoas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        [Route("novo")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("novo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Email,Sexo,Nacionalidade,DataNascimento,Cpf,Telefone")] Pessoa pessoa)
        {
            var numberOfPhoneInput = pessoa.Telefone.Count;
            foreach (var tel in pessoa.Telefone)
            {
                try
                {
                    if (tel == null || tel == "" || tel == "null")
                    {
                        continue;
                    }

                    if (!Regex.IsMatch(tel, @"^-?[1-9][0-9,\.]+$"))
                    {
                        ModelState.AddModelError("Telefone[0]", "Digite apenas números");
                        return View(pessoa);
                        
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaExists(pessoa.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            if (ModelState.IsValid)
            {
                pessoa.Telefone.RemoveAll(item => item == null);
                var cpfToCheck = pessoa.Cpf;

                var checkCpfDatabase = await _context.Pessoas.Where(x => x.Cpf == cpfToCheck).ToListAsync();
                foreach (var teste in checkCpfDatabase)
                {
                    ModelState.AddModelError("Cpf", "Esse CPF já está em uso.");
                    return View(pessoa);
                }

                _context.Add(pessoa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pessoa);
        }

        [Route("editar/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }
            return View(pessoa);
        }

        [HttpPost("editar/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Email,Sexo,Nacionalidade,DataNascimento,Cpf,Telefone")] Pessoa pessoa)
        {
            if (id != pessoa.Id)
            {
                return NotFound();
            }

            foreach (var tel in pessoa.Telefone)
            {
                try
                {
                    if (tel == null || tel == "" || tel == "null")
                    {
                        continue;
                    }

                    if (!Regex.IsMatch(tel, @"^-?[1-9][0-9,\.]+$"))
                    {
                        throw new Exception("Digite apenas números");
                        return View(pessoa);

                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaExists(pessoa.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            if (ModelState.IsValid)
            {
                pessoa.Telefone.RemoveAll(item => item == null);
                try
                {
                    _context.Update(pessoa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaExists(pessoa.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pessoa);
        }

        [Route("excluir/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var pessoa = await _context.Pessoas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        [HttpPost("excluir/{id:int}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa != null)
            {
                _context.Pessoas.Remove(pessoa);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PessoaExists(int id)
        {
            return _context.Pessoas.Any(e => e.Id == id);
        }
    }
}
