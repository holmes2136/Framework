using System;
using System.Collections.Generic;
using System.Web;

namespace Filter
{
    public abstract class Filter
    {
        public Filter filter;

        public Filter() { 
        
        }

        public Filter(Filter filter){
               this.filter = filter;
        }
       

        public abstract bool IsEqual(string newCity,string oddCity,string newCountry,string oddCountry);



        public bool doNext(string newCity, string oddCity, string newCountry, string oddCountry){

            bool returnVal = true;

            if (filter != null)
            {
                returnVal =  filter.IsEqual(newCity, oddCity, newCountry, oddCountry);
            }

            return returnVal;

       

        }

   }    
   
    
}