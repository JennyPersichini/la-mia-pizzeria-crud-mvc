using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaMiaPizzeria.Models
{
    public class Pizza
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(300)]
        [Required(ErrorMessage = "Campo obbligatorio!")]
        [Url(ErrorMessage = "L'URL inserito non è valido!")]
        public string Immagine { get; set; }

        [MaxLength(30)]
        [Required(ErrorMessage = "Campo obbligatorio!")]
        public string Nome { get; set; }

        [Column(TypeName = "text")]
        public string Descrizione { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio!")]
        public double Prezzo { get; set; }


        //COSTRUTTORE
        public Pizza(string immagine, string nome, string descrizione, double prezzo )
        {
           
        }
    }
}
