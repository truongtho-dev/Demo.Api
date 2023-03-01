using Demo.Api.Config;
using System;

namespace Demo.Api.Attributes
{
	public class AppRoleAttribute: Attribute
	{
		public AppRoles[] AppRoles { get; private set; }
        public AppRoleAttribute(params AppRoles[] appRoles) => AppRoles = appRoles;
        
    }
}
