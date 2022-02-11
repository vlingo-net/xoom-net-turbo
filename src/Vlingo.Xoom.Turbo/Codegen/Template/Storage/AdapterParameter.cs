// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using System.Linq;

namespace Vlingo.Xoom.Turbo.Codegen.Template.Storage;

public class AdapterParameter
{
    private readonly string _sourceClass;
    private readonly string _adapterClass;
    private readonly bool _last;

    private AdapterParameter(int index, int numberOfAdapters, string sourceClass, string adapterClass)
    {
        _sourceClass = sourceClass;
        _adapterClass = adapterClass;
        _last = index == numberOfAdapters - 1;
    }

    private AdapterParameter(int index, int numberOfAdapters, TemplateParameters parameters) : this(index, numberOfAdapters, parameters.Find<string>(TemplateParameter.SourceName), parameters.Find<string>(TemplateParameter.AdapterName))
    {
    }

    public static List<AdapterParameter> From(List<TemplateData> templatesData)
    {
        var adapterTemplates = templatesData.Where(x => x.HasStandard(TemplateStandardType.Adapter)).ToList();
        return Enumerable.Range(0, adapterTemplates.Count).Select(x => new AdapterParameter(x, adapterTemplates.Count, adapterTemplates[x].Parameters())).ToList();
    }

    public string GetSourceClass() => _sourceClass;

    public string GetAdapterClass() => _adapterClass;

    public bool IsLast() => _last;
}