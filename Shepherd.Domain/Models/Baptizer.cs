﻿using Shepherd.Domain.Contracts.Models;

namespace Shepherd.Domain.Models
{
	public sealed class Baptizer
		: IBaptizer
	{
		public int Id { get; set; }
	}
}
