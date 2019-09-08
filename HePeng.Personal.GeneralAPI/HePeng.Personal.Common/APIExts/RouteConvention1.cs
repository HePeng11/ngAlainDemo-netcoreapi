using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HePeng.Personal.Common.ApiExts
{
    public class RouteConvention1 : IApplicationModelConvention
    {
        private readonly string RoutePrefix;

        public RouteConvention1(string routePrefix)
        {
            RoutePrefix = routePrefix;
        }

        public void Apply(ApplicationModel application)
        {
            foreach (var item in application.Controllers)
            {
                var matchedSelectors = item.Selectors.Where(p => p.AttributeRouteModel != null).ToList();
                matchedSelectors.ForEach(p =>
                {
                    p.AttributeRouteModel = new AttributeRouteModel(new RouteAttribute(RoutePrefix + p.AttributeRouteModel.Template.ToLower())); //AttributeRouteModel.CombineAttributeRouteModel(_centralPrefix, p.AttributeRouteModel);
                });
                var unmatchedSeletors = item.Selectors.Where(p => p.AttributeRouteModel == null).ToList();
                unmatchedSeletors.ForEach(p =>
                {
                    p.AttributeRouteModel = new AttributeRouteModel(new RouteAttribute(RoutePrefix));
                });
            }
        }
    }
}
