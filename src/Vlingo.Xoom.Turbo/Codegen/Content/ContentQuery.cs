// Copyright © 2012-2023 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;
using Vlingo.Xoom.Turbo.Codegen.Template;

namespace Vlingo.Xoom.Turbo.Codegen.Content;

public class ContentQuery
{
    //TODO: IsPresent will be added
    public static bool Exists(TemplateStandard standard, IReadOnlyList<ContentBase> contents) => FilterByStandard(standard, contents).ToList().Any();

    public static HashSet<string> FindClassNames(TemplateStandard standard, IReadOnlyList<ContentBase> contents) => new HashSet<string>(FilterByStandard(standard, contents).Select(x => x.RetrieveClassName()));

    public static HashSet<string> FindClassNames(IReadOnlyList<ContentBase> contents, params TemplateStandard[] standards) => new HashSet<string>(standards.SelectMany(standard => FindClassNames(standard, contents)));

    public static HashSet<string> FindClassNames(TemplateStandard standard, string packageName, List<ContentBase> contents) => new HashSet<string>(FilterByStandard(standard, contents).Where(content => content.RetrievePackage() == packageName).Select(x => x.RetrieveClassName()));

    public static HashSet<string> FindFullyQualifiedClassNames(IReadOnlyList<ContentBase> contents, params TemplateStandard[] standards) => new HashSet<string>(standards.SelectMany(standard => FindFullyQualifiedClassNames(standard, contents)));

    public static string FindFullyQualifiedClassName(TemplateStandard standard, string className, IReadOnlyList<ContentBase> contents) => FindFullyQualifiedClassNames(standard, contents).Where(qualifiedClassName => qualifiedClassName.EndsWith(string.Concat(".", className))).FirstOrDefault() ?? throw new ArgumentException();

    public static HashSet<string> FindFullyQualifiedClassNames(TemplateStandard standard, IReadOnlyList<ContentBase> contents) => new HashSet<string>(FilterByStandard(standard, contents).Select(x => x.RetrieveQualifiedName()));

    public static string FindPackage(TemplateStandard standard, List<ContentBase> contents) => FilterByStandard(standard, contents).Select(x => x.RetrievePackage()).FirstOrDefault() ?? string.Empty;

    public static string FindPackage(TemplateStandard standard, string className, List<ContentBase> contents) => FilterByStandard(standard, contents).Where(content => content.RetrieveClassName() == className).Select(x => x.RetrievePackage()).FirstOrDefault() ?? string.Empty;

    public static IEnumerable<ContentBase> FilterByStandard(TemplateStandard standard, IReadOnlyList<ContentBase> contents) => contents.Where(content => content.Has(standard));

}