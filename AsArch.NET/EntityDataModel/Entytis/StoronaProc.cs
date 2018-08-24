using System.ComponentModel.DataAnnotations;

namespace AsArch.NET.EntityDataModel.Entytis
{
    public class StoronaProc
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class StoronaProcParam
    {
        [Display(Name = "ИНН")]
        public string INN { get; set; }
        [Display(Name = "Адрес")]
        public string Adres { get; set; }
    }
}