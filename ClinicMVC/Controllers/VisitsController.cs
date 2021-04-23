using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Clinic.DataAccessLayer.Repositories.Abstract;
using Clinic.Entities;
using Clinic.Entities.Models;
using Microsoft.AspNet.Identity;

namespace ClinicMVC.Controllers
{
    [Authorize]
    public class VisitsController : Controller
    {
        private readonly IVisitRepository _visitRepository;
        private readonly ISpecializationRepository _specializationRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;


        public VisitsController(IVisitRepository visitRepository, ISpecializationRepository specializationRepository, IDoctorRepository doctorRepository, IPatientRepository patientRepository)
        {
            _visitRepository = visitRepository;
            _specializationRepository = specializationRepository;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
        }

        // GET: Visits
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Index()
        {
            if (!this.IsPatientCreated())
                return Redirect("/Patients/Create");

            var visits = await _visitRepository.GetVisitsAsync();
            return View(visits);
        }

        // GET: Visits/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visit visit = await _visitRepository.GetVisitAsync(id.Value);

            if (visit == null)
            {
                return HttpNotFound();
            }
            return View(visit);
        }

        // GET: Visits/Create
        public async Task<ActionResult> Create()
        {
            //ViewBag.DoctorId = new SelectList(db.Doctors, "Id", "Name");
            //ViewBag.PatientId = new SelectList(db.Patients, "Id", "Name");
            if (!this.IsPatientCreated())
                return Redirect("/Patients/Create");

            var specializations = await _specializationRepository.GetSpecializationsAsync();
            return View(specializations);
        }

        [HttpGet]
        public ActionResult SignUp(int? doctorId)
        {
            if (!this.IsPatientCreated())
                return Redirect("/Patients/Create");

            if (doctorId == null)
                return Redirect("/Visits");
            Visit newVisit = new Visit
            {
                DoctorId = doctorId.Value,
                Date = DateTime.Today.AddDays(1)
            };


            ViewBag.Hour = new SelectList(this.getHoursList2(newVisit.DoctorId));
            return View(newVisit);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> SignUp([Bind(Include = "Id,DoctorId,PatientId,Date,Hour")] Visit visit)
        {
            var account_id = User.Identity.GetUserId();
            Patient patient = Task.Run(() => _patientRepository.GetPatientAsync(account_id)).Result;
            visit.PatientId = patient.Id;
            if (!ModelState.IsValid)
            {
                return Json(new { info = "Niepoprawne dane" });
            }

            var result = await _visitRepository.SaveVisitAsync(visit);
            if (!result)
                return Json(new { info = "Nie udało się zapisać" });

            return Json(new { info = "Zapisano pomyślnie" });
        }

        public ActionResult SignUpDoctorInfo(int id)
        {
            var doctor = Task.Run(()=>_doctorRepository.GetDoctorAsync(id)).Result;
            return PartialView("_doctorInfoPartial", doctor);
        }

        // GET: Visits/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return Redirect("/Visits");
            }

            Visit visit = await _visitRepository.GetVisitAsync(id.Value);
            if (visit == null)
            {
                return HttpNotFound();
            }
            return View(visit);
        }

        // POST: Visits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Visit visit = await _visitRepository.GetVisitAsync(id);
            await _visitRepository.DeleteVisitAsync(visit);          
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<ActionResult> GetDoctorsBySpecialization(int specializationId)
        {
            var doctors = await _doctorRepository.GetDoctorsBySpecializationAsync(specializationId);
            return PartialView("_doctorsSelectListPartial", doctors);
        }


        public ActionResult ConstructHoursList(int? doctorId, DateTime date)
        {
            var visits = Task.Run(()=> _visitRepository.GetVisitsAsync(doctorId.Value, date)).Result;
            ViewBag.Hour = new SelectList(getHoursList(doctorId.Value, visits));

            return PartialView("_hoursSelectPartial");
        }
        public ActionResult GetVisitList(string UserId)
        {
            UserId = User.Identity.GetUserId();
            var visits = Task.Run(() => _visitRepository.GetVisitsAsync()).Result;
            List<Visit> UserVisits = new List<Visit>();
            foreach (var visit in visits)
            {
                if (UserId == visit.Patient.AccountId)
                {
                    UserVisits.Add(visit);
                }
                
            }
            UserVisits.Sort((x, y) => DateTime.Compare(x.Date, y.Date));
            return PartialView("Index", UserVisits);
        }

        private List<string> getHoursList(int doctorId,List<Visit> visits)
        {
            DateTime min = new DateTime(1, 1, 1, 9, 0, 0);
            DateTime max = new DateTime(1, 1, 1, 17, 30, 0);
            List<string> hoursList = new List<string>();

            while (min != max)
            {
                var visitFound = visits.FirstOrDefault(x => (x.Hour.Hour == min.Hour && x.Hour.Minute == min.Minute));
                if (visitFound == null)
                    hoursList.Add(min.ToString("HH:mm"));
                min = min.AddMinutes(30);
            }

            return hoursList;
        }

        private List<string> getHoursList2(int doctorId)
        {
            DateTime min = new DateTime(1, 1, 1, 9, 0, 0);
            DateTime max = new DateTime(1, 1, 1, 17, 30, 0);
            List<string> hoursList = new List<string>();

            while (min != max)
            {
                hoursList.Add(min.ToString("HH:mm"));
                min = min.AddMinutes(30);
            }

            return hoursList;
        }

        private bool IsPatientCreated()
        {
            var account_id = User.Identity.GetUserId();
            Patient patient = Task.Run(() => _patientRepository.GetPatientAsync(account_id)).Result;
            if (patient == null)
                return false;
            return true;
        }
    }
}
