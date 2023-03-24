using System.ComponentModel.DataAnnotations;

namespace NZWalks_Api.Models.DTOs
{
    public class UpdateRegionRequest
    {
        //validation
        [Required] //since it is nonnullable
        //After that, let's say you have a certain condition on the code because we know code is for example,for an Auckland region, it's AKL, so it's around the three
        //character Mark.So let's say if your application or your business decides to go on with that validation, that code will always be three characters.I can have a min
        //and max length check on the code field,
        //ASP.NET Core also provides us with min and max length checks.
        [MinLength(3, ErrorMessage = "Code has to be of length 3")]
        [MaxLength(3, ErrorMessage = "Code has to be of length 3")]
        public string Code { get; set; }
        [Required]//validation ,required is in system.componenet.dataannotations namespace
        [MaxLength(100, ErrorMessage = "Name has to be of maximum length 3")]
        public string Name { get; set; }

        public string? RegionImageURL { get; set; }
    }
}
