﻿using NHibernate;
using NHibernate.Mapping.ByCode.Conformist;
using StackOverflow.Infrastructure.Membership.Entities;

namespace StackOverflow.Infrastructure.Membership.IdentityMappings
{
    public class ApplicationUserRoleMap : ClassMapping<ApplicationUserRole>
    {
        public ApplicationUserRoleMap()
        {
            Schema("dbo");
            Table("ApplicationUserRoles");
            ComposedId(id =>
            {
                id.Property(e => e.UserId, prop =>
                {
                    prop.Column("UserId");
                    prop.Type(NHibernateUtil.Guid);
                });
                id.Property(e => e.RoleId, prop =>
                {
                    prop.Column("RoleId");
                    prop.Type(NHibernateUtil.Guid);
                });
            });
        }
    }
}
