using System;
using System.Collections.Generic;

namespace PartsClient.Data
{
    public class Part
    {
        public string PartID { get; set; }
        public string PartName { get; set; }
        public string PartType { get; set; }
        public DateTime PartAvailableDate { get; set; }
        public List<string> Suppliers { get; set; }

        public string SupplierString => string.Join(", ", Suppliers);
    }
}