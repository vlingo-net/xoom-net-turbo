// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.IO;
using System.Linq;

namespace Vlingo.Xoom.Codegen.Template
{
    public class TemplateProcessor
    {
        private static TemplateProcessor _instance;
        private static readonly string _templatePathPattern = "codegen/{0}.ftl";

        private TemplateProcessor()
        {
        }

        public static TemplateProcessor Instance()
        {
            if (_instance == null)
            {
                _instance = new TemplateProcessor();
            }
            return _instance;
        }

        public string Process(TemplateData mainTemplateData)
        {
            mainTemplateData.Dependencies().ToList().ForEach(templateData =>
            {
                var outcome = Process(templateData.Standard(), templateData.Parameters());

                mainTemplateData.HandleDependencyOutcome(templateData.Standard(), outcome);
            });

            return Process(mainTemplateData.Standard(), mainTemplateData.Parameters());
        }

        //TODO: Configuration and Template from freemarker is now visible.
        private string Process(TemplateStandard standard, TemplateParameters parameters)
        {
            try
            {
                var templateFilename = standard.RetrieveTemplateFilename(parameters);

                var templatePath = string.Format(_templatePathPattern, templateFilename);

                //var template = TemplateProcessorConfiguration.Instance().configuration.GetTemplate(templatePath);

                var writer = new StringWriter();
                //template.process(parameters.map(), writer);
                return writer.ToString();
            }
            catch (IOException exception)
            {
                throw new CodeGenerationException(exception);
            }
            //catch (TemplateException exception)
            //    throw new CodeGenerationException(exception);
            //}
        }
    }
}