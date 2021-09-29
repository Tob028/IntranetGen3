﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Havit.Data.Patterns.Repositories;
using MensaGymnazium.IntranetGen3.Model.Security;

namespace MensaGymnazium.IntranetGen3.DataLayer.Repositories.Security
{
	public partial interface IUserRepository
	{
		List<User> GetAllIncludingDeleted();
		Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
		Task<User> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
		Task<List<User>> GetUsersInRoleAsync(Role.Entry roleEntry, CancellationToken cancellationToken = default);
	}
}