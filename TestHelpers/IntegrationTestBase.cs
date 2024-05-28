﻿using Havit.Data.EntityFrameworkCore;
using Havit.Data.Patterns.DataSeeds;
using MensaGymnazium.IntranetGen3.DataLayer.Seeds.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MensaGymnazium.IntranetGen3.DependencyInjection;

namespace MensaGymnazium.IntranetGen3.TestHelpers;

public class IntegrationTestBase
{
	private IDisposable _scope;

	protected IServiceProvider ServiceProvider { get; private set; }

	protected virtual bool UseLocalDb => false;
	protected virtual bool DeleteDbData => true;

	protected virtual bool SeedData => true;

	[TestInitialize]
	public virtual void TestInitialize()
	{
		IServiceCollection services = CreateServiceCollection();
		IServiceProvider serviceProvider = services.BuildServiceProvider();

		_scope = serviceProvider.CreateScope();

		var dbContext = serviceProvider.GetRequiredService<IDbContext>();
		if (DeleteDbData)
		{
			dbContext.Database.EnsureDeleted();
		}
		if (UseLocalDb)
		{
			dbContext.Database.Migrate();
		}

		if (this.SeedData)
		{
			var dataSeedRunner = serviceProvider.GetRequiredService<IDataSeedRunner>();
			dataSeedRunner.SeedData<CoreProfile>();
		}

		this.ServiceProvider = serviceProvider;
	}

	[TestCleanup]
	public virtual void TestCleanup()
	{
		_scope.Dispose();
		if (this.ServiceProvider is IDisposable)
		{
			((IDisposable)this.ServiceProvider).Dispose();
		}
		this.ServiceProvider = null;
	}

	protected virtual IServiceCollection CreateServiceCollection()
	{
		IServiceCollection services = new ServiceCollection();
		services.ConfigureForTests(useInMemoryDb: !UseLocalDb);

		return services;
	}
}
