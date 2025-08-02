using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.AvailableEntities
{
    public class AvailableCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        private AvailableCategory() { }

        public AvailableCategory(int id, string name) 
        { 
            Id = id;
            Name = name;
        }
    }
}
