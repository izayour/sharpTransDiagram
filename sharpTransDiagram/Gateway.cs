 
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
 

namespace WebApp.Domain.Models
{    
    public partial class Gateway
    {
        [Key]
        public int GatewayId { get; set; }
        public virtual string Type { get; set; }
        public virtual string Attribute { get; set; }
        public ConsoleApp1.DummyData theDummy { get; set; } 
    }
    public partial class Gateway
    {
       
    
        public Gateway()
        {

        }
   
        public virtual bool GetTargetObjectAndUpdate(int targetId, double quantity)
        { var targetList = theDummy.GetList<Target>(Type);

           int index= targetList.FindIndex(t=> t.GetTargetId()== targetId);
            targetList[index].Update(quantity, Attribute);
            return true;
        }
    }

}
