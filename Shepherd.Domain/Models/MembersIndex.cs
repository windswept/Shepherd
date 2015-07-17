﻿using Shepherd.Data.Contracts.Infrastructure;
using Shepherd.Data.Infrastructure;
using Shepherd.Domain.Contracts.Models;
using System.Collections.Generic;
using System.Linq;

namespace Shepherd.Domain.Models
{
	public sealed class MembersIndex
		: IMembersIndex
	{
		public string ChurchId { get; set; }

		public string Name { get; set; }

		public List<Member> Members { get; set; }

		private readonly IUnitOfWork unitOfWork;

		public MembersIndex()
			: this(UnitOfWork.Instance)
		{
		}

		public MembersIndex(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
			this.Members = new List<Member>();
		}

		public void Load()
		{
			unitOfWork.MemberRepository
				.FindBy(_ =>
					(_.Person.FirstName.Contains(this.Name) || _.Person.LastName.Contains(this.Name))
					&& _.ChurchId.Contains(this.ChurchId))
				.OrderByDescending(_ => _.DateCreated)
				.ToList()
				.ForEach(_ =>
				{
					var member = new Member();
					member.LoadFromEntity(_);
					this.Members.Add(member);
				});
		}
	}
}