// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.IO;
using Vlingo.Xoom.Codegen.Template;

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

        //        private void HandleDefaultCreation() throws IOException
        //        {
        //                if (Files.isRegularFile(file.toPath()))
        //                {
        //                    Files.write(file.toPath(), text.getBytes(), APPEND);
        //                }
        //                else
        //                {
        //                    file.getParentFile().mkdirs();
        //    file.createNewFile();
        //                    Files.write(file.toPath(), text.getBytes());
        //                }
        //            }

        //            private void handleCreationFromSourceElement() throws IOException
        //{
        //    final Writer writer =
        //                        filer.createSourceFile(retrieveQualifiedName(), source).openWriter();
        //    writer.write(text);
        //    writer.close();
        //}

        public override string RetrieveClassName() => FilenameUtils.RemoveExtension(file.getName());

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
