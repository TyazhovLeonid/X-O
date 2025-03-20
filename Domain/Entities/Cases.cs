using MyWebApplication.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MyWebApplication.Domain.Entities
{
    public class Cases : EntityBase
    {
        

        [Display(Name ="Название команды")]
        [MaxLength(200)]
        public string? Title { get; set; }

        [Display(Name = "Краткое описание проекта")]
        [MaxLength(1000)]
        public string? ShortDescriptionAboutProject { get; set; }

        [Display(Name = "Описание проекта")]
        [MaxLength(4000)]
        public string? DescriptionAboutProject { get; set; }

        [Display(Name = "Статус проекта")]
        public StatusTypeEnum Status { get; set; }
    }
}
