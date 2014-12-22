using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DataAccessSample.DataAccessLayer;

namespace DataAccessSample.VO
{

   
    public class People{

        public string name { get; set; }
        public int age { get; set; }
        public string _sex {get;set;}


        public People(string name,int age,string _sex) {
            
            this.name = name;
            this.age = age;
            this._sex = _sex;
        }





        public void Add() {

            //logical layer
            //if condition then access db
            DataAccessLayer.DataAccessHelper.getDataAccess().AddPeople(new People(this.name,this.age,this._sex));
            
        }

      


        public class Sex { 
        
            public const string Male = "Male";
            public const string Female = "Femail";
            public const string Neutral = "Neutral";

        }
  
        
        
    
    }


  
    



}
