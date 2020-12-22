using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kantar_Islemleri.Model
{
    [Table("YaziciBilgileri")]
    public partial class YaziciBilgileri
    { public int Id { get; set; }
        public string Yazici1 { get; set; }
        public string Yazici2 { get; set; }
    }
}
