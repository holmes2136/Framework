using System;
using System.Collections.Generic;
using System.Web;

namespace Filter
{
    public class CountryFilter : Filter                
    {
        public CountryFilter() { }

        public CountryFilter(Filter filter):base(filter)
        { 
              
        }

        public override bool IsEqual(string newCity, string oddCity, string newCountry, string oddCountry)
        {
            //Non-Match 
            if (!newCountry.Equals(oddCountry)) {

                return false;
            }
            else{
                    
                 //Match 
                return this.doNext(newCity,oddCity,newCountry,oddCountry);
               
                
            }
            
        }
            
          
     
    
       

   

    }
}