using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Iface { 
/// <summary>
/// IValidator 的摘要描述
/// </summary>
    public interface IValidator
    {
         List<ValidateError> Validate();
    }

}