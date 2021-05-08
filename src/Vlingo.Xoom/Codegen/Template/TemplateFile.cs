// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using static System.IO.File;
using Vlingo.Xoom.Codegen.File;
using System.IO;

namespace Vlingo.Xoom.Codegen.Template
{
    public class TemplateFile
    {
        private readonly string _absolutePath;
        private readonly string _filename;
        private readonly string _offset;
        private readonly bool _placeholder;

        public TemplateFile(CodeGenerationContext context, TemplateData templateData) : this(context.IsInternalGeneration ? "" : FileLocationResolver.From(context, templateData),
                    templateData.Filename(), templateData.Parameters().Find<string>(TemplateParameter.Offset), templateData.IsPlaceholder)
        {
        }

        public TemplateFile(string absolutePath, string filename) : this(absolutePath, filename, "", false)
        {
        }

        private TemplateFile(string absolutePath, string filename, string offset, bool placeholder)
        {
            _absolutePath = absolutePath;
            _filename = filename;
            _offset = offset;
            _placeholder = placeholder;
        }

        public bool IsPlaceholder => _placeholder;

        public string Filename() => _filename;

        public string FilePath() => Path.Combine(_absolutePath, _filename);

        public FileStream ToFile() => Create(FilePath());

        public string Offset() => _offset;
    }
}
