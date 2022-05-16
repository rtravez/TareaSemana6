using System;
using System.Collections.Generic;
using System.Text;

namespace TareaSemana6.Models
{
	public class User {
		public int usuarioId { get; set; }
		public Boolean enabled { get; set; }

		public String password { get; set; }
		public String username { get; set; }
	}
}
