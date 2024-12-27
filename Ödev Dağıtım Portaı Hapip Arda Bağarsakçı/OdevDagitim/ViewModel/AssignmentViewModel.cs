namespace OdevDagitim.ViewModel
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AssignmentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Başlık zorunludur")]
        [Display(Name = "Başlık")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Açıklama zorunludur")]
        [Display(Name = "Açıklama")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Son teslim tarihi zorunludur")]
        [Display(Name = "Son Teslim Tarihi")]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = "Sınıf seçimi zorunludur")]
        [Display(Name = "Sınıf")]
        public int ClassId { get; set; }

        [Required(ErrorMessage = "Öğretmen seçimi zorunludur")]
        [Display(Name = "Öğretmen")]
        public string TeacherId { get; set; }

        public string FilePath { get; set; }
        public string TeacherName { get; set; }
        public string ClassName { get; set; }
    }
} 