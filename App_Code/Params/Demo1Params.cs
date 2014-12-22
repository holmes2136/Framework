using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using liarAttributes;
using Iface;


namespace Params { 
/// <summary>
/// Demo1Params 的摘要描述
/// </summary>
public class Demo1Params:IValidator
{
    [Required(true, "姓名不能為空")]
    [MaxLength(12)]
    public string name { get; set; }

    [Required(true, "密碼不能為空")]
    public string pwd { get; set; }

    [Required(true, "性別不能為空")]
    public string sex { get; set; }


	public Demo1Params()
	{
		
	}

    public Demo1Params(string name, string pwd, string sex)
    {
        this.name = name;
        this.pwd = pwd;
        this.sex = sex;
    }

    public List<ValidateError>  Validate()
    {
 	     List<ValidateError> errors = new List<ValidateError>();
         var props = new System.ComponentModel.DataAnnotations.AssociatedMetadataTypeTypeDescriptionProvider(typeof(Demo1Params)).GetTypeDescriptor(typeof(Demo1Params)).GetProperties();
         for (int i = 0; i < props.Count; i++) {

                for (int j = 0; j < props[i].Attributes.Count; j++) {
                
                    if (props[i].Attributes[j].GetType().FullName == "liarAttributes.Required")
                    {
                        var req = props[i].Attributes.OfType<Required>().FirstOrDefault();
                        if (req != null && req.isRequire == true && string.IsNullOrEmpty(this.name))
                        {
                            errors.Add(new ValidateError("NAME", req.message));
                        

                        }
                    }
                }

          }

        return errors;
    }

  }

}