using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using CountryFilterService.DataAccess;

namespace CountryFilterService.MyInterface
{
    public class IPLoactionConverter:IConverter
    {


        public string Converter(string ip)
        {


           // you can implement a service which can get the country name and city name
            string returnVal = "";


            return returnVal;
    

        }

       


    }
}