    
Microsoft.CodeAnalysis.CSharp.Syntax.StructuredTriviaSyntax

Unhandled Exception: System.NotSupportedException: The language 'C#' is not supported.
   at Microsoft.CodeAnalysis.Host.HostWorkspaceServices.GetLanguageServices(String languageName) in /_/src/Workspaces/Core/Portable/Workspace/Host/HostWorkspaceServices.cs:line 93
   at Microsoft.CodeAnalysis.Host.Mef.MefWorkspaceServices.GetLanguageServices(String languageName) in /_/src/Workspaces/Core/Portable/Workspace/Host/Mef/MefWorkspaceServices.cs:line 153
   at Microsoft.CodeAnalysis.Editing.SyntaxGenerator.GetGenerator(Workspace workspace, String language) in /_/src/Workspaces/Core/Portable/Editing/SyntaxGenerator.cs:line 47
   at AstRoslyn.ClassGenerator.CreateClassInFolder(String folder, String className, String baseClassName) in /home/eugene/Projects/iso3/AstGraphDbConnector/AstRoslyn/ClassGenerator.cs:line 19
   at AstRoslyn.SyntaxGeneratorVisitor.Visit(Type t) in /home/eugene/Projects/iso3/AstGraphDbConnector/AstRoslyn/SyntaxGeneratorVisitor.cs:line 12
   at AstRoslyn.SyntaxNodesTree.CreateTypesTree() in /home/eugene/Projects/iso3/AstGraphDbConnector/AstRoslyn/SyntaxNodesTree.cs:line 59
   at AstTests.Program.Main(String[] args) in /home/eugene/Projects/iso3/AstGraphDbConnector/AstTests/Program.cs:line 33