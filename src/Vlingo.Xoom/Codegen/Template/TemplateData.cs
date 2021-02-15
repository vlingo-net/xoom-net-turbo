// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Vlingo.Xoom.Codegen.Template
{
    public abstract class TemplateData
    {
        private readonly List<TemplateData> _dependencies = new List<TemplateData>();

        public abstract TemplateParameters Parameters();

        public abstract TemplateStandard Standard();

        protected void DependOn(TemplateData templateData) => DependOn(new List<TemplateData> { templateData });

        protected void DependOn(params List<TemplateData>[] templatesData) => _dependencies.AddRange(templatesData.SelectMany(x => x));

        public void HandleDependencyOutcome(TemplateStandard standard, string outcome) => throw new NotSupportedException("Unable to handle dependency outcome");

        public string Filename() => Standard().ResolveFilename(Parameters());

        public bool HasStandard(TemplateStandard standard) => Standard().Equals(standard);

        public IReadOnlyList<TemplateData> Dependencies() => _dependencies;

        public bool IsPlaceholder => false;
    }
}
