﻿using Newtonsoft.Json.Linq;
using RupBNB.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;

namespace RupBNB.Controllers
{
    public class UsersController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            User usersData = new User();

            return usersData.AdminViewUsersInfo();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] User user)
        {
            User signedUser= user.Insert();

            if (signedUser != null){
                return Request.CreateResponse(HttpStatusCode.OK, signedUser);
            }
            else{
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        [HttpPost]
        [Route("api/Users/userlogin")]
        public HttpResponseMessage userlogin([FromBody] JObject emailAndPassword)
        {
            string email = emailAndPassword.First.First.ToString();
            string password = emailAndPassword.Last.First.ToString();

            User user = new User();
            user= user.getUserByEmailAndPassword(email,password);

            if (user != null)   //user matching the inputed email found
            {
                if (user.Password == password)//password is correct
                { 
                    return Request.CreateResponse(HttpStatusCode.OK, user);
                }
                else //password isn't correct
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
            else //user matching the inputed email not found
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

    }
}