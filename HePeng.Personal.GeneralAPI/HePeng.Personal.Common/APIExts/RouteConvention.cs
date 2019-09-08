using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HePeng.Personal.Common.ApiExts
{
    public class RouteConvention : IApplicationModelConvention
    {
        private readonly AttributeRouteModel _centralPrefix;

        public RouteConvention(IRouteTemplateProvider routeTemplateProvider)
        {
            _centralPrefix = new AttributeRouteModel(routeTemplateProvider);
        }

        public void Apply(ApplicationModel application)
        {
            foreach (var item in application.Controllers)
            {
                var matchedSelectors = item.Selectors.Where(p => p.AttributeRouteModel != null).ToList();
                matchedSelectors.ForEach(p =>
                {
                    p.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(_centralPrefix, p.AttributeRouteModel);
                });
                var unmatchedSeletors = item.Selectors.Where(p => p.AttributeRouteModel == null).ToList();
                unmatchedSeletors.ForEach(p =>
                {
                    p.AttributeRouteModel = _centralPrefix;
                });
            }
        }
    }
}
