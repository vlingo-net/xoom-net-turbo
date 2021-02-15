// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.IO;

namespace Vlingo.Xoom.Codegen.Template
{
    public class TemplateFile
    {

        private readonly string _absolutePath;
        private readonly string _filename;
        private readonly string _offset;
        private readonly bool _placeholder;

        public TemplateFile(CodeGenerationContext context, TemplateData templateData) : this(context.isInternalGeneration() ? "" : FileLocationResolver.from(context, templateData),
                    templateData.filename(), templateData.parameters().find(OFFSET), templateData.isPlaceholder())
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

        public FileStream ToFile() => File.Create(FilePath());

        public string Offset() => _offset;
    }
}
