using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace task2.Models
{
    public class Folder : FileSystemElement
    {
        
        // Список вложенных элементов
        public ObservableCollection<FileSystemElement> Elements { get; set; } = new();

        public Folder(string name, Folder? parentFolder = null) : base(name, parentFolder)
        {
        }

        public override ElementType Type => ElementType.Folder;

        public override long Size => Elements.Sum(e => e.Size);
    }
}
