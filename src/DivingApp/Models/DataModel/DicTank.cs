namespace DivingApp.Models.DataModel
{

    using System.ComponentModel.DataAnnotations;

    public class DicTank
    {    
        [Key]
        public byte TankId { get; set; }

        public string TankValue { get; set; }
    }
}
