namespace LaMiaPizzeria.Models
{
    public class PizzaCategory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }

        public List<Pizza> Pizze { get; set; }

        public PizzaCategory()
        {

        }

        public PizzaCategory(string title, string? description)
        {
            Title = title;
            Description = description;
            Pizze = new List<Pizza>();
        }

    }

}
