// // Copyright © 2012-2020 VLINGO LABS. All rights reserved.
// //
// // This Source Code Form is subject to the terms of the
// // Mozilla Public License, v. 2.0. If a copy of the MPL
// // was not distributed with this file, You can obtain
// // one at https://mozilla.org/MPL/2.0/.
//
// using System;
// using System.Collections.Generic;
// using System.Text;
//
// namespace Vlingo.Xoom.Codegen.Content {
//     public abstract class Content {
//
//         public readonly TemplateStandard standard;
//
//         protected Content(TemplateStandard standard) {
//             this.standard = standard;
//         } 
//
//         public static Content with(TemplateStandard standard,
//                                    TemplateFile templatefile,
//                                    Filer filer,
//                                    Element source,
//                                    string text) {
//             return new TextBasedContent(standard, templatefile, source, filer, text);
//         }
//
//         public static Content with(TemplateStandard standard,
//                                    TypeElement type) {
//             return new TypeBasedContent(standard, type);
//         }
//
//         public static Content with(TemplateStandard standard,
//                                    TypeElement protocolType,
//                                    TypeElement actorType) {
//             return new ProtocolBasedContent(standard, protocolType, actorType);
//         }
//
//         public abstract void create();
//
//         public abstract string retrieveClassName();
//
//         public abstract string retrievePackage();
//
//         public abstract string retrieveQualifiedName();
//
//         public abstract bool canWrite();
//
//         public abstract bool contains(string term);
//
//         public String retrieveProtocolQualifiedName() {
//             throw new UnsupportedOperationException("Content does not have a protocol by default");
//         }
//
//         public bool isProtocolBased() {
//             return false;
//         }
//
//         public bool has(TemplateStandard standard) {
//             return this.standard.Equals(standard);
//         }
//
//     }
// }
