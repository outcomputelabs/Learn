using System;
using System.Net;

namespace Microsoft.AspNetCore.Mvc
{
    internal static class ControllerExtensions
    {
        /// <summary>
        /// Creates a <see cref="StatusCodeResult"/> that produces a <see cref="HttpStatusCode.ServiceUnavailable"/> response.
        /// </summary>
        public static StatusCodeResult ServiceUnavailable(this ControllerBase controller)
        {
            if (controller is null) throw new ArgumentNullException(nameof(controller));

            return controller.StatusCode((int)HttpStatusCode.ServiceUnavailable);
        }
    }
}