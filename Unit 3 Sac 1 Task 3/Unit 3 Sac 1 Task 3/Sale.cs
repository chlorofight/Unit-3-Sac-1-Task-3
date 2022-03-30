using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_3_Sac_1_Task_3
{
    internal class Sale
    {
        public string Textbook { get; set; }
        public string Subject { get; set; }
        public string Seller { get; set; }
        public string Purchaser { get; set; }
        public float PurchasePrice { get; set; }
        public string SalePrice { get; set;}
        //string incase there is no rating
        public string Rating { get; set; }
    }
}
