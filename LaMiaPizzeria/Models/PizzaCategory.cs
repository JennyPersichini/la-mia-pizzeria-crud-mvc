using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace LaMiaPizzeria.Models
{
    public class PizzaCategory
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string? Description { get; set; }

        public List<Pizza> Pizze { get; set; }

        public PizzaCategory()
        {

        }

        public PizzaCategory(string nome, string? description)
        {
            Nome = nome;
            Description = description;
            Pizze = new List<Pizza>();
        }

    }

}
