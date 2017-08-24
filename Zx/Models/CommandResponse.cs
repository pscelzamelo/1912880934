using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zx.Models
{
    public class CommandResponse
    {
        public bool success { get; private set; }
        public IEnumerable<string> errors { get; private set; }
        public object data { get; private set; }

        public CommandResponse(bool success, object data)
        {
            this.success = success;
            errors = new List<string>();
            this.data = data;
        }

        public CommandResponse(bool success, IEnumerable<string> errors, object data)
        {
            this.success = success;
            this.errors = errors;
            this.data = data;
        }
    }
}