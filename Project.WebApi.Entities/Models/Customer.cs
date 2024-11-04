using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Project.WebApi.Entities.BaseEntities;

namespace Project.WebApi.Entities.Models
{
    public class Customer : BaseEntity
    {       
        public string? Name { get; set; }
        public string Address { get; set; }
    }
}
