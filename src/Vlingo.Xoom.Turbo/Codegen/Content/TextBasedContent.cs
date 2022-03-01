// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using Vlingo.Xoom.Turbo.Codegen.Template;

namespace Vlingo.Xoom.Turbo.Codegen.Content;

using System.IO;
using static System.IO.File;

public class TextBasedContent : ContentBase
{
	public readonly FileStream File;
	public readonly string Text;
	private readonly FileStream _filer;
	private readonly Type _source;

	public TextBasedContent(TemplateStandard standard, TemplateFile templateFile, Type source, FileStream filer,
		string text) : base(standard)
	{
		Text = text;
		_filer = filer;
		_source = source;
		File = templateFile.ToFile();
	}

	public TextBasedContent(TemplateStandard standard, OutputFile file, Type source, FileStream filer, string text) :
		base(standard)
	{
		Text = text;
		_filer = filer;
		_source = source;
		File = file.ToFile();
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
			if (Exists(File.Name))
			{
				AppendAllText(File.Name, Text);
			}
			else
			{
				WriteAllText(Directory.GetParent(File.Name).FullName, Text);
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
			WriteAllText(path, Text);
		}
		catch (Exception ex)
		{
			throw new IOException(ex.Message, ex);
		}
	}

	public override string RetrieveClassName() => Path.GetFileNameWithoutExtension(File.Name);

	public override string RetrievePackage()
	{
		var packageStartIndex = Text.IndexOf("package");
		var packageEndIndex = Text.IndexOf(";");
		return Text.Substring(packageStartIndex + 8, packageEndIndex);
	}

	public override string RetrieveQualifiedName() => string.Format("{0}.{1}", RetrievePackage(), RetrieveClassName());

	public override bool Contains(string term) => Text.Contains(term);

	public override bool CanWrite() => true;
}