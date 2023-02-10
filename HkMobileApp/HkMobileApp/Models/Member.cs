using Android.Net.Wifi.Aware;
using System;
using System.Collections.Generic;
using System.Text;

namespace HkMobileApp.Models
{

    public class Member
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MemberNumber { get; set; }
        public string AddressPhysical { get; set; }
        public string AddressPostal { get; set; }
        public string AddressStreet { get; set; }
        public string AddressPostalCode { get; set; }
        public string AddressCity { get; set; }
        public string AddressEmail { get; set; }
        public string AddressLandLine { get; set; }
        public int AlusteredId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string AddressMobileNumber { get; set; }
        public int RecordStatus { get; set; }
        public bool IsPrincipalDependant { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int Status { get; set; }
        public string StatusDescription { get; set; }
        public string Remarks { get; set; }
        public string IdNumber { get; set; }
        public string Gender { get; set; }
        public bool IsLastExpenseBeneficiary { get; set; }
        public bool IsGroupLifeBeneficiary { get; set; }
        public string SuspensionReasons { get; set; }
        public string AgentId { get; set; }
        public string Relationship { get; set; }
        public string UserId { get; set; }
        public Depedentdocument[] DepedentDocuments { get; set; }

        private string fullName;

        public string FullName
        {
            get { return fullName; }
            set
            {
                fullName = (FirstName ?? "") + " " + (MiddleName ?? "") + " " + (LastName ?? "");
            }
        }

        public string Age { get; set; }


        public string StatusName
        {
            get
            {

                if (Status == 0)
                {
                    return "pending";
                }
                else if (Status == 1)
                {
                    return "active";
                }
                else if (Status == 2)
                {
                    return "rejected";
                }
                else if (Status == 3)
                {
                    return "suspended";
                }
                else
                {
                    return "Unknown";
                }

            }
        }

        public string ColorName
        {
            get
            {
                string ColorName = "";

                if (Status == 0)
                {
                    ColorName = "#ff33cc";
                }
                else if (Status == 1)
                {
                    ColorName = "Green";
                }
                else if (Status == 2)
                {
                    ColorName = "Red";
                }
                else if (Status == 3)
                {
                    ColorName = "Blue";
                }
                else
                {
                    ColorName = "Gray";
                }

                return ColorName;
            }
        }


    }

    public class Depedentdocument
    {
        public string id { get; set; }
        public string fileName { get; set; }
        public string fileType { get; set; }
        public string memberId { get; set; }
    }



}


//public class Member
//{


//    public string Id { get; set; }
//    public string ParentId { get; set; }
//    public string FirstName { get; set; }
//    public string MiddleName { get; set; }
//    public string LastName { get; set; }
//    public string MemberNumber { get; set; }
//    public string AddressPhysical { get; set; }
//    public string AddressPostal { get; set; }
//    public string AddressStreet { get; set; }
//    public string AddressPostalCode { get; set; }
//    public string AddressCity { get; set; }
//    public string AddressEmail { get; set; }
//    public string AddressLandLine { get; set; }
//    public int ClusteredId { get; set; }
//    public string CreatedBy { get; set; }
//    public DateTime CreatedDate { get; set; }
//    public string UpdatedBy { get; set; }
//    public DateTime UpdatedDate { get; set; }
//    public string AddressMobileNumber { get; set; }
//    public byte RecordStatus { get; set; }
//    public bool IsPrincipalDependant { get; set; }
//    public DateTime? DateOfBirth { get; set; }

//    public byte Status { get; set; }

//    public string statusDescription { get; set; }

//    public string StatusName
//    {
//        get
//        {

//            if (Status == 0)
//            {
//                return "pending";
//            }
//            else if (Status == 1)
//            {
//                return "active";
//            }
//            else if (Status == 2)
//            {
//                return "rejected";
//            }
//            else if (Status == 3)
//            {
//                return "suspended";
//            }
//            else
//            {
//                return "Unknown";
//            }

//        }
//    }

//    public string Remarks { get; set; }
//    public string IdNumber { get; set; }
//    public string Gender { get; set; }
//    public bool IsLastExpenseBeneficiary { get; set; }
//    public bool IsGroupLifeBeneficiary { get; set; }
//    public string SuspensionReasons { get; set; }
//    public string AgentId { get; set; }
//    public string Relationship { get; set; }




//    private string fullName;

//    public string FullName
//    {
//        get { return fullName; }
//        set
//        {
//            fullName = (FirstName ?? "") + " " + (MiddleName ?? "");
//        }
//    }

//    public string ColorName
//    {
//        get
//        {
//            string ColorName = "";

//            if (StatusName != null)
//            {
//                if (StatusName.ToLower() == "pending")
//                {
//                    ColorName = "#ff33cc";
//                }
//                else if (StatusName.ToLower() == "approved")
//                {
//                    ColorName = "Green";
//                }
//                else if (StatusName.ToLower() == "rejected")
//                {
//                    ColorName = "Red";
//                }
//                else if (StatusName.ToLower() == "suspended")
//                {
//                    ColorName = "Blue";
//                }
//            }




//            return ColorName;
//        }
//    }


//    public string Age { get; set; }

//}
