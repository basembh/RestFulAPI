using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestFulAPI.Models
{
    public class SchoolViewModel
    {
        public int RawID { set; get; }

        public string SchoolID { set; get; }

        public string School_Name { set; get; }

        public string City { set; get; }

        public string Directorate { set; get; }

        public string Lat { set; get; }

        public string Long { set; get; }


    }
}
