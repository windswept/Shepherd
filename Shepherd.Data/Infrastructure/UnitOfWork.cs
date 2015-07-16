﻿using Shepherd.Data.Contracts.Infrastructure;
using Shepherd.Data.Contracts.Repository;
using Shepherd.Data.Repository;
using Shepherd.Entities;
using Shepherd.Entities.Contracts;
using System;

namespace Shepherd.Data.Infrastructure
{
	public class UnitOfWork : IUnitOfWork
	{
		private static readonly UnitOfWork instance = new UnitOfWork();
		public static UnitOfWork Instance { get { return instance; } }

		private IShepherdContext context;
		public IShepherdContext Context
		{
			get
			{
				if (context == null)
				{
                    context = new ShepherdContext();
				}
				return context;
			}
			set
			{
				context = value;
			}
		}

		private ILookupRepository lookupRepository;
		public ILookupRepository LookupRepository
		{
			get
			{
				if (lookupRepository == null)
				{
					lookupRepository = new LookupRepository();
					lookupRepository.Context = this.Context;
				}
				return lookupRepository;
			}
			set
			{
				lookupRepository = value;
			}
		}

		private ILookupTypeRepository lookupTypeRepository;
		public ILookupTypeRepository LookupTypeRepository
		{
			get
			{
				if (lookupTypeRepository == null)
				{
					lookupTypeRepository = new LookupTypeRepository();
					lookupTypeRepository.Context = this.Context;
				}
				return lookupTypeRepository;
			}
			set
			{
				lookupTypeRepository = value;
			}
		}

		private IMemberRepository memberRepository;
		public IMemberRepository MemberRepository
		{
			get
			{
				if (memberRepository == null)
				{
					memberRepository = new MemberRepository();
					memberRepository.Context = this.Context;
				}
				return memberRepository;
			}
			set
			{
				memberRepository = value;
			}
		}

		private IPersonRepository personRepository;
		public IPersonRepository PersonRepository
		{
			get
			{
				if (personRepository == null)
				{
					personRepository = new PersonRepository();
					personRepository.Context = this.Context;
				}
				return personRepository;
			}
			set
			{
				personRepository = value;
			}
		}

		public int Save()
		{
			// TODO: Provide exception handling
			return this.Context.SaveChanges();
		}

		private bool disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing && context != null)
				{
					context.Dispose();
				}
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}