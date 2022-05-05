using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCFLibraryForTP
{
    [DataContract]
    public class CategoryS
    {
        [DataMember][Key]
        public int categoryId { get; set; }
        [DataMember]
        public string categoryName { get; set; }

        public CategoryS() { }

        public CategoryS(string categoryName)
        {
            this.categoryName = categoryName;
        }
        public CategoryS(Category category)
        {
            this.categoryId = category.id;
            this.categoryName = category.name;
        }
    }
}
