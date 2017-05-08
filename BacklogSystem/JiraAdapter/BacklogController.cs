using System;
using Microsoft.AspNetCore.Mvc;

namespace JiraAdapter {
    
    [Route("api/backlog")]
    public class BacklogController : Controller {
        
        private readonly JiraConfiguration configuration;

        public BacklogController(JiraConfiguration configuration){
            this.configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get(){
            
            return this.Ok(new 
            { 
                User = this.configuration.JiraUser,
                Password = this.configuration.JiraPassword
            });
        }   
    }
}