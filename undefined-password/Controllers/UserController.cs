using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using undefined_password.Business;
using undefined_password.Dtos;
using undefined_password.Models;
using undefined_password.Service;

namespace undefined_password.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SqlContext _db;

        public UserController(SqlContext db)
        {
            _db = db;
        }

        [HttpGet("Passwords")]
        public ActionResult<DenemeDto> GetPasswords(int id)
        {
           return GetAllPasswordFn(id);
        }
        [HttpGet("PasswordsWithCategory")]
        public ActionResult<DenemeDto> GetPasswordsByCategory(int id,int categoryId)
        {
            
            return GetAllPasswordFn2(id,categoryId);
        }
        [HttpPost("CreatePw")]
        public async Task<ActionResult<CreatePassword>> CreatePassword(CreatePasswordDto _pwDto)
        {
            Console.WriteLine(_db.CreatePasswords.LongCount());
            CreatePassword createdPassword = new CreatePassword
            {
                UserId = _pwDto.UserId,
                CategoryId = _pwDto.CategoryId,
                Email = _pwDto.Email,
                PasswordName = _pwDto.PasswordName,
            };
            if (_pwDto.isActive)
            {
                createdPassword.Password = _pwDto.Password;
            }
            else
            {
                RandomService randomService = new RandomService();
                string pwtemp = randomService.CreateRandomPw(_pwDto._lowerCase,_pwDto._upperCase,_pwDto._numbers,_pwDto._symbols,_pwDto._length);
                createdPassword.Password = pwtemp;
            }

            _db.CreatePasswords.Add(createdPassword);
            await _db.SaveChangesAsync();
            return Ok(createdPassword);

        }
        [HttpPost("Login")]
        public ActionResult<User> LoginUser(LoginDto _login)
        {
            var tempUser = _db.Users.FirstOrDefault(x => x.Email == _login.Email);
            if (tempUser != null)
            {
                if (tempUser.Password == _login.Password)
                {
                    UserDto userDto = new UserDto() {
                        UserId = tempUser.UserId,
                        SecurityQuestion = tempUser.SecurityQuestion
                    };
                    return Ok(userDto);
                }
                else
                {
                    return BadRequest("Wrong Password");
                }
            }
            else
            {
                return NotFound("User Not Found");
            }
        }
        [HttpPost("Register")]
        public async Task<ActionResult<User>> CreateUser(RegisterDto _user)
        {
            var tempUser = _db.Users.FirstOrDefault(x => x.Email == _user.Email);
            if (tempUser == null)
            {
                if (!ValidateRegister(_user))
                {
                    return BadRequest(new { error = "Registration Failed, Information Does Not Match" });
                }
                User user = new User
                {
                    Email = _user.Email,
                    Password = _user.Password,
                    SecurityAnswer = _user.SecurityAnswer.ToLower().Replace(" ",""),
                    SecurityQuestion = _user.SecurityQuestion
                };
                _db.Users.Add(user);
                await _db.SaveChangesAsync();
                return Ok(_user);
            }
            else
            {
                return BadRequest("Email Already Exists");
            }
        }
        [HttpPut]
        public async Task<ActionResult<CreatePassword>> UpdatePassword(UpdatePwDto _updateDto)
        {
            var tempPw = _db.CreatePasswords.FirstOrDefault(x => x.PasswordId == _updateDto.PasswordId);
            if (tempPw == null)
            {
                return NotFound("Password Not Found");
            }
            tempPw.Password = _updateDto.Password;
            tempPw.Email = _updateDto.Email;
            tempPw.PasswordName = _updateDto.PasswordName;
            _db.CreatePasswords.Update(tempPw);
            await _db.SaveChangesAsync();
            return Ok(_updateDto);
        }
        [HttpDelete]
        public async Task<ActionResult<CreatePassword>> DeletePassword(DeletePwDto _deleteDto)
        {
            var tempPw = _db.CreatePasswords.FirstOrDefault(x => x.PasswordId == _deleteDto.PasswordId);
            if(tempPw == null)
            {
                return NotFound("Password Not Found");
            }
            _db.CreatePasswords.Remove(tempPw);
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("SecurityQuery")]
        public async Task<ActionResult<User>> SecurityQuestion(SecurityDto _secDto)
        {
            var tempUser = _db.Users.FirstOrDefault(x => x.UserId == _secDto.UserId);
            if (tempUser.SecurityAnswer == _secDto.SecurityAnswer.Replace(" ","").ToLower()) {
              
                return Ok(GetAllPasswordFn(_secDto.UserId));
            }
            else
            {
                return BadRequest("Wrong Answer");
            }
        }
        private bool ValidateRegister(RegisterDto _user)
        {
            if (_user.Password == _user.ValidataPw)
            {
                if (_user.Password.Length >= 8 && _user.Password.Length <= 16)
                {
                    return true;
                }
                else
                {
                    throw new ArgumentException("Your password cannot be less than 8 characters and no more than 16 characters.");
                }
            }
            else
            {
                throw new ArgumentException("Passwords do not match");
            }
        }
        private DenemeDto GetAllPasswordFn(int id)
        {
            List<CreatePassword> tempPw = new List<CreatePassword>();

            foreach (var item in _db.CreatePasswords)
            {
                if (id == item.UserId)
                {
                   
                    tempPw.Add(item);
                }           
            }
            var x = tempPw.ToList();

            List<ArrayDto> arrayDto = new List<ArrayDto>();

            foreach (var item in x)
            {
                ArrayDto arrayDto2 = new ArrayDto()
                {
                    CategoryId = item.CategoryId,
                    Email = item.Email,
                    PasswordName = item.PasswordName,
                    Password = item.Password,
                    PasswordId = item.PasswordId,
                };
                arrayDto.Add(arrayDto2);
            }

            DenemeDto denemeDto = new DenemeDto()
            {
                UserId = x[0].UserId,
                Passwords = arrayDto,
            };
            return denemeDto;

        } private DenemeDto GetAllPasswordFn2(int id,int categoryId)
        {
            List<CreatePassword> tempPw = new List<CreatePassword>();

            foreach (var item in _db.CreatePasswords)
            {
                if (id == item.UserId && categoryId == item.CategoryId)
                {
                   
                    tempPw.Add(item);
                }           
            }
            var x = tempPw.ToList();

            List<ArrayDto> arrayDto = new List<ArrayDto>();

            foreach (var item in x)
            {
                ArrayDto arrayDto2 = new ArrayDto()
                {
                    CategoryId = item.CategoryId,
                    Email = item.Email,
                    PasswordName = item.PasswordName,
                    Password = item.Password,
                    PasswordId = item.PasswordId,
                };
                arrayDto.Add(arrayDto2);
            }

            DenemeDto denemeDto = new DenemeDto()
            {
                UserId = x[0].UserId,
                Passwords = arrayDto,
            };
            return denemeDto;

        }



    }
}
