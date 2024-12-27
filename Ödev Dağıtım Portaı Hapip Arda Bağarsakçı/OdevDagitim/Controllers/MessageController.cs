using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using System.Threading.Tasks;
using System;

using Models;
using AspNetCoreHero.ToastNotification.Abstractions;

using OdevDagitimUI.ViewModel;

namespace App.Controllers
{
    public class MessageController : Controller
    {
        private readonly MessageRepository _messageRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly INotyfService _notyf;

        public MessageController(
            MessageRepository messageRepository,
            UserManager<AppUser> userManager,
            IMapper mapper,
            INotyfService notyf)
        {
            _messageRepository = messageRepository;
            _userManager = userManager;
            _mapper = mapper;
            _notyf = notyf;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var chats = await _messageRepository.GetUserChats(currentUser.Id);
            return View(chats);
        }

        [Authorize]
        public async Task<IActionResult> Chat(string userId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var otherUser = await _userManager.FindByIdAsync(userId);

            if (otherUser == null)
            {
                _notyf.Error("Kullanıcı bulunamadı.");
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CurrentUserId = currentUser.Id;
            ViewBag.OtherUserId = userId;
            ViewBag.OtherUserName = otherUser.UserName;

            var messages = await _messageRepository.GetMessagesBetweenUsers(currentUser.Id, userId);
            return View(messages);
        }

        [HttpGet]
        public async Task<IActionResult> GetMessages(string userId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var messages = await _messageRepository.GetMessagesBetweenUsers(currentUser.Id, userId);
            return Json(messages);
        }

        [HttpGet]
        public async Task<IActionResult> GetUnreadCount()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound();
            }
            var count = await _messageRepository.GetUnreadMessageCount(currentUser.Id);
            if (count == null)
            {
                return Json(count);
            }
            if (count == 0) {
                return Json(count);
            }
            return Json(count);
        }

        [HttpPost]
        public async Task<IActionResult> SaveMessage([FromBody] SaveMessageViewModel model)
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                
                var message = new Message
                {
                    SenderId = currentUser.Id,
                    ReceiverId = model.ReceiverId,
                    Content = model.Content,
                    MessageDate = DateTime.Now,
                    IsRead = false
                };

                await _messageRepository.AddAsync(message);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsRead(string userId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            await _messageRepository.MarkMessagesAsRead(currentUser.Id, userId);
            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> GetUserChats()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var users = await _messageRepository.GetUserChats(currentUser.Id);
            
            var result = users.Select(u => new {
                id = u.Id,
                userName = u.UserName,
                email = u.Email
            });
            
            return Json(result);
        }
    }
} 