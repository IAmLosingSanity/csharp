using System;
using System.IO;
using System.Linq;

namespace task2.Models
{
    public enum ElementType
    {
        Folder,
        File
    }

    public abstract class FileSystemElement
    {
        public string Name { get; set; }
        public Folder? ParentFolder { get; set; }  // Если элемент корневой, то null

        // Вычисляемое свойство: местоположение = путь родительской папки + имя элемента
        public string Location => ParentFolder == null ? Name : Path.Combine(ParentFolder.Location, Name);

        public FileSystemElement(string name, Folder? parentFolder)
        {
            Name = name;
            ParentFolder = parentFolder;
        }

        // Абстрактное свойство, определяющее тип элемента
        public abstract ElementType Type { get; }

        // Абстрактное свойство, возвращающее размер
        public abstract long Size { get; }

        // Переопределения для сравнения (warning CS0660/0661)
        public override bool Equals(object? obj)
        {
            if (obj is FileSystemElement other)
                return Name == other.Name && Location == other.Location;
            return false;
        }

        public override int GetHashCode() => HashCode.Combine(Name, Location);

        // Статический метод копирования элемента в целевую папку
        public static FileSystemElement Copy(FileSystemElement element, Folder targetFolder)
        {
            if (element is File file)
            {
                // Для файла создаём новый с таким же размером
                return new File(file.Name, targetFolder, file.FileSize);
            }
            else if (element is Folder folder)
            {
                // Для папки создаём копию, копируя вложенные элементы
                Folder newFolder = new Folder(folder.Name, targetFolder);
                foreach (var child in folder.Elements)
                {
                    var childCopy = Copy(child, newFolder);
                    newFolder.Elements.Add(childCopy);
                }
                return newFolder;
            }
            else
            {
                throw new InvalidOperationException("Неизвестный тип элемента.");
            }
        }

        // Статический метод перемещения элемента в целевую папку
        public static void Move(FileSystemElement element, Folder targetFolder)
        {
            if (element.ParentFolder != null)
            {
                element.ParentFolder.Elements.Remove(element);
            }
            element.ParentFolder = targetFolder;
            targetFolder.Elements.Add(element);
        }
    }
}
