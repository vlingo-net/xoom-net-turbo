// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.IO;
using System.Linq;
using Vlingo.Xoom.Codegen.Parameter;
using Vlingo.Xoom.Codegen.Template;

namespace Vlingo.Xoom.Codegen.File
{
    public class ExternalFileLocationResolver : IFileLocationResolver
    {
        private static readonly string[] _sourceFolder = new string[] { "src", "main", "java" };
        private static readonly string[] _schemataFolder = new string[] { "src", "main", "vlingo", "schemata" };
        private static readonly string[] _resourceFolder = new string[] { "src", "main", "resources" };

        public string Resolve(CodeGenerationContext context, TemplateData templateData)
        {
            var projectPath = ResolveProjectPath(context);
            var sourceFolders = ListSourceFolders(templateData);
            return Path.Combine(projectPath, string.Join("\\",sourceFolders));
        }

        private string ResolveProjectPath(CodeGenerationContext context)
        {
            var appName = context.ParameterOf<string>(Label.ApplicationName);
            var targetFolder = context.ParameterOf<string>(Label.TargetFolder);
            return Path.Combine(targetFolder, appName);
        }

        private string[] ListSourceFolders(TemplateData templateData)
        {
            if (templateData.Parameters().Find<bool>(TemplateParameter.ResourceFile, false))
            {
                return _resourceFolder;
            }
            if (templateData.Parameters().Find<bool>(TemplateParameter.SchemataFile, false))
            {
                return _schemataFolder;
            }
            if (templateData.Parameters().Find<bool>(TemplateParameter.PomSection, false))
            {
                return new String[] { };
            }
            var packageName = templateData.Parameters().Find<string>(TemplateParameter.PackageName);
            var tempSourceFolder = _sourceFolder.ToList();
            tempSourceFolder.AddRange(packageName.Split(new string[] { "\\." }, StringSplitOptions.None));
            return tempSourceFolder.ToArray();
        }
    }
}
