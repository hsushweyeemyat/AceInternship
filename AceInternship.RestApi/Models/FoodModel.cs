using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceInternship.RestApi.Model;
[Table("Tbl_Food")]
public class FoodModel
{
    [Key]
    public int FoodId { get; set; }
    public string ? FoodName { get; set; }
    public string ? FoodType { get; set; }
    public string ? FoodPrice { get; set; }
}
