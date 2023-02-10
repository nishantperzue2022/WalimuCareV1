using System;
using System.Collections.Generic;
using System.Text;

namespace HkMobileApp.Models
{
    public class AddDependantMember
    {
        public string id { get; set; }
        public string UserEmail { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Relationship { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string ParentId { get; set; }

        public string NationalIdOrBirthCertificateFile { get; set; }

        public bool isDisabled { get; set; }

        public string phoneNumber { get; set; }

        public string nationalIDorBirthCert { get; set; }

        public string PassportPhotoFile { get; set; }

        public string MarriageCertificateFile { get; set; }

        public string ProofOfSchoolingFile { get; set; }
        public string ProofOfDisabilityFile { get; set; }

        public string NationalIdOrBirthCertificateFileName { get; set; }
        public string PassportPhotoFileName { get; set; }

        public string MarriageCertificateFileName { get; set; }

        public string ProofOfSchoolingFileName { get; set; }
        public string ProofOfDisabilityFileName { get; set; }

    }

}
