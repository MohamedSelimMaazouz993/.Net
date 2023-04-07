using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using Service.DTO;
using Service.IService;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using API.Models;
using Core.Entities;

namespace API.Controllers
{
    //[Authorize]
    [Produces("application/json")]
    [Route("User")]
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly ILogger _logger;

        public UserController(IUserService service, ILogger logger)
        {
            _service = service;
            _logger = logger;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="Suportid"></param>
        /// <returns></returns>
        [Route("GetUser")]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetUser(int Userid)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                var Usr = await _service.GetUser(Userid).ConfigureAwait(false);

                if (Usr != null)
                {
                    return new OkObjectResult(Usr);
                }
                else
                {
                    var showmessage = "User inexistant";
                    dict.Add("Message", showmessage);
                    return NotFound(dict);

                }

            }
            catch (Exception ex)
            {
                _logger.Error("Erreur GetUser <==> " + ex.ToString());
                var showmessage = "Erreur" + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }




        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        [Route("GetAllUser")]
        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                var lst = await _service.GetAllUsers();
                var lstUsr= lst.ToList();

                if (lstUsr.Count != 0)
                {
                    return new OkObjectResult(lstUsr);
                }
                else
                {
                    var showmessage = "Pas d'element dans la liste";
                    dict.Add("Message", showmessage);
                    return NotFound(dict);

                }

            }
            catch (Exception ex)
            {

                _logger.Error("Erreur GetUsers <==> " + ex.ToString());
                var showmessage = "Erreur" + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usr"></param>
        /// <returns></returns>
        [Route("IsLogin")]
        [HttpPost]
        public async Task<ActionResult<UserDto>> IsLogin(Login usr)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                var userLog = await _service.IsLogin(usr.Username,usr.Pswd).ConfigureAwait(false);
                if (userLog!=null)
                {
                    return new OkObjectResult(userLog);

                }
                else
                {
                    var showmessage = "Mot de passe ou username invalid";
                    dict.Add("Message", showmessage);
                    return BadRequest(dict);
                }

            }
            catch (Exception ex)
            {

                _logger.Error("Erreur IsLogin <==> " + ex.ToString());
                var showmessage = "Erreur" + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }



        /// <summary>
        /// Ajout du User.
        /// </summary>
        /// <param name="User">Le User.</param>
        /// <returns></returns>
        [Route("AddUser")]
        [HttpPost]
        public async Task<ActionResult> AjoutUser(UserDto User)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                var ajt = await _service.AddUser(User).ConfigureAwait(false);
                if (ajt)
                {
                    var showmessage = "Insertion effectuee avec succes";
                    dict.Add("Message", showmessage);
                    return Ok(dict);

                }
                else
                {
                    var showmessage = "Echec d'insertion... Reprendre l'operation";
                    dict.Add("Message", showmessage);
                    return BadRequest(dict);
                }

            }
            catch (Exception ex)
            {

                _logger.Error("Erreur AjoutUser <==> " + ex.ToString());
                var showmessage = "Erreur" + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }


        /// <summary>
        /// Modifs the User.
        /// </summary>
        /// <param name="User">The User.</param>
        /// <returns></returns>
        [Route("UpdUser")]
        [HttpPut]
        public async Task<ActionResult> ModifUser(UserDto User)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                var upd = await _service.UpdUser(User).ConfigureAwait(false);
                if (upd)
                {
                    var showmessage = "Modification effectuee avec succes";
                    dict.Add("Message", showmessage);
                    return Ok(dict);

                }
                else
                {
                    var showmessage = "Echec de modification... Reprendre l'operation";
                    dict.Add("Message", showmessage);
                    return BadRequest(dict);

                }

            }
            catch (Exception ex)
            {

                _logger.Error("Erreur ModifUser <==> " + ex.ToString());
                var showmessage = "Erreur" + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }
        /// <summary>
        /// Suppression du User.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns></returns>
        [Route("DelUser")]
        [HttpDelete]
        public async Task<ActionResult> DeletUser(int Suportid)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                var del = await _service.DelUser(Suportid).ConfigureAwait(false);

                if (del)
                {
                    var showmessage = "User supprime avec succes";
                    dict.Add("Message", showmessage);
                    return Ok(dict);
                }

                
                else
                {
                    var showmessage = "Erreur lors de la suppression...Svp reprendre l'operation";
                    dict.Add("Message", showmessage);
                    return BadRequest(dict);
                }

            }
            catch (Exception ex)
            {

                _logger.Error("Erreur DeletUser <==> " + ex.ToString());
                var showmessage = "Erreur" + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }



    }
}

