using System.Collections.Generic;
using System.Text;

namespace VersOne.Epub.Schema
{
    public class EpubSpineItemRef
    {
        public string Id { get; set; }
        public string IdRef { get; set; }
        public bool IsLinear { get; set; }
        public List<SpineProperty> Properties { get; set; }

        public override string ToString()
        {
            StringBuilder resultBuilder = new StringBuilder();
            if (Id != null)
            {
                resultBuilder.Append("Id: ");
                resultBuilder.Append(Id);
                resultBuilder.Append("; ");
            }
            resultBuilder.Append("IdRef: ");
            resultBuilder.Append(IdRef ?? "(null)");
            return resultBuilder.ToString();
        }
    }
}
