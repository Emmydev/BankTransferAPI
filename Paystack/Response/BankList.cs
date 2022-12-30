using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paystack.Response
{
    public class BankList
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public List<Data> Data { get; set; }
    }
    public class Data
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Code { get; set; }
        public string Longcode { get; set; }
        public object Gateway { get; set; }
        public bool Pay_with_bank { get; set; }
        public bool Active { get; set; }
        public bool isDeleted { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
        public string Type { get; set; }
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
