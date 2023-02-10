using System;
using System.Collections.Generic;
using System.Text;

namespace HkMobileApp.Models.HkApiBody
{
    public class EditMemberDetailsBody
    {
        public string id { get; set; }
        public string parentId { get; set; }
        public string userEmail { get; set; }
        public string fullName { get; set; }
        public string gender { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string relationship { get; set; }
        public bool isDisabled { get; set; }
        public string nationalIDorBirthCert { get; set; }
        public string phoneNumber { get; set; }
        public string memberNumber { get; set; }
        public string passportPhotoFile { get; set; }
        public string passportPhotoFileName { get; set; }
        public string nationalIdOrBirthCertificateFile { get; set; }
        public string nationalIdOrBirthCertificateFileName { get; set; }
        public string marriageCertificateFile { get; set; }
        public string marriageCertificateFileName { get; set; }
        public string proofOfSchoolingFile { get; set; }
        public string proofOfSchoolingFileName { get; set; }
        public string proofOfDisabilityFile { get; set; }
        public string proofOfDisabilityFileName { get; set; }
    }


}
