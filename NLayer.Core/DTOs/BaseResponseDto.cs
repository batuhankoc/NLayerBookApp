using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class BaseResponseDto
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

    }
}
