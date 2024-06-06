using System.ComponentModel.DataAnnotations;

namespace ShoppingWeb
{
    public class SetLanguageDto
    {
        /// <summary>
        /// 語言
        /// </summary>
        [Required]
        public string Language { get; set; }
    }
}