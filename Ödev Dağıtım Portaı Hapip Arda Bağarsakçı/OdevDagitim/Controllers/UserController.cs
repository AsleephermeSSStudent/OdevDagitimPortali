using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using OdevDagitim.Models;
using OdevDagitim.Repositories;
using OdevDagitim.ViewModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt.Extensions;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Models;

namespace OdevDagitim.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserRepository _userRepository;
        private readonly ClassRepository _classRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly INotyfService _notyf;

        public UserController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager,
            UserRepository userRepository,
            ClassRepository classRepository,
            IMapper mapper,
            IConfiguration config,
            INotyfService notyf
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userRepository = userRepository;
            _classRepository = classRepository;
            _mapper = mapper;
            _config = config;
            _notyf = notyf;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            
                var user = new AppUser
                {
                    Name=model.Username,
                    Surname = model.Username,
                    UserName = model.Username,
                    Email = model.Email,
                    Created = DateTime.Now,
                    Updated = DateTime.Now
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Ogrenci");
                    _notyf.Success("Kullanıcı başarıyla kaydedildi.");
                    return RedirectToAction("Login");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
           
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles.Contains("admin"))
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    _notyf.Success("Başarıyla giriş yapıldı.");
                    return RedirectToAction("Index", "Home");
                }
            }

            _notyf.Error("Kullanıcı adı veya şifre hatalı!");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _notyf.Success("Başarıyla çıkış yapıldı.");
            return RedirectToAction("Login");
        }



        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UserList()
        {
            var users = await _userRepository.GetUsersWithClassAsync();
            var viewModel = new List<UserListViewModel>();
            
            foreach (var user in users)
            {
                var currentRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                var userViewModel = _mapper.Map<UserListViewModel>(user);
                userViewModel.Role = currentRole;
                viewModel.Add(userViewModel);
            }
            
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                _notyf.Error("Kullanıcı bulunamadı.");
                return RedirectToAction(nameof(UserList));
            }

            var currentRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            var viewModel = new UserListViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Role = currentRole,
                ClassId = user.ClassId,
                ClassName = user.Class?.ClassName
            };

            var classes = await _classRepository.GetAllAsync();
            ViewBag.Classes = new SelectList(classes, "Id", "ClassName");
            ViewBag.Roles = new SelectList(new[] { "admin", "teacher", "student" });

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser([FromBody] UserListViewModel model)
        {
           

            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return Json(new { success = false, message = "Kullanıcı bulunamadı." });
            }

            try
            {
                // Mevcut rolü al
                var currentRoles = await _userManager.GetRolesAsync(user);
                var currentRole = currentRoles.FirstOrDefault();

                // Rol değişikliği kontrolü
                if (currentRole != model.Role)
                {
                    // Mevcut rolü kaldır
                    if (currentRole != null)
                    {
                        await _userManager.RemoveFromRoleAsync(user, currentRole);
                    }

                    // Yeni rolü ata
                    if (!string.IsNullOrEmpty(model.Role))
                    {
                        var result = await _userManager.AddToRoleAsync(user, model.Role);
                        if (!result.Succeeded)
                        {
                            return Json(new { success = false, message = "Rol atama işlemi başarısız oldu." });
                        }
                    }
                }

                // Diğer bilgileri güncelle
                user.ClassId = model.ClassId;
                user.Updated = DateTime.Now;

                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    return Json(new { success = false, message = "Kullanıcı güncelleme işlemi başarısız oldu." });
                }

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Güncelleme sırasında bir hata oluştu: {ex.Message}" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserDetails(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return Json(new { success = false, message = "Kullanıcı bulunamadı." });
            }

            var currentRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            
            return Json(new { 
                success = true,
                id = user.Id,
                userName = user.UserName,
                email = user.Email,
            });
        }
    }
}
