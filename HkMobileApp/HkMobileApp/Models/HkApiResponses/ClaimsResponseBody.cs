using System;
using System.Collections.Generic;
using System.Text;

namespace HkMobileApp.Models.HkApiResponses
{
    public class ClaimsResponseBody
    {
        public bool success { get; set; }
        public Claim[] data { get; set; }
        public string message { get; set; }
    }

    public class Claim
    {
        public long id { get; set; }
        public long claim_id { get; set; }
        public int service_provider_id { get; set; }
        public int department_id { get; set; }
        public string scheme_name { get; set; }
        public int sub_department_id { get; set; }
        public string member_no { get; set; }
        public string status { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string treatment_date { get; set; }
        public object admission_date { get; set; }
        public object discharge_date { get; set; }
        public string invoice_no { get; set; }
        public object nhif { get; set; }
        public object rebuild { get; set; }
        public int approved_rebuild { get; set; }
        public int total { get; set; }
        public int accepted_total { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string hospital_name { get; set; }
        public object sp_name { get; set; }
        public string department_name { get; set; }

        public string ClaimDate { 
            get 
            {
                var date = Convert.ToDateTime(created_at);

                return date.ToString("dd-MMM-yyyy");
            } 
        }

        public string VisitDate
        {
            get
            {
                var date = Convert.ToDateTime(treatment_date);

                return date.ToString("dd-MMM-yyyy");
            }
        }

        public string IsApprovedColor {
            get
            {
                if(status!=null && status.Trim().ToLower().Contains("approved"))
                {
                    return "Green";
                }
                else
                {
                    return "Red";
                }
            }
         }

        public int Rating {
            get
            {
                Random random = new Random();

                return random.Next(0, 6);
            }
        }


    }

}
