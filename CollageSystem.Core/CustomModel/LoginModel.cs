﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollageSystem.Core.CustomModel
{
	public class LoginModel
	{
		public string EmailAddress { get; set; }
		public string Password { get; set; }

		public bool RememberMe { get; set; }
	}
}
