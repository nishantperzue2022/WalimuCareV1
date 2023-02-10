using System;
using System.Collections.Generic;
using System.Text;

namespace HkMobileApp.Models.HkApiResponses
{
   
    public class GetDependantsResponse
    {
        public Class1[] Property1 { get; set; }
    }

    public class Class1
    {
        public Member members { get; set; }
        public int status { get; set; }
        public string statusName { get; set; }
    }

    //public class Members
    //{
    //    public string id { get; set; }
    //    public string parentId { get; set; }
    //    public string firstName { get; set; }
    //    public string middleName { get; set; }
    //    public string lastName { get; set; }
    //    public string memberNumber { get; set; }
    //    public object addressPhysical { get; set; }
    //    public object addressPostal { get; set; }
    //    public object addressStreet { get; set; }
    //    public object addressPostalCode { get; set; }
    //    public object addressCity { get; set; }
    //    public string addressEmail { get; set; }
    //    public object addressLandLine { get; set; }
    //    public int clusteredId { get; set; }
    //    public object createdBy { get; set; }
    //    public DateTime createdDate { get; set; }
    //    public object updatedBy { get; set; }
    //    public DateTime updatedDate { get; set; }
    //    public string addressMobileNumber { get; set; }
    //    public int recordStatus { get; set; }
    //    public bool isPrincipalDependant { get; set; }
    //    public DateTime? dateOfBirth { get; set; }
    //    public int status { get; set; }
    //    public object remarks { get; set; }
    //    public string idNumber { get; set; }
    //    public string gender { get; set; }
    //    public bool isLastExpenseBeneficiary { get; set; }
    //    public bool isGroupLifeBeneficiary { get; set; }
    //    public string suspensionReasons { get; set; }
    //    public string agentId { get; set; }
    //    public string relationship { get; set; }
    //    public object userId { get; set; }
    //    public object agent { get; set; }
    //    public object parent { get; set; }
    //    public object[] inverseParent { get; set; }
    //    public object[] tMemberDocuments { get; set; }
    //    public object[] tMemberInsuranceCovers { get; set; }
    //}

}
