using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using AddressHistoryDAL.Models;
using AddressHistoryDAL.Repos;

namespace AddressHistoryWebApp.Controllers
{
    public class AddressController : Controller
    {
        private readonly IDataRepo _repo;

        public AddressController(IDataRepo repo)
        {
            _repo = repo;
        }

        // GET: Address
        public IActionResult Index()
        {
            return View(_repo.GetAddresses());
        }

        // GET: Address/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Address/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("StartDate,EndDate,Address1,Address2,City,State,Zip5,Zip4")] Address address)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repo.InsertAddress(address);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $@"Unable to add addess: {ex.Message}");
                    return View(address);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(address);
        }

        // GET: Address/Edit/5
        public IActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            DateTime _startDate = DateTime.Parse(id);

            Address _address = _repo.GetAddress(_startDate);

            if (_address == null)
            {
                return NotFound();
            }
            return View(_address);
        }

        // POST: Address/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DateTime id, [Bind("StartDate,EndDate,Address1,Address2,City,State,Zip5,Zip4")] Address address)
        {
            if (id != address.StartDate)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repo.UpdateAddress(address);
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    ModelState.AddModelError(string.Empty, $@"Unable to update address. Another user has updated it. {ex.Message}");
                    return View(address);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $@"Unable to update the address. {ex.Message}");
                    return View(address);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(address);
        }

        // GET: Address/Delete/5
        public IActionResult Delete()
        {
            Address _address = _repo.GetLastAddress();

            if (_address == null)
            {
                return NotFound();
            }

            return View(_address);
        }

        // POST: Inventory/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([Bind("Id,Timestamp")] Address address)
        {
            try
            {
                _repo.DeleteAddress();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ModelState.AddModelError(string.Empty, $@"Unable to delete record. Another user updated the record. {ex.Message}");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $@"Unable to create record: {ex.Message}");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
