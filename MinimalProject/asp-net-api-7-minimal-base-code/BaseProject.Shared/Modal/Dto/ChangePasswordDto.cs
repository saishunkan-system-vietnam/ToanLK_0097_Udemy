﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Shared.Modal.Dto
{
    public class ChangePasswordDto
    {
        public string? CurrentPassword { get; set; }
        public string? Password { get; set; }
    }
}
