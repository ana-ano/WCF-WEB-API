using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductOrderAPI.Application.DTOs
{
    public class CategoryCreateDto
    {
        public string Name { get; set; } = string.Empty;
    }

    public class CategoryReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
