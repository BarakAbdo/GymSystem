using GymSystem.Application.Dtos.TrainerDtos;
using GymSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GymSystem.Web.Controllers
{
    public class TrainersController : Controller
    {
        private readonly ITrainerService _trainerService;

        public TrainersController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        // GET: Trainers
        public IActionResult Index()
        {
            var trainers = _trainerService.GetAll();
            return View(trainers);
        }

        // POST: Trainers/Create
        [HttpPost]
        public IActionResult Create(CreateTrainerDto dto)
        {
            if (ModelState.IsValid)
            {
                _trainerService.Add(dto);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Trainers/Edit
        [HttpPost]
        public IActionResult Edit(string uid, UpdateTrainerDto dto)
        {
            if (ModelState.IsValid)
            {
                _trainerService.Update(uid, dto);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Trainers/Delete
        [HttpPost]
        public IActionResult Delete(string uid)
        {
            _trainerService.Delete(uid);
            return RedirectToAction(nameof(Index));
        }
    }
}