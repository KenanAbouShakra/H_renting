using Microsoft.AspNetCore.Mvc;
using HouseRenting.Models;
using HouseRenting.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using HouseRenting.DAL;
using Microsoft.AspNetCore.Authorization;


namespace HouseRenting.Controllers

{
    public class ItemController : Controller
    {
        private readonly IItemRepository _itemRepository;
        private readonly ILogger<ItemController> _logger;
        public ItemController(IItemRepository itemRepository, ILogger<ItemController> logger)
        {
            _itemRepository = itemRepository;
            _logger = logger;
        }
        //public List<Booking> BookingConsole(){return _itemDbContext.Bookings.ToList();}
        public async Task<IActionResult> Table()
        {
            var items = await _itemRepository.GetAll();
            if (items == null)
            {
                _logger.LogError("[ItemController] Item list not found while executing _itemRepository.GetAll()");
                return NotFound("Item list not found");
            }
            var itemListViewModel = new ItemListViewModel(items, "Table");
            return View(itemListViewModel);
        }
        public async Task<IActionResult> Details(int id)
        {
            var item = await _itemRepository.GetItemById(id);
            if (item == null)
            {
                _logger.LogError("[ItemController] Item not found for the ItemId {ItemId:0000}", id);
                return NotFound("Item not found for the ItemId");
            }
            return View(item);
        }
        public async Task<IActionResult> Grid()
        {
            var items = await _itemRepository.GetAll();
            if (items == null)
            {
                _logger.LogError("[ItemController] Item list not found while executing _itemRepository.GetAll()");
                return NotFound("Item list not found");
            }
            var itemListViewModel = new ItemListViewModel(items, "Grid");
            return View(itemListViewModel);
        }
        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(Item item)
        {
            if (ModelState.IsValid)
            {
                bool Ok = await _itemRepository.Create(item);
                if (Ok)
                    return RedirectToAction(nameof(Table));
            }
            _logger.LogWarning("[ItemController] Item creation failed {@item}", item);
            return View(item);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Update(int id)
        {
            var item = await _itemRepository.GetItemById(id);
            if (item == null) 
            {
                _logger.LogError("[ItemController] Item not found when updating the ItemId {ItemId:0000}", id);
                return BadRequest("Item not found for the ItemId");
            }
            return View(item);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(Item item)
        {
            if (ModelState.IsValid)
            {
                bool Ok = await _itemRepository.Update(item);
                if (Ok)
                    return RedirectToAction(nameof(Table));
            }
            _logger.LogWarning("[ItemController] Item update failed {@item}", item);
            return View(item);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _itemRepository.GetItemById(id);
            if (item == null)
            {
                _logger.LogError("[ItemController] Item not found for the ItemId {ItemId:0000}", id);
                return BadRequest("Item not found for the ItemId");
            }
            return View(item);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool Ok = await _itemRepository.Delete(id);
            if (!Ok)
            {
                _logger.LogError("[ItemController] Item deletion failed for the ItemId {ItemId:0000}", id);
                return BadRequest("Item deletion failed");
            }
            return RedirectToAction(nameof(Table));
        }

    }
}