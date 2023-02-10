using System;
using System.Collections.Generic;
using System.Text;

namespace HkMobileApp.Models
{
    public class SingleClaim
    {
        public string HospitalName { get; set; }
        public string Department { get; set; }
        public DateTime VisitaionDate { get; set; }
        public long ClaimNumber { get; set; }
        public DateTime ClaimDate { get; set; }
        public int ClaimAmount { get; set; }
        public int ClaimAcceptedTotal { get; set; }
        public int ClaimStatus { get; set; }
        public int ApprvAmount { get; set; }
        public string MbrDiagnosis { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string InvestigationDescription { get; set; }
        public int InvestigationAmount { get; set; }
        public string PrescriptionDescription { get; set; }
        public int PrescriptionAmount { get; set; }
        public string ConsultationDescription { get; set; }
        public int ConsultationAmount { get; set; }


        public string ClaimCreatedAt
        {
            get
            {
                return ClaimDate.ToString("dd-MMM-yyyy");
            }
        }
        public string FullName
        {
            get
            {
                return FirstName + " "+ LastName;
            }
        }

        public string HospitalVisitDate
        {
            get
            {
                return VisitaionDate.ToString("dd-MMM-yyyy");
            }
        }
        public string IsApprovedColor
        {
            get
            {
                if (ClaimStatus == 1)
                {
                    return "Green";
                }
                else
                {
                    return "Red";
                }
            }
        }
        public string Status
        {
            get
            {
                if (ClaimStatus == 0)
                {
                    return "Pending";
                }
                else if (ClaimStatus == 1)
                {
                    return "Approved";
                }
                else
                {
                    return "Rejected";
                }
            }
        }

        public int Rating
        {
            get
            {
                Random random = new Random();

                return random.Next(0, 6);
            }
        }
    }
}
