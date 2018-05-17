﻿using AutoMapper;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyStore.Business.Entities;
using MyStore.Business.Managers;

namespace MyStore.Mvc.Identity.Stores
{
    public class IdentityUserStore : IUserStore<IdentityUser, string>,
        IUserPasswordStore<IdentityUser, string>,
        IUserLockoutStore<IdentityUser, string>,
        IUserTwoFactorStore<IdentityUser, string>,
        IUserRoleStore<IdentityUser, string>
    {
        private readonly UserManager userManager;


        public IdentityUserStore(UserManager userManager)
        {
            this.userManager = userManager;
        }

        private IdentityUser GetIdentityUser(AppUser appUser)
        {
            var identityUser = Mapper.Map<IdentityUser>(appUser);

            if (appUser == null)
                return null;

            return identityUser;
        }

        public Task CreateAsync(IdentityUser user)
        {
            var appUser = Mapper.Map<AppUser>(user);
            userManager.CreateUser(appUser);
            user.Id = appUser.Id;

            return Task.CompletedTask;
        }

        public Task DeleteAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUser> FindByIdAsync(string userId)
        {
            return Task.FromResult(GetIdentityUser(userManager.GetById(userId)));
        }

        public Task<IdentityUser> FindByNameAsync(string userName)
        {
            return Task.FromResult(GetIdentityUser(userManager.GetByEmail(userName)));
        }

        public Task UpdateAsync(IdentityUser user)
        {
            userManager.UpdateUser(Mapper.Map<AppUser>(user));

            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(IdentityUser user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task<string> GetPasswordHashAsync(IdentityUser user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(IdentityUser user)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetLockoutEndDateAsync(IdentityUser user, DateTimeOffset lockoutEnd)
        {
            return Task.CompletedTask;
        }

        public Task<int> IncrementAccessFailedCountAsync(IdentityUser user)
        {
            return Task.FromResult(0);
        }

        public Task ResetAccessFailedCountAsync(IdentityUser user)
        {
            return Task.CompletedTask;
        }

        public Task<int> GetAccessFailedCountAsync(IdentityUser user)
        {
            return Task.FromResult(0);
        }

        public Task<bool> GetLockoutEnabledAsync(IdentityUser user)
        {
            return Task.FromResult(false);
        }

        public Task SetLockoutEnabledAsync(IdentityUser user, bool enabled)
        {
            return Task.CompletedTask;
        }

        public Task SetTwoFactorEnabledAsync(IdentityUser user, bool enabled)
        {
            return Task.CompletedTask;
        }

        public Task<bool> GetTwoFactorEnabledAsync(IdentityUser user)
        {
            return Task.FromResult(false);
        }

        public Task AddToRoleAsync(IdentityUser user, string roleName)
        {
            user.Role = roleName;
            return Task.CompletedTask;
        }

        public Task RemoveFromRoleAsync(IdentityUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetRolesAsync(IdentityUser user)
        {
            IList<string> rolesList = new List<string>();

            if (!string.IsNullOrEmpty(user.Role))
                rolesList.Add(user.Role);

            return Task.FromResult(rolesList);
        }

        public Task<bool> IsInRoleAsync(IdentityUser user, string roleName)
        {
            return Task.FromResult(user.Role.Equals(roleName, StringComparison.InvariantCultureIgnoreCase));
        }

        public void Dispose()
        {

        }
    }
}