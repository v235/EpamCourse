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
        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.AnalyzerTitle),
            Resources.ResourceManager, typeof(Resources));

        private static readonly LocalizableString MessageFormat =
            new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormat), Resources.ResourceManager,
                typeof(Resources));

        private static readonly LocalizableString Description =
            new LocalizableResourceString(nameof(Resources.AnalyzerDescription), Resources.ResourceManager,
                typeof(Resources));

        private const string Category = "Attribute";
        private static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat,
            Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);


        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        {
            get { return ImmutableArray.Create(Rule); }
        }

        public override void Initialize(AnalysisContext context)
        {
            // See https://github.com/dotnet/roslyn/blob/master/docs/analyzers/Analyzer%20Actions%20Semantics.md for more information
            context.RegisterSymbolAction(CheckControllersSuffix, SymbolKind.NamedType);
        }

        private static void CheckControllersSuffix(SymbolAnalysisContext context)
        {
            var namedTypeSymbol = (INamedTypeSymbol) context.Symbol;
            var ControllerType = context.Compilation.GetTypeByMetadataName("System.Web.Mvc.Controller");
            var baseTypes = GetBaseClasses(namedTypeSymbol, context.Compilation.ObjectType);
            var baseAttrs = GetClassAttributes(namedTypeSymbol);
            var methods = GetPublicMethods(namedTypeSymbol);

            if (baseTypes.Contains(ControllerType))
            {
                if (!baseAttrs.Any(a => a.Contains("AuthorizeAttribute")))
                {
                    var diagnostic = Diagnostic.Create(Rule, namedTypeSymbol.Locations[0], namedTypeSymbol.Name);
                    context.ReportDiagnostic(diagnostic);
                }
                if ((methods.Count() > 0) && (!methods.All(m =>
                        m.GetAttributes().Select(a => a.AttributeClass.MetadataName)
                            .Any(a => a.Contains("AuthorizeAttribute")))))
                {
                    var diagnostic = Diagnostic.Create(Rule, namedTypeSymbol.Locations[0], namedTypeSymbol.Name);
                    context.ReportDiagnostic(diagnostic);
                }
            }

        }

        private static IEnumerable<ISymbol> GetPublicMethods(INamedTypeSymbol type)
        {
            return type.GetMembers()
                .Where(m => m.DeclaredAccessibility == Accessibility.Public && m.MetadataName != ".ctor")
                .Select(m => m);
        }

        private static IEnumerable<string> GetClassAttributes(INamedTypeSymbol type)
        {
            return type.GetAttributes().Select(a => a.AttributeClass.MetadataName);
        }

        public static ImmutableArray<INamedTypeSymbol> GetBaseClasses(INamedTypeSymbol type,
            INamedTypeSymbol objectType)
        {
            if (type == null || type.TypeKind == TypeKind.Error)
                return ImmutableArray<INamedTypeSymbol>.Empty;

            if (type.BaseType != null && type.BaseType.TypeKind != TypeKind.Error)
                return GetBaseClasses(type.BaseType, objectType).Add(type.BaseType);

            return ImmutableArray<INamedTypeSymbol>.Empty;
        }
    }
}
