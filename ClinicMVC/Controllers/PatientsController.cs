using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Clinic.Entities;
using Clinic.Entities.Models;
using Clinic.DataAccessLayer.Repositories.Abstract;
using Microsoft.AspNet.Identity;

namespace ClinicMVC.Controllers
{
    [Authorize]
    public class PatientsController : Controller
    {
        //private AppDbContext db = new AppDbContext();
        private readonly IPatientRepository _patientRepository;
        public PatientsController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        // GET: Patients
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Index()
        {
            var patients = await _patientRepository.GetPatientsAsync();
            return View(patients);
        }

        // GET: Patients/Details/5
        public async Task<ActionResult> Details(string accountId)
        {
            if (String.IsNullOrEmpty(accountId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = await _patientRepository.GetPatientAsync(accountId);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // GET: Patients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Surname,Phone,Adress,PostCode,City")] Patient patient)
        {
            patient.AccountId = User.Identity.GetUserId();
            if (!ModelState.IsValid)
            {
                return View(patient);
            }
            var result = await _patientRepository.SavePatientAsync(patient);
            if (!result)
                return View("Error");
            return RedirectToAction("Index", "Home", null);
        }

        // GET: Patients/Edit/5
        public async Task<ActionResult> Edit(string accountId)
        {
            if (String.IsNullOrEmpty(accountId))
                return RedirectToAction("Create");
            Patient patient = await _patientRepository.GetPatientAsync(accountId);
            if (patient == null)
                return RedirectToAction("Create");
            return View(patient);
        }

        // POST: Patients/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Surname,Phone,Adress,PostCode,City,AccountId")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                await _patientRepository.SavePatientAsync(patient);
                //return RedirectToAction("Details", new { account_id = User.Identity.GetUserId() });
                return RedirectToAction("Index","Home",null);
            }
            return View(patient);
        }

        // GET: Patients/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(string accountId)
        {
            if (String.IsNullOrEmpty(accountId))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Patient patient = await _patientRepository.GetPatientAsync(accountId);
            if (patient == null)
                return HttpNotFound();
            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteConfirmed(string accountId)
        {
            Patient patient = await _patientRepository.GetPatientAsync(accountId);
            await _patientRepository.DeletePatientAsync(patient);
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
