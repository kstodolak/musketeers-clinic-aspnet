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

namespace ClinicMVC.Controllers
{
    [Authorize(Roles = "admin")]
    public class DoctorsController : Controller
    {
        //private AppDbContext db = new AppDbContext();
        private readonly IDoctorRepository _doctorRepository;
        private readonly ISpecializationRepository _specializationRepository;

        public DoctorsController(IDoctorRepository doctorRepository, ISpecializationRepository specializationRepository)
        {
            _doctorRepository = doctorRepository;
            _specializationRepository = specializationRepository;
        }
        // GET: Doctors
        public async Task<ActionResult> Index()
        {
            var doctors = await _doctorRepository.GetDoctorsAsync();           
            return View(doctors);
        }


        // GET: Doctors/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = await _doctorRepository.GetDoctorAsync(id.Value);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // GET: Doctors/Create
        public async Task<ActionResult> Create()
        {
            var specializations = await _specializationRepository.GetSpecializationsAsync();
            ViewBag.SpecializationId = new SelectList(specializations, "Id", "Name");
            return View();
        }

        // POST: Doctors/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Surname,ConsultingRoom,SpecializationId")] Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return View(doctor);
            }

            var result = await _doctorRepository.SaveDoctorAsync(doctor);
            if (!result)
                return View("Error");

            var doctors = await _doctorRepository.GetDoctorsAsync();
            return PartialView("_doctorsListPartial", doctors);
        }

        // GET: Doctors/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Doctor doctor = await _doctorRepository.GetDoctorAsync(id.Value);
            var specializations = await _specializationRepository.GetSpecializationsAsync();
            if (doctor == null || specializations == null)
                return HttpNotFound();
            ViewBag.SpecializationId = new SelectList(specializations, "Id", "Name");
            return View(doctor);
        }

        // POST: Doctors/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Surname,ConsultingRoom,SpecializationId")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                await _doctorRepository.SaveDoctorAsync(doctor);
                return RedirectToAction("Index");
            }
            return View(doctor);
        }

        // GET: Doctors/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = await _doctorRepository.GetDoctorAsync(id.Value);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Doctor doctor = await _doctorRepository.GetDoctorAsync(id);
            await _doctorRepository.DeleteDoctorAsync(doctor);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DoctorsListPartial()
        {
            var doctors = Task.Run(() => _doctorRepository.GetDoctorsAsync()).Result;
            return PartialView("_doctorsListPartial", doctors);
        }

        [HttpGet]
        public ActionResult GetSpecializationList()
        {
            var specialization = Task.Run(() => _specializationRepository.GetSpecializationsAsync()).Result;
            return PartialView("_specializationListPartial", specialization);
        }

    }
}
