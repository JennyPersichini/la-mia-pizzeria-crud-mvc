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
        public string Immagine { get; set; }

        [MaxLength(20)]
        public string Nome { get; set; }

        [Column(TypeName = "text")]
        public string Descrizione { get; set; }

        public double Prezzo { get; set; }

        public Pizza(string immagine, string nome, string descrizione, double prezzo )
        {
            Immagine = immagine;
            Nome = nome;
            Descrizione = descrizione;
            Prezzo = prezzo;
        }
    }
}
