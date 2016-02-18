using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivingApp.Models.ViewModel
{
    public class PhotoViewModel
    {
        public long PhotoId { get; set; }

        public string Comment { get; set; }

        public DateTime Date { get; set; }

        public int Size { get; set; }
    }
}
