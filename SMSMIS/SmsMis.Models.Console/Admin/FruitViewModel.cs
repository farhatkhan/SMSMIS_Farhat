using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsMis.Models.Console.Admin
{
    public class FruitViewModel
    {
        public IEnumerable<Fruit> AvailableFruits { get; set; }
        public IEnumerable<Fruit> SelectedFruits { get; set; }
        public PostedFruits PostedFruits { get; set; }
    }
}
