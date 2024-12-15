using Products.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Core
{

    public interface IEventsServices
    {
        List<Products.DB.Event> GetEvents(); 
        Products.DB.Event GetEvent(int id);
        Products.DB.Event CreateEvent(Products.DB.Event eventObj);
        void DeleteEvent(Products.DB.Event eventObj);
        Products.DB.Event EditEvent(Products.DB.Event eventObj);
    }
}
