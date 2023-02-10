using System;
using System.Collections.Generic;
using System.Text;

namespace HkMobileApp.Models.HkApiBody.MedicalCover
{
    public class MedicalCoverBody
    {
        public Mbrbasicdetails mbrBasicDetails { get; set; }
        public Nextofkin nextOfKin { get; set; }
        public Medicalcoverdetails medicalCoverDetails { get; set; }
    }

    public class Mbrbasicdetails
    {
        public string memberId { get; set; }
        public string fullName { get; set; }
        public string gender { get; set; }
        public string phoneNumber { get; set; }
        public string nhifNumber { get; set; }
        public string nhifCardFile { get; set; }
        public string nhifCardFileName { get; set; }
        public string nationalIDName { get; set; }
        public string nationalIdFile { get; set; }
        public string passportName { get; set; }
        public string passportFile { get; set; }
        public string postalCode { get; set; }
        public string postalAddress { get; set; }
        public string physicalAddress { get; set; }
        public string residentCounty { get; set; }
        public string medicalProductCode { get; set; }
    }

    public class Nextofkin
    {
        public string id { get; set; }
        public string insuranceCoverId { get; set; }
        public string name { get; set; }
        public string relationship { get; set; }
        public string phoneNumber { get; set; }
        public string emailAddress { get; set; }
    }

    public class Medicalcoverdetails
    {
        public string beneficiaries { get; set; }
        public string medicalCoverProductCode { get; set; }
        public string medicalCoverProdictType { get; set; }
        public string insuranceCompany { get; set; }
        public int familySize { get; set; }
        public decimal amount { get; set; }
        public decimal discount { get; set; }
        public int period { get; set; }
    }

}
