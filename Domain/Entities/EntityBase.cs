using System.ComponentModel.DataAnnotations;

namespace MyWebApplication.Domain.Entities
{
    public abstract class EntityBase
    {
        public int Id { get; set; }

        //Характеристики проекта или команды

        [Required(ErrorMessage ="Заполните название команды")]
        [Display(Name ="Название команды")]
        [MaxLength(200)]
        public string? GroupName { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Today;

        [Display(Name = "Статус проекта")]
        public string? GroupStatus { get; set; }

        public int? PeopleCount { get; set; }
    }
}
