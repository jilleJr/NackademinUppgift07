﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NackademinUppgift07.Models;
using NackademinUppgift07.ViewModels;

namespace NackademinUppgift07.Controllers
{
    public class AuthController : Controller
    {
	    private readonly TomasosContext context;

	    public AuthController(TomasosContext context)
	    {
		    this.context = context;
	    }

	    #region Actions
		public async Task<IActionResult> Index()
		{
			Kund kund = await AuthGetCurrentUser();

			if (kund == null)
	            return RedirectToAction("Login");

			return View(new ViewKund(kund));
		}

		[HttpPost]
		[AutoValidateAntiforgeryToken]
	    public async Task<IActionResult> Index(ViewKund changed)
		{
			Kund kund = await AuthGetCurrentUser();

			if (kund == null)
				return RedirectToAction("Login");

			if (!ModelState.IsValid)
				return View(changed);

			if (kund.Losenord != changed.OldLosenord)
			{
				ModelState.AddModelError(nameof(changed.OldLosenord), "Felaktigt lösenord.");
				return View(changed);
			}

			if (!string.IsNullOrEmpty(changed.NewLosenord))
			{
				kund.Losenord = changed.NewLosenord;
			}

			kund.Namn = changed.Namn;
			kund.Email = changed.Email;
			kund.Postnr = changed.Postnr;
			kund.Postort = changed.Postort;
			kund.Gatuadress = changed.Gatuadress;
			kund.Telefon = changed.Telefon;

			await context.SaveChangesAsync();
			
			return View(new ViewKund(kund));
		}

		[HttpGet]
	    public IActionResult Login()
	    {
		    return View();
	    }

		[HttpPost]
		[AutoValidateAntiforgeryToken]
	    public async Task<IActionResult> Login(ViewLogin login)
		{
			if (!ModelState.IsValid)
				return View(login);

			Kund kund = await context.Kund.SingleOrDefaultAsync(k =>
				k.AnvandarNamn == login.AnvandarNamn);

			if (kund == null)
			{
				// Login failed : username
				ModelState.AddModelError(nameof(login.AnvandarNamn), "Felaktigt användarnamn.");
				AuthLogout();
				return View(login);
			}

			if (kund.Losenord != login.Losenord)
			{
				// Login failed : password
				ModelState.AddModelError(nameof(login.Losenord), "Felaktigt lösenord.");
				AuthLogout();
				return View(login);
			}

			// Login success
			AuthLogin(kund.KundId);
			return RedirectToAction("Index");
		}

		[HttpGet]
	    public IActionResult Register()
	    {
		    return View();
	    }

	    [HttpPost]
	    [AutoValidateAntiforgeryToken]
	    public async Task<IActionResult> Register(Kund kund)
	    {
		    if (!ModelState.IsValid)
				return View(kund);

		    context.Kund.Add(kund);
		    await context.SaveChangesAsync();

			AuthLogin(kund.KundId);

		    return RedirectToAction("Index");
	    }

	    public IActionResult Logout()
	    {
		    return RedirectToAction("Index", "Tomasos");
	    }
		#endregion

	    internal void AuthLogin(int id)
	    {
			HttpContext.Session.SetInt32("Auth", id);
		}

	    internal void AuthLogout()
	    {
		    HttpContext.Session.Remove("Auth");
	    }

	    internal int? AuthGetCurrentID()
	    {
		    return HttpContext.Session.GetInt32("Auth");
	    }

	    internal async Task<Kund> AuthGetCurrentUser()
	    {
			int? id = AuthGetCurrentID();
		    if (!id.HasValue) return null;

		    return await context.Kund
				.SingleOrDefaultAsync(k => k.KundId == id.Value);
	    }
	}
}