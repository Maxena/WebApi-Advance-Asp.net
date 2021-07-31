using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Repositories;
using Entities;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebFramework.Api;
using WebFramework.Filters;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiResultFilter]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region BaseConfigure

        private readonly IUserRepository _userRepository;


        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;

        }

        #endregion

        #region Get
        [HttpGet]
        [ApiResultFilter]
        public async Task<ActionResult<List<Users>>> GetUsersList()
        {

            var users = await _userRepository.TableNoTracking.ToListAsync();
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        [ApiResultFilter]
        public async Task<ActionResult<Users>> GetUsersById(int id, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetByIdAsync(cancellationToken, id);
            if (users == null)
            {
                return NotFound();
            }
            return users;
        }


        #endregion

        #region Post

        [HttpPost]
        [ApiResultFilter]
        public async Task<ActionResult<Users>> CreatUser(UserDto userDto, CancellationToken cancellationToken)
        {
            var users = new Users()
            {
                Age = userDto.Age,
                FullName = userDto.FullName,
                Gender = userDto.Gender,
                UserName = userDto.UserName,
            };
            await _userRepository.AddAsync(users, userDto.Password, cancellationToken);
            return Ok(users);
        }

        #endregion

        #region Put

        [HttpPut]
        [ApiResultFilter]
        public async Task<ActionResult> UpdateUsers(int id, Users users, CancellationToken cancellationToken)
        {
            var updateusers = await _userRepository.GetByIdAsync(cancellationToken, id);
            updateusers.UserName = users.UserName;
            updateusers.PasswordHash = users.PasswordHash;
            updateusers.FullName = users.FullName;
            updateusers.Age = users.Age;
            updateusers.Gender = users.Gender;
            updateusers.IsActive = users.IsActive;
            updateusers.LastLoginDate = users.LastLoginDate;
            await _userRepository.UpdateAsync(updateusers, cancellationToken);
            return Ok(updateusers);

        }
        #endregion

        #region Delete
        [HttpDelete]
        public async Task<ActionResult> DeleteUser(int id, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetByIdAsync(cancellationToken, id);
            await _userRepository.DeleteAsync(users, cancellationToken);
            return Ok(users);
        }


        #endregion

    }
}
