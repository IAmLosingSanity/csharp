using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using task2.Models;
using System.Collections.Generic;

namespace task2.ViewModels
{
    public partial class FileSystemViewModel : ObservableObject
    {
        public Folder RootFolder { get; set; }

        [ObservableProperty]
        private Folder currentFolder;

        [ObservableProperty]
        private FileSystemElement? selectedElement;

        [ObservableProperty]
        private string resultInfo = string.Empty;

        [ObservableProperty]
        private bool isHold;

        [ObservableProperty]
        private FileSystemElement? holdElement;

        public FileSystemViewModel()
        {
            RootFolder = new Folder("Root", null);
            var folderA = new Folder("FolderA", RootFolder);
            var folderB = new Folder("FolderB", RootFolder);
            RootFolder.Elements.Add(folderA);
            RootFolder.Elements.Add(folderB);
            var file1 = new File("File1.txt", RootFolder, 100);
            RootFolder.Elements.Add(file1);
            var file2 = new File("File2.txt", folderA, 200);
            folderA.Elements.Add(file2);

            CurrentFolder = RootFolder;
        }

        public IEnumerable<FileSystemElement> DisplayedElements
        {
            get
            {
                if (CurrentFolder.ParentFolder != null)
                {
                    yield return new ParentFolderPlaceholder(CurrentFolder.ParentFolder);
                }
                foreach (var elem in CurrentFolder.Elements)
                {
                    yield return elem;
                }
            }
        }

        partial void OnCurrentFolderChanged(Folder value)
        {
            OnPropertyChanged(nameof(DisplayedElements));
        }

        [RelayCommand]
        private void CopySelected()
        {
            if (SelectedElement != null && SelectedElement.Name != "..")
            {
                var copy = FileSystemElement.Copy(SelectedElement, CurrentFolder);
                CurrentFolder.Elements.Add(copy);
                ResultInfo = $"Copied {SelectedElement.Name} to {CurrentFolder.Name}.";
                OnPropertyChanged(nameof(DisplayedElements));
            }
            else
            {
                ResultInfo = "No element selected.";
            }
        }

        [RelayCommand]
        private void OpenFolder()
        {
            if (SelectedElement is ParentFolderPlaceholder)
            {
                if (CurrentFolder.ParentFolder != null)
                {
                    CurrentFolder = CurrentFolder.ParentFolder;
                    ResultInfo = $"Moved back to folder: {CurrentFolder.Name}.";
                }
                else
                {
                    ResultInfo = "Already at root folder.";
                }
            }
            else if (SelectedElement is Folder folder)
            {
                CurrentFolder = folder;
                ResultInfo = $"Now inside folder: {folder.Name}.";
            }
            else
            {
                ResultInfo = "Selected element is not a folder.";
            }
            OnPropertyChanged(nameof(DisplayedElements));
        }

        [RelayCommand]
        private void GoBack()
        {
            if (CurrentFolder.ParentFolder != null)
            {
                CurrentFolder = CurrentFolder.ParentFolder;
                ResultInfo = $"Moved back to folder: {CurrentFolder.Name}.";
                OnPropertyChanged(nameof(DisplayedElements));
            }
            else
            {
                ResultInfo = "Already at root folder.";
            }
        }

        [RelayCommand]
        private void MoveOrPaste()
        {
            if (!IsHold)
            {
                if (SelectedElement != null && SelectedElement.Name != "..")
                {
                    HoldElement = SelectedElement;
                    IsHold = true;
                    OnPropertyChanged(nameof(MoveButtonText)); // Обновляем текст кнопки!
                    ResultInfo = $"Cut {HoldElement.Name}. Navigate to target folder and press Paste.";
                }
                else
                {
                    ResultInfo = "No element selected to cut.";
                }
            }
            else
            {
                if (HoldElement != null)
                {
                    FileSystemElement.Move(HoldElement, CurrentFolder);
                    ResultInfo = $"Pasted {HoldElement.Name} into {CurrentFolder.Name}.";
                    HoldElement = null;
                    IsHold = false;
                    OnPropertyChanged(nameof(MoveButtonText)); // Обновляем текст кнопки!
                    OnPropertyChanged(nameof(DisplayedElements)); // Обновляем список файлов.
                }
                else
                {
                    ResultInfo = "Nothing to paste.";
                }
            }
        }

        public string MoveButtonText => IsHold ? "Paste" : "Move";
    }
}
