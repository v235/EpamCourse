using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace RoslynMyRules
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class RoslynMyRulesAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "RoslynMyRules";

        // You can change these strings in the Resources.resx file. If you do not want your analyzer to be localize-able, you can use regular strings for Title and MessageFormat.
        // See https://github.com/dotnet/roslyn/blob/master/docs/analyzers/Localizing%20Analyzers.md for more on localization
        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.AnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.AnalyzerDescription), Resources.ResourceManager, typeof(Resources));
        private const string Category = "Naming";

        private static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            // TODO: Consider registering other actions that act on syntax instead of or in addition to symbols
            // See https://github.com/dotnet/roslyn/blob/master/docs/analyzers/Analyzer%20Actions%20Semantics.md for more information
            context.RegisterSymbolAction(CheckControllersSuffix, SymbolKind.NamedType);
        }

        private static void CheckControllersSuffix(SymbolAnalysisContext context)
        {
            var namedTypeSymbol = (INamedTypeSymbol)context.Symbol;
            var ControllerType = context.Compilation.GetTypeByMetadataName("System.Web.Mvc.Controller");
           // var ourBaseController = context.Compilation.GetTypeByMetadataName("WebApplication.Controllers.BaseController");
            var baseTypes = GetBaseClasses(namedTypeSymbol, context.Compilation.ObjectType);
            var baseAttr = GetAttributes(namedTypeSymbol);
            var Name = namedTypeSymbol.MetadataName;
            var atr=namedTypeSymbol.GetAttributes();
            var Member = GetPublicMethods(namedTypeSymbol);
            if (baseTypes.Contains(ControllerType)&& !baseTypes.Select(b=>b.Name).Contains("Controller"))
            {
                var diagnostic = Diagnostic.Create(Rule, namedTypeSymbol.Locations[0], namedTypeSymbol.Name);
                context.ReportDiagnostic(diagnostic);
            }

        }

        public static ImmutableArray<AttributeData> GetPublicMethods(INamedTypeSymbol type)
        {
            var clas = type.GetAttributes().Select(a=>a.AttributeClass.MetadataName);
            var m1 = type.GetMembers();
            var methods = type.GetMembers()
                .Where(m => m.DeclaredAccessibility == Accessibility.Public && m.MetadataName != ".ctor").Select(c =>
                    c.GetAttributes().Select(a=>a.AttributeClass.MetadataName));
            return type.GetAttributes();
        }

        public static ImmutableArray<AttributeData> GetAttributes(INamedTypeSymbol type)
        {
            return type.GetAttributes();
        }
        public static ImmutableArray<INamedTypeSymbol> GetBaseClasses(INamedTypeSymbol type, INamedTypeSymbol objectType)
        {
            if (type == null || type.TypeKind == TypeKind.Error)
                return ImmutableArray<INamedTypeSymbol>.Empty;

            if (type.BaseType != null && type.BaseType.TypeKind != TypeKind.Error)
                return GetBaseClasses(type.BaseType, objectType).Add(type.BaseType);

            return ImmutableArray<INamedTypeSymbol>.Empty;
        }
    }
}
