using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Talabat.API.DTOs;
using Talabat.API.Errors;
using Talabat.API.Extensions;
using Talabat.Core.Enitites.Identitiy;
using Talabat.Core.Services;

namespace Talabat.API.Controllers
{

    public class AccountsController : BaseAPIController
    {
        private readonly UserManager<AppUser> _userManger;
        private readonly SignInManager<AppUser> _signManger;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountsController(UserManager<AppUser> userManger,
                                  SignInManager<AppUser> signManger,
                                  ITokenService tokenService,
                                  IMapper mapper)
        {
            _userManger = userManger;
            _signManger = signManger;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        [HttpPost("login")] // Post : api/accounts/login
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _userManger.FindByEmailAsync(loginDTO.Email);
            if (user == null) return Unauthorized(new ApiResponse(401));
            var result = await _signManger.CheckPasswordSignInAsync(user, loginDTO.Password, false);
            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            return Ok(new UserDTO()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user, _userManger)
            }) ;
        }

        [HttpPost("register")] // Post : api/accounts/register
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            if (CheckEmailExists(registerDTO.Email).Result.Value)
                return BadRequest(new ApivalidationErrorsResponse() { Errors = new[] { "This email is already used" } });
            var user = new AppUser()
            {
                DisplayName = registerDTO.DisplayName,
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNumber,
                UserName = registerDTO.Email.Split("@")[0]
            };
            var result = await _userManger.CreateAsync(user, registerDTO.Password);
            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            return Ok(new UserDTO()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user,_userManger)
            });
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManger.FindByEmailAsync(email);

            return Ok(new UserDTO()
            {
                DisplayName= user.DisplayName,
                Email = user.Email,
                Token=await _tokenService.CreateToken(user,_userManger)
            });
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<Address>> UpdateAddress(AddressDTO addressDTO)
        {
            var address = _mapper.Map<AddressDTO,Address>(addressDTO);

            var user = await _userManger.FindUserWithaddressByEmailasync(User);

            user.Address = address;
            var result = await _userManger.UpdateAsync(user);
            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            return Ok(_mapper.Map<Address,AddressDTO>(user.Address));

        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDTO>> GetAddress()
        {
            var user = await _userManger.FindUserWithaddressByEmailasync(User);
            return Ok(_mapper.Map<Address, AddressDTO>(user.Address));

        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            return await _userManger.FindByEmailAsync(email) != null;
        }
    }
}
