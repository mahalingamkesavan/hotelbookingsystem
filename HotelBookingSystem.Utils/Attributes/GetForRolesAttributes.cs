using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Diagnostics.CodeAnalysis;

namespace HotelBookingSystem.Utils.Attributes
{
    public class GetForRolesAttributes : AuthorizeAttribute, IActionHttpMethodProvider, IRouteTemplateProvider
    {
        private readonly List<string> _httpMethods = new() { "GET" };

        private int? _order;

        public GetForRolesAttributes() { }

        /// <summary>
        /// Creates a new <see cref="HttpMethodAttribute"/> with the given
        /// set of HTTP methods an the given route template.
        /// </summary>
        /// <param name="httpMethods">The set of supported methods. May not be null.</param>
        /// <param name="template">The route template.</param>
        public GetForRolesAttributes(string? template, string roles)
        {
            Template = template;
            Roles = roles;
        }

        /// <inheritdoc />
        public IEnumerable<string> HttpMethods => _httpMethods;

        /// <inheritdoc />
        public string? Template { get; }

        /// <summary>
        /// Gets the route order. The order determines the order of route execution. Routes with a lower
        /// order value are tried first. When a route doesn't specify a value, it gets the value of the
        /// <see cref="RouteAttribute.Order"/> or a default value of 0 if the <see cref="RouteAttribute"/>
        /// doesn't define a value on the controller.
        /// </summary>
        public int Order
        {
            get { return _order ?? 0; }
            set { _order = value; }
        }

        /// <inheritdoc />
        int? IRouteTemplateProvider.Order => _order;

        /// <inheritdoc />
        [DisallowNull]
        public string? Name { get; set; }
    }
}
