using FluentValidation;
using JWTAuthAPI.Application.Core.DTOs.Authentication;
using JWTAuthAPI.Application.Core.DTOs.UserAccount;
using JWTAuthAPI.Application.Core.Helpers;
using JWTAuthAPI.Application.Core.Interfaces;
using JWTAuthAPI.Application.Core.Mappings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthAPI.Application.API.Controllers.v1
{
    public class AccountsController : V1BaseController
    {
        private readonly IAccountService _accountService;
        private readonly IValidator<UserUpdateRequest> _updateValidator;
        private readonly IValidator<UserCreateRequest> _createValidator;
        private readonly IValidator<AuthenticateRequest> _authenticateValidator;

        public AccountsController(IAccountService accountService,
            IValidator<UserUpdateRequest> updateValidator, 
            IValidator<UserCreateRequest> createValidator,
            IValidator<AuthenticateRequest> authenticateValidator)
        {
            _accountService = accountService;
            _updateValidator = updateValidator;
            _createValidator = createValidator;
            _authenticateValidator = authenticateValidator;
        }

        // POST api/v1/Accounts/Login
        [HttpPost(RoutesConstant.Login)]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(AuthenticateResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> LoginAsync([FromBody] AuthenticateRequest authenticateRequest)
        {
            var validationResult = await _authenticateValidator.ValidateAsync(authenticateRequest);
            if (!validationResult.IsValid) { return BadRequest(validationResult.ToString()); }

            var user = await _accountService.AuthenticateUserAsync(authenticateRequest);
            return user == null ? BadRequest() : Ok(user);
        }

        // POST api/v1/Accounts/Register
        [HttpPost(RoutesConstant.Register)]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> RegisterAsync([FromBody] UserCreateRequest request)
        {
            var validationResult = await _createValidator.ValidateAsync(request);
            if (!validationResult.IsValid) { return BadRequest(validationResult.ToString()); }

            var user = await _accountService.AddUserAsync(request.ToEntity(), request.Password);
            return user == null ? BadRequest() : Created(string.Format("api/v1/Accounts/Users/{0}", user.Id), user.ToResponseDTO());
        }

        // GET api/v1/Accounts/Users/id
        [HttpGet(RoutesConstant.GetUser)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) { return BadRequest(); }

            var user = await _accountService.GetUserByIdAsync(id);

            return user == null ? NotFound() : Ok(user.ToResponseDTO());
        }

        // PUT api/v1/Accounts/Users/id
        [HttpPut(RoutesConstant.UpdateUser)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUserAsync(string id, [FromBody] UserUpdateRequest request)
        {
            if (string.IsNullOrEmpty(id)) { return BadRequest(); }

            var validationResult = await _updateValidator.ValidateAsync(request);
            if (!validationResult.IsValid) { return BadRequest(validationResult.ToString()); }

            var user = await _accountService.GetUserByIdAsync(id);

            if (user == null) { return NotFound(); }
            if (!await _accountService.AuthorizeOwnerAsync(User, user)) { return Forbid(); }

            var succeeded = await _accountService.UpdateUserAsync(user.UpdateEntity(request));

            if (!succeeded) { throw new Exception($"Failed User Update : {user.Id}"); }

            return NoContent();
        }

        // DELETE api/v1/Accounts/Users/id
        [HttpDelete(RoutesConstant.DeleteUser)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUserAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) { return BadRequest(); }

            var user = await _accountService.GetUserByIdAsync(id);

            if (user == null) { return NotFound(); }
            if (!await _accountService.AuthorizeOwnerAsync(User, user)) { return Forbid(); }

            var succeeded = await _accountService.DeleteUserAsync(user);

            if (!succeeded) { throw new Exception($"Failed User Delete : {user.Id}"); }

            return NoContent();
        }
    }
}
