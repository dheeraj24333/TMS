using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMS.Models
{
    public class Registration_Conform
    {

        public int user_id { get; set; }
        public int class_id { get; set; }
        public Nullable<int> manager_id { get; set; }
        public string status { get; set; }
        public string is_manager_approved { get; set; }
        public string is_admin_approved { get; set; }


        public IEnumerable<employee_table> employee_table1 { get; set; }
        public IEnumerable<registration_table> registration_table1 { get; set; }

        public class registration_table
        {
            public int registration_id { get; set; }
            public Nullable<int> user_id { get; set; }
            public Nullable<int> class_id { get; set; }
            public Nullable<int> manager_id { get; set; }
            public string status { get; set; }
            public string is_manager_approved { get; set; }
            public string is_admin_approved { get; set; }

        }

        public class employee_table
        {
            public int user_id { get; set; }
            public Nullable<int> manager_id { get; set; }
        }
    }



  
    

}