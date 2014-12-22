using System;
using System.Collections.Generic;
using System.Web;

namespace Filter
{
    public class DistanceFilter : Filter
    {
         public DistanceFilter(){
            
        }

         public DistanceFilter(Filter filter):base(filter)
         {

            
        }

         public override bool IsEqual(string newCity, string oddCity, string newCountry, string oddCountry)
         {
             throw new NotImplementedException("No implement");
         }


    }
}