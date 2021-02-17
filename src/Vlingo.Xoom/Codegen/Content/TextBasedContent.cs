// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using static System.IO.File;
using Vlingo.Xoom.Codegen.Template;
using System.IO;

namespace Vlingo.Xoom.Codegen.Content
{
    public class TextBasedContent : ContentBase
    {
        public readonly FileStream file;
        public readonly string text;
        private readonly FileStream _filer;
        private readonly Type _source;

        public TextBasedContent(TemplateStandard standard, TemplateFile templateFile, Type source, FileStream filer, string text) : base(standard)
        {
            this.text = text;
            _filer = filer;
            _source = source;
            file = templateFile.ToFile();
        }

        public override void Create()
        {
            try
            {
                if (_filer == null)
                {
                    HandleDefaultCreation();
                }
                else
                {
                    HandleCreationFromSourceElement();
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void HandleDefaultCreation()
        {
            try
            {
                if (Exists(file.Name))
                {
                    AppendAllText(file.Name, text);
                }
                else
                {
                    WriteAllText(System.IO.Directory.GetParent(file.Name).FullName, text);
                }
            }
            catch (Exception ex)
            {
                throw new IOException(ex.Message, ex);
            }

        }

        private void HandleCreationFromSourceElement()
        {
            try
            {
                var path = string.Format("{0}/{1}.{2}", Environment.CurrentDirectory, RetrieveQualifiedName(), _source.Name);
                WriteAllText(path, text);
            }
            catch (Exception ex)
            {
                throw new IOException(ex.Message, ex);
            }
        }

        public override string RetrieveClassName() => Path.GetFileNameWithoutExtension(file.Name);

        public override string RetrievePackage()
        {
            var packageStartIndex = text.IndexOf("package");
            var packageEndIndex = text.IndexOf(";");
            return text.Substring(packageStartIndex + 8, packageEndIndex);
        }

        public override string RetrieveQualifiedName() => string.Format("{0}.{1}", RetrievePackage(), RetrieveClassName());

        public override bool Contains(string term) => text.Contains(term);

        public override bool CanWrite() => true;
    }
}
