using System;

namespace task2.Models
{
    // Псевдоэлемент для возврата к родительской папке.
    public class ParentFolderPlaceholder : FileSystemElement
    {
        public ParentFolderPlaceholder(Folder parentFolder)
            : base("..", parentFolder)
        {
        }

        public override ElementType Type => ElementType.Folder;

        public override long Size => 0;
    }
}
