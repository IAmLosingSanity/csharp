namespace task2.Models
{
    public class File : FileSystemElement
    {
        public long FileSize { get; private set; }

        public File(string name, Folder? parentFolder, long fileSize) : base(name, parentFolder)
        {
            FileSize = fileSize;
        }

        public override ElementType Type => ElementType.File;

        public override long Size => FileSize;
    }
}
