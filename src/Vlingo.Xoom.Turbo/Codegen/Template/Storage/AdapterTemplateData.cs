//// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
////
//// This Source Code Form is subject to the terms of the
//// Mozilla Public License, v. 2.0. If a copy of the MPL
//// was not distributed with this file, You can obtain
//// one at https://mozilla.org/MPL/2.0/.

//using System;
//using System.Collections.Generic;
//using System.Text;
//using Vlingo.Xoom.Annotation.Persistence;
//using Vlingo.Xoom.Codegen.Content;

//namespace Vlingo.Xoom.Codegen.Template.Storage
//{
//    public class AdapterTemplateData : TemplateData
//    {
//        private readonly string _sourceClassName;
//        private readonly TemplateStandard _sourceClassStandard;
//        private readonly TemplateParameters _parameters;

//        public static List<TemplateData> From(string persistencePackage, StorageType storageType, List<ContentBase> contents)
//        {
//            return ContentQuery.findClassNames(storageType.adapterSourceClassStandard, contents)
//                        .stream().map(sourceClassName->
    
//                            new AdapterTemplateData(sourceClassName,
//                                    storageType.adapterSourceClassStandard,
//                                    persistencePackage, storageType, contents)
//                        ).collect(Collectors.toList());

//            return ContentQuery.FindClassNames(storageType)
//        }

//        public AdapterTemplateData(final String sourceClassName,
//                                   final TemplateStandard sourceClassStandard,
//                                   final String persistencePackage,
//                                   final StorageType storageType,
//                                   final List<Content> contents)
//        {
//            this.sourceClassName = sourceClassName;
//            this.sourceClassStandard = sourceClassStandard;
//            this.parameters = loadParameters(persistencePackage, storageType, contents);
//        }

//        private TemplateParameters loadParameters(final String packageName,
//                                                  final StorageType storageType,
//                                                  final List<Content> contents)
//        {
//            final String sourceQualifiedClassName =
//                    ContentQuery.findFullyQualifiedClassName(sourceClassStandard, sourceClassName, contents);

//            return TemplateParameters.with(PACKAGE_NAME, packageName)
//                    .and(IMPORTS, ImportParameter.of(sourceQualifiedClassName))
//                    .and(SOURCE_NAME, sourceClassName)
//                    .and(ADAPTER_NAME, ADAPTER.resolveClassname(sourceClassName))
//                    .and(STORAGE_TYPE, storageType);
//        }

//        @Override
//    public TemplateParameters parameters()
//        {
//            return parameters;
//        }

//        @Override
//    public String filename()
//        {
//            return standard().resolveFilename(sourceClassName, parameters);
//        }

//        @Override
//    public TemplateStandard standard()
//        {
//            return ADAPTER;
//        }

//    }
//}
