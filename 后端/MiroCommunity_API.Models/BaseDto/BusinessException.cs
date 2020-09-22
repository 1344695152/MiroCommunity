using System;
using System.Collections.Generic;
using System.Text;

namespace MiroCommunity_API.Models.BaseDto
{
    public class BusinessException : Exception
    {
        public int Code { get; set; }

        public BusinessException(string msg) : base(msg)
        {
        }

        public BusinessException(string msg, int code) : base(msg)
        {
            Code = code;
        }
    }
}