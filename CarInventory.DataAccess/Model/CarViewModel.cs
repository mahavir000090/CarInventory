using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarInventory.DataAccess.Model
{
    public class CarViewModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<bool> New { get; set; }

    }
}
