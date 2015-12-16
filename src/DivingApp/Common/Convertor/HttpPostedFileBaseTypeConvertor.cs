using AutoMapper;
using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DivingApp.Common.Convertor
{
    public class HttpPostedFileBaseTypeConverter : ITypeConverter<IFormFile, byte[]>
    {
        public byte[] Convert(ResolutionContext ctx)
        {
            if (ctx.SourceValue != null)
            {
                var fileBase = (IFormFile)ctx.SourceValue;
                MemoryStream target = new MemoryStream();
                fileBase.OpenReadStream().CopyTo(target);
                return target.ToArray();
            }
            return null;
        }
    }
}
