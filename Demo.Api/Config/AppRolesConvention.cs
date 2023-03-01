using Demo.Api.Attributes;
using Demo.Api.Extensions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Linq;

namespace Demo.Api.Config
{
	public class AppRolesConvention : IControllerModelConvention
	{
		private readonly AppRoles _appRoles;

        public AppRolesConvention(AppRoles appRoles)
        {
            _appRoles = appRoles;
        }
        public void Apply(ControllerModel controller)
		{
			var actions = controller.Actions.Where(a => !a.Attributes.Any(attr => attr.GetType() == typeof(AppRoleAttribute) && ((AppRoleAttribute)attr).AppRoles.Contains(_appRoles))).ToList();
			controller.Actions.RemoveRange(actions);
		}
	}

	public enum AppRoles
	{
		App = 1,
		IntegrationTest = 2,
		OtherApp = 3
	}
}
