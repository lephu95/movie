using Aspose.Email.Exceptions;
using Azure.Core;
using Microsoft.IdentityModel.Tokens;
using movie.Entities;
using movie.Handle.Email;
using movie.Playloads.Converter;
using movie.Playloads.DataRequest;
using movie.Playloads.DataResponses;
using movie.Playloads.Responses;
using movie.Services.Interface;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Eventing.Reader;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Aspose.Email.PersonalInfo;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;

namespace movie.Services.Implement
{
    public class Userservice : BaseService, IUserservice
    {
        private readonly ResponsesObject<DataResponsesUser> responses;
        private readonly UserConverter converter;
        private readonly IConfiguration _configuration;
        private readonly ResponsesObject<DataResponsesToken> responsesObject;
        private readonly ResponsesObject<DataResponsesUser> responsesObject1;
        private readonly IUserservice _userservice;
        private readonly IUserservice userservice;
       



        public Userservice(IConfiguration configuration)
        {
            responses = new ResponsesObject<DataResponsesUser>();
            converter = new UserConverter();
            _configuration = configuration;
            responsesObject = new ResponsesObject<DataResponsesToken>();
        }

        public ResponsesObject<DataResponsesUser> Register(Request_Registers request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) ||
                string.IsNullOrWhiteSpace(request.Password) ||
                string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.PhoneNumber) ||
                string.IsNullOrWhiteSpace(request.Point))
            {
                return responses.ResponsesErr(StatusCodes.Status400BadRequest, "vui long dien day du thong tin", null);
            }
            if (contex.Users.Any(x => x.Email.Equals(request.Email)))
            {
                return responses.ResponsesErr(StatusCodes.Status400BadRequest, "email da ton tai", null);
            }
            if (contex.Users.Any(x => x.Username.Equals(request.Username)))
            {
                return responses.ResponsesErr(StatusCodes.Status400BadRequest, "ten dang nhap da ton tai", null);
            }
            if (!validate.IsvalidEmail(request.Email))
            {
                return responses.ResponsesErr(StatusCodes.Status400BadRequest, "email ko hop le", null);
            }
            

            var user = new User();
            user.Email = request.Email;
            user.Password = BCryptNet.HashPassword(request.Password);
            user.PhoneNumber = request.PhoneNumber;
            user.Point = request.Point;
            user.Username = request.Username;
            user.RoleId = 1;
            user.UserStatusId = 1;
            user.RankCustomerId = 1;
            user.IsActive = true;
            user.Name= request.Name;
            contex.Users.Add(user);

            contex.SaveChanges();
            DataResponsesUser result = converter.EntityToDTO(user);
            
            return responses.ResponsesSucsess("dang ki thanh cong", result);

        }

        DataResponsesToken IUserservice.RenewAccessToken(Request_RenewAccessToken request)
        {
            throw new NotImplementedException();
        }
        private string GenerateRefeshToken()
        {
            var random = new byte[32];
            using (var item = RandomNumberGenerator.Create())
            {
                item.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }


        public ResponsesObject<DataResponsesToken> login(Request_Login request)
        {
            var user = contex.Users.SingleOrDefault(x => x.Username.Equals(request.username));
            if (string.IsNullOrWhiteSpace(request.username) || string.IsNullOrWhiteSpace(request.password))
            {
                return responsesObject.ResponsesErr(StatusCodes.Status400BadRequest, "vui long fien day du thong tin", null);
            }
            bool checkpass = BCryptNet.Verify(request.password, user.Password);
            if (!checkpass)
            {
                return responsesObject.ResponsesErr(StatusCodes.Status400BadRequest, "mk ko chinh xac", null);
            }
            return responsesObject.ResponsesSucsess("dang nhap thanh cong", GennerateAccessToken(user));

        }



        public DataResponsesToken GennerateAccessToken(User user)
        {
            var JwtTokenHanler = new JwtSecurityTokenHandler();
            var secretKeyBytes = System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:SecretKey").Value);
            var role = contex.Roles.SingleOrDefault(x => x.Id == user.RoleId);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Email", user.Email),
                    new Claim("RoleName", user.Name),
                }),
                Expires = DateTime.Now.AddHours(4),
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = JwtTokenHanler.CreateToken(tokenDescription);
            var accesstoken = JwtTokenHanler.WriteToken(token);
            var refeshToken = GenerateRefeshToken();
            RefreshToken rf = new RefreshToken
            {
                Token = refeshToken,
                ExpreidTime = DateTime.Now.AddDays(1),
                UserId = user.Id,
            };
            contex.RefreshTokens.Add(rf);
            contex.SaveChanges();
            DataResponsesToken result = new DataResponsesToken
            {
                AccessToken = accesstoken,
                RefeshToken = refeshToken
            };
            return result;
        }

        IQueryable<DataResponsesUser> IUserservice.Getall()
        {
            var resault = contex.Users.Select(x => converter.EntityToDTO(x));
            return resault;
        }



        bool IUserservice.SendConfirmEmail(Request_Registers request)
        {
            try
            {
                var fromadress = new MailAddress("lp@gmail.com", "hp");
                var toadress = new MailAddress(request.Email);
                const string fromPass = "1";
                const string fromsubject = "xn email";
                var code = contex.ComfimEmails.FirstOrDefault(x => x.ConFirmCode == request.confimcode);
                string body = $"ma xn cua ban  la {code}";

                var stmp = new SmtpClient()
                {
                    Host = "stmp.email.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(fromadress.Address, fromPass)
                };

                using (var message = new MailMessage(fromadress, toadress)
                {
                    Subject = fromsubject,
                    Body = body
                })
                {
                    stmp.Send(message);
                }

                return true; // Gửi email thành công
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi gửi email: " + ex.Message);
                return false; // Gửi email thất bại
            }
        }
        

        bool IUserservice.FogotPass(Request_ForgotPassword request)
        {
            try
            {
                var fromadress = new MailAddress("lp@gmail.com", "hp");
                var toadress = new MailAddress(request.email);
                const string fromPass = "1";
                const string fromsubject = "mk";
                var pass = contex.Users.FirstOrDefault(x => x.Password == request.password);
                string body = $"mk cua ban  la {pass}";

                var stmp = new SmtpClient()
                {
                    Host = "stmp.email.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(fromadress.Address, fromPass)
                };

                using (var message = new MailMessage(fromadress, toadress)
                {
                    Subject = fromsubject,
                    Body = body
                })
                {
                    stmp.Send(message);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi gửi email: " + ex.Message);
                return false; // Gửi email thất bại
            }

        }
        private bool VerifyPassword(string password, string hashedPassword)
        {
            using (var sha256 = SHA256.Create())
            {
                // Mã hóa mật khẩu người dùng cung cấp
                byte[] hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                // Chuyển đổi giá trị băm thành một chuỗi hexa
                string hashedInputPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                // So sánh giá trị băm với giá trị băm đã được lưu trữ
                if (hashedInputPassword == hashedPassword)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public ResponsesObject<DataResponsesUser> ChangePass(int id, Request_ForgotPassword request)
        {
            var user = contex.Users.SingleOrDefault(x => x.Username.Equals(request.username));
            var currentUser = contex.Users.FirstOrDefault(x => x.Id == id);
            if (currentUser != null)
            {
                if (VerifyPassword(request.password, currentUser.Password))
                {
                    string hashedNewPass = BCryptNet.HashPassword(request.newpass);
                    UpdatePassInDb(currentUser.Id, hashedNewPass);
                    return responsesObject1.ResponsesSucsess("Đổi mật khẩu thành công", converter.EntityToDTO(currentUser)); 
                }
                else
                {
                    return responsesObject1.ResponsesErr(StatusCodes.Status400BadRequest, "Mật khẩu cũ không đúng", null);
                }
            }
            else
            {
                 return responsesObject1.ResponsesErr(StatusCodes.Status400BadRequest, "vui long fien day du thong tin", null);
            }
        }
        private void UpdatePassInDb(int userId, string hashedNewPass)
        {
            var user = contex.Users.FirstOrDefault(x => x.Id == userId);
            if (user != null)
            {
                user.Password = hashedNewPass;
            }
        }

        
        public ResponsesObject<DataResponsesUser> ADMrmv(int id)
        {
            var u = contex.Users.FirstOrDefault(x => x.Id == id);
            if (contex.Users == null)
            {
                return responses.ResponsesErr(StatusCodes.Status400BadRequest, "ko ton tai ", null);
            }
            else
            {
                if (u == null)
                {
                    return responses.ResponsesErr(StatusCodes.Status400BadRequest, "ko ton tai ", null);
                }
                else
                {
                    contex.Users.Remove(u);
                    contex.SaveChanges();

                    return responses.ResponsesSucsess("xoa thanh cong", null);
                }
            }
        }
        //cấp quyền admin cho người dùng
        public async Task<bool> AssignAdminRoleAsync(string username)
        {
            var user = await contex.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user != null)
            {
                var adminRole = await contex.Roles.FirstOrDefaultAsync(r => r.RoleName == "Admin");
                if (adminRole != null)
                {
                    user.RoleId = adminRole.Id;
                    await contex.SaveChangesAsync();
                    return true;
                }
            }

            return false;
        }
        //khóa tài khoản 
        public void LockUserAccount(int userId)
        {
            var user = contex.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                user.IsActive = false; // Khóa tài khoản người dùng bằng cách đặt IsActive thành false
                contex.SaveChanges();
            }
            else
            {
                
                throw new Exception("User not found");
            }
        }
        //mở khóa tài khoản
        public void UnlockUserAccount(int userId)
        {
            var user = contex.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                user.IsActive = true; // Mở khóa tài khoản người dùng bằng cách đặt IsActive thành true
                contex.SaveChanges();
            }
            else
            {
                
                throw new Exception("User not found");
            }
        }
    }
         
}
