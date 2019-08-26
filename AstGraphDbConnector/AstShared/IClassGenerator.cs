namespace AstShared
{
    public interface IClassGenerator
    {
        void CreateClassInFolder(string folder, string className, string baseClassName, string @namespace);
    }
}
