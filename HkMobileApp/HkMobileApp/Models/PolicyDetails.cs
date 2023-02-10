using HkMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HkMobileApp.Models
{
    public class PolicyDetails
    {

        public string mbr_id { get; set; }
        public string mbrInsrCoverId { get; set; }
        public string insuranceCoverName { get; set; }
        public string insuranceCoverTypeId { get; set; }
        public double insuranceCoverAmount { get; set; }
        public string insuranceCoverTypeName { get; set; }
        public string schemeName { get; set; }
        public string benefitId { get; set; }
        public string benefitName { get; set; }
        public double benefitAmount { get; set; }
        public double amountUsed { get; set; }

        public List<Person> Data { get; set; }

        public ObservableCollection<Series> ChartData { get; set; }
    }

    public class Series
    {
        public string DeptName { get; set; }
        public double Benefit { get; set; }
        public double AmountUsed { get; set; }


        public string Name { get; set; }

        public double Amount { get; set; }
    }
}
