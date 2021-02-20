// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Actors;

namespace Vlingo.Xoom.Codegen.Template
{
    public class TemplateProcessorConfiguration
    {
        public readonly Configuration configuration;

        private static TemplateProcessorConfiguration _instance;

        public static TemplateProcessorConfiguration Instance()
        {
            if (_instance == null)
            {
                _instance = new TemplateProcessorConfiguration();
            }
            return _instance;
        }

        //TODO
        //private TemplateProcessorConfiguration()
        //{
        //    configuration = new Configuration("DEFAULT_INCOMPATIBLE_IMPROVEMENTS");
        //    configuration.setClassForTemplateLoading(TemplateProcessor.class, "/");
        //    configuration.setDefaultEncoding("UTF-8");
        //    configuration.setLocale(Locale.US);
        //    configuration.setTemplateExceptionHandler(TemplateExceptionHandler.RETHROW_HANDLER);
        //}
    }
}
