using System;
using System.Collections.Generic;
using System.Text;

namespace HkMobileApp.Models.HkApiResponses
{
    public class PolicyDetailsResponseBody
    {
        public int id { get; set; }
        public int member_id { get; set; }
        public string member_no { get; set; }
        public int scheme_id { get; set; }
        public int job_group_id { get; set; }
        public int department_id { get; set; }
        public int limit { get; set; }
        public int utilised_limit { get; set; }
        public int available_limit { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public Department department { get; set; }
    }


    public class Department
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }
}
