// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Vlingo.Xoom.Turbo.Annotation;

public class TypeReader
{
	private const string DefaultMatchPattern = " {0} ";
	private const string VariableMatchPattern = " {0}=";

	private readonly Type _type;
	private readonly string _sourceCode;

	public static TypeReader From(Type typeElement) => new TypeReader(typeElement);

	private TypeReader(Type type)
	{
		_type = type;
		_sourceCode = ReadSourceCode(type);
	}

	public string FindMemberValue(MemberInfo member)
	{
		var memberName = member.Name;
		if (!HasMember(memberName))
		{
			throw InvalidMemberArgumentException(memberName);
		}

		return ExtractMemberValue(memberName);
	}

	public string FindMemberValue(string memberName) =>
		FindMembers().Select(member => member.Name)
			.Where(name => name == memberName)
			.Select(member => ExtractMemberValue(memberName))
			.FirstOrDefault() ?? throw InvalidMemberArgumentException(memberName);

	public IEnumerable<MemberInfo> FindMembers() => _type.GetMembers();

	private string ExtractMemberValue(string memberName)
	{
		var elementIndex = CodeElementIndex(memberName, new[] { DefaultMatchPattern, VariableMatchPattern });
		var valueIndex = elementIndex + memberName.Count() + 1;
		var codeSlice = _sourceCode.Substring(valueIndex).Replace("=", "").Trim();
		return codeSlice.Substring(0, codeSlice.IndexOf(";", StringComparison.Ordinal)).Trim();
	}

	private string ReadSourceCode(Type typeElement)
	{
		try
		{
			var location = typeElement.Assembly.Location;
			using var stream = ClassFile.From(location, typeElement).OpenInputStream();
			return File.ReadAllText(new StreamReader(stream).ReadLine(), Encoding.UTF8).Replace("\r\n", " ");
		}
		catch (IOException e)
		{
			throw new ProcessingAnnotationException(e);
		}
	}

	private ArgumentException InvalidMemberArgumentException(string memberName) => 
		new ArgumentException($"Member {memberName} not found in {_type.AssemblyQualifiedName}");

	private bool HasMember(string memberName) =>
		_type.GetMembers()
			.Select(element => element.Name)
			.Any(name => name == memberName);

	private int CodeElementIndex(string elementName, string[] patterns) =>
		patterns.Select(pattern => string.Format(pattern, elementName))
			.Where(term => _sourceCode.Contains(term))
			.Select(term => _sourceCode.IndexOf(term, StringComparison.Ordinal))
			.FirstOrDefault();
}