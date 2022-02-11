// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Xoom.Turbo.Codegen.Parameter;

namespace Vlingo.Xoom.Turbo.Annotation.Codegen.AutoDispatch;

public class PathFormatter
{
    public static string FormatAbsoluteRoutePath(CodeGenerationParameter routeParameter)
    {
        var routePath = routeParameter.RetrieveRelatedValue(Label.RoutePath);
        var uriRoot = routeParameter.Parent().RetrieveRelatedValue(Label.UriRoot);
        return FormatAbsoluteRoutePath(uriRoot, routePath);
    }

    public static string FormatRelativeRoutePath(CodeGenerationParameter routeParameter)
    {
        var routePath = routeParameter.RetrieveRelatedValue(Label.RoutePath);
        if (routePath == string.Empty || RemoveSurplusesSlashes(routePath) == "/")
        {
            return string.Empty;
        }
        if (routePath.EndsWith("/"))
        {
            return routePath.Substring(0, routePath.Length - 1);
        }
        return routePath;
    }

    public static string FormatRootPath(string uriRoot) => RemoveSurplusesSlashes(string.Format("/{0}", uriRoot));

    public static string FormatAbsoluteRoutePath(string rootPath, string routePath)
    {
        if (routePath == string.Empty || routePath == "/")
        {
            return rootPath;
        }

        if (!routePath.StartsWith(rootPath))
        {
            return PrependRootPath(rootPath, routePath);
        }

        return routePath;
    }

    private static string PrependRootPath(string rootPath, string routePath) => RemoveSurplusesSlashes(string.Format("{0}/{1}", rootPath, routePath));

    private static string RemoveSurplusesSlashes(string path)
    {
        var cleanPath = path;
        while (cleanPath.Contains("//"))
        {
            cleanPath = cleanPath.Replace("//", "/");
        }
        return cleanPath;
    }
}