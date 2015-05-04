using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersFx.Formats.Text.Epub.Entities
{
    public class EpubByteContentFile : EpubContentFile
    {
        public byte[] Content { get; set; }
    }
}
