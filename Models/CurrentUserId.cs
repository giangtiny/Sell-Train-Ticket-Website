using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sell_Train_Ticket.Models;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Sell_Train_Ticket.Models
{
    //Apply Singleton design pattern
    public class CurrentUserId
    {
        private static CurrentUserId instance;
        private string userId;

        private CurrentUserId()
        {

        }

        public static CurrentUserId GetInstance()
        {
            if(instance == null)
            {
                instance = new CurrentUserId();
            }

            return instance;
        }

        public string GetUserId()
        {
            return this.userId;
        }

        public void SetUserId(string id)
        {
            this.userId = id;
        }
    }
}