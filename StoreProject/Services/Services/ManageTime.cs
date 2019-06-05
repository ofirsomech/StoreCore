using StoreProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreProject.Services.Services
{
    public class ManageTime : IManageTime
    {
        public string GetGreeting()
        {
            TimeSpan currentTime = DateTime.Now.TimeOfDay;

            if(currentTime.CompareTo(new TimeSpan(4 , 0 , 0)) <= 0)
            {
                return "Good Night";
            }
            if (currentTime.CompareTo(new TimeSpan(11, 0, 0)) <= 0)
            {
                return "Good Morning";
            }
            if (currentTime.CompareTo(new TimeSpan(18, 0, 0)) <= 0)
            {
                return "Good Afternoon";
            }
            if (currentTime.CompareTo(new TimeSpan(21, 0, 0)) <= 0)
            {
                return "Good Evning";
            }
            else
                return "Good Night";
        }



    }
}
