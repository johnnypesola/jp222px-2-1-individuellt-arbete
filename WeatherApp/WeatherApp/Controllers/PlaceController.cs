using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
    public class PlaceController : Controller
    {
        // Fields
        private readonly IPlaceWeatherRepository _repository;

        // Constructor for injecting repository (makes testing easier)
        public PlaceController(IPlaceWeatherRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            try
            {
                IEnumerable<Place> places = _repository.GetAllPlaces();

                return View(places);
            }
            // Something went wrong
            catch (HandleableExeption e)
            {
                ViewBag.Message = e.Message;
                return View("Error");
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        public ActionResult Create()
        {
            ViewBag.Title = "Skapa plats";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name, Latitude, Longitude")] Place place)
        {
            ViewBag.Title = "Skapa plats";

            if (ModelState.IsValid)
            {
                try
                {
                    // Save contact
                    _repository.InsertPlace(place);
                    _repository.Save();

                    ViewBag.Title = "Plats skapad";
                    ViewBag.ActionName = "skapades";
                    return View("Success", place);
                }
                // Something went wrong
                catch (HandleableExeption e)
                {
                    ViewBag.Message = e.Message;
                    return View("Error");
                }
                catch (Exception e)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
            }

            return View();
        }

        public ActionResult Delete(int? id)
        {
            ViewBag.Title = "Radera plats";

            // Invalid request
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Valid request
            else
            {
                try
                {
                    Place place = _repository.GetPlace((int)id);

                    // If contact does not exist
                    if (place == null)
                    {
                        return HttpNotFound();
                    }

                    return View(place);
                }
                // Something went wrong
                catch (HandleableExeption e)
                {
                    ViewBag.Message = e.Message;
                    return View("Error");
                }
                catch (Exception e)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.Title = "Radera plats";

            // We need the place information, it's displayed in the Success view.
            Place place = _repository.GetPlace(id);

            // If place does not exist
            if (place == null)
            {
                return HttpNotFound();
            }
            // Only delete place if it exists
            else
            {
                try
                {
                    // Delete place
                    _repository.DeletePlace(place);
                    _repository.Save();

                    // Set session variable that only survives one roundtrip
                    ViewBag.Title = "Plats raderad";
                    ViewBag.ActionName = "raderades";

                    return View("Success", place);
                }
                // Something went wrong
                catch (HandleableExeption e)
                {
                    ViewBag.Message = e.Message;
                    return View("Error");
                }
                catch (Exception e)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
            }

            return View(place);
        }

        public ActionResult Edit(int? id)
        {
            // Invalid request
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Valid request
            else
            {
                try
                {
                    // Get place
                    Place place = _repository.GetPlace((int)id);

                    // If no place was found
                    if (place == null)
                    {
                        return HttpNotFound();
                    }

                    return View(place);
                }
                // Something went wrong
                catch (HandleableExeption e)
                {
                    ViewBag.Message = e.Message;
                    return View("Error");
                }
                catch (Exception e)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
            }
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int id)
        {
            try
            {
                // Fetch place
                Place place = _repository.GetPlace(id);

                // If no contact exists
                if (place == null)
                {
                    return HttpNotFound();
                }

                if (TryUpdateModel(place, String.Empty, new string[] { "Name", "Longitude", "Latitude" }))
                {
                    // Save contact
                    _repository.UpdatePlace(place);
                    _repository.Save();

                    ViewBag.Title = "Plats redigerad";
                    ViewBag.ActionName = "sparades";

                    return View("Success", place);
                }

                return View();
            }
            // Something went wrong
            catch (HandleableExeption e)
            {
                ViewBag.Message = e.Message;
                return View("Error");
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        protected override void Dispose(bool disposing)
        {
            // The ASP.NET MVC framework calls Dispose when the request has completed processing. 

            _repository.Dispose();
            base.Dispose(disposing);
        }
    }
}