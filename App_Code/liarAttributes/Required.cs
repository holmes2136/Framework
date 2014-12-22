using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;


namespace liarAttributes { 

    public class Required : Attribute
    {

        public bool isRequire { get; set; }
        public string message { get; set; }

        public Required(bool isRequire,string message)
        {
            this.isRequire = isRequire;
            this.message = message;
        }

    }

}